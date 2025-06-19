using System.Text.Json.Nodes;

namespace HTML2EXE_2
{
    public partial class ConfigDialog : Form
    {
        public JsonNode config = new JsonObject();
        public BuildDialog buildDialog;
        public string iconPath = null;

        private Dictionary<string, CheckBox> checkBoxes;
        private Dictionary<string, TextBox> textBoxes;
        private Dictionary<string, TextBox> intTextBoxes;
        public ConfigDialog()
        {
            InitializeComponent();
            
            checkBoxes = new Dictionary<string, CheckBox> {
                { "maximized", maximized },
                { "resizable", resizable },
                { "control_box", controlBox },
                { "minimizable", minimizable },
                { "maximizable", maximizable },
                { "fullscreen", fullscreen },
                { "always_on_top", alwaysOnTop },
                { "zoom_control", zoomControl },
                { "show_in_taskbar", showInTaskbar },
                { "context_menu", contextMenu },
                { "dev_tools", devTools },
                { "block_close", blockClose },
                { "include_runtime", includeNETbox }
            };
            textBoxes = new Dictionary<string, TextBox> {
                { "url", urlTextBox },
                { "title", titleTextBox },
                { "additional_cmd", extraCmdTextBox }
            };
            intTextBoxes = new Dictionary<string, TextBox> {
                { "width", widthTextBox },
                { "height", heightTextBox }
            };
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
                if (File.Exists(HTML2EXE.tempConfigJson)) File.Delete(HTML2EXE.tempConfigJson);
                File.WriteAllText(HTML2EXE.tempConfigJson, generateConfigJson().ToString());

                // Close this form and show the build dialog
                this.Close();
                buildDialog = new BuildDialog();
                buildDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private JsonNode generateConfigJson(bool export = false, bool removenulls = true) // Export meass we're saving the config as an exportation and not a build
        {
            // Set checkboxes ignoring includeNETbox
            foreach (var option in checkBoxes.Except(new Dictionary<string, CheckBox> {{"include_runtime",includeNETbox}})) config[option.Key] = option.Value.Checked;
            // Set textboxes
            foreach (var option in textBoxes) config[option.Key] = string.IsNullOrEmpty(option.Value.Text) ? null : option.Value.Text;
            // Set int textboxes
            foreach (var option in intTextBoxes) config[option.Key] = string.IsNullOrEmpty(option.Value.Text) ? null : Int32.Parse(option.Value.Text);

            if (!string.IsNullOrEmpty(iconPath)) {
                if (iconPath.StartsWith("http://") || iconPath.StartsWith("https://")) { // If the icon is a URL download it
                    try {
                        string tempIconPath = Path.Combine(HTML2EXE.tmpPath, "webfiles", "icon.ico");
                        using (var client = new HttpClient()) {
                            var data = client.GetByteArrayAsync(iconPath).Result;
                            File.WriteAllBytes(tempIconPath, data);
                        }
                        config["icon"] = Path.Combine("webfiles", "icon.ico"); // Set the icon to the downloaded file
                    } catch (Exception ex) {
                        MessageBox.Show($"Error downloading icon: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        config["icon"] = null;
                    }
                }
                else {
                    config["icon"] = Path.Combine("webfiles", Path.GetFileName(iconPath)); // Set the icon
                    if (File.Exists(iconPath)) File.Copy(iconPath, Path.Combine(HTML2EXE.tmpPath, "webfiles", Path.GetFileName(iconPath)), true); // Copy the icon to the webfiles directory
                }
            }
            else config["icon"] = null; // If the icon is empty, set it as null

            if (export) config["include_runtime"] = includeNETbox.Checked; // If exporting, set the include runtime option
            HTML2EXE.webviewURL = includeNETbox.Checked ? HTML2EXE.webview_big : HTML2EXE.webview; // Set the webview URL based on the include runtime option

            // Clean nulls and order alphabetically
            if (removenulls) {
                var ordered = new JsonObject();
                foreach (var kvp in config.AsObject()
                    .Where(kvp => kvp.Value != null && !string.IsNullOrEmpty(kvp.Value.ToString()))
                    .OrderBy(kvp => kvp.Key, StringComparer.OrdinalIgnoreCase)
                    )
                    ordered[kvp.Key] = kvp.Value.DeepClone();
                config = ordered;
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
            if (!string.IsNullOrEmpty(iconOpener.FileName) && File.Exists(iconOpener.FileName)) {
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
                if (File.Exists(jsonOpener.FileName))
                {
                    JsonNode newConfig = JsonNode.Parse(File.ReadAllText(jsonOpener.FileName)) ?? new JsonObject(); // Load the config file
                    
                    // Set checkboxes
                    foreach (var option in checkBoxes) if (newConfig[option.Key] != null) option.Value.Checked = newConfig[option.Key].GetValue<bool>();
                    // Set textboxes
                    foreach (var option in textBoxes) if (newConfig[option.Key] != null) option.Value.Text = newConfig[option.Key].ToString();
                    // Set int textboxes
                    foreach (var option in intTextBoxes) if (newConfig[option.Key] != null) option.Value.Text = newConfig[option.Key].ToString();

                    // Define icon path
                    iconPathLabel.Text = Path.GetFileName(iconPath);
                    removeIconBtn.Visible = true;
                    if (newConfig["icon"] != null && (newConfig["icon"].ToString().StartsWith("http://") || newConfig["icon"].ToString().StartsWith("https://")))
                        iconPath = newConfig["icon"].ToString(); // If the icon is a URL, use it directly
                    else if (newConfig["icon"] != null && HTML2EXE.TryGetFilePath(newConfig["icon"].ToString()) != null) // Otherwise, try to get the file path from the webfiles directory
                        iconPath = HTML2EXE.TryGetFilePath(newConfig["icon"].ToString());
                    else {
                        iconPathLabel.Text = "No icon";
                        removeIconBtn.Visible = false;
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show($"Error loading config: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {}

        private void saveConfigBtnClick(object sender, EventArgs e)
        {
            jsonSaver.ShowDialog();
            File.WriteAllText(jsonSaver.FileName, generateConfigJson(true, true).ToString()); // Generate and save the config
        }
    }
}
