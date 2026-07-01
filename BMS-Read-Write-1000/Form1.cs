using System.ComponentModel;
using System.IO.Ports;

namespace BMS_Read_Write_1000
{
    public partial class Form1 : Form
    {
        private readonly BmsDevice _bms = new();
        private readonly CanDevice _can = new();
        private readonly System.Windows.Forms.Timer _refreshTimer = new();
        private readonly System.Windows.Forms.Timer _canPollTimer = new();
        private const string voltagedatasName = "电压(mV)";
        UInt32[] m_arrdevtype = new UInt32[20];

        bool _canConnected;
        int _canReadIndex;
        ushort[] _cellVoltages = new ushort[32];
        ushort[] _temperatures = new ushort[8];
        ushort _totalVoltage;
        short _current;
        ushort _remainingCapacity;
        ushort _fullCapacity;
        ushort _cycleCount;
        ushort _rsoc;
        ushort _protectionFlags;
        byte _fetStatus;
        ushort _fwVersion;
        byte _cellCount;
        byte _ntcCount;

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            _refreshTimer.Interval = 2000;
            _refreshTimer.Tick += RefreshTimer_Tick;
            _canPollTimer.Interval = 100;
            _canPollTimer.Tick += CanPollTimer_Tick;
            _bms.LogMessage += (_, msg) =>
            {
                if (InvokeRequired)
                {
                    Invoke(() => AppendLog(msg));
                }
                else
                {
                    AppendLog(msg);
                }
            };
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            #region can初始化
            comboBox_DevIndex.SelectedIndex = 0;
            comboBox_CANIndex.SelectedIndex = 0;
            textBox_AccCode.Text = "00000000";
            textBox_AccMask.Text = "FFFFFFFF";
            textBox_Time0.Text = "00";
            textBox_Time1.Text = "1C";
            comboBox_Filter.SelectedIndex = 0;              //接收所有类型
            comboBox_Mode.SelectedIndex = 2;                //还回测试模式

            Int32 curindex = 0;
            comboBox_devtype.Items.Clear();

            curindex = comboBox_devtype.Items.Add("DEV_USBCAN");
            m_arrdevtype[curindex] = CanDevice.DEV_USBCAN;

            curindex = comboBox_devtype.Items.Add("DEV_USBCAN2");
            m_arrdevtype[curindex] = CanDevice.DEV_USBCAN2;

            comboBox_devtype.SelectedIndex = 1;
            comboBox_devtype.MaxDropDownItems = comboBox_devtype.Items.Count;
            #endregion

            button_StartCAN.Click += BtnStartCan_Click;
            button_StopCAN.Click += BtnStopCan_Click;

            foreach (string port in SerialPort.GetPortNames())
                comboPort.Items.Add(port);
            if (comboPort.Items.Count > 0)
                comboPort.SelectedIndex = 0;

            comboBaud.SelectedItem ??= "9600";

            initDgvStatusInfo();
            _can.DataReceived += OnCanDataReceived;
        }

        private void BtnConnect_Click(object? sender, EventArgs e)
        {
            if (_bms.IsConnected)
            {
                _refreshTimer.Stop();
                _bms.Disconnect();
                btnConnect.Text = "连接";
                SetConfigEditable(false);
                return;
            }

            if (comboPort.SelectedItem == null)
            {
                MessageBox.Show("请选择COM口", "提示");
                return;
            }

            int baud = int.Parse(comboBaud.SelectedItem?.ToString() ?? "9600");
            try
            {
                _bms.Connect(comboPort.SelectedItem.ToString()!, baud);
                btnConnect.Text = "断开";
                _refreshTimer.Start();
                SetConfigEditable(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"连接失败: {ex.Message}", "错误");
            }
        }

        public bool ConnectCan(uint devType, uint devIndex, uint canIndex)
        {
            if (_canConnected)
            {
                _can.Close();
                _canConnected = false;
                return false;
            }

            var config = new VCI_INIT_CONFIG
            {
                AccCode = 0x00000000,
                AccMask = 0xFFFFFFFF,
                Timing0 = 0x00,
                Timing1 = 0x1C,
                Filter = 0,
                Mode = 0
            };

            if (!_can.Open(devType, devIndex, canIndex, config))
            {
                MessageBox.Show("打开CAN设备失败", "错误");
                return false;
            }

            _canConnected = true;
            _can.StartCAN();
            _canReadIndex = 0;
            _canPollTimer.Start();
            return true;
        }

