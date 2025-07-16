using System.Diagnostics;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Webview
{

    [SupportedOSPlatform("windows6.1")] // Remove warnings
    public partial class WebviewForm : Form
    {
        JsonNode config = new JsonObject();
        public WebviewForm()
        {
            InitializeComponent();
            string folderName = "default"; // Default folder name
            try { // Try to read the title from config.json
                if (File.Exists(Webview.configPath))
                {
                    var configJson = JsonNode.Parse(File.ReadAllText(Webview.configPath));
                    if (!string.IsNullOrEmpty(configJson!["title"]?.ToString()))
                        folderName = configJson["title"]!.ToString();
                }
            }
            catch { } // Ignore errors in reading config.json

            // Initialize WebView2 with user data folder
            webView2.CreationProperties = new Microsoft.Web.WebView2.WinForms.CoreWebView2CreationProperties{UserDataFolder = Path.Combine(Webview.appData, folderName) };
            InitializeWebview();
        }
        async void InitializeWebview()
        {
            try
            {
                FormClosing += WebView_FormClosing;
                if (File.Exists(Webview.configPath)) config = JsonNode.Parse(await File.ReadAllTextAsync(Webview.configPath)) ?? new JsonObject(); // Parse JSON config file

                if (!string.IsNullOrEmpty(config["url"]?.ToString())) webView2.Source = new Uri(Environment.ExpandEnvironmentVariables(config["url"]!.ToString()!)); // Set config URL
                else
                {
                    string? url = null; // Initialize URL to null
                    string[] filesInWebfiles = Directory.EnumerateFiles(Webview.webfilesPath).ToArray(); // Get all files in webfiles folder
                    var htmlFiles = filesInWebfiles.Where(file => file.EndsWith(".html", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".htm", StringComparison.OrdinalIgnoreCase)).ToArray(); // Get all HTML files in webfiles folder

                    // First priority: If there is only one file, use it
                    if (filesInWebfiles.Length == 1)
                        url = filesInWebfiles.First();
                    // Second priority: If there is only one HTML file, use it
                    if (htmlFiles.Length == 1)
                        url = htmlFiles.First();
                    // Third priority: If there is a index.html file, use it
                    else if (File.Exists(Path.Combine(Webview.webfilesPath, "index.html")))
                        url = Path.Combine(Webview.webfilesPath, "index.html");
                    // Fourth priority: If there is a index.htm file, use it
                    else if (File.Exists(Path.Combine(Webview.webfilesPath, "index.htm")))
                        url = Path.Combine(Webview.webfilesPath, "index.htm");
                    // Fifth priority: if there are 2 files in webfiles and only one is not an .ico file, use the other file
                    else if (filesInWebfiles.Length == 2 // 2 files
                        && filesInWebfiles.Any(file => file.EndsWith(".ico", StringComparison.OrdinalIgnoreCase)) // At least one is an .ico file
                        && filesInWebfiles.FirstOrDefault(file => !file.EndsWith(".ico", StringComparison.OrdinalIgnoreCase)) is var nonIcoFile // Get the first file that is not an .ico file
                        && nonIcoFile is not null)
                        url = nonIcoFile; // Use the non-ico file
                    // Sixth priority: If there are HTML files, use the first one found
                    else if (htmlFiles.Length > 0)
                        url = htmlFiles.First();

                    webView2.Source = new Uri(url ?? Webview.webfilesPath); // Set url to the determined URL or webfiles folder
                }

                if (config["maximized"]?.GetValue<bool>() ?? false) WindowState = FormWindowState.Maximized; // Maximize windo
                else WindowState = FormWindowState.Normal; // Normal window

                if (config["resizable"]?.GetValue<bool>() ?? true) FormBorderStyle = FormBorderStyle.Sizable; // Resizable window
                else {
                    FormBorderStyle = FormBorderStyle.FixedSingle; // Fixed window
                    MaximizeBox = false; // Disable maximize button
                }

                ControlBox = config["control_box"]?.GetValue<bool>() ?? true; // Config control box

                MinimizeBox = config["minimizable"]?.GetValue<bool>() ?? true; // Config minimize button

                MaximizeBox = config["maximizable"]?.GetValue<bool>() ?? true; // Config minimize button

                if (config["fullscreen"]?.GetValue<bool>() ?? false) // Config fullscreen
                {
                    TopMost = true; // Always on top
                    FormBorderStyle = FormBorderStyle.None; // No border
                    WindowState = FormWindowState.Maximized; // Maximize window
                }

                ShowInTaskbar = config["show_in_taskbar"]?.GetValue<bool>() ?? true; // Config show in taskbar

                if (config["icon"] is not null) {
#pragma warning disable CS8602
                    Icon = new Icon(config["icon"].ToString()); // Config icon
                    ShowIcon = true; // Show icon
                }

                if (config["width"] is not null) Width = config["width"].GetValue<int>(); // Set config width
                if (config["height"] is not null) Height = config["height"].GetValue<int>(); // Set config height

                await webView2.EnsureCoreWebView2Async(); // Wait for WebView to be initialized

                if (config["title"] is not null) Text = config["title"].ToString(); // Set config title
                else webView2.CoreWebView2.DocumentTitleChanged += WebView_DocumentTitleChanged; // Set default title

                webView2.CoreWebView2.Settings.AreDefaultContextMenusEnabled = config["context_menu"]?.GetValue<bool>() ?? false; // Config context menu
                
                webView2.CoreWebView2.Settings.AreDevToolsEnabled = config["dev_tools"]?.GetValue<bool>() ?? false; // Config dev tools
                
                webView2.CoreWebView2.Settings.IsZoomControlEnabled = config["zoom_control"]?.GetValue<bool>() ?? false; // Config zoom
                
                TopMost = config["always_on_top"]?.GetValue<bool>() ?? false; // Always on top

                webView2.CoreWebView2.ContainsFullScreenElementChanged += (obj, args) => { // Fullscreen change event
                    if (config["fullscreen"] is not null) return; // If fullscreen is set in config return
                    if (webView2.CoreWebView2.ContainsFullScreenElement) // If fullscreen
                    {
                        TopMost = true; // Always on top
                        FormBorderStyle = FormBorderStyle.None; // No border
                        if (WindowState == FormWindowState.Maximized) WindowState = FormWindowState.Normal; // Unmaximize window to avoid problems
                        WindowState = FormWindowState.Maximized; // Maximize window
                    }
                    else
                    {
                        TopMost = config["always_on_top"]?.GetValue<bool>() ?? false; // Set always on top
                        FormBorderStyle = FormBorderStyle.Sizable; // Border
                        if (config["maximized"]?.GetValue<bool>() ?? false) WindowState = FormWindowState.Maximized; // Config maximize
                        else WindowState = FormWindowState.Normal; // Unmaximize
                    }
                };

                webView2.Visible = true; // Show WebView

                if (config["additional_cmd"] is not null) { // Config additional command
                    Process process = new Process();
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd",
                        Arguments = $"/c {config["additional_cmd"].ToString()}",
                        WorkingDirectory = Path.Combine(Environment.CurrentDirectory, "webfiles"),
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    process.Start(); // Start process
                }
#pragma warning restore CS8602
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show($"JSON Error: {jsonEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close(); // Close form on error
            }
        }
        void WebView_DocumentTitleChanged(object? sender, object e)
        {
            Text = webView2.CoreWebView2.DocumentTitle;
        }
        private void WebView_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (config["block_close"]?.GetValue<bool>() ?? false) e.Cancel = true; // Block close event
        }
        private void webView21_Click(object sender, EventArgs e) {}
        private void WebviewForm_Load(object sender, EventArgs e) {}
    }
}
