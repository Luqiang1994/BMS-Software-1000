using System.Runtime.InteropServices;

namespace BMS_Read_Write_1000;

[StructLayout(LayoutKind.Sequential)]
public struct VCI_BOARD_INFO
{
    public ushort hw_Version;
    public ushort fw_Version;
    public ushort dr_Version;
    public ushort in_Version;
    public ushort irq_Num;
    public byte can_Num;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public byte[] str_Serial_Num;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
    public byte[] str_hw_Type;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public byte[] Reserved;

    public VCI_BOARD_INFO(int _)
    {
        str_Serial_Num = new byte[20];
        str_hw_Type = new byte[40];
        Reserved = new byte[8];
    }
}

public struct VCI_CAN_OBJ
{
    public uint ID;
    public uint TimeStamp;
    public byte TimeFlag;
    public byte SendType;
    public byte RemoteFlag;
    public byte ExternFlag;
    public byte DataLen;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public byte[] Data;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public byte[] Reserved;

    public VCI_CAN_OBJ(int _)
    {
        Data = new byte[8];
        Reserved = new byte[3];
    }
}

public struct VCI_INIT_CONFIG
{
    public uint AccCode;
    public uint AccMask;
    public uint Reserved;
    public byte Filter;
    public byte Timing0;
    public byte Timing1;
    public byte Mode;
}

public struct VCI_BOARD_INFO1
{
    public ushort hw_Version;
    public ushort fw_Version;
    public ushort dr_Version;
    public ushort in_Version;
    public ushort irq_Num;
    public byte can_Num;
    public byte Reserved;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public byte[] str_Serial_Num;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public byte[] str_hw_Type;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public byte[] str_Usb_Serial;

    public VCI_BOARD_INFO1(int _)
    {
        str_Serial_Num = new byte[8];
        str_hw_Type = new byte[16];
        str_Usb_Serial = new byte[16];
    }
}

public class CanDevice : IDisposable
{
    public const uint DEV_USBCAN = 3;
    public const uint DEV_USBCAN2 = 4;

