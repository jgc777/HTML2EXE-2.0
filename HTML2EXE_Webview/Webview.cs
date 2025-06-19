using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Webview {
    internal static class Webview
    {
        [STAThread]
        [SupportedOSPlatform("windows6.1")] // Remove warnings
        static void Main(string[] args)
        {
            try
            {
                if (File.Exists(Path.Combine(Environment.CurrentDirectory, "webfiles.zip"))) {
                    ZipFile.ExtractToDirectory(Path.Combine(Environment.CurrentDirectory, "webfiles.zip"), Environment.CurrentDirectory);
                    File.Delete(Path.Combine(Environment.CurrentDirectory, "webfiles.zip"));
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new WebviewForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
