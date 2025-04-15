
namespace HTML2EXE_2._0
{
    public partial class BrowseDialog : Form
    {
        public ConfigDialog configDialog;

        public BrowseDialog()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { }

        private void selectFileBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string htmlPath = openFileDialog1.FileName;
            if (File.Exists(htmlPath))
            {
                string destinationPath = Path.Combine(Path.GetTempPath(), "HTML2EXE", "webfiles");
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
                string destinationPath = Path.Combine(Path.GetTempPath(), "HTML2EXE", "webfiles");

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
            string destinationPath = Path.Combine(Path.GetTempPath(), "HTML2EXE", "webfiles");
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
    }
}