    static CanDevice()
    {
        var arch = Environment.Is64BitProcess ? "x64" : "x86";
        var dllDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, arch);
        var dllPath = Path.Combine(dllDir, "ControlCAN.dll");
        if (File.Exists(dllPath))
            NativeLibrary.Load(dllPath);
    }

    [DllImport("controlcan.dll")]
    static extern uint VCI_OpenDevice(uint DeviceType, uint DeviceInd, uint Reserved);

    [DllImport("controlcan.dll")]
    static extern uint VCI_CloseDevice(uint DeviceType, uint DeviceInd);

    [DllImport("controlcan.dll")]
    static extern uint VCI_InitCAN(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_INIT_CONFIG pInitConfig);

    [DllImport("controlcan.dll")]
    static extern uint VCI_ReadBoardInfo(uint DeviceType, uint DeviceInd, ref VCI_BOARD_INFO pInfo);

    [DllImport("controlcan.dll")]
    static extern uint VCI_GetReceiveNum(uint DeviceType, uint DeviceInd, uint CANInd);

    [DllImport("controlcan.dll")]
    static extern uint VCI_ClearBuffer(uint DeviceType, uint DeviceInd, uint CANInd);

    [DllImport("controlcan.dll")]
    static extern uint VCI_StartCAN(uint DeviceType, uint DeviceInd, uint CANInd);

    [DllImport("controlcan.dll")]
    static extern uint VCI_ResetCAN(uint DeviceType, uint DeviceInd, uint CANInd);

    [DllImport("controlcan.dll")]
    static extern uint VCI_Transmit(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_CAN_OBJ pSend, uint Len);

    [DllImport("controlcan.dll")]
    static extern uint VCI_Receive(uint DeviceType, uint DeviceInd, uint CANInd, [In, Out] VCI_CAN_OBJ[] pReceive, uint Len, int WaitTime);

    [DllImport("controlcan.dll")]
    static extern uint VCI_ConnectDevice(uint DevType, uint DevIndex);

    [DllImport("controlcan.dll")]
    static extern uint VCI_UsbDeviceReset(uint DevType, uint DevIndex, uint Reserved);

    [DllImport("controlcan.dll")]
    static extern uint VCI_FindUsbDevice2(ref VCI_BOARD_INFO pInfo);

    uint _devType = DEV_USBCAN2;
    uint _devIndex;
    uint _canIndex;
    bool _isOpen;
    VCI_CAN_OBJ[] _recvBuffer = new VCI_CAN_OBJ[1000];
    CancellationTokenSource? _cts;

    public bool IsOpen => _isOpen;
    public uint DeviceType => _devType;
    public uint DeviceIndex => _devIndex;
    public uint CanIndex => _canIndex;

    public event Action<VCI_CAN_OBJ>? DataReceived;
    public event Action<string>? LogMessage;

    public bool Open(uint devType, uint devIndex, uint canIndex, VCI_INIT_CONFIG config)
    {
        if (_isOpen)
            Close();

        _devType = devType;
        _devIndex = devIndex;
        _canIndex = canIndex;

        if (VCI_OpenDevice(_devType, _devIndex, 0) != 0)
            return false;

        _isOpen = true;

        if (VCI_InitCAN(_devType, _devIndex, _canIndex, ref config) != 0)
        {
            VCI_StartCAN(_devType, _devIndex, _canIndex);
            StartReceiveThread();
            return true;
        }

        Close();
        return false;
    }

    public void Close()
    {
        StopReceiveThread();

        if (_isOpen)
        {
            VCI_ResetCAN(_devType, _devIndex, _canIndex);
            VCI_CloseDevice(_devType, _devIndex);
            _isOpen = false;
        }
    }

    public bool StartCAN()
    {
        if (!_isOpen) return false;
        return VCI_StartCAN(_devType, _devIndex, _canIndex) == 0;
    }

    public void ResetCAN()
    {
        if (!_isOpen) return;
        VCI_ResetCAN(_devType, _devIndex, _canIndex);
    }

    public bool Transmit(ref VCI_CAN_OBJ obj)
    {
        if (!_isOpen) return false;
        return VCI_Transmit(_devType, _devIndex, _canIndex, ref obj, 1) != 0;
    }

    public void ClearBuffer()
    {
        if (!_isOpen) return;
        VCI_ClearBuffer(_devType, _devIndex, _canIndex);
    }

    void StartReceiveThread()
    {
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        Task.Run(() =>
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    uint count = VCI_Receive(_devType, _devIndex, _canIndex, _recvBuffer, 1000, 100);
                    if (count == 0xFFFFFFFF) continue;

                    for (uint i = 0; i < count; i++)
                    {
                        DataReceived?.Invoke(_recvBuffer[i]);
                    }
                }
                catch
                {
                }
            }
        }, token);
    }

    void StopReceiveThread()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }

    public bool SendRemoteFrame(uint id, bool externFlag = false)
    {
        if (!_isOpen) return false;
        var obj = new VCI_CAN_OBJ(0)
        {
            ID = id,
            RemoteFlag = 1,
            ExternFlag = (byte)(externFlag ? 1 : 0),
            DataLen = 0,
            SendType = 0
        };
        return Transmit(ref obj);
    }

    public bool TransmitDataFrame(uint id, byte[] data, bool externFlag = false)
    {
        if (!_isOpen || data.Length > 8) return false;
        var obj = new VCI_CAN_OBJ(0)
        {
            ID = id,
            RemoteFlag = 0,
            ExternFlag = (byte)(externFlag ? 1 : 0),
            DataLen = (byte)data.Length,
            SendType = 0
        };
        Array.Copy(data, obj.Data, data.Length);
        return Transmit(ref obj);
    }

    public bool Write2ByteParam(uint id, ushort value)
    {
        var data = new byte[4];
        data[0] = (byte)(value >> 8);
        data[1] = (byte)value;
        ushort crc = CanProtocol.Crc16Xmodem(data, 0, 2);
        data[2] = (byte)(crc >> 8);
        data[3] = (byte)crc;
        return TransmitDataFrame(id, data);
    }

    public bool Write4ByteParam(uint id, uint value)
    {
        var data = new byte[6];
        data[0] = (byte)(value >> 24);
        data[1] = (byte)(value >> 16);
        data[2] = (byte)(value >> 8);
        data[3] = (byte)value;
        ushort crc = CanProtocol.Crc16Xmodem(data, 0, 4);
        data[4] = (byte)(crc >> 8);
        data[5] = (byte)crc;
        return TransmitDataFrame(id, data);
    }

    public bool WriteCommand(uint id, byte b0, byte b1)
    {
        var data = new byte[4];
        data[0] = b0;
        data[1] = b1;
        ushort crc = CanProtocol.Crc16Xmodem(data, 0, 2);
        data[2] = (byte)(crc >> 8);
        data[3] = (byte)crc;
        return TransmitDataFrame(id, data);
    }

    public void Dispose()
    {
        Close();
    }
}
