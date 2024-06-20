using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FermaBananov
{
    internal class Program
    {
        [DllImport("user32", EntryPoint = "ShowWindow")]
        static extern void Minimize(IntPtr hWnd, int state = 6); //6 - SW_MINIMIZE
        static void Main(string[] args)
        {
            DateTime latest = DateTime.Now;
            var url = "steam://rungameid/2923300/";
            var psi = new ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = url;
            new Thread(s =>
            {
                while (true)
                {
                    try
                    {
                        Console.Title =
                            $"Next start in {(latest + new TimeSpan(3, 0, 0) - DateTime.Now).ToString("g")}";
                    }
                    catch { } //if console window is unavailable by some reason
                }
            }).Start();
            while (true)
            {
                Console.Write($"{(latest = DateTime.Now).ToString("g")} Starting...   ");
                Process.Start(psi);
                Thread.Sleep(5000);
                var process = Process.GetProcessesByName("banana")[0];
                Minimize(process!.MainWindowHandle);
                Thread.Sleep(60 * 1000);
                process.Kill();
                Console.WriteLine("Killed");
                Thread.Sleep(1000 * 60 * 60 * 3);
            }
        }
    }
}
