using System.Text.Json.Nodes;

namespace HTML2EXE_2._0
{
    public partial class ConfigDialog : Form
    {
        public JsonNode config;
        public BuildDialog buildDialog;
        public string iconPath = null;
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
        {
            this.Text = "HTML2EXE 2.0 v" + (Program.IsBigBuild ? Program.CurrentVersion + " (BIG)" : Program.CurrentVersion);
        }

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
                if (File.Exists(iconPath))
                {
                    File.Copy(iconPath, Path.Combine(Path.GetTempPath(), "HTML2EXE", "webfiles", Path.GetFileName(iconPath)), true); // Copy the icon to the webfiles directory
                    config["icon"] = Path.Combine("webfiles", Path.GetFileName(iconPath)); // Set the icon
                }
                if (!string.IsNullOrEmpty(widthTextBox.Text)) config["width"] = Int32.Parse(widthTextBox.Text); // If the width is not empty, set it as the width
                if (!string.IsNullOrEmpty(heightTextBox.Text)) config["height"] = Int32.Parse(heightTextBox.Text); // If the height is not empty, set it as the height
                if (!string.IsNullOrEmpty(extraCmdTextBox.Text)) config["additional_cmd"] = extraCmdTextBox.Text; // If the extra command is not empty, set it as the extra command
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
            iconPath = null;
            iconPathLabel.Text = "No icon";
            removeIconBtn.Visible = false;
        }

        private void iconBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (!string.IsNullOrEmpty(openFileDialog1.FileName) && File.Exists(openFileDialog1.FileName))
            {
                iconPathLabel.Text = Path.GetFileName(openFileDialog1.FileName);
                removeIconBtn.Visible = true;
                iconPath = openFileDialog1.FileName;
            }
        }

        private void loadConfigBtn_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            try {
                if (!string.IsNullOrEmpty(openFileDialog2.FileName) && File.Exists(openFileDialog2.FileName))
                {
                    JsonNode config = JsonNode.Parse(File.ReadAllText(openFileDialog2.FileName)); // Load the config file
                    // Set inputs
                    if (config["maximized"]!=null) maximized.Checked = config["maximized"].GetValue<bool>();
                    if (config["resizable"]!=null) resizable.Checked = config["resizable"].GetValue<bool>();
                    if (config["control_box"]!=null) controlBox.Checked = config["control_box"].GetValue<bool>();
                    if (config["minimizable"]!=null) minimizable.Checked = config["minimizable"].GetValue<bool>();
                    if (config["maximizable"]!=null) maximizable.Checked = config["maximizable"].GetValue<bool>();
                    if (config["fullscreen"]!=null) fullscreen.Checked = config["fullscreen"].GetValue<bool>();
                    if (config["always_on_top"]!=null) alwaysOnTop.Checked = config["always_on_top"].GetValue<bool>();
                    if (config["zoom_control"]!=null) zoomControl.Checked = config["zoom_control"].GetValue<bool>();
                    if (config["show_in_taskbar"]!=null) showInTaskbar.Checked = config["show_in_taskbar"].GetValue<bool>();
                    if (config["context_menu"]!=null) contextMenu.Checked = config["context_menu"].GetValue<bool>();
                    if (config["dev_tools"]!=null) devTools.Checked = config["dev_tools"].GetValue<bool>();
                    if (config["block_close"]!=null) blockClose.Checked = config["block_close"].GetValue<bool>();
                    if (config["url"]!=null) urlTextBox.Text = config["url"].ToString();
                    if (config["title"]!=null) titleTextBox.Text = config["title"].ToString();
                    if (config["width"]!=null) widthTextBox.Text = config["width"].ToString();
                    if (config["height"]!=null) heightTextBox.Text = config["height"].ToString();
                    if (config["additional_cmd"]!=null) extraCmdTextBox.Text = config["additional_cmd"].ToString();
                    
                    if (!string.IsNullOrEmpty(config["icon"]?.ToString())) // Set icon
                    {
                        // Define icon path
                        if (File.Exists(Environment.ExpandEnvironmentVariables(config["icon"].ToString()))) iconPath = Environment.ExpandEnvironmentVariables(config["icon"].ToString());
                        if (File.Exists(Path.Combine(Directory.GetParent(openFileDialog2.FileName)?.FullName ?? string.Empty, config["icon"].ToString()))) iconPath = Path.Combine(Directory.GetParent(openFileDialog2.FileName)?.FullName ?? string.Empty, config["icon"].ToString());
                        if (File.Exists(Path.Combine(Environment.CurrentDirectory, config["icon"].ToString()))) iconPath = Path.Combine(Environment.CurrentDirectory, config["icon"].ToString());
                        
                        iconPathLabel.Text = Path.GetFileName(iconPath);
                        removeIconBtn.Visible = true;
                    }
                    else
                    {
                        iconPathLabel.Text = "No icon";
                        removeIconBtn.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
