using System.Text.Json.Nodes;

namespace HTML2EXE_2
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
            string output = Path.Combine(Environment.CurrentDirectory, "out.exe");
            JsonNode config = JsonNode.Parse(File.ReadAllText(Path.Combine(HTML2EXE.tmpPath, "config.json")));
            string configTitle = config["title"]?.ToString();
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
