namespace BMS_Read_Write_1000
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            if (!TrialManager.CheckTrial())
            {
                MessageBox.Show(
                    "产品试用期已过（30天），请联系软件供应商！",
                    "试用到期",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int remain = TrialManager.GetRemainingDays();
            if (remain <= 7 && remain > 0)
            {
                MessageBox.Show(
                    $"产品试用期还剩 {remain} 天，到期将无法使用！",
                    "试用提醒",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            Application.Run(new Form1());
        }
    }
}