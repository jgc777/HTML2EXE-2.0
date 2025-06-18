using System.Text.Json.Nodes;

namespace HTML2EXE_2
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
            this.Text = $"HTML2EXE 2.0 v{(HTML2EXE.IsBigBuild ? $"{HTML2EXE.CurrentVersion} (BIG)" : HTML2EXE.CurrentVersion)}";
            this.includeNETbox.Checked = HTML2EXE.IsBigBuild; // Set the include .NET runtime checkbox based on the build type
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
                // Write the config to a file
                string configPath = Path.Combine(HTML2EXE.tmpPath, "config.json");
                if (File.Exists(configPath)) File.Delete(configPath);
                File.WriteAllText(configPath, generateConfigJson().ToString());

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

        private JsonNode generateConfigJson(bool export = false, bool removenulls = true) // Export meass we're saving the config as an exportation and not a build
        {
            if (!string.IsNullOrEmpty(urlTextBox.Text)) config["url"] = urlTextBox.Text; // If the URL is not empty, set it as the URL
            else config["url"] = null; // If the URL is empty, set it as null
            if (!string.IsNullOrEmpty(titleTextBox.Text)) config["title"] = titleTextBox.Text; // If the title is not empty, set it as the title
            else config["title"] = null; // If the title is empty, set it as null
            if (!string.IsNullOrEmpty(iconPath)) {
                if (iconPath.StartsWith("http://") || iconPath.StartsWith("https://")) {
                    try {
                        string tempIconPath = Path.Combine(HTML2EXE.tmpPath, "webfiles", "icon.ico");
                        using (var client = new HttpClient()) {
                            var data = client.GetByteArrayAsync(iconPath).Result;
                            File.WriteAllBytes(tempIconPath, data);
                        }
                        config["icon"] = Path.Combine("webfiles", "icon.ico");
                    }
                    catch (Exception ex) {
                        MessageBox.Show("Error downloading icon: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        config["icon"] = null;
                    }
                }
                else {
                    config["icon"] = Path.Combine("webfiles", Path.GetFileName(iconPath)); // Set the icon
                    if (File.Exists(iconPath)) File.Copy(iconPath, Path.Combine(HTML2EXE.tmpPath, "webfiles", Path.GetFileName(iconPath)), true); // Copy the icon to the webfiles directory
                }
            }
            else config["icon"] = null; // If the icon is empty, set it as null
            if (!string.IsNullOrEmpty(widthTextBox.Text)) config["width"] = Int32.Parse(widthTextBox.Text); // If the width is not empty, set it as the width
            else config["width"] = null; // If the width is empty, set it as null
            if (!string.IsNullOrEmpty(heightTextBox.Text)) config["height"] = Int32.Parse(heightTextBox.Text); // If the height is not empty, set it as the height
            else config["height"] = null; // If the height is empty, set it as null
            if (!string.IsNullOrEmpty(extraCmdTextBox.Text)) config["additional_cmd"] = extraCmdTextBox.Text; // If the extra command is not empty, set it as the extra command
            else config["additional_cmd"] = null; // If the extra command is empty, set it as null
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
            if (export) config["include_runtime"] = includeNETbox.Checked; // If exporting, set the include runtime option
            HTML2EXE.webviewURL = includeNETbox.Checked ? HTML2EXE.webview_big : HTML2EXE.webview;

            // Clean nulls and order alphabetically
            if (config is JsonObject obj)
            {
                if (removenulls) obj.Where(kvp => kvp.Value == null).Select(kvp => kvp.Key).ToList().ForEach(key => obj.Remove(key)); // Remove null values

                var ordered = new JsonObject();
                foreach (var kvp in obj.OrderBy(kvp => kvp.Key)) // Order the config alphabetically
                    ordered[kvp.Key] = kvp.Value?.DeepClone();

                return ordered;
            }
            return config;
        }

        private void removeIconBtn_Click(object sender, EventArgs e)
        {
            iconOpener.FileName = null;
            iconPath = null;
            iconPathLabel.Text = "No icon";
            removeIconBtn.Visible = false;
        }

        private void iconBtn_Click(object sender, EventArgs e)
        {
            iconOpener.ShowDialog();
            if (!string.IsNullOrEmpty(iconOpener.FileName) && File.Exists(iconOpener.FileName))
            {
                iconPathLabel.Text = Path.GetFileName(iconOpener.FileName);
                removeIconBtn.Visible = true;
                iconPath = iconOpener.FileName;
            }
        }

        private void loadConfigBtn_Click(object sender, EventArgs e)
        {
            jsonOpener.ShowDialog();
            try
            {
                if (!string.IsNullOrEmpty(jsonOpener.FileName) && File.Exists(jsonOpener.FileName))
                {
                    JsonNode config = JsonNode.Parse(File.ReadAllText(jsonOpener.FileName)); // Load the config file
                    // Set inputs
                    if (config["maximized"] != null) maximized.Checked = config["maximized"].GetValue<bool>();
                    if (config["resizable"] != null) resizable.Checked = config["resizable"].GetValue<bool>();
                    if (config["control_box"] != null) controlBox.Checked = config["control_box"].GetValue<bool>();
                    if (config["minimizable"] != null) minimizable.Checked = config["minimizable"].GetValue<bool>();
                    if (config["maximizable"] != null) maximizable.Checked = config["maximizable"].GetValue<bool>();
                    if (config["fullscreen"] != null) fullscreen.Checked = config["fullscreen"].GetValue<bool>();
                    if (config["always_on_top"] != null) alwaysOnTop.Checked = config["always_on_top"].GetValue<bool>();
                    if (config["zoom_control"] != null) zoomControl.Checked = config["zoom_control"].GetValue<bool>();
                    if (config["show_in_taskbar"] != null) showInTaskbar.Checked = config["show_in_taskbar"].GetValue<bool>();
                    if (config["context_menu"] != null) contextMenu.Checked = config["context_menu"].GetValue<bool>();
                    if (config["dev_tools"] != null) devTools.Checked = config["dev_tools"].GetValue<bool>();
                    if (config["block_close"] != null) blockClose.Checked = config["block_close"].GetValue<bool>();
                    if (config["url"] != null) urlTextBox.Text = config["url"].ToString();
                    if (config["title"] != null) titleTextBox.Text = config["title"].ToString();
                    if (config["width"] != null) widthTextBox.Text = config["width"].ToString();
                    if (config["height"] != null) heightTextBox.Text = config["height"].ToString();
                    if (config["additional_cmd"] != null) extraCmdTextBox.Text = config["additional_cmd"].ToString();

                    if (!string.IsNullOrEmpty(config["icon"]?.ToString())) // Set icon
                    {
                        // Define icon path
                        if (File.Exists(Environment.ExpandEnvironmentVariables(config["icon"].ToString()))) iconPath = Environment.ExpandEnvironmentVariables(config["icon"].ToString());
                        else if (File.Exists(Path.Combine(Environment.CurrentDirectory, config["icon"].ToString()))) iconPath = Path.Combine(Environment.CurrentDirectory, config["icon"].ToString());
                        else if (config["icon"].ToString().StartsWith("http://") || config["icon"].ToString().StartsWith("https://")) iconPath = config["icon"].ToString();

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {}

        private void saveConfigBtnClick(object sender, EventArgs e)
        {
            jsonSaver.ShowDialog();
            File.WriteAllText(jsonSaver.FileName, generateConfigJson(true, false).ToString()); // Generate and save the config
        }
    }
}
