
using System.Diagnostics;

namespace HTML2EXE_2
{
    public partial class BrowseDialog : Form
    {
        public ConfigDialog configDialog;

        public BrowseDialog()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = $"HTML2EXE 2.0 v{(HTML2EXE.IsBigBuild ? $"{HTML2EXE.CurrentVersion} (BIG)" : HTML2EXE.CurrentVersion)}";
        }

        private void selectFileBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string htmlPath = openFileDialog1.FileName;
            if (File.Exists(htmlPath))
            {
                string destinationPath = Path.Combine(HTML2EXE.tmpPath, "webfiles");
                Directory.CreateDirectory(destinationPath);
                File.Copy(htmlPath, Path.Combine(destinationPath, Path.GetFileName(htmlPath)), true);
                this.Visible = false;
                configDialog = new ConfigDialog();
                configDialog.ShowDialog();
            }
            this.Close();
        }

        private void selectFolderBtn_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            string folderPath = folderBrowserDialog1.SelectedPath;

            if (Directory.Exists(folderPath))
            {
                string destinationPath = Path.Combine(HTML2EXE.tmpPath, "webfiles");

                CopyDirectory(folderPath, destinationPath);

                this.Visible = false;
                configDialog = new ConfigDialog();
                configDialog.ShowDialog();
            }
            this.Close();
        }

        private void CopyDirectory(string sourceDir, string destinationDir)
        {
            if (!Directory.Exists(destinationDir)) Directory.CreateDirectory(destinationDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            foreach (var directory in Directory.GetDirectories(sourceDir))
            {
                string destDir = Path.Combine(destinationDir, Path.GetFileName(directory));
                CopyDirectory(directory, destDir);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void noFileBtn_Click_1(object sender, EventArgs e)
        {
            string destinationPath = Path.Combine(HTML2EXE.tmpPath, "webfiles");
            Directory.CreateDirectory(destinationPath);
            this.Visible = false;
            configDialog = new ConfigDialog();
            configDialog.ShowDialog();
            this.Close();
        }

        private void cancelBtn_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void websiteBtn_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo {
                FileName = "https://jgc777.github.io/HTML2EXE-2.0/",
                UseShellExecute = true
            });
        }
    }
}