        public void DisconnectCan()
        {
            if (_canConnected)
            {
                _canPollTimer.Stop();
                _can.Close();
                _canConnected = false;
            }
        }

        void OnCanDataReceived(VCI_CAN_OBJ obj)
        {
            if (InvokeRequired)
            {
                Invoke(() => ProcessCanFrame(obj));
                return;
            }
            ProcessCanFrame(obj);
        }

        void ProcessCanFrame(VCI_CAN_OBJ obj)
        {
            if (obj.RemoteFlag != 0) return;

            var log = new System.Text.StringBuilder();
            log.Append("CAN RX: ID=0x").Append(obj.ID.ToString("X3")).Append(" [");
            for (int i = 0; i < obj.DataLen && i < obj.Data.Length; i++)
                log.Append(' ').Append(obj.Data[i].ToString("X2"));
            log.Append(" ]");

            byte[] d = obj.Data;
            int len = obj.DataLen;
            bool crcOk = len >= 2 && CanProtocol.VerifyCrc(d, len);

            switch (obj.ID)
            {
                case 0x300:
                    if (len >= 8 && crcOk)
                    {
                        _totalVoltage = CanProtocol.GetU16(d, 0);
                        _current = CanProtocol.GetI16(d, 2);
                        _remainingCapacity = CanProtocol.GetU16(d, 4);
                        tx_TotalVoltage.Text = $"{_totalVoltage / 100.0:F2} V";
                        tx_Current.Text = $"{_current / 100.0:F2} A";
                        txtTotalCapacity.Text = _remainingCapacity.ToString();
                    }
                    break;

                case 0x301:
                    if (len >= 8 && crcOk)
                    {
                        _fullCapacity = CanProtocol.GetU16(d, 0);
                        _cycleCount = CanProtocol.GetU16(d, 2);
                        _rsoc = CanProtocol.GetU16(d, 4);
                        txtBatteryCycles.Text = _cycleCount.ToString();
                        labelSoc.Text = $"{_rsoc / 10.0:F1} %";
                        progressBarSoc.Value = Math.Min((int)_rsoc, 1000);
                    }
                    break;

                case 0x302:
                    if (len >= 8 && crcOk)
                    {
                        ushort balanceLow = CanProtocol.GetU16(d, 0);
                        ushort balanceHigh = CanProtocol.GetU16(d, 2);
                        _protectionFlags = CanProtocol.GetU16(d, 4);
                        lblWarning.Text = CanProtocol.GetProtectionText(_protectionFlags);
                        panelOverVoltage.BackColor = CanProtocol.IsBitSet(_protectionFlags, 0) ? Color.Red : Color.Green;
                        panelUnderVoltage.BackColor = CanProtocol.IsBitSet(_protectionFlags, 1) ? Color.Red : Color.Green;
                        bool overTemp = CanProtocol.IsBitSet(_protectionFlags, 2) || CanProtocol.IsBitSet(_protectionFlags, 4) || CanProtocol.IsBitSet(_protectionFlags, 8);
                        bool underTemp = CanProtocol.IsBitSet(_protectionFlags, 3) || CanProtocol.IsBitSet(_protectionFlags, 5);
                        bool overCurr = CanProtocol.IsBitSet(_protectionFlags, 6) || CanProtocol.IsBitSet(_protectionFlags, 7);
                        panelOverTemp.BackColor = overTemp ? Color.Red : Color.Green;
                        panelUnderTemp.BackColor = underTemp ? Color.Red : Color.Green;
                        panelOverCurrent.BackColor = overCurr ? Color.Red : Color.Green;
                    }
                    break;

                case 0x303:
                    if (len >= 8 && crcOk)
                    {
                        _fetStatus = d[0];
                        _fwVersion = CanProtocol.GetU16(d, 4);
                        bool chargeMos = (_fetStatus & 0x01) != 0;
                        bool dischargeMos = (_fetStatus & 0x02) != 0;
                        panelChargeMos.BackColor = chargeMos ? Color.Green : Color.Red;
                        panelDischargeMos.BackColor = dischargeMos ? Color.Green : Color.Red;
                        lblFirmwareVersion.Text = _fwVersion.ToString();
                    }
                    break;

                case 0x304:
                    if (len >= 4 && crcOk)
                    {
                        _cellCount = d[0];
                        _ntcCount = d[1];
                    }
                    break;

                case 0x305:
                    if (len >= 8 && crcOk)
                    {
                        for (int i = 0; i < 3 && i < _ntcCount; i++)
                        {
                            _temperatures[i] = CanProtocol.GetU16(d, i * 2);
                            UpdateTemperatureDisplay(i);
                        }
                    }
                    break;

                case 0x306:
                    if (len >= 8 && crcOk)
                    {
                        for (int i = 0; i < 3 && i + 3 < _ntcCount; i++)
                        {
                            _temperatures[i + 3] = CanProtocol.GetU16(d, i * 2);
                            UpdateTemperatureDisplay(i + 3);
                        }
                    }
                    break;

                case 0x307:
                    if (len >= 6 && crcOk)
                    {
                        for (int i = 0; i < 2 && i + 6 < _ntcCount; i++)
                        {
                            _temperatures[i + 6] = CanProtocol.GetU16(d, i * 2);
                            UpdateTemperatureDisplay(i + 6);
                        }
                    }
                    break;

                case 0x308: ParseCellVoltages(d, len, crcOk, 0); break;
                case 0x309: ParseCellVoltages(d, len, crcOk, 3); break;
                case 0x30A: ParseCellVoltages(d, len, crcOk, 6); break;
                case 0x30B: ParseCellVoltages(d, len, crcOk, 9); break;
                case 0x30C: ParseCellVoltages(d, len, crcOk, 12); break;
                case 0x30D: ParseCellVoltages(d, len, crcOk, 15); break;
                case 0x30E: ParseCellVoltages(d, len, crcOk, 18); break;
                case 0x30F: ParseCellVoltages(d, len, crcOk, 21); break;

                default:
                    if (obj.ID >= 0x310 && obj.ID <= 0x333 && len >= 4 && crcOk)
                    {
                        if (d[0] == 0x55 && d[1] == 0xAA)
                        {
                            log.Append(" 写入成功");
                            AppendLog("CAN: 参数写入成功");
                        }
                    }
                    break;
            }

            AppendLog(log.ToString());
        }

