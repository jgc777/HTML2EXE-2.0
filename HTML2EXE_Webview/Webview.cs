using System.IO.Compression;
using System.Runtime.Versioning;

namespace Webview
{
    [SupportedOSPlatform("windows6.1")] // Remove warnings
    internal static class Webview
    {
        private static readonly string webfilesZipPath = Path.Combine(AppContext.BaseDirectory, "webfiles.zip");
        public static readonly string webfilesPath = Path.Combine(AppContext.BaseDirectory, "webfiles");
        public static readonly string appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HTML2EXE");
        public static readonly string configPath = Path.Combine(AppContext.BaseDirectory, "config.json");


        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (File.Exists(webfilesZipPath))
                {
                    ZipFile.ExtractToDirectory(webfilesZipPath, AppContext.BaseDirectory);
                    File.Delete(webfilesZipPath);
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
