using System.Diagnostics;
using System.IO.Compression;

namespace Webview {
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0 && (args[0] == "-h" || args[0] == "--help" || args[0] == "/?" || args[0] == "/help"))
                Process.Start(new ProcessStartInfo {
                    FileName = "https://jgc.corefn.xyz/HTML2EXE-2.0/",
                    UseShellExecute = true
                });
                else {
                    if (File.Exists(Path.Combine(Environment.CurrentDirectory, "webfiles.zip"))) {
                        ZipFile.ExtractToDirectory(Path.Combine(Environment.CurrentDirectory, "webfiles.zip"), Environment.CurrentDirectory);
                        File.Delete(Path.Combine(Environment.CurrentDirectory, "webfiles.zip"));
                    }
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new WebviewForm());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