        void ParseCellVoltages(byte[] d, int len, bool crcOk, int cellIndex)
        {
            if (!crcOk || len < 8) return;
            int dataLen = Math.Min(3, (len - 2) / 2);
            for (int i = 0; i < dataLen && cellIndex + i < _cellVoltages.Length; i++)
            {
                _cellVoltages[cellIndex + i] = CanProtocol.GetU16(d, i * 2);
            }
            UpdateCellVoltageGrid();
        }

        void UpdateCellVoltageGrid()
        {
            if (_cellCount == 0) return;
            if (dgvCellVoltages.Rows.Count == 0)
            {
                for (int i = 0; i < _cellCount; i++)
                {
                    dgvCellVoltages.Rows.Add();
                    dgvCellVoltages.Rows[i].Cells[0].Value = i + 1;
                    dgvCellVoltages.Rows[i].Cells[1].Value = voltagedatasName;
                    dgvCellVoltages.Rows[i].Cells[2].Value = _cellVoltages[i].ToString();
                }
            }
            else
            {
                int maxRow = Math.Min(_cellCount, dgvCellVoltages.Rows.Count);
                for (int i = 0; i < maxRow; i++)
                    dgvCellVoltages.Rows[i].Cells[2].Value = _cellVoltages[i].ToString();
            }
        }

        void UpdateTemperatureDisplay(int index)
        {
            if (index >= 8) return;
            int celsius = CanProtocol.RawToCelsius(_temperatures[index]);
            if (index < 8 && dgvStatusInfo.Rows.Count > 9 + index)
                dgvStatusInfo.Rows[9 + index].Cells[1].Value = celsius.ToString();
        }

        void CanPollTimer_Tick(object? sender, EventArgs e)
        {
            _canPollTimer.Stop();
            try
            {
                if (!_canConnected) return;
                uint[] ids = CanProtocol.AllReadIds;
                uint id = ids[_canReadIndex];
                _canReadIndex = (_canReadIndex + 1) % ids.Length;
                _can.SendRemoteFrame(id);
            }
            finally
            {
                _canPollTimer.Start();
            }
        }

        void BtnStartCan_Click(object? sender, EventArgs e)
        {
            uint devType = m_arrdevtype[comboBox_devtype.SelectedIndex];
            uint devIndex = (uint)comboBox_DevIndex.SelectedIndex;
            uint canIndex = (uint)comboBox_CANIndex.SelectedIndex;
            ConnectCan(devType, devIndex, canIndex);
        }

        void BtnStopCan_Click(object? sender, EventArgs e)
        {
            DisconnectCan();
        }

