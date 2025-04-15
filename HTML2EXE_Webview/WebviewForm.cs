using System.Text.Json.Nodes;
using System.Diagnostics;
using System.Text.Json;

namespace Webview {
    public partial class WebviewForm : Form
    {
        JsonNode config;
        public WebviewForm()
        {
            InitializeComponent();
            InitializeWebview();
        }
        async void InitializeWebview()
        {
            try
            {
                this.FormClosing += WebView_FormClosing;
                string configPath = Path.Combine(Environment.CurrentDirectory, "config.json"); // Config file path
                if (File.Exists(configPath)) {
                    string jsonString = await File.ReadAllTextAsync(configPath); // Read config file
                    config = JsonNode.Parse(jsonString); // Parse JSON config file
                }
                else config = new JsonObject(); // Create empty JSON object

                if (!string.IsNullOrEmpty(config["url"]?.ToString())) webView2.Source = new Uri(System.Environment.ExpandEnvironmentVariables(config["url"]?.ToString())); // Set config URL
                else
                {
                    string url = null;
                    if (Directory.EnumerateFiles(Path.Combine(Environment.CurrentDirectory, "webfiles")).Count() == 1) url = Path.Combine(Environment.CurrentDirectory, "webfiles", Path.GetFileName(Directory.EnumerateFiles(Path.Combine(Environment.CurrentDirectory, "webfiles")).First())); // If there is only one file in the directory, set it as the URL
                    else if (File.Exists(Path.Combine(Environment.CurrentDirectory, "webfiles", "index.html"))) url = Path.Combine(Environment.CurrentDirectory, "webfiles", "index.html"); // Check for index.html
                    else if (File.Exists(Path.Combine(Environment.CurrentDirectory, "webfiles", "index.htm"))) url = Path.Combine(Environment.CurrentDirectory, "webfiles", "index.htm"); // Check for index.htm
                    else if (Directory.EnumerateFiles(Path.Combine(Environment.CurrentDirectory, "webfiles")).Count() == 2)
                    {
                        string[] filesInWebfiles = Directory.EnumerateFiles(Path.Combine(Environment.CurrentDirectory, "webfiles")).ToArray(); // Get all files in the webfiles directory
                        if (filesInWebfiles[0].EndsWith(".ico")) url = Path.Combine(Environment.CurrentDirectory, "webfiles", filesInWebfiles[1].ToString()); // If the first file is a ico file, set the second file as the URL
                        else if (filesInWebfiles[1].EndsWith(".ico")) url = Path.Combine(Environment.CurrentDirectory, "webfiles", filesInWebfiles[0].ToString()); // If the second file is an icon, set the first file as the URL
                    }
                    else foreach (var file in Directory.EnumerateFiles(Path.Combine(Environment.CurrentDirectory, "webfiles"))) if (file.EndsWith(".html") || file.EndsWith(".htm")) { // When a html is found
                            url = Path.Combine(Environment.CurrentDirectory, "webfiles", file); // Set the html as the url
                            break;
                    }

                    webView2.Source = new Uri(url ?? Path.Combine(Environment.CurrentDirectory, "webfiles")); // Set webview source
                }

                if (config["maximized"]?.GetValue<bool>() ?? false) this.WindowState = FormWindowState.Maximized; // Maximize windo
                else this.WindowState = FormWindowState.Normal; // Normal window

                if (config["resizable"]?.GetValue<bool>() ?? true) this.FormBorderStyle = FormBorderStyle.Sizable; // Resizable window
                else {
                    this.FormBorderStyle = FormBorderStyle.FixedSingle; // Fixed window
                    this.MaximizeBox = false; // Disable maximize button
                }

                this.ControlBox = config["control_box"]?.GetValue<bool>() ?? true; // Config control box

                this.MinimizeBox = config["minimizable"]?.GetValue<bool>() ?? true; // Config minimize button

                this.MaximizeBox = config["maximizable"]?.GetValue<bool>() ?? true; // Config minimize button

                if (config["fullscreen"]?.GetValue<bool>() ?? false) // Config fullscreen
                {
                    this.TopMost = true; // Always on top
                    this.FormBorderStyle = FormBorderStyle.None; // No border
                    this.WindowState = FormWindowState.Maximized; // Maximize window
                }

                this.ShowInTaskbar = config["show_in_taskbar"]?.GetValue<bool>() ?? true; // Config show in taskbar

                if (config["icon"]?.ToString() != null) {
                    this.Icon = new Icon(config["icon"].ToString()); // Config icon
                    this.ShowIcon = true; // Show icon
                }

                if (config["width"]?.ToString() != null) this.Width = config["width"].GetValue<int>(); // Set config width
                if (config["height"]?.ToString() != null) this.Height = config["height"].GetValue<int>(); // Set config height

                await webView2.EnsureCoreWebView2Async(); // Wait for WebView to be initialized

                if (config["title"]?.ToString() != null) this.Text = config["title"].ToString(); // Set config title
                else webView2.CoreWebView2.DocumentTitleChanged += WebView_DocumentTitleChanged; // Set default title

                webView2.CoreWebView2.Settings.AreDefaultContextMenusEnabled = config["context_menu"]?.GetValue<bool>() ?? false; // Config context menu
                
                webView2.CoreWebView2.Settings.AreDevToolsEnabled = config["dev_tools"]?.GetValue<bool>() ?? false; // Config dev tools
                
                webView2.CoreWebView2.Settings.IsZoomControlEnabled = config["zoom_control"]?.GetValue<bool>() ?? false; // Config zoom
                
                this.TopMost = config["always_on_top"]?.GetValue<bool>() ?? false; // Always on top

                webView2.CoreWebView2.ContainsFullScreenElementChanged += (obj, args) =>
                { // Fullscreen change event
                    if (config["fullscreen"] != null) return; // If fullscreen is set in config return
                    if (webView2.CoreWebView2.ContainsFullScreenElement) // If fullscreen
                    {
                        this.TopMost = true; // Always on top
                        this.FormBorderStyle = FormBorderStyle.None; // No border
                        if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal; // Unmaximize window to avoid problems
                        this.WindowState = FormWindowState.Maximized; // Maximize window
                    }
                    else
                    {
                        this.TopMost = config["always_on_top"]?.GetValue<bool>() ?? false; // Set always on top
                        this.FormBorderStyle = FormBorderStyle.Sizable; // Border
                        if (config["maximized"]?.GetValue<bool>() ?? false) this.WindowState = FormWindowState.Maximized; // Config maximize
                        else this.WindowState = FormWindowState.Normal; // Unmaximize
                    }
                };

                webView2.Visible = true; // Show WebView

                if (config["additional_cmd"] != null)
                { // Config additional command
                    Process process = new Process();
                    process.StartInfo.FileName = config["additional_cmd"].ToString();
                    process.StartInfo.Arguments = config["additional_cmd_args"]?.ToString() ?? "";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                }
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show($"JSON Error: {jsonEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); // Close form on error
            }
        }
        void WebView_DocumentTitleChanged(object sender, object e)
        {
            this.Text = webView2.CoreWebView2.DocumentTitle;
        }
        private void WebView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (config["block_close"]?.GetValue<bool>() ?? false)e.Cancel = true; // Block close event
        }
        private void webView21_Click(object sender, EventArgs e) { }
        private void WebviewForm_Load(object sender, EventArgs e) { }
    }
}
