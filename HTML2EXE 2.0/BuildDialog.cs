using System.Text.Json.Nodes;

namespace HTML2EXE_2._0
{
    public partial class BuildDialog : Form
    {
        public BuildDialog()
        {
            InitializeComponent();
        }

        private void BuildDialog_Load(object sender, EventArgs e)
        {
            this.Text = "HTML2EXE 2.0 v" + (HTML2EXE.IsBigBuild ? HTML2EXE.CurrentVersion + " (BIG)" : HTML2EXE.CurrentVersion);
            this.Visible = true;
            string tmpPath = Path.Combine(Path.GetTempPath(), "HTML2EXE");
            string output = Path.Combine(Environment.CurrentDirectory, "out.exe");
            string configTitle = JsonNode.Parse(File.ReadAllText(Path.Combine(tmpPath, "config.json")))["title"]?.ToString() ?? null;
            if (configTitle != null) output = Path.Combine(Environment.CurrentDirectory, configTitle + ".exe");
            HTML2EXE.build(output);
        }

        private void copyBtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(logTextBox.Text);
            MessageBox.Show("Log copied to clipboard!", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
