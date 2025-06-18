using System.Diagnostics;
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
            JsonNode config = JsonNode.Parse(File.ReadAllText(Path.Combine(HTML2EXE.tmpPath, "config.json"))) ?? new JsonObject();
            string output = (config["title"]?.ToString() != null) ? Path.Combine(Environment.CurrentDirectory, $"{config["title"]?.ToString()}.exe") : Path.Combine(Environment.CurrentDirectory, "out.exe");
            // Set the output path using the config title or default to "out.exe"
            try {
                HTML2EXE.build(output);
                this.Close();
                Process.Start("explorer.exe", $"/select, \"{output}\"");
            } catch (Exception ex) {
                HTML2EXE.log($"Build error: {ex.Message}",true,false,true);
            }
        }

        private void copyBtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(logTextBox.Text);
            MessageBox.Show("Log copied to clipboard!", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
