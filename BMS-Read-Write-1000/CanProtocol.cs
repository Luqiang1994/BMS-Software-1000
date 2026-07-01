namespace BMS_Read_Write_1000;

public static class CanProtocol
{
    public static class Read
    {
        public const uint TotalVoltageCurrentCapacity = 0x300;
        public const uint FullCapacityCyclesRsoc = 0x301;
        public const uint BalanceProtectStatus = 0x302;
        public const uint FETStatus = 0x303;
        public const uint CellCountNtcCount = 0x304;
        public const uint Ntc1to3 = 0x305;
        public const uint Ntc4to6 = 0x306;
        public const uint Ntc7to8 = 0x307;
        public const uint Cell1to3 = 0x308;
        public const uint Cell4to6 = 0x309;
        public const uint Cell7to9 = 0x30A;
        public const uint Cell10to12 = 0x30B;
        public const uint Cell13to15 = 0x30C;
        public const uint Cell16to18 = 0x30D;
        public const uint Cell19to21 = 0x30E;
        public const uint Cell22to24 = 0x30F;
    }

    public static class Write
    {
        public const uint OverVoltageProtect = 0x310;
        public const uint OverVoltageRestore = 0x311;
        public const uint OverVoltageDelay = 0x312;
        public const uint UnderVoltageProtect = 0x313;
        public const uint UnderVoltageRestore = 0x314;
        public const uint UnderVoltageDelay = 0x315;
        public const uint ChargeOvercurrent = 0x316;
        public const uint ChargeOvercurrentDelay = 0x317;
        public const uint DischargeOvercurrentL1 = 0x318;
        public const uint DischargeOvercurrentL1Delay = 0x319;
        public const uint DischargeOvercurrentL2 = 0x31A;
        public const uint DischargeOvercurrentL2Delay = 0x31B;
        public const uint BalanceStartVoltage = 0x31C;
        public const uint BalanceStartPressureDiff = 0x31D;
        public const uint SleepLeakageCurrent = 0x31E;
        public const uint SelfConsumptionPower = 0x31F;
        public const uint FunctionParameter = 0x320;
        public const uint ChargeOverTempProtect = 0x321;
        public const uint ChargeOverTempRestore = 0x322;
        public const uint ChargeUnderTempProtect = 0x323;
        public const uint ChargeUnderTempRestore = 0x324;
        public const uint DischargeOverTempProtect = 0x325;
        public const uint DischargeOverTempRestore = 0x326;
        public const uint DischargeUnderTempProtect = 0x327;
        public const uint DischargeUnderTempRestore = 0x328;
        public const uint MosOverTempProtect = 0x329;
        public const uint MosOverTempRestore = 0x32A;
        public const uint ChargeCorrection = 0x32B;
        public const uint DischargeCorrection = 0x32C;
        public const uint ChargeOnDischargeOff = 0x32D;
        public const uint ChargeOffDischargeOn = 0x32E;
        public const uint ChargeOnDischargeOn = 0x32F;
        public const uint ChargeOffDischargeOff = 0x330;
        public const uint ExitManualMode = 0x331;
        public const uint CurrentZeroing = 0x332;
        public const uint RestoreFactory = 0x333;
    }

    public static readonly uint[] AllReadIds =
    {
        Read.TotalVoltageCurrentCapacity,
        Read.FullCapacityCyclesRsoc,
        Read.BalanceProtectStatus,
        Read.FETStatus,
        Read.CellCountNtcCount,
        Read.Ntc1to3, Read.Ntc4to6, Read.Ntc7to8,
        Read.Cell1to3, Read.Cell4to6, Read.Cell7to9,
        Read.Cell10to12, Read.Cell13to15, Read.Cell16to18,
        Read.Cell19to21, Read.Cell22to24,
    };

    public static ushort Crc16Xmodem(byte[] data, int offset, int length)
    {
        ushort crc = 0;
        for (int i = offset; i < offset + length; i++)
        {
            ushort curVal = (ushort)(data[i] << 8);
            for (int j = 0; j < 8; j++)
            {
                if ((crc ^ curVal) >= 0x8000)
                    crc = (ushort)((crc << 1) ^ 0x1021);
                else
                    crc <<= 1;
                curVal <<= 1;
            }
        }
        return crc;
    }

    public static ushort GetU16(byte[] data, int offset)
    {
        return (ushort)((data[offset] << 8) | data[offset + 1]);
    }

    public static uint GetU32(byte[] data, int offset)
    {
        return (uint)((data[offset] << 24) | (data[offset + 1] << 16) |
                       (data[offset + 2] << 8) | data[offset + 3]);
    }

    public static short GetI16(byte[] data, int offset)
    {
        return (short)((data[offset] << 8) | data[offset + 1]);
    }

    public static bool VerifyCrc(byte[] data, int dataLen)
    {
        if (dataLen < 2) return false;
        ushort expected = (ushort)((data[dataLen - 2] << 8) | data[dataLen - 1]);
        ushort calc = Crc16Xmodem(data, 0, dataLen - 2);
        return expected == calc;
    }

    public static int RawToCelsius(ushort raw)
    {
        return (raw - 2731) / 10;
    }

    public static ushort CelsiusToRaw(int celsius)
    {
        return (ushort)(2731 + celsius * 10);
    }

    [Flags]
    public enum ProtectionFlags : ushort
    {
        OverVoltage = 1 << 0,
        UnderVoltage = 1 << 1,
        DischargeOverTemp = 1 << 2,
        DischargeUnderTemp = 1 << 3,
        ChargeOverTemp = 1 << 4,
        ChargeUnderTemp = 1 << 5,
        DischargeOverCurrent = 1 << 6,
        ChargeOverCurrent = 1 << 7,
        ShortCircuit = 1 << 8,
        MosOverTemp = 1 << 9,
    }

    public static bool IsBitSet(int value, int position)
    {
        return (value & (1 << position)) != 0;
    }

    public static string GetProtectionText(ushort flags)
    {
        var sb = new System.Text.StringBuilder();
        if ((flags & (ushort)ProtectionFlags.OverVoltage) != 0) sb.Append("过压 ");
        if ((flags & (ushort)ProtectionFlags.UnderVoltage) != 0) sb.Append("欠压 ");
        if ((flags & (ushort)ProtectionFlags.DischargeOverTemp) != 0) sb.Append("放电过温 ");
        if ((flags & (ushort)ProtectionFlags.DischargeUnderTemp) != 0) sb.Append("放电低温 ");
        if ((flags & (ushort)ProtectionFlags.ChargeOverTemp) != 0) sb.Append("充电过温 ");
        if ((flags & (ushort)ProtectionFlags.ChargeUnderTemp) != 0) sb.Append("充电低温 ");
        if ((flags & (ushort)ProtectionFlags.DischargeOverCurrent) != 0) sb.Append("放电过流 ");
        if ((flags & (ushort)ProtectionFlags.ChargeOverCurrent) != 0) sb.Append("充电过流 ");
        if ((flags & (ushort)ProtectionFlags.ShortCircuit) != 0) sb.Append("短路 ");
        if ((flags & (ushort)ProtectionFlags.MosOverTemp) != 0) sb.Append("MOS过温 ");
        return sb.Length > 0 ? sb.ToString() : "正常";
    }
}
