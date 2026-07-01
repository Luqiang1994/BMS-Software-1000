using System.Text;

namespace BMS_Read_Write_1000;

public static class TrialManager
{
    const int TrialDays = 30;
    const byte XorKey = 0x5A;
    static readonly string StorageDir = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "BMS-Read-Write-1000");
    static readonly string StoragePath = Path.Combine(StorageDir, "trial.dat");

    static string Encrypt(string text)
    {
        var data = Encoding.UTF8.GetBytes(text);
        for (int i = 0; i < data.Length; i++)
            data[i] ^= XorKey;
        return Convert.ToBase64String(data);
    }

    static string Decrypt(string base64)
    {
        var data = Convert.FromBase64String(base64);
        for (int i = 0; i < data.Length; i++)
            data[i] ^= XorKey;
        return Encoding.UTF8.GetString(data);
    }

    public static bool CheckTrial()
    {
        if (!File.Exists(StoragePath))
        {
            Directory.CreateDirectory(StorageDir);
            File.WriteAllText(StoragePath, Encrypt(DateTime.Now.ToString("yyyy-MM-dd")));
            return true;
        }

        try
        {
            var installDate = DateTime.ParseExact(
                Decrypt(File.ReadAllText(StoragePath)),
                "yyyy-MM-dd", null);
            return (DateTime.Now - installDate).TotalDays <= TrialDays;
        }
        catch
        {
            return false;
        }
    }

    public static int GetRemainingDays()
    {
        if (!File.Exists(StoragePath)) return TrialDays;

        try
        {
            var installDate = DateTime.ParseExact(
                Decrypt(File.ReadAllText(StoragePath)),
                "yyyy-MM-dd", null);
            return Math.Max(0, TrialDays - (int)(DateTime.Now - installDate).TotalDays);
        }
        catch
        {
            return 0;
        }
    }
}
