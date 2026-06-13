using System.IO.Ports;

namespace BMS_Read_Write_1000
{
    public class BmsData
    {
        public ushort TotalBatteryStrings { get; set; }
        public ushort[] SingleVoltages { get; set; } = Array.Empty<ushort>();
        public short[] Temperatures { get; set; } = Array.Empty<short>();
        public short MaxTemperature { get; set; }
        public ushort MaxTemperatureNo { get; set; }
        public short MinTemperature { get; set; }
        public ushort MinTemperatureNo { get; set; }
        public ushort SingleMaxVoltage { get; set; }
        public ushort SingleMaxVoltageNo { get; set; }
        public ushort SingleMinVoltage { get; set; }
        public ushort SingleMinVoltageNo { get; set; }
        public uint TotalVoltage { get; set; }
        public int Current { get; set; }
        public uint DesignedCapacity { get; set; }
        public ushort StateOfCapacity { get; set; }
        public ushort BatteryLoopTimes { get; set; }
        public ushort FirmwareVersion { get; set; }
        public ushort BatterySequenceNo { get; set; }
        public ushort BmsWarningWord1 { get; set; }
        public ushort BmsWarningWord2 { get; set; }
        public ushort BalancedWord1 { get; set; }
        public ushort BalancedWord2 { get; set; }
        public ushort DischargeMosState { get; set; }
        public ushort ChargeMosState { get; set; }
        public ushort BatteryType { get; set; }

        public ushort HardwareVersion { get; set; }
        public ushort TotalNtcCount { get; set; }
        public uint RemainingCapacity { get; set; }
    }

    public class BmsConfig
    {
        public ushort OverVoltageProtectValue { get; set; }
        public ushort OverVoltageProtectDelay { get; set; }
        public ushort OverVoltageRestoreValue { get; set; }
        public ushort UnderVoltageProtectValue { get; set; }
        public ushort UnderVoltageProtectDelay { get; set; }
        public ushort UnderVoltageRestoreValue { get; set; }
        public ushort BalanceStartVoltage { get; set; }
        public ushort BalanceStartPressureDiff { get; set; }
        public ushort DischargeOvercurrentL1 { get; set; }
        public ushort DischargeOvercurrentL1Delay { get; set; }
        public ushort DischargeOvercurrentL2 { get; set; }
        public ushort DischargeOvercurrentL2Delay { get; set; }
        public ushort ChargeOvercurrentValue { get; set; }
        public ushort ChargeOvercurrentDelay { get; set; }
        public short DischargeOvertempProtect { get; set; }
        public short DischargeOvertempRestore { get; set; }
        public short DischargeUndertempProtect { get; set; }
        public short DischargeUndertempRestore { get; set; }
        public short ChargeOvertempProtect { get; set; }
        public short ChargeOvertempRestore { get; set; }
        public short ChargeUndertempProtect { get; set; }
        public short ChargeUndertempRestore { get; set; }
        public uint RemainingCapacity { get; set; }
        public ushort FunctionParameterSet { get; set; }
        public ushort SelfConsumptionPower { get; set; }
        public ushort SleepLeakageCurrent { get; set; }
        public ushort Afe2BatteryStrings { get; set; }
        public short MosOvertempProtect { get; set; }
        public short MosOvertempRestore { get; set; }
        
    }

    public class BmsDevice : IDisposable
    {
        private SerialPort? _serialPort;
        private readonly object _lock = new();
        private const int DefaultTimeout = 1000;

        public bool IsConnected => _serialPort?.IsOpen ?? false;

        public event EventHandler<string>? LogMessage;

        public void Connect(string portName, int baudRate = 9600, Parity parity = Parity.None,
            int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            Disconnect();

            var sp = new SerialPort(portName, baudRate, parity, dataBits, stopBits)
            {
                ReadTimeout = DefaultTimeout,
                WriteTimeout = DefaultTimeout,
                DtrEnable = true,
                RtsEnable = true
            };
            sp.Open();
            _serialPort = sp;
            Log($"已连接到 {portName} ({baudRate},{parity},{dataBits},{stopBits})");
        }

