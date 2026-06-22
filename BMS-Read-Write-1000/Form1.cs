using System.ComponentModel;
using System.IO.Ports;

namespace BMS_Read_Write_1000
{
    public partial class Form1 : Form
    {
        private readonly BmsDevice _bms = new();
        private readonly System.Windows.Forms.Timer _refreshTimer = new();
        private const string voltagedatasName = "电压(mV)";

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            _refreshTimer.Interval = 2000;
            _refreshTimer.Tick += RefreshTimer_Tick;
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
            foreach (string port in SerialPort.GetPortNames())
                comboPort.Items.Add(port);
            if (comboPort.Items.Count > 0)
                comboPort.SelectedIndex = 0;

            comboBaud.SelectedItem ??= "9600";

            initDgvStatusInfo();
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

        private static void SetComboValue(ComboBox cb, string value)
        {
            if (cb.Items.Contains(value))
                cb.SelectedItem = value;
            else
            {
                cb.Items.Clear();
                cb.Items.Add(value);
                cb.SelectedIndex = 0;
            }
        }

        private static ushort ParseU16(ComboBox cb)
        {
            if (ushort.TryParse(cb.Text, out ushort val))
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
                _refreshTimer.Dispose();
                _bms.Dispose();
            }
            base.Dispose(disposing);
        }

        private void btn_ChargeOnDischargeOff_Click(object sender, EventArgs e)
        {
            BmsRegisters.SlaveAddress = byte.Parse(comboSlaveAddress.Text);
            _bms.WriteSingleRegister(BmsRegisters.ChargeOnDischargeOff, 0x0001);
        }

        private void btn_ChargeOffDischargeOn_Click(object sender, EventArgs e)
        {
            BmsRegisters.SlaveAddress = byte.Parse(comboSlaveAddress.Text);
            _bms.WriteSingleRegister(BmsRegisters.ChargeOffDischargeOn, 0x0001);
        }

        private void btn_ChargeOnDischargeOn_Click(object sender, EventArgs e)
        {
            BmsRegisters.SlaveAddress = byte.Parse(comboSlaveAddress.Text);
            _bms.WriteSingleRegister(BmsRegisters.ChargeOnDischargeOn, 0x0001);
        }

        private void btn_ChargeOffDischargeOff_Click(object sender, EventArgs e)
        {
            BmsRegisters.SlaveAddress = byte.Parse(comboSlaveAddress.Text);
            _bms.WriteSingleRegister(BmsRegisters.ChargeOffDischargeOff, 0x0001);
        }

        private void btn_ExitManulMode_Click(object sender, EventArgs e)
        {
            BmsRegisters.SlaveAddress = byte.Parse(comboSlaveAddress.Text);
            _bms.WriteSingleRegister(BmsRegisters.ExitManualMode, 0x0001);
        }

        private void btn_CurrentZeroing_Click(object sender, EventArgs e)
        {
            BmsRegisters.SlaveAddress = byte.Parse(comboSlaveAddress.Text);
            _bms.WriteSingleRegister(BmsRegisters.CurrentZeroing, 0x0001);
        }

        private void btn_RestoreFactoryDefault_Click(object sender, EventArgs e)
        {
            BmsRegisters.SlaveAddress = byte.Parse(comboSlaveAddress.Text);
            _bms.WriteSingleRegister(BmsRegisters.RestoreFactoryDefault, 0x0001);
        }

        private void btn_StopMonitoring_Click(object sender, EventArgs e)
        {

        }
    }
    
}