        private async void RefreshTimer_Tick(object? sender, EventArgs e)
        {
            _refreshTimer.Stop();
            try
            {
                await Task.Run(() =>
                {
                    var data = _bms.ReadMonitoringData();
                    if (data.TotalBatteryStrings > 0)
                    {
                        Invoke(() => UpdateMonitorUI(data));
                    }
                });
            }
            catch { }
            finally
            {
                _refreshTimer.Start();
            }
        }

        private void UpdateMonitorUI(BmsData data)
        {
            tx_TotalVoltage.Text = $"{data.TotalVoltage / 1000.0:F3} V";
            tx_Current.Text = $"{data.Current / 1000.0:F2} A";
            txtBatteryCycles.Text = data.DesignedCapacity.ToString();
            txtTotalCapacity.Text = data.StateOfCapacity.ToString();

            if (data.TotalBatteryStrings > 0 && dgvCellVoltages.Rows.Count == 0)
            {
                for (int i = 0; i < data.TotalBatteryStrings; i++)
                {
                    dgvCellVoltages.Rows.Add();
                    dgvCellVoltages.Rows[i].Cells[0].Value = i + 1;
                    dgvCellVoltages.Rows[i].Cells[1].Value = voltagedatasName;
                    dgvCellVoltages.Rows[i].Cells[2].Value = data.SingleVoltages[i].ToString();
                }
            }
            else
            {
                for (int i = 0; i < data.TotalBatteryStrings; i++)
                {
                    dgvCellVoltages.Rows[i].Cells[2].Value = data.SingleVoltages[i].ToString();
                }
            }

            dgvStatusInfo.Rows[0].Cells[1].Value = data.BatteryType.ToString();
            dgvStatusInfo.Rows[1].Cells[1].Value = data.RemainingCapacity.ToString();
            dgvStatusInfo.Rows[2].Cells[1].Value = data.DesignedCapacity.ToString();
            dgvStatusInfo.Rows[3].Cells[1].Value = data.SingleMaxVoltage.ToString();
            dgvStatusInfo.Rows[4].Cells[1].Value = data.SingleMaxVoltageNo.ToString();
            dgvStatusInfo.Rows[5].Cells[1].Value = data.SingleMinVoltage.ToString();
            dgvStatusInfo.Rows[6].Cells[1].Value = data.SingleMinVoltageNo.ToString();
            //电芯压差和总电压待完善


            dgvStatusInfo.Rows[9].Cells[1].Value = data.Temperatures[0].ToString();
            dgvStatusInfo.Rows[10].Cells[1].Value = data.Temperatures[1].ToString();
            dgvStatusInfo.Rows[11].Cells[1].Value = data.Temperatures[2].ToString();
            dgvStatusInfo.Rows[12].Cells[1].Value = data.Temperatures[3].ToString();
            dgvStatusInfo.Rows[13].Cells[1].Value = data.Temperatures[4].ToString();
            dgvStatusInfo.Rows[14].Cells[1].Value = data.Temperatures[5].ToString();
            dgvStatusInfo.Rows[15].Cells[1].Value = data.Temperatures[6].ToString();
            dgvStatusInfo.Rows[16].Cells[1].Value = data.Temperatures[7].ToString();


            labelSoc.Text = $"{data.StateOfCapacity / 10.0:F1} %";
            progressBarSoc.Value = data.StateOfCapacity;
            panelDischargeMos.BackColor = data.DischargeMosState == 1 ? Color.Green : Color.Red;
            panelChargeMos.BackColor = data.ChargeMosState == 1 ? Color.Green : Color.Red;
            lblWarning.Text = data.BmsWarningWord1.ToString();
            lblHardwareVersion.Text = data.HardwareVersion.ToString();
            lblFirmwareVersion.Text = data.FirmwareVersion.ToString();
            panelOverVoltage.BackColor = IsBitSet(data.BmsWarningWord1, 0) ? Color.Red : Color.Green;
            panelUnderVoltage.BackColor = IsBitSet(data.BmsWarningWord1, 1) ? Color.Red : Color.Green;
            panelOverTemp.BackColor = (IsBitSet(data.BmsWarningWord1, 2) || IsBitSet(data.BmsWarningWord1, 4) || IsBitSet(data.BmsWarningWord1, 8)) ? Color.Red : Color.Green;
            panelUnderTemp.BackColor = (IsBitSet(data.BmsWarningWord1, 3) || IsBitSet(data.BmsWarningWord1, 5)) ? Color.Red : Color.Green;
            panelOverCurrent.BackColor = (IsBitSet(data.BmsWarningWord1, 6) || IsBitSet(data.BmsWarningWord1, 7)) ? Color.Red : Color.Green;
            if (data.Current > 0)
            {
                panelChargeDischarge.BackColor = Color.Red;
            }
            else if (data.Current < 0)
            {
                panelChargeDischarge.BackColor = Color.Green;
            }
            else
            {
                panelChargeDischarge.BackColor = Color.Gray;
            }
            //labelTemp.Text = $"{data.MaxTemperature / 10.0:F1} °C";
        }


