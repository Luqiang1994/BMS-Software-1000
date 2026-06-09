using System.IO.Ports;

namespace BMS_Read_Write_1000
{
    public partial class Form1 : Form
    {
        private readonly BmsDevice _bms = new();
        private readonly System.Windows.Forms.Timer _refreshTimer = new();

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
            labelBatCount.Text = data.TotalBatteryStrings.ToString();

            SetComboValue(comboOCV, data.SingleMaxVoltage.ToString());
            SetComboValue(comboOCR, data.SingleMaxVoltageNo.ToString());
            SetComboValue(comboOCDelay, data.BmsWarningWord1.ToString());

            SetComboValue(comboUCV, data.SingleMinVoltage.ToString());
            SetComboValue(comboUCR, data.SingleMinVoltageNo.ToString());

            SetComboValue(comboCC, (data.Current / 1000.0).ToString("F2"));
            SetComboValue(comboDC1, (data.Current / 1000.0).ToString("F2"));
            SetComboValue(comboDC2, (data.Current / 1000.0).ToString("F2"));

            labelTotalVoltage.Text = $"{data.TotalVoltage / 1000.0:F3} V";
            labelCurrent.Text = $"{data.Current / 1000.0:F2} A";
            labelSoc.Text = $"{data.StateOfCapacity / 10.0:F1} %";
            labelTemp.Text = $"{data.MaxTemperature / 10.0:F1} °C";
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
        }

        private void BtnWriteConfig_Click(object? sender, EventArgs e)
        {
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
            listLog.Items.Insert(0, msg);
            while (listLog.Items.Count > 100)
                listLog.Items.RemoveAt(listLog.Items.Count - 1);
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
    }
}