        public void Disconnect()
        {
            lock (_lock)
            {
                if (_serialPort != null)
                {
                    var portName = _serialPort.PortName;
                    _serialPort.Close();
                    _serialPort.Dispose();
                    _serialPort = null;
                    Log($"已断开 {portName}");
                }
            }
        }

        public BmsData ReadMonitoringData()
        {
            var data = new BmsData();

            ushort[] header = ReadRegisters(BmsRegisters.TotalBatteryStrings, 1);
            if (header.Length == 0) return data;
            data.TotalBatteryStrings = header[0];

            ushort cellCount = Math.Min(data.TotalBatteryStrings, (ushort)32);
            if (cellCount > 0)
            {
                ushort[] voltages = ReadRegisters(BmsRegisters.SingleVoltage1, cellCount);
                data.SingleVoltages = voltages;
            }

            ushort[] temps = ReadRegisters(BmsRegisters.Temperature1, 8);
            data.Temperatures = new short[8];
            for (int i = 0; i < 8 && i < temps.Length; i++)
                data.Temperatures[i] = (short)temps[i];

            ushort[] misc = ReadRegisters(BmsRegisters.MaxTemperature, 25);
            if (misc.Length >= 25)
            {
                data.MaxTemperature = (short)misc[0];
                data.MaxTemperatureNo = misc[1];
                data.MinTemperature = (short)misc[2];
                data.MinTemperatureNo = misc[3];
                data.SingleMaxVoltage = misc[4];
                data.SingleMaxVoltageNo = misc[5];
                data.SingleMinVoltage = misc[6];
                data.SingleMinVoltageNo = misc[7];
                data.TotalVoltage = (uint)(misc[8] << 16 | misc[9]);
                data.Current = misc[10] << 16 | misc[11];
                data.DesignedCapacity = (uint)(misc[12] << 16 | misc[13]);
                data.StateOfCapacity = misc[14];
                data.BatteryLoopTimes = misc[15];
                data.FirmwareVersion = misc[16];
                data.BatterySequenceNo = misc[17];
                data.BmsWarningWord1 = misc[18];
                data.BmsWarningWord2 = misc[19];
                data.BalancedWord1 = misc[20];
                data.BalancedWord2 = misc[21];
                data.DischargeMosState = misc[22];
                data.ChargeMosState = misc[23];
                data.BatteryType = misc[24];
            }

            ushort[] extra = ReadRegisters(BmsRegisters.HardwareVersion, 2);
            if (extra.Length >= 2)
            {
                data.HardwareVersion = extra[0];
                data.TotalNtcCount = extra[1];
            }

            ushort[] remain_capacity = ReadRegisters(BmsRegisters.RemainingCapacityHigh, 2);
            data.RemainingCapacity = (uint)(remain_capacity[0] << 16 | remain_capacity[1]);
            return data;
        }

        public BmsConfig ReadConfigData()
        {
            var cfg = new BmsConfig();

            ushort[] config = ReadRegisters(BmsRegisters.OverVoltageProtectValue, 30);
            if (config.Length >= 30)
            {
                cfg.OverVoltageProtectValue = config[0];
                cfg.OverVoltageProtectDelay = config[1];
                cfg.OverVoltageRestoreValue = config[2];
                cfg.UnderVoltageProtectValue = config[3];
                cfg.UnderVoltageProtectDelay = config[4];
                cfg.UnderVoltageRestoreValue = config[5];
                cfg.BalanceStartVoltage = config[6];
                cfg.BalanceStartPressureDiff = config[7];
                cfg.DischargeOvercurrentL1 = config[8];
                cfg.DischargeOvercurrentL1Delay = config[9];
                cfg.DischargeOvercurrentL2 = config[10];
                cfg.DischargeOvercurrentL2Delay = config[11];
                cfg.ChargeOvercurrentValue = config[12];
                cfg.ChargeOvercurrentDelay = config[13];
                cfg.DischargeOvertempProtect = (short)config[14];
                cfg.DischargeOvertempRestore = (short)config[15];
                cfg.DischargeUndertempProtect = (short)config[16];
                cfg.DischargeUndertempRestore = (short)config[17];
                cfg.ChargeOvertempProtect = (short)config[18];
                cfg.ChargeOvertempRestore = (short)config[19];
                cfg.ChargeUndertempProtect = (short)config[20];
                cfg.ChargeUndertempRestore = (short)config[21];
                cfg.RemainingCapacity = (uint)(config[22] << 16 | config[23]);
                cfg.FunctionParameterSet = config[24];
                cfg.SelfConsumptionPower = config[25];
                cfg.SleepLeakageCurrent = config[26];
                cfg.Afe2BatteryStrings = config[27];
                cfg.MosOvertempProtect = (short)config[28];
                cfg.MosOvertempRestore = (short)config[29];
            }

            return cfg;
        }