        /// <summary>
        /// 判断特定位是否为 1
        /// </summary>
        public bool IsBitSet(int number, int position)
        {
            return (number & (1 << position)) != 0;
        }

        private void initDgvStatusInfo()
        {
            dgvStatusInfo.Rows.Add(17);
            dgvStatusInfo.Rows[0].Cells[0].Value = "电池类型";
            dgvStatusInfo.Rows[1].Cells[0].Value = "剩余容量(mAH)";
            dgvStatusInfo.Rows[2].Cells[0].Value = "设计容量(mAH)";
            dgvStatusInfo.Rows[3].Cells[0].Value = "最高电压(mV)";
            dgvStatusInfo.Rows[4].Cells[0].Value = "最高电压序号";
            dgvStatusInfo.Rows[5].Cells[0].Value = "最低电压(mV)";
            dgvStatusInfo.Rows[6].Cells[0].Value = "最低电压序号";
            dgvStatusInfo.Rows[7].Cells[0].Value = "电芯压差(mV)";
            dgvStatusInfo.Rows[8].Cells[0].Value = "总电压(mV)";

            dgvStatusInfo.Rows[9].Cells[0].Value = "温度1(℃)";
            dgvStatusInfo.Rows[10].Cells[0].Value = "温度2(℃)";
            dgvStatusInfo.Rows[11].Cells[0].Value = "温度3(℃)";
            dgvStatusInfo.Rows[12].Cells[0].Value = "温度4(℃)";
            dgvStatusInfo.Rows[13].Cells[0].Value = "温度5(℃)";
            dgvStatusInfo.Rows[14].Cells[0].Value = "温度6(℃)";
            dgvStatusInfo.Rows[15].Cells[0].Value = "温度7(℃)";
            dgvStatusInfo.Rows[16].Cells[0].Value = "温度8(℃)";
        }

