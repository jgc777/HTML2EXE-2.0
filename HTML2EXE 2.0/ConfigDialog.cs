using System.Text.Json.Nodes;

namespace HTML2EXE_2._0
{
    public partial class ConfigDialog : Form
    {
        public JsonNode config;
        public BuildDialog buildDialog;
        public ConfigDialog()
        {
            InitializeComponent();
            config = new JsonObject();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        { }

        private void textBox2_TextChanged(object sender, EventArgs e)
        { }

        private void label3_Click(object sender, EventArgs e)
        { }

        public void ConfigDialog_Load(object sender, EventArgs e)
        { }

        private void textBox3_TextChanged(object sender, EventArgs e)
        { }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(urlTextBox.Text)) config["url"] = urlTextBox.Text; // If the URL is not empty, set it as the URL
                if (!string.IsNullOrEmpty(titleTextBox.Text)) config["title"] = titleTextBox.Text; // If the title is not empty, set it as the title
                if (File.Exists(openFileDialog1.FileName)) {
                    File.Copy(openFileDialog1.FileName, Path.Combine(Path.GetTempPath(), "HTML2EXE", "webfiles", Path.GetFileName(openFileDialog1.FileName)), true); // Copy the icon to the webfiles directory
                    config["icon"] = Path.Combine("webfiles", Path.GetFileName(openFileDialog1.FileName)); // Set the icon
                }
                if (!string.IsNullOrEmpty(widthTextBox.Text)) config["width"] = Int32.Parse(widthTextBox.Text); // If the width is not empty, set it as the width
                if (!string.IsNullOrEmpty(heightTextBox.Text)) config["height"] = Int32.Parse(heightTextBox.Text); // If the height is not empty, set it as the height
                if (!string.IsNullOrEmpty(extraCmdTextBox.Text)) config["additional_cmd"] = extraCmdTextBox.Text; // If the extra command is not empty, set it as the extra command
                if (!string.IsNullOrEmpty(cmdArgsTextBox.Text)) config["additional_cmd_args"] = cmdArgsTextBox.Text; // If the extra command arguments are not empty, set them as the extra command arguments
                config["context_menu"] = contextMenu.Checked;
                config["dev_tools"] = devTools.Checked;
                config["maximized"] = maximized.Checked;
                config["maximizable"] = maximizable.Checked;
                config["resizable"] = resizable.Checked;
                config["control_box"] = controlBox.Checked;
                config["minimizable"] = minimizable.Checked;
                config["fullscreen"] = fullscreen.Checked;
                config["always_on_top"] = alwaysOnTop.Checked;
                config["zoom_control"] = zoomControl.Checked;
                config["show_in_taskbar"] = showInTaskbar.Checked;
                config["block_close"] = blockClose.Checked;

                // Clean nulls and order alphabetically
                if (config is JsonObject obj)
                {
                    var keysToRemove = obj.Where(kvp => kvp.Value == null).Select(kvp => kvp.Key).ToList();
                    foreach (var key in keysToRemove)
                        obj.Remove(key);

                    var ordered = new JsonObject();
                    foreach (var kvp in obj.OrderBy(kvp => kvp.Key))
                        ordered[kvp.Key] = kvp.Value?.DeepClone();

                    config = ordered;
                }

                // Write the config to a file
                string configPath = Path.Combine(Path.GetTempPath(), "HTML2EXE", "config.json");
                if (File.Exists(configPath)) File.Delete(configPath);
                File.WriteAllText(configPath, config.ToString());

                // Close this form and show the build dialog
                this.Close();
                buildDialog = new BuildDialog();
                buildDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeIconBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = null;
            iconPathLabel.Text = "No icon";
            removeIconBtn.Visible = false;
        }

        private void iconBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (!string.IsNullOrEmpty(openFileDialog1.FileName)) {
                iconPathLabel.Text = Path.GetFileName(openFileDialog1.FileName);
                removeIconBtn.Visible = true;
            }
        }
    }
}