        public bool WriteSingleRegister(ushort address, ushort value)
        {
            var request = ModbusFrame.CreateWriteSingleRequest(BmsRegisters.SlaveAddress, address, value);
            var response = ExecuteTransaction(request, 8);
            return response != null;
        }

        public bool WriteMultipleRegisters(ushort startAddress, ushort[] values)
        {
            var request = ModbusFrame.CreateWriteMultipleRequest(BmsRegisters.SlaveAddress, startAddress, values);
            var response = ExecuteTransaction(request, 8);
            return response != null;
        }

        public bool SendCommand(ushort address)
        {
            return WriteSingleRegister(address, 1);
        }

        private ushort[] ReadRegisters(ushort startAddress, ushort quantity)
        {
            var request = ModbusFrame.CreateReadRequest(BmsRegisters.SlaveAddress, startAddress, quantity);
            var response = ExecuteTransaction(request, 5 + quantity * 2);
            if (response == null)
            {
                Log($"读取寄存器 0x{startAddress:X4} 失败");
                return Array.Empty<ushort>();
            }
            return response.GetUInt16Values();
        }

        private ModbusFrame? ExecuteTransaction(ModbusFrame request, int expectedMinLength)
        {
            lock (_lock)
            {
                if (_serialPort == null || !_serialPort.IsOpen)
                {
                    Log("串口未连接");
                    return null;
                }

                try
                {
                    byte[] requestBytes = request.Build();
                    _serialPort.DiscardInBuffer();
                    _serialPort.Write(requestBytes, 0, requestBytes.Length);

                    Thread.Sleep(50);

                    var buffer = new List<byte>();
                    int totalToRead = expectedMinLength;
                    int attempts = 0;
                    while (buffer.Count < totalToRead && attempts < 10)
                    {
                        int remaining = totalToRead - buffer.Count;
                        var chunk = new byte[remaining];
                        try
                        {
                            int read = _serialPort.Read(chunk, 0, remaining);
                            if (read > 0)
                            {
                                buffer.AddRange(chunk.Take(read));
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (TimeoutException)
                        {
                            attempts++;
                            if (buffer.Count >= 4) break;
                        }
                    }

                    if (buffer.Count < 4)
                    {
                        Log("响应数据不足");
                        return null;
                    }

                    var responseFrame = ModbusFrame.ParseResponse(buffer.ToArray());
                    if (responseFrame == null)
                    {
                        Log("CRC校验失败");
                        return null;
                    }

                    if (responseFrame.Data.Length > 0 && (responseFrame.Data[0] & 0x80) != 0)
                    {
                        Log($"Modbus异常: 功能码 0x{responseFrame.Data[0]:X2}, 错误码 {responseFrame.Data[1]}");
                        return null;
                    }

                    return responseFrame;
                }
                catch (Exception ex)
                {
                    Log($"通讯错误: {ex.Message}");
                    return null;
                }
            }
        }

        private void Log(string message)
        {
            LogMessage?.Invoke(this, $"[{DateTime.Now:HH:mm:ss}] {message}");
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}