        private void BtnReadConfig_Click(object? sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var cfg = _bms.ReadConfigData();
                Invoke(() => UpdateConfigUI(cfg));
            });
        }

        private void UpdateConfigUI(BmsConfig cfg)
        {
            SetComboValue(comboOCV, cfg.OverVoltageProtectValue.ToString());
            SetComboValue(comboOCDelay, cfg.OverVoltageProtectDelay.ToString());
            SetComboValue(comboOCR, cfg.OverVoltageRestoreValue.ToString());

            SetComboValue(comboUCV, cfg.UnderVoltageProtectValue.ToString());
            SetComboValue(comboUCDelay, cfg.UnderVoltageProtectDelay.ToString());
            SetComboValue(comboUCR, cfg.UnderVoltageRestoreValue.ToString());

            SetComboValue(comboCC, cfg.ChargeOvercurrentValue.ToString());
            SetComboValue(comboCCDelay, cfg.ChargeOvercurrentDelay.ToString());
            SetComboValue(comboDC1, cfg.DischargeOvercurrentL1.ToString());
            SetComboValue(comboDC1Delay, cfg.DischargeOvercurrentL1Delay.ToString());
            SetComboValue(comboDC2, cfg.DischargeOvercurrentL2.ToString());
            SetComboValue(comboDC2Delay, cfg.DischargeOvercurrentL2Delay.ToString());

            SetComboValue(comboBALP, cfg.BalanceStartPressureDiff.ToString());
            SetComboValue(comboBALV, cfg.BalanceStartVoltage.ToString());

            SetComboValue(comboFunctionParamSet, cfg.FunctionParameterSet.ToString());
            SetComboValue(comboSelfConsumptionPower, cfg.SelfConsumptionPower.ToString());
            SetComboValue(comboSleepLeakageCurrent, cfg.SleepLeakageCurrent.ToString());

            SetComboValue(comboChargeOverTempProtect, cfg.ChargeOvertempProtect.ToString());
            SetComboValue(comboDischargeOverTempProtect, cfg.DischargeOvertempProtect.ToString());
            SetComboValue(comboChargeOverTempRestore, cfg.ChargeOvertempRestore.ToString());
            SetComboValue(comboDischargeOverTempRestore, cfg.DischargeOvertempRestore.ToString());
            SetComboValue(comboChargeUnderTempProtect, cfg.ChargeUndertempProtect.ToString());
            SetComboValue(comboDischargeUnderTempProtect, cfg.DischargeUndertempProtect.ToString());
            SetComboValue(comboChargeUnderTempRestore, cfg.ChargeUndertempRestore.ToString());
            SetComboValue(comboDischargeUnderTempRestore, cfg.DischargeUndertempRestore.ToString());

            SetComboValue(comboMosOverTempProtect, cfg.MosOvertempProtect.ToString());
            SetComboValue(comboMosOverTempRestore, cfg.MosOvertempRestore.ToString());
        }

        private void BtnWriteConfig_Click(object? sender, EventArgs e)
        {
            BmsRegisters.SlaveAddress = byte.Parse(comboSlaveAddress.Text);
            var values = new ushort[]
            {
                ParseU16(comboOCV), ParseU16(comboOCDelay), ParseU16(comboOCR),
                ParseU16(comboUCV), ParseU16(comboUCDelay), ParseU16(comboUCR),
                ParseU16(comboBALV), ParseU16(comboBALP),
                ParseU16(comboDC1), ParseU16(comboDC1Delay), ParseU16(comboDC2), ParseU16(comboDC2Delay),
                ParseU16(comboCC), ParseU16(comboCCDelay),
            };

            Task.Run(() =>
            {
                if (_bms.WriteMultipleRegisters(BmsRegisters.OverVoltageProtectValue, values))
                {
                    Invoke(() => AppendLog("参数写入成功"));
                }
                else
                {
                    Invoke(() => AppendLog("参数写入失败"));
                }
            });
        }

        private void AppendLog(string msg)
        {

        }

        private static void SetComboValue(TextBox tb, string value)
        {
            tb.Text = value;
        }

        private static ushort ParseU16(TextBox tb)
        {
            if (ushort.TryParse(tb.Text, out ushort val))
                return val;
            return 0;
        }

        private void SetConfigEditable(bool connected)
        {
            btnReadConfig.Enabled = connected;
            btnWriteConfig.Enabled = connected;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisconnectCan();
                _can.Dispose();
                _refreshTimer.Dispose();
                _bms.Dispose();
            }
            base.Dispose(disposing);
        }

        void CanWriteCommand(uint canId, byte b0, byte b1)
        {
            Task.Run(() =>
            {
                if (_canConnected)
                    _can.WriteCommand(canId, b0, b1);
                else
                {
                    BmsRegisters.SlaveAddress = byte.Parse(comboSlaveAddress.Text);
                    _bms.WriteSingleRegister(BmsRegisters.ChargeOnDischargeOff, 0x0001);
                }
            });
        }

        private void btn_ChargeOnDischargeOff_Click(object sender, EventArgs e)
        {
            CanWriteCommand(CanProtocol.Write.ChargeOnDischargeOff, 0x00, 0x01);
        }

        private void btn_ChargeOffDischargeOn_Click(object sender, EventArgs e)
        {
            CanWriteCommand(CanProtocol.Write.ChargeOffDischargeOn, 0x00, 0x02);
        }

        private void btn_ChargeOnDischargeOn_Click(object sender, EventArgs e)
        {
            CanWriteCommand(CanProtocol.Write.ChargeOnDischargeOn, 0x00, 0x03);
        }

        private void btn_ChargeOffDischargeOff_Click(object sender, EventArgs e)
        {
            CanWriteCommand(CanProtocol.Write.ChargeOffDischargeOff, 0x00, 0x04);
        }

        private void btn_ExitManulMode_Click(object sender, EventArgs e)
        {
            CanWriteCommand(CanProtocol.Write.ExitManualMode, 0x00, 0x07);
        }

        private void btn_CurrentZeroing_Click(object sender, EventArgs e)
        {
            CanWriteCommand(CanProtocol.Write.CurrentZeroing, 0x00, 0x05);
        }

        private void btn_RestoreFactoryDefault_Click(object sender, EventArgs e)
        {
            CanWriteCommand(CanProtocol.Write.RestoreFactory, 0x00, 0x06);
        }

        private void btn_StopMonitoring_Click(object sender, EventArgs e)
        {

        }

        static uint CanWriteIdForRegister(ushort reg)
        {
            return reg switch
            {
                BmsRegisters.OverVoltageProtectValue => CanProtocol.Write.OverVoltageProtect,
                BmsRegisters.OverVoltageRestoreValue => CanProtocol.Write.OverVoltageRestore,
                BmsRegisters.OverVoltageProtectDelay => CanProtocol.Write.OverVoltageDelay,
                BmsRegisters.UnderVoltageProtectValue => CanProtocol.Write.UnderVoltageProtect,
                BmsRegisters.UnderVoltageRestoreValue => CanProtocol.Write.UnderVoltageRestore,
                BmsRegisters.UnderVoltageProtectDelay => CanProtocol.Write.UnderVoltageDelay,
                BmsRegisters.ChargeOvercurrentValue => CanProtocol.Write.ChargeOvercurrent,
                BmsRegisters.ChargeOvercurrentDelay => CanProtocol.Write.ChargeOvercurrentDelay,
                BmsRegisters.DischargeOvercurrentL1 => CanProtocol.Write.DischargeOvercurrentL1,
                BmsRegisters.DischargeOvercurrentL1Delay => CanProtocol.Write.DischargeOvercurrentL1Delay,
                BmsRegisters.DischargeOvercurrentL2 => CanProtocol.Write.DischargeOvercurrentL2,
                BmsRegisters.DischargeOvercurrentL2Delay => CanProtocol.Write.DischargeOvercurrentL2Delay,
                BmsRegisters.BalanceStartVoltage => CanProtocol.Write.BalanceStartVoltage,
                BmsRegisters.BalanceStartPressureDiff => CanProtocol.Write.BalanceStartPressureDiff,
                BmsRegisters.SleepLeakageCurrent => CanProtocol.Write.SleepLeakageCurrent,
                BmsRegisters.SelfConsumptionPower => CanProtocol.Write.SelfConsumptionPower,
                BmsRegisters.FunctionParameterSet => CanProtocol.Write.FunctionParameter,
                BmsRegisters.ChargeOvertempProtect => CanProtocol.Write.ChargeOverTempProtect,
                BmsRegisters.ChargeOvertempRestore => CanProtocol.Write.ChargeOverTempRestore,
                BmsRegisters.ChargeUndertempProtect => CanProtocol.Write.ChargeUnderTempProtect,
                BmsRegisters.ChargeUndertempRestore => CanProtocol.Write.ChargeUnderTempRestore,
                BmsRegisters.DischargeOvertempProtect => CanProtocol.Write.DischargeOverTempProtect,
                BmsRegisters.DischargeOvertempRestore => CanProtocol.Write.DischargeOverTempRestore,
                BmsRegisters.DischargeUndertempProtect => CanProtocol.Write.DischargeUnderTempProtect,
                BmsRegisters.DischargeUndertempRestore => CanProtocol.Write.DischargeUnderTempRestore,
                BmsRegisters.MosOvertempProtect => CanProtocol.Write.MosOverTempProtect,
                BmsRegisters.MosOvertempRestore => CanProtocol.Write.MosOverTempRestore,
                _ => 0
            };
        }

        private void WriteParam(TextBox tb, ushort reg)
        {
            ushort val = ParseU16(tb);
            Task.Run(() =>
            {
                if (_canConnected)
                {
                    uint canId = CanWriteIdForRegister(reg);
                    if (canId != 0 && _can.Write2ByteParam(canId, val))
                    {
                        Invoke(() => AppendLog("CAN参数写入成功"));
                        return;
                    }
                }
                BmsRegisters.SlaveAddress = byte.Parse(comboSlaveAddress.Text);
                if (_bms.WriteSingleRegister(reg, val))
                    Invoke(() => AppendLog("参数写入成功"));
                else
                    Invoke(() => AppendLog("参数写入失败"));
            });
        }

        private void BtnOCV_Click(object? sender, EventArgs e) => WriteParam(comboOCV, BmsRegisters.OverVoltageProtectValue);
        private void BtnOCR_Click(object? sender, EventArgs e) => WriteParam(comboOCR, BmsRegisters.OverVoltageRestoreValue);
        private void BtnOCDelay_Click(object? sender, EventArgs e) => WriteParam(comboOCDelay, BmsRegisters.OverVoltageProtectDelay);
        private void BtnUCV_Click(object? sender, EventArgs e) => WriteParam(comboUCV, BmsRegisters.UnderVoltageProtectValue);
        private void BtnUCR_Click(object? sender, EventArgs e) => WriteParam(comboUCR, BmsRegisters.UnderVoltageRestoreValue);
        private void BtnUCDelay_Click(object? sender, EventArgs e) => WriteParam(comboUCDelay, BmsRegisters.UnderVoltageProtectDelay);
        private void BtnCC_Click(object? sender, EventArgs e) => WriteParam(comboCC, BmsRegisters.ChargeOvercurrentValue);
        private void BtnCCDelay_Click(object? sender, EventArgs e) => WriteParam(comboCCDelay, BmsRegisters.ChargeOvercurrentDelay);
        private void BtnDC1_Click(object? sender, EventArgs e) => WriteParam(comboDC1, BmsRegisters.DischargeOvercurrentL1);
        private void BtnDC1Delay_Click(object? sender, EventArgs e) => WriteParam(comboDC1Delay, BmsRegisters.DischargeOvercurrentL1Delay);
        private void BtnDC2_Click(object? sender, EventArgs e) => WriteParam(comboDC2, BmsRegisters.DischargeOvercurrentL2);
        private void BtnDC2Delay_Click(object? sender, EventArgs e) => WriteParam(comboDC2Delay, BmsRegisters.DischargeOvercurrentL2Delay);
        private void BtnBALV_Click(object? sender, EventArgs e) => WriteParam(comboBALV, BmsRegisters.BalanceStartVoltage);
        private void BtnBALP_Click(object? sender, EventArgs e) => WriteParam(comboBALP, BmsRegisters.BalanceStartPressureDiff);
        private void BtnSleepLeakageCurrent_Click(object? sender, EventArgs e) => WriteParam(comboSleepLeakageCurrent, BmsRegisters.SleepLeakageCurrent);
        private void BtnSelfConsumptionPower_Click(object? sender, EventArgs e) => WriteParam(comboSelfConsumptionPower, BmsRegisters.SelfConsumptionPower);
        private void BtnFunctionParamSet_Click(object? sender, EventArgs e) => WriteParam(comboFunctionParamSet, BmsRegisters.FunctionParameterSet);
        private void BtnChargeOverTempProtect_Click(object? sender, EventArgs e) => WriteParam(comboChargeOverTempProtect, BmsRegisters.ChargeOvertempProtect);
        private void BtnChargeOverTempRestore_Click(object? sender, EventArgs e) => WriteParam(comboChargeOverTempRestore, BmsRegisters.ChargeOvertempRestore);
        private void BtnChargeUnderTempProtect_Click(object? sender, EventArgs e) => WriteParam(comboChargeUnderTempProtect, BmsRegisters.ChargeUndertempProtect);
        private void BtnChargeUnderTempRestore_Click(object? sender, EventArgs e) => WriteParam(comboChargeUnderTempRestore, BmsRegisters.ChargeUndertempRestore);
        private void BtnDischargeOverTempProtect_Click(object? sender, EventArgs e) => WriteParam(comboDischargeOverTempProtect, BmsRegisters.DischargeOvertempProtect);
        private void BtnDischargeOverTempRestore_Click(object? sender, EventArgs e) => WriteParam(comboDischargeOverTempRestore, BmsRegisters.DischargeOvertempRestore);
        private void BtnDischargeUnderTempProtect_Click(object? sender, EventArgs e) => WriteParam(comboDischargeUnderTempProtect, BmsRegisters.DischargeUndertempProtect);
        private void BtnDischargeUnderTempRestore_Click(object? sender, EventArgs e) => WriteParam(comboDischargeUnderTempRestore, BmsRegisters.DischargeUndertempRestore);
        private void BtnMosOverTempProtect_Click(object? sender, EventArgs e) => WriteParam(comboMosOverTempProtect, BmsRegisters.MosOvertempProtect);
        private void BtnMosOverTempRestore_Click(object? sender, EventArgs e) => WriteParam(comboMosOverTempRestore, BmsRegisters.MosOvertempRestore);
        private void BtnChargeCorrection_Click(object? sender, EventArgs e)
        {
            if (_canConnected)
            {
                uint val = uint.Parse(comboChargeCorrection.Text);
                _can.Write4ByteParam(CanProtocol.Write.ChargeCorrection, val);
            }
            else
                WriteParam(comboChargeCorrection, BmsRegisters.ChargingCorrection);
        }
        private void BtnDischargeCorrection_Click(object? sender, EventArgs e)
        {
            if (_canConnected)
            {
                uint val = uint.Parse(comboDischargeCorrection.Text);
                _can.Write4ByteParam(CanProtocol.Write.DischargeCorrection, val);
            }
            else
                WriteParam(comboDischargeCorrection, BmsRegisters.DischargingCorrection);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click_1(object sender, EventArgs e)
        {

        }

        private void button_StartCAN_Click(object sender, EventArgs e)
        {

        }
    }

}
