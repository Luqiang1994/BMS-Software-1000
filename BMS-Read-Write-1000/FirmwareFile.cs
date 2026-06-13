namespace BMS_Read_Write_1000
{
    public class FirmwareFile
    {
        public string FilePath { get; }
        public string FileName { get; }
        public int FileSize { get; }
        public byte[] Data { get; }
        public ushort Crc16 { get; }

        private FirmwareFile(string filePath, byte[] data)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
            FileSize = data.Length;
            Data = data;
            Crc16 = ModbusCrc.Calculate(data, 0, data.Length);
        }

        public static FirmwareFile Load(string filePath)
        {
            var data = File.ReadAllBytes(filePath);
            return new FirmwareFile(filePath, data);
        }

        public ushort CalculateChunkCrc(int offset, int length)
        {
            if (offset < 0 || offset + length > Data.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));
            return ModbusCrc.Calculate(Data, offset, length);
        }

        public byte[] GetChunk(int offset, int length)
        {
            if (offset < 0 || length < 0 || offset + length > Data.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));
            var chunk = new byte[length];
            Array.Copy(Data, offset, chunk, 0, length);
            return chunk;
        }

        public int ChunkCount(int chunkSize)
        {
            return (FileSize + chunkSize - 1) / chunkSize;
        }
    }
}
