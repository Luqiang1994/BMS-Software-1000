namespace BMS_Read_Write_1000
{
    public static class BmsRegisters
    {
        public const byte SlaveAddress = 0x97;

        public const ushort TotalBatteryStrings = 0x0200;
        public const ushort SingleVoltage1 = 0x0201;
        public const ushort SingleVoltage32 = 0x0220;
        public const ushort Temperature1 = 0x0221;
        public const ushort Temperature8 = 0x0228;
        public const ushort MaxTemperature = 0x0229;
        public const ushort MaxTemperatureNo = 0x022a;
        public const ushort MinTemperature = 0x022b;
        public const ushort MinTemperatureNo = 0x022c;
        public const ushort SingleMaxVoltage = 0x022d;
        public const ushort SingleMaxVoltageNo = 0x022e;
        public const ushort SingleMinVoltage = 0x022f;
        public const ushort SingleMinVoltageNo = 0x0230;
        public const ushort TotalVoltageHigh = 0x0231;
        public const ushort TotalVoltageLow = 0x0232;
        public const ushort CurrentHigh = 0x0233;
        public const ushort CurrentLow = 0x0234;
        public const ushort DesignedCapacityHigh = 0x0235;
        public const ushort DesignedCapacityLow = 0x0236;
        public const ushort StateOfCapacity = 0x0237;
        public const ushort BatteryLoopTimes = 0x0238;
        public const ushort FirmwareVersion = 0x0239;
        public const ushort BatterySequenceNo = 0x023a;

        public const ushort BmsWarningWord1 = 0x023b;
        public const ushort BmsWarningWord2 = 0x023c;
        public const ushort BalancedWord1 = 0x023d;
        public const ushort BalancedWord2 = 0x023e;
        public const ushort DischargeMosState = 0x023f;
        public const ushort ChargeMosState = 0x0240;
        public const ushort BatteryType = 0x0241;

        public const ushort OverVoltageProtectValue = 0x0242;
        public const ushort OverVoltageProtectDelay = 0x0243;
        public const ushort OverVoltageRestoreValue = 0x0244;
        public const ushort UnderVoltageProtectValue = 0x0245;
        public const ushort UnderVoltageProtectDelay = 0x0246;
        public const ushort UnderVoltageRestoreValue = 0x0247;
        public const ushort BalanceStartVoltage = 0x0248;
        public const ushort BalanceStartPressureDiff = 0x0249;
        public const ushort DischargeOvercurrentL1 = 0x024a;
        public const ushort DischargeOvercurrentL1Delay = 0x024b;
        public const ushort DischargeOvercurrentL2 = 0x024c;
        public const ushort DischargeOvercurrentL2Delay = 0x024d;
        public const ushort ChargeOvercurrentValue = 0x024e;
        public const ushort ChargeOvercurrentDelay = 0x024f;
        public const ushort DischargeOvertempProtect = 0x0250;
        public const ushort DischargeOvertempRestore = 0x0251;
        public const ushort DischargeUndertempProtect = 0x0252;
        public const ushort DischargeUndertempRestore = 0x0253;
        public const ushort ChargeOvertempProtect = 0x0254;
        public const ushort ChargeOvertempRestore = 0x0255;
        public const ushort ChargeUndertempProtect = 0x0256;
        public const ushort ChargeUndertempRestore = 0x0257;
        public const ushort RemainingCapacityHigh = 0x0258;
        public const ushort RemainingCapacityLow = 0x0259;
        public const ushort FunctionParameterSet = 0x025a;
        public const ushort SelfConsumptionPower = 0x025b;
        public const ushort SleepLeakageCurrent = 0x025c;
        public const ushort Afe2BatteryStrings = 0x025d;
        public const ushort MosOvertempProtect = 0x025e;
        public const ushort MosOvertempRestore = 0x025f;

        public const ushort ChargeOnDischargeOff = 0x0260;
        public const ushort ChargeOffDischargeOn = 0x0261;
        public const ushort ChargeOnDischargeOn = 0x0262;
        public const ushort ChargeOffDischargeOff = 0x0263;
        public const ushort ExitManualMode = 0x0264;
        public const ushort CurrentZeroing = 0x0265;
        public const ushort ChargingCorrection = 0x0266;
        public const ushort DischargingCorrection = 0x0267;
        public const ushort RestoreFactoryDefault = 0x0268;
        public const ushort HardwareVersion = 0x0269;
        public const ushort TotalNtcCount = 0x026a;
    }

    public enum FunctionCode : byte
    {
        ReadHoldingRegisters = 0x03,
        WriteSingleRegister = 0x06,
        WriteMultipleRegisters = 0x10
    }

    public static class ModbusCrc
    {
        public static ushort Calculate(byte[] data, int offset, int length)
        {
            ushort crc = 0xFFFF;
            for (int i = offset; i < offset + length; i++)
            {
                crc ^= data[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }

        public static bool Verify(byte[] frame)
        {
            if (frame.Length < 4)
                return false;
            ushort expected = Calculate(frame, 0, frame.Length - 2);
            ushort actual = (ushort)(frame[frame.Length - 1] << 8 | frame[frame.Length - 2]);
            return expected == actual;
        }
    }

    public class ModbusFrame
    {
        public byte SlaveAddress { get; private set; }
        public FunctionCode Function { get; private set; }
        public byte[] Data { get; private set; }

        public ModbusFrame(byte slaveAddress, FunctionCode function, byte[] data)
        {
            SlaveAddress = slaveAddress;
            Function = function;
            Data = data;
        }

        public byte[] Build()
        {
            var frame = new byte[2 + Data.Length + 2];
            frame[0] = SlaveAddress;
            frame[1] = (byte)Function;
            Array.Copy(Data, 0, frame, 2, Data.Length);
            ushort crc = ModbusCrc.Calculate(frame, 0, frame.Length - 2);
            frame[frame.Length - 2] = (byte)(crc & 0xFF);
            frame[frame.Length - 1] = (byte)((crc >> 8) & 0xFF);
            return frame;
        }

        public static ModbusFrame CreateReadRequest(byte slaveAddress, ushort startAddress, ushort quantity)
        {
            var data = new byte[4];
            data[0] = (byte)(startAddress >> 8);
            data[1] = (byte)(startAddress & 0xFF);
            data[2] = (byte)(quantity >> 8);
            data[3] = (byte)(quantity & 0xFF);
            return new ModbusFrame(slaveAddress, FunctionCode.ReadHoldingRegisters, data);
        }

        public static ModbusFrame CreateWriteSingleRequest(byte slaveAddress, ushort address, ushort value)
        {
            var data = new byte[4];
            data[0] = (byte)(address >> 8);
            data[1] = (byte)(address & 0xFF);
            data[2] = (byte)(value >> 8);
            data[3] = (byte)(value & 0xFF);
            return new ModbusFrame(slaveAddress, FunctionCode.WriteSingleRegister, data);
        }

        public static ModbusFrame CreateWriteMultipleRequest(byte slaveAddress, ushort startAddress, ushort[] values)
        {
            int byteCount = values.Length * 2;
            var data = new byte[5 + byteCount];
            data[0] = (byte)(startAddress >> 8);
            data[1] = (byte)(startAddress & 0xFF);
            data[2] = (byte)(values.Length >> 8);
            data[3] = (byte)(values.Length & 0xFF);
            data[4] = (byte)byteCount;
            for (int i = 0; i < values.Length; i++)
            {
                data[5 + i * 2] = (byte)(values[i] >> 8);
                data[5 + i * 2 + 1] = (byte)(values[i] & 0xFF);
            }
            return new ModbusFrame(slaveAddress, FunctionCode.WriteMultipleRegisters, data);
        }

        public static ModbusFrame? ParseResponse(byte[] response)
        {
            if (response.Length < 4)
                return null;
            if (!ModbusCrc.Verify(response))
                return null;

            byte slave = response[0];
            var func = (FunctionCode)response[1];
            var data = new byte[response.Length - 4];
            Array.Copy(response, 2, data, 0, data.Length);
            return new ModbusFrame(slave, func, data);
        }

        public ushort[] GetUInt16Values()
        {
            if (Function == FunctionCode.ReadHoldingRegisters && Data.Length > 0)
            {
                int byteCount = Data[0];
                int count = byteCount / 2;
                var result = new ushort[count];
                for (int i = 0; i < count; i++)
                {
                    result[i] = (ushort)(Data[1 + i * 2] << 8 | Data[1 + i * 2 + 1]);
                }
                return result;
            }
            return Array.Empty<ushort>();
        }
    }
}
