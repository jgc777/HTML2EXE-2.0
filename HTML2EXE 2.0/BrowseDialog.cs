
using System.Diagnostics;

namespace HTML2EXE_2
{
    public partial class BrowseDialog : Form
    {
        public ConfigDialog? configDialog;

        public BrowseDialog()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = $"HTML2EXE 2.0 v{(HTML2EXE.IsBigBuild ? $"{HTML2EXE.CurrentVersion} (BIG)" : HTML2EXE.CurrentVersion)}";
        }

        private void selectFileBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string htmlPath = openFileDialog1.FileName;
            if (File.Exists(htmlPath))
            {
                File.Copy(htmlPath, Path.Combine(HTML2EXE.tmpWebfilesPath, Path.GetFileName(htmlPath)), true);
                Visible = false;
                configDialog = new ConfigDialog();
                configDialog.ShowDialog();
            }
            Close();
        }

        private void selectFolderBtn_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            string folderPath = folderBrowserDialog1.SelectedPath;

            if (Directory.Exists(folderPath))
            {
                HTML2EXE.CopyDirectory(folderPath, HTML2EXE.tmpWebfilesPath);

                Visible = false;
                configDialog = new ConfigDialog();
                configDialog.ShowDialog();
            }
            Close();
        }



        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void noFileBtn_Click_1(object sender, EventArgs e)
        {
            Visible = false;
            configDialog = new ConfigDialog();
            configDialog.ShowDialog();
            Close();
        }

        private void websiteBtn_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://jgc777.github.io/HTML2EXE-2.0/",
                UseShellExecute = true
            });
        }
    }
}
