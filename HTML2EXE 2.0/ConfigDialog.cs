using System.Text.Json.Nodes;

namespace HTML2EXE_2
{
    public partial class ConfigDialog : Form
    {
        public JsonNode config = new JsonObject();
        public BuildDialog? buildDialog;
        public string? iconPath = null;

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

        public void ConfigDialog_Load(object sender, EventArgs e)
        {
            this.Text = $"HTML2EXE 2.0 v{(HTML2EXE.IsBigBuild ? $"{HTML2EXE.CurrentVersion} (BIG)" : HTML2EXE.CurrentVersion)}";
            this.includeNETbox.Checked = HTML2EXE.IsBigBuild; // Set the include .NET runtime checkbox based on the build type
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Write the config to a file
                if (File.Exists(HTML2EXE.tmpConfigJson)) File.Delete(HTML2EXE.tmpConfigJson);
                File.WriteAllText(HTML2EXE.tmpConfigJson, generateConfigJson().ToString());

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

        private JsonNode generateConfigJson(bool export = false, bool removenulls = true) // Export means we're saving the config as an exportation and not a build
        {
            // Set checkboxes ignoring includeNETbox
            foreach (var option in checkBoxes.Except(new Dictionary<string, CheckBox> { { "include_runtime", includeNETbox } })) config[option.Key] = option.Value.Checked;
            // Set textboxes
            foreach (var option in textBoxes) config[option.Key] = string.IsNullOrEmpty(option.Value.Text) ? null : option.Value.Text;
            // Set int textboxes
            foreach (var option in intTextBoxes) config[option.Key] = string.IsNullOrEmpty(option.Value.Text) ? null : Int32.Parse(option.Value.Text);

            if (!string.IsNullOrEmpty(iconPath))
            {
                if (export) config["icon"] = iconPath;
                else if (iconPath.StartsWith("http://") || iconPath.StartsWith("https://"))
                { // If the icon is a URL download it
                    try
                    {
                        using (var client = new HttpClient())
                        {
                            var data = client.GetByteArrayAsync(iconPath).Result;
                            File.WriteAllBytes(Path.Combine(HTML2EXE.tmpWebfilesPath, "icon.ico"), data);
                        }
                        config["icon"] = Path.Combine("webfiles", "icon.ico"); // Set the icon to the downloaded file
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error downloading icon: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        config["icon"] = null;
                    }
                }
                else
                {
                    config["icon"] = Path.Combine("webfiles", Path.GetFileName(iconPath)); // Set the icon
                    if (File.Exists(iconPath)) File.Copy(iconPath, Path.Combine(HTML2EXE.tmpWebfilesPath, Path.GetFileName(iconPath)), true); // Copy the icon to the webfiles directory
                }
            }
            else config["icon"] = null; // If the icon is empty, set it as null

            if (export) config["include_runtime"] = includeNETbox.Checked; // If exporting, set the include runtime option
            HTML2EXE.webviewURL = includeNETbox.Checked ? HTML2EXE.webview_big : HTML2EXE.webview; // Set the webview URL based on the include runtime option

            if (export) config["webview"] = webviewOpener.FileName;

            // Clean nulls and order alphabetically
            if (removenulls)
            {
                var ordered = new JsonObject();
                foreach (var kvp in config.AsObject()
                    .Where(kvp => kvp.Value is not null && !string.IsNullOrEmpty(kvp.Value.ToString()))
                    .OrderBy(kvp => kvp.Key, StringComparer.OrdinalIgnoreCase)
                    )
                    ordered[kvp.Key] = kvp.Value?.DeepClone(); // ? to remove warning and DeepClone to ensure correct copying
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
            if (!string.IsNullOrEmpty(iconOpener.FileName) && File.Exists(iconOpener.FileName))
            {
                iconPathLabel.Text = Path.GetFileName(iconOpener.FileName);
                removeIconBtn.Visible = true;
                iconPath = iconOpener.FileName;
            }
        }

        private void saveConfigBtnClick(object sender, EventArgs e)
        {
            jsonSaver.ShowDialog();
            File.WriteAllText(jsonSaver.FileName, generateConfigJson(true,true).ToString()); // Generate and save the config
        }

        private void webviewBtn_Click(object sender, EventArgs e)
        {
            webviewOpener.ShowDialog();
            if (!string.IsNullOrEmpty(webviewOpener.FileName) && File.Exists(webviewOpener.FileName))
            {
                webviewPathLabel.Text = Path.GetFileName(webviewOpener.FileName);
                removeWebviewBtn.Visible = true;
                File.Copy(webviewOpener.FileName, HTML2EXE.tmpWebviewPath, true); // Copy the webview file to the temporary path
            }
        }

        private void removeWebviewBtn_Click(object sender, EventArgs e)
        {
            webviewOpener.FileName = null;
            webviewPathLabel.Text = "No icon";
            removeWebviewBtn.Visible = false;
        }

    }
}
