using System.IO.Compression;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace HTML2EXE_2
{
    internal static class HTML2EXE
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        #if BIG_BUILD
            public static readonly bool IsBigBuild = true;
        #else
            public static readonly bool IsBigBuild = false;
        #endif

        private static readonly bool update = true; // Set to false to disable update check
        public static readonly int CurrentVersion = 999; // Updated by GitHub at build
        private static readonly string LatestJsonUrl = "https://github.com/jgc777/HTML2EXE-2.0/releases/latest/download/latest.json";
        public static readonly string webview = "https://github.com/jgc777/HTML2EXE-2.0/releases/latest/download/webview.zip";
        public static readonly string webview_big = "https://github.com/jgc777/HTML2EXE-2.0/releases/latest/download/webview-big.zip";
        public static string? webviewURL;

        public static readonly string tmpPath = Path.Combine(Path.GetTempPath(), "HTML2EXE");
        private static readonly string TempFilePath = Path.Combine(Path.GetTempPath(), "HTML2EXE-latest.exe");
        public static readonly string tempConfigJson = Path.Combine(tmpPath, "config.json");

        public static bool GUI = false;
        private static readonly string guiFlag = Path.Combine(tmpPath, "gui.flag");
        private static BrowseDialog? browseDialog;

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                ApplicationConfiguration.Initialize();
                Application.EnableVisualStyles();
                Console.Title = $"HTML2EXE 2.0 v{(IsBigBuild ? $"{CurrentVersion} (BIG)" : CurrentVersion)}";

                if (args.Length > 0 && new[]{"-h", "--help", "/?", "/help"}.Contains(args[0])) {
                    Console.WriteLine("Opening web documentation...");
                    Process.Start(new ProcessStartInfo {
                        FileName = "https://jgc777.github.io/HTML2EXE-2.0/",
                        UseShellExecute = true
                    });
                    Environment.Exit(0);
                }

                if (Directory.Exists(tmpPath)) Directory.Delete(tmpPath, true);
                Directory.CreateDirectory(tmpPath);
                Directory.CreateDirectory(Path.Combine(tmpPath, "webfiles"));

                CheckForUpdatesAsync(args).Wait();

                if (args.Length > 0) {
                    string? htmlPath = TryGetFileFolderPath(args[0]); // Try to get the file path from the first argument
                    if (string.IsNullOrEmpty(htmlPath)) throw new Exception($"File/Folder not found: {args[0]}"); // If the first argument is still not a file or folder, throw an error
                    if (Directory.Exists(htmlPath)) new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(htmlPath, Path.Combine(tmpPath, "webfiles"), true); // Copy directory to webfiles
                    else File.Copy(htmlPath, Path.Combine(tmpPath, "webfiles", Path.GetFileName(htmlPath)), true); // Copy file to webfiles

                    // Copy config icon and modify config file
                    if (args.Length >= 3)
                    {
                        string argsConfigPath = TryGetFilePath(args[2]) ?? throw new Exception($"Config file not found: {args[2]}"); // If the config file is still not found, throw an error

                        JsonNode config = JsonNode.Parse(File.ReadAllText(argsConfigPath)) ?? new JsonObject(); // Parse the config file
                        
                        string? configIcon = config["icon"]?.ToString(); // Read the icon path from the config file
                        string? iconPath = (!string.IsNullOrEmpty(configIcon)) ? iconPath = TryGetFilePath(configIcon) : null; // If the icon path is set in the config, check if it exists, otherwise set it to null
                        if (!string.IsNullOrEmpty(iconPath)) { // If the icon path has been set
                            if (iconPath.StartsWith("http://") || iconPath.StartsWith("https://")) { // If the icon path is a URL
                                try {
                                    using (var client = new HttpClient()) { // Download the icon to webfiles\icon.ico
                                        var data = client.GetByteArrayAsync(iconPath).Result;
                                        File.WriteAllBytes(Path.Combine(tmpPath, "webfiles", "icon.ico"), data);
                                    }
                                    config["icon"] = Path.Combine("webfiles", "icon.ico"); // Modify the config icon path to point to the downloaded icon
                                }
                                catch (Exception ex) {
                                    MessageBox.Show($"Error downloading icon: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    config["icon"] = null;
                                }
                            }
                            else {
                                config["icon"] = Path.Combine("webfiles", Path.GetFileName(iconPath)); // Set the icon
                                if (File.Exists(iconPath)) File.Copy(iconPath, Path.Combine(tmpPath, "webfiles", Path.GetFileName(iconPath)), true); // Copy the icon to the webfiles directory
                            }
                        }
                        File.WriteAllText(Path.Combine(tmpPath, "config.json"), config.ToString()); // Save the config file
                    }

                    string output = Path.Combine(Environment.CurrentDirectory, "out.exe"); // Define default output path
                    string tempConfigJson = Path.Combine(tmpPath, "config.json");
                    if (File.Exists(tempConfigJson)) { // If a config file exists, read it and set the output name and webview URL
                        JsonNode config = JsonNode.Parse(File.ReadAllText(tempConfigJson)) ?? new JsonObject();
                        // If the config file has a title, use it as the output name
                        if (config["title"] is not null) output = Path.Combine(Environment.CurrentDirectory, $"{config["title"]}.exe");
                        webviewURL = (config["include_runtime"]?.GetValue<bool>() ?? IsBigBuild) ? webview_big : webview; // Set the webview URL based on the config or the build type
                    }
                    else webviewURL = IsBigBuild ? webview_big : webview; // Set the webview URL based on the build type

                    if (args.Length >= 2 && args[1]!=null) output = args[1]; // If the second argument is provided, use it as the output path
                    if (Directory.GetParent(output) is DirectoryInfo parentDirectory && !Directory.Exists(parentDirectory.ToString())) Directory.CreateDirectory(parentDirectory.ToString()); // Ensure the output directory exists

                    build(output); // Build the executable
                }
                else
                {
                    GUI = true;
                    Console.WriteLine("No arguments provided, starting GUI...");
                    File.WriteAllText(guiFlag, "");
                    FreeConsole();
                    Application.Run(browseDialog = new BrowseDialog());
                }
            }
            catch (Exception ex)
            {
                log($"Error: {ex.Message}", true, false, GUI);
            }
            finally
            {
                if (Directory.Exists(tmpPath)) Directory.Delete(tmpPath, true);
            }
        }

        public static async Task CheckForUpdatesAsync(string[] args)
        {
            if (update) try
            {
                log("Checking for updates...");
                using HttpClient client = new HttpClient();
                string json = await client.GetStringAsync(LatestJsonUrl);

                using JsonDocument doc = JsonDocument.Parse(json);
                int latestVersion = doc.RootElement.GetProperty("version").GetInt32();
                string? downloadUrl = IsBigBuild ? doc.RootElement.GetProperty("url_big").GetString() : doc.RootElement.GetProperty("url").GetString();

                if (latestVersion > CurrentVersion && doc.RootElement.GetProperty("update").GetBoolean() && !string.IsNullOrEmpty(downloadUrl))
                {
                    log($"New version available ({CurrentVersion} --> {latestVersion}). Updating...", false, true);
                    byte[] data = await client.GetByteArrayAsync(downloadUrl);
                    await File.WriteAllBytesAsync(TempFilePath, data);
                    string? argsString = null;
                    if (args.Length > 0) argsString = string.Join(" ", args.Select(arg => $"\"{arg}\"")); // Join the arguments into a single string, escaping them
                    using (Process update = new Process()) {
                        update.StartInfo = new ProcessStartInfo {
                            FileName = TempFilePath,
                            Arguments = argsString,
                            UseShellExecute = false,
                            WorkingDirectory = Environment.CurrentDirectory,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        };
                        update.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data ?? "");
                        update.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data ?? "", true);
                        update.Start();
                        update.BeginOutputReadLine();
                        update.BeginErrorReadLine();
                        while (!(update.HasExited || File.Exists(guiFlag))) await Task.Delay(100); // Wait until the new process exits or the GUI flag is created
                        if (File.Exists(guiFlag)) File.Delete(guiFlag);
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception ex)
            {
                log($"Error searching for updates: \"{ex.Message}\". Try updating manually", true, false, true);
            }
        }

        public static void build(string output)
        {
            Directory.CreateDirectory(tmpPath);
            if (!Directory.Exists(Path.Combine(tmpPath, "webfiles"))) Directory.CreateDirectory(Path.Combine(tmpPath, "webfiles"));

            string configPath = Path.Combine(tmpPath, "config.json");
            bool hasConfig = File.Exists(configPath);
            JsonNode config = hasConfig ? JsonNode.Parse(File.ReadAllText(configPath)) ?? new JsonObject() : new JsonObject(); // Parse the config file if it exists, otherwise create a new JsonObject
            string iexpressConfig = $@"[Version]
Class=IEXPRESS
SEDVersion=3
[Options]
PackagePurpose=InstallApp
ShowInstallProgramWindow=1
HideExtractAnimation=0
UseLongFileName=1
InsideCompressed=1
CAB_FixedSize=0
CAB_ResvCodeSigning=0
RebootMode=N
InstallPrompt=%InstallPrompt%
DisplayLicense=%DisplayLicense%
FinishMessage=%FinishMessage%
TargetName=%TargetName%
FriendlyName=%FriendlyName%
AppLaunched=%AppLaunched%
PostInstallCmd=%PostInstallCmd%
AdminQuietInstCmd=%AdminQuietInstCmd%
UserQuietInstCmd=%UserQuietInstCmd%
SourceFiles=SourceFiles
VersionInfo=VersionSection
[VersionSection]
Internalname={Path.GetFileName(output)}
OriginalFilename={Path.GetFileName(output)}
FileDescription=%FileDesc%
CompanyName={config["title"]?.ToString() ?? "Jgc7"}
ProductName={config["title"]?.ToString() ?? "HTML2EXE - Set title to change"}
LegalCopyright=Copyright {DateTime.Now.Year} {config["title"]?.ToString() ?? "Jgc7"}
[Strings]
FileDesc={config["title"]?.ToString() ?? "HTML2EXE 2.0"}
InstallPrompt=
DisplayLicense=
FinishMessage=
TargetName=out.exe
FriendlyName={config["title"]?.ToString() ?? "HTML2EXE 2.0"}
AppLaunched=.\Webview.exe
PostInstallCmd=cmd /c del /f /q Webview.exe.WebView2
AdminQuietInstCmd=
UserQuietInstCmd=
FILE0=""Webview.exe""
FILE1=""WebView2Loader.dll""
FILE2=""webfiles.zip""
{(hasConfig ? "FILE3=\"config.json\"" : "")}
[SourceFiles]
SourceFiles0=.\ 
[SourceFiles0]
%FILE0%=
%FILE1%=
%FILE2%=
{(hasConfig ? "%FILE3%=" : "")}
";
            string iexpressConfigPath = Path.Combine(tmpPath, "HTML2EXE.sed");

            log("Started building");
            log("Downloading webview.zip...");
            using (HttpClient client = new HttpClient()) {
                var response = client.GetAsync(webviewURL).Result;
                response.EnsureSuccessStatusCode();
                var fileBytes = response.Content.ReadAsByteArrayAsync().Result;
                string tempZipPath = Path.Combine(tmpPath, "webview.zip");
                File.WriteAllBytes(tempZipPath, fileBytes);

                log("Extracting webview.zip...");
                ZipFile.ExtractToDirectory(tempZipPath, tmpPath);
                File.Delete(tempZipPath);
            }

            log("Compressing web files...");
            ZipFile.CreateFromDirectory(Path.Combine(tmpPath, "webfiles"), Path.Combine(tmpPath, "webfiles.zip"), CompressionLevel.SmallestSize, true);
            
            log("Building...");
            File.WriteAllText(iexpressConfigPath, iexpressConfig);
            using (Process iexpress = new Process())
            {
                iexpress.StartInfo = new ProcessStartInfo {
                    FileName = "iexpress",
                    WorkingDirectory = tmpPath,
                    Arguments = $"/Q /N {iexpressConfigPath}",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                iexpress.Start();
                iexpress.WaitForExit();
                if (iexpress.ExitCode != 0) throw new Exception($"Building failed with exit code: {iexpress.ExitCode}");
                log($"IExpress finished with code {iexpress.ExitCode}");
            }

            string ? configIcon = config?["icon"]?.ToString();
            if (!string.IsNullOrEmpty(configIcon)) { // If there is a config icon, add it to the executable using rcedit
                // Check if rcedit is installed
                bool rceditExists = false;
                try {
                    using (Process checkRcedit = new Process())
                    {
                        checkRcedit.StartInfo = new ProcessStartInfo() {
                            FileName = "rcedit",
                            Arguments = "-h",
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true
                        };
                        checkRcedit.Start();
                        checkRcedit.WaitForExit(2000);
                        rceditExists = checkRcedit.ExitCode == 0;
                    }
                } catch {
                    rceditExists = false;
                }

                if (!rceditExists) {
                    log("Installing rcedit...");
                    using (Process rceditwinget = new Process())
                    {
                        rceditwinget.StartInfo = new ProcessStartInfo()
                        {
                            FileName = "winget",
                            Arguments = "install --id=ElectronCommunity.rcedit  -e",
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true
                        };
                        rceditwinget.Start();
                        rceditwinget.WaitForExit();
                        if (rceditwinget.ExitCode != 0) throw new Exception("Error installing rcedit.");
                    }   
                }

                log("Adding icon...");
                using (Process rcedit = new Process())
                {
                    rcedit.StartInfo = new ProcessStartInfo()
                    {
                        FileName = "rcedit",
                        // Add the icon to the executable, checking if it's a full path or relative to the tmpPath
                        Arguments = $"\"{Path.Combine(tmpPath, "out.exe")}\" --set-icon \"{(File.Exists(configIcon) ? configIcon : Path.Combine(tmpPath, configIcon))}\"",
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };
                    rcedit.Start();
                    rcedit.WaitForExit();
                    if (rcedit.ExitCode != 0) log($"rcedit failed with exit code {rcedit.ExitCode}", true);
                }  
            }

            log("Cleaning up...");
            File.Move(Path.Combine(tmpPath, "out.exe"), output, true);

            log("Finished building!", false, true, GUI);
            log($"Output: {output}", false, true);
        }

        public static string? TryGetFilePath(string path)
        { // Returns the full path of a file if it exists, otherwise returns null
            var expanded = Environment.ExpandEnvironmentVariables(path);
            var currentDir = Path.Combine(Environment.CurrentDirectory, path);
            return (File.Exists(expanded)) ? expanded : (File.Exists(currentDir) ? currentDir : null);
        }

        public static string? TryGetFolderPath(string path)
        { // Returns the full path of a folder if it exists, otherwise returns null
            var expanded = Environment.ExpandEnvironmentVariables(path);
            var currentDir = Path.Combine(Environment.CurrentDirectory, path);
            return (Directory.Exists(expanded)) ? expanded : (Directory.Exists(currentDir) ? currentDir : null);
        }

        public static string? TryGetFileFolderPath(string path)
        { // Returns the full path of a file or folder if it exists, otherwise returns null
            return (TryGetFilePath(path) ?? TryGetFolderPath(path) ?? null);
        }

        public static void log(string message = "", bool isError = false, bool isGreen = false, bool messageBox = false)
        { // Logs a message to the console or GUI if the GUI is active
            if (GUI) {
                if (browseDialog is BrowseDialog && browseDialog?.configDialog?.buildDialog?.logTextBox is RichTextBox logTextBox)
                { // If used to hide warnings and make code cleaner
                    int start = logTextBox.TextLength;
                    logTextBox.AppendText($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] [HTML2EXE] {message}\n");
                    logTextBox.Select(start, logTextBox.TextLength - start); // Select the new text
                    logTextBox.SelectionColor = logTextBox.ForeColor; // Default color
                    if (isError) logTextBox.SelectionColor = Color.Red; // Red for isError
                    if (isGreen) logTextBox.SelectionColor = Color.Green; // Green for isGreen
                    logTextBox.SelectionLength = 0; // Remove selection
                }
                else throw new Exception("logTextBox not found (GUI=true)");
            }
            else {
                var originalColor = Console.ForegroundColor;
                if (isError) Console.ForegroundColor = ConsoleColor.Red;
                if (isGreen) Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] [HTML2EXE] {message}");
                if (isError || isGreen) Console.ForegroundColor = originalColor;
            }

            if (messageBox) {
                if (isError) MessageBox.Show($"Error: {message}", "HTML2EXE Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else MessageBox.Show(message, $"HTML2EXE 2.0 v{(IsBigBuild ? $"{CurrentVersion} (BIG)" : CurrentVersion)}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
