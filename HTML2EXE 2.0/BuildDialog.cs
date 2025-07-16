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
            this.Text = $"HTML2EXE 2.0 v{(HTML2EXE.IsBigBuild ? $"{HTML2EXE.CurrentVersion} (BIG)" : HTML2EXE.CurrentVersion)}";
            this.Visible = true;
            JsonNode config = JsonNode.Parse(File.ReadAllText(HTML2EXE.tmpConfigJson)) ?? new JsonObject();
            string output = Path.Combine(Environment.CurrentDirectory, (config["title"]?.ToString() is not null) ?  $"{config["title"]!.ToString()}.exe" : "out.exe");
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
