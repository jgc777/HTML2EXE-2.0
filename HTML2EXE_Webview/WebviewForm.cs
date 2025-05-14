using System.Text.Json.Nodes;
using System.Diagnostics;
using System.Text.Json;
using System.Security.Cryptography;

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
                    string url = null; // Initialize URL to null
                    string webfilesPath = Path.Combine(Environment.CurrentDirectory, "webfiles"); // Path to webfiles folder
                    string[] filesInWebfiles = Directory.EnumerateFiles(webfilesPath).ToArray(); // Get all files in webfiles folder
                    var htmlFiles = filesInWebfiles.Where(file => file.EndsWith(".html", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".htm", StringComparison.OrdinalIgnoreCase)).ToArray();

                    // Zero priority: If there is only one file, use it
                    if (filesInWebfiles.Length == 1) 
                    {
                        url = filesInWebfiles.First();
                    }
                    // First priority: If there is only one HTML file, use it
                    if (htmlFiles.Length == 1)
                    {
                        url = htmlFiles.First();
                    }
                    // Second priority: If there is a index.html or index.htm file, use it
                    else if (File.Exists(Path.Combine(webfilesPath, "index.html")))
                    {
                        url = Path.Combine(webfilesPath, "index.html");
                    }
                    else if (File.Exists(Path.Combine(webfilesPath, "index.htm")))
                    {
                        url = Path.Combine(webfilesPath, "index.htm");
                    }
                    // Third priority: if there are 2 files in webfiles, use the first one that is not an icon
                    else if (filesInWebfiles.Length == 2)
                    {
                        var nonIcoFile = filesInWebfiles.FirstOrDefault(file => !file.EndsWith(".ico", StringComparison.OrdinalIgnoreCase));
                        if (nonIcoFile != null)url = nonIcoFile;
                    }
                    // Fourth priority: If there are HTML files, use the first one found
                    else if (htmlFiles.Length > 0)
                    {
                        url = htmlFiles.First();
                    }

                    // If no file has been set, use the webfiles folder as the default URL
                    webView2.Source = new Uri(url ?? webfilesPath); // Set URL
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

                if (config["icon"] != null) {
                    this.Icon = new Icon(config["icon"].ToString()); // Config icon
                    this.ShowIcon = true; // Show icon
                }

                if (config["width"] != null) this.Width = config["width"].GetValue<int>(); // Set config width
                if (config["height"] != null) this.Height = config["height"].GetValue<int>(); // Set config height

                await webView2.EnsureCoreWebView2Async(); // Wait for WebView to be initialized

                if (config["title"] != null) this.Text = config["title"].ToString(); // Set config title
                else webView2.CoreWebView2.DocumentTitleChanged += WebView_DocumentTitleChanged; // Set default title

                webView2.CoreWebView2.Settings.AreDefaultContextMenusEnabled = config["context_menu"]?.GetValue<bool>() ?? false; // Config context menu
                
                webView2.CoreWebView2.Settings.AreDevToolsEnabled = config["dev_tools"]?.GetValue<bool>() ?? false; // Config dev tools
                
                webView2.CoreWebView2.Settings.IsZoomControlEnabled = config["zoom_control"]?.GetValue<bool>() ?? false; // Config zoom
                
                this.TopMost = config["always_on_top"]?.GetValue<bool>() ?? false; // Always on top

                webView2.CoreWebView2.ContainsFullScreenElementChanged += (obj, args) => { // Fullscreen change event
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

                if (config["additional_cmd"] != null) { // Config additional command
                    Process process = new Process();
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd",
                        Arguments = "/c " + config["additional_cmd"].ToString(),
                        WorkingDirectory = Path.Combine(Environment.CurrentDirectory, "webfiles"),
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    process.Start(); // Start process
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
            if (config["block_close"]?.GetValue<bool>() ?? false) e.Cancel = true; // Block close event
        }
        private void webView21_Click(object sender, EventArgs e) {}
        private void WebviewForm_Load(object sender, EventArgs e) {}
    }
}
