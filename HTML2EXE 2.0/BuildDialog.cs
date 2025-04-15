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
            string tmpPath = Path.Combine(Path.GetTempPath(), "HTML2EXE");
            Task.Delay(1500);
            logTextBox.Cursor = Cursors.Default; // Default cursor
            logTextBox.GotFocus += (s, ev) => logTextBox.Parent.Focus(); // Prevent focus on the RichTextBox
            string output = Path.Combine(Environment.CurrentDirectory, "out.exe");
            if (JsonNode.Parse(File.ReadAllText(Path.Combine(tmpPath, "config.json")))["title"] != null) output = Path.Combine(Environment.CurrentDirectory, JsonNode.Parse(File.ReadAllText(Path.Combine(tmpPath, "config.json")))["title"] + ".exe");
            Program.build(output);
        }

        private void copyBtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(logTextBox.Text);
            MessageBox.Show("Log copied to clipboard!", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
