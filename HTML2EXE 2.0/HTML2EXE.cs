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
        private static readonly string latestJsonUrl = "https://github.com/jgc777/HTML2EXE-2.0/releases/latest/download/latest.json";
        public static readonly int CurrentVersion = 0; // Updated by GitHub at build, version 0 also disables update check
        public static readonly string webview = "https://github.com/jgc777/HTML2EXE-2.0/releases/latest/download/webview.zip";
        public static readonly string webview_big = "https://github.com/jgc777/HTML2EXE-2.0/releases/latest/download/webview-big.zip";
        public static string? webviewURL;

        public static readonly string tmpPath = Path.Combine(Path.GetTempPath(), "HTML2EXE");
        public static readonly string tmpWebfilesPath = Path.Combine(tmpPath, "webfiles");

        private static readonly string tmpUpdatePath = Path.Combine(Path.GetTempPath(), "HTML2EXE-latest.exe");
        public static readonly string tmpConfigJson = Path.Combine(tmpPath, "config.json");
        public static readonly string tmpWebviewPath = Path.Combine(tmpPath, "webview.zip");
        private static readonly string tmpOutputPath = Path.Combine(tmpPath, "out.exe");

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

                if (args.Length > 0 && new[] { "-h", "--help", "/?", "/help" }.Contains(args[0]))
                {
                    Console.WriteLine("Opening web documentation...");
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://jgc777.github.io/HTML2EXE-2.0/",
                        UseShellExecute = true
                    });
                    Environment.Exit(0);
                }

                if (Directory.Exists(tmpPath)) Directory.Delete(tmpPath, true);
                Directory.CreateDirectory(tmpPath);
                Directory.CreateDirectory(tmpWebfilesPath);

                CheckForUpdatesAsync(args).Wait();

                if (args.Length > 0)
                {
                    string? htmlPath = TryGetFileFolderPath(args[0]); // Try to get the file path from the first argument
                    if (string.IsNullOrEmpty(htmlPath)) throw new Exception($"HTML file/folder not found: {args[0]}"); // If the first argument is still not a file or folder, throw an error
                    if (Directory.Exists(htmlPath)) CopyDirectory(htmlPath, tmpWebfilesPath); // Copy directory to webfiles
                    else File.Copy(htmlPath, Path.Combine(tmpWebfilesPath, Path.GetFileName(htmlPath)), true); // Copy file to webfiles

                    // Copy config icon and modify config file
                    if (args.Length >= 3)
                    {
                        JsonNode config = new JsonObject();
                        string? argsConfigPath = TryGetFilePath(args[2]); // If the config file is still not found, throw an error
                        if (string.IsNullOrEmpty(argsConfigPath)) log($"Warning: config file not found: {args[2]}", true, false); // If the config file is not found, log a warning and use an empty config
                        else
                        {
                            try
                            {
                                config = JsonNode.Parse(File.ReadAllText(argsConfigPath)) ?? new JsonObject();
                                string? configIcon = config["icon"]?.ToString(); // Read the icon path from the config file
                                if (!string.IsNullOrEmpty(configIcon))
                                {
                                    if (configIcon.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                                        configIcon.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                                    {
                                        using (var client = new HttpClient())
                                        {
                                            var data = client.GetByteArrayAsync(configIcon).Result;
                                            File.WriteAllBytes(Path.Combine(tmpWebfilesPath, "icon.ico"), data);
                                            config["icon"] = Path.Combine("webfiles", "icon.ico");
                                            log($"Downloaded icon from URL: {configIcon}");
                                        }
                                    }
                                    else
                                    {
                                        string? iconPath = TryGetFilePath(configIcon);
                                        if (!string.IsNullOrEmpty(iconPath))
                                        {
                                            var iconName = Path.GetFileName(iconPath);
                                            File.Copy(iconPath, Path.Combine(tmpWebfilesPath, iconName), true);
                                            config["icon"] = Path.Combine("webfiles", iconName);
                                            log($"Loaded icon: {iconPath}");
                                        }
                                        else
                                        {
                                            log($"Warning: icon file not found: {configIcon}", true, false);
                                            config["icon"] = null;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                log($"Warning: error loading icon: {ex.Message}", true, false);
                                config["icon"] = null;

                            }
                            finally
                            {
                                File.WriteAllText(tmpConfigJson, config.ToString()); // Save the config file
                            }
                        }
                    }

                    //Set output path and webview URL
                    string output = Path.Combine(Environment.CurrentDirectory, "out.exe"); // Define default output path
                    if (File.Exists(tmpConfigJson))
                    { // If a config file exists, read it and set the output name and webview URL
                        JsonNode config = JsonNode.Parse(File.ReadAllText(tmpConfigJson)) ?? new JsonObject();
                        // If the config file has a title, use it as the output name
                        if (config["title"] is not null) output = Path.Combine(Environment.CurrentDirectory, $"{config["title"]}.exe");
                        webviewURL = (config["include_runtime"]?.GetValue<bool>() ?? IsBigBuild) ? webview_big : webview; // Set the webview URL based on the config or the build type
                    }
                    else webviewURL = IsBigBuild ? webview_big : webview; // Set the webview URL based on the build type

                    if (args.Length >= 2)
                    {
                        if (!string.IsNullOrEmpty(args[1])) output = args[1]; // If the second argument is provided, use it as the output path
                        else log("Warning: no output path provided, using default: out.exe", true, false);
                    }
                    if (Directory.GetParent(output) is DirectoryInfo parentDirectory && !Directory.Exists(parentDirectory.ToString()))
                        Directory.CreateDirectory(parentDirectory.ToString()); // Ensure the output directory exists

                    //Set local webview path
                    if (args.Length >= 4)
                    {
                        if (args[3].StartsWith("http://") || args[3].StartsWith("https://")) // If the fourth argument is a URL, set the webview URL
                        {
                            webviewURL = args[3];
                            log($"Using webview URL: {webviewURL}");
                            return;
                        }

                        string? webviewPath = TryGetFileFolderPath(args[3]); // Try to get the webview path from the fourth argument
                        if (string.IsNullOrEmpty(webviewPath)) log($"Warning: webview path not found: {args[3]}", true, false);
                        else
                        {
                            if (Directory.Exists(webviewPath)) CopyDirectory(webviewPath, tmpPath);
                            else File.Copy(webviewPath, tmpWebviewPath, true);
                        }
                    }

                    build(output); // Build the executable
                }
                else
                {
                    GUI = true;
                    Console.WriteLine("No arguments provided, starting GUI...");
                    File.WriteAllText(guiFlag, ""); // Used to close the updater console window when the updated GUI is opened
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
#if DEBUG // No updates in debug mode
            await Task.Delay(0); // Just to keep the method async
#else
            if (CurrentVersion != 0 && update) try
                {
                    log("Checking for updates...");
                    using HttpClient client = new HttpClient();
                    string json = await client.GetStringAsync(latestJsonUrl);

                    using JsonDocument doc = JsonDocument.Parse(json);
                    int latestVersion = doc.RootElement.GetProperty("version").GetInt32();
                    string downloadUrl = doc.RootElement.GetProperty(IsBigBuild ? "url_big" : "url").GetString()!;

                    if (latestVersion > CurrentVersion &&
                    doc.RootElement.GetProperty("update").GetBoolean() &&
                    !string.IsNullOrEmpty(downloadUrl))
                    {
                        log($"New version available ({CurrentVersion} --> {latestVersion}). Updating...", false, true);
                        byte[] data = await client.GetByteArrayAsync(downloadUrl);
                        await File.WriteAllBytesAsync(tmpUpdatePath, data);
                        string argsString = "";
                        if (args.Length > 0) argsString = string.Join(" ", args.Select(arg => $"\"{arg}\"")); // Join the arguments into a single string, escaping them
                        using (Process update = new Process())
                        {
                            update.StartInfo = new ProcessStartInfo
                            {
                                FileName = tmpUpdatePath,
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
#endif
        }

        public static void build(string output)
        { // Builds the final executable
            bool hasConfig = File.Exists(tmpConfigJson);
            JsonNode config = hasConfig ? JsonNode.Parse(File.ReadAllText(tmpConfigJson)) ?? new JsonObject() : new JsonObject(); // Parse the config file if it exists, otherwise create a new JsonObject
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
PostInstallCmd=<None>
AdminQuietInstCmd=
UserQuietInstCmd=
FILE0=""Webview.exe""
FILE1=""webfiles.zip""
{(File.Exists(Path.Combine(tmpPath, "WebView2Loader.dll")) ? "FILE2 =\"WebView2Loader.dll\"" : "")}
{(hasConfig ? "FILE3=\"config.json\"" : "")}
[SourceFiles]
SourceFiles0=.\ 
[SourceFiles0]
%FILE0%=
%FILE1%=
{(File.Exists(Path.Combine(tmpPath, "WebView2Loader.dll")) ? "%FILE2%=" : "")}
{(hasConfig ? "%FILE3%=" : "")}
";
            log("Started building");

            log("Writing IExpress config...");
            string iexpressConfigPath = Path.Combine(tmpPath, "HTML2EXE.sed");
            File.WriteAllText(iexpressConfigPath, iexpressConfig);

            if (!File.Exists(Path.Combine(tmpPath, "Webview.exe"))) // If there is no Webview.exe in the tmpPath
            {
                if (!File.Exists(tmpWebviewPath)) // If the webview.zip file does not exist download it
                {
                    log("Downloading webview.zip...");
                    if (webviewURL is null) throw new Exception("Error: webview URL is null.");
                    using (HttpClient client = new HttpClient())
                    {
                        var response = client.GetAsync(webviewURL).Result;
                        response.EnsureSuccessStatusCode();
                        var fileBytes = response.Content.ReadAsByteArrayAsync().Result;
                        File.WriteAllBytes(tmpWebviewPath, fileBytes);
                    }
                }

                log("Extracting webview.zip...");
                ZipFile.ExtractToDirectory(tmpWebviewPath, tmpPath);
                File.Delete(tmpWebviewPath);
            }

            log("Compressing web files...");
            ZipFile.CreateFromDirectory(tmpWebfilesPath, Path.Combine(tmpPath, "webfiles.zip"), CompressionLevel.SmallestSize, true);

            log("Building...");
            using (Process iexpress = new Process())
            {
                iexpress.StartInfo = new ProcessStartInfo
                {
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

            string? configIcon = config?["icon"]?.ToString();
            if (!string.IsNullOrEmpty(configIcon))
            { // If there is a config icon, add it to the executable using rcedit
                // Check if rcedit is installed
                bool rceditExists = false;
                try
                {
                    using (Process checkRcedit = new Process())
                    {
                        checkRcedit.StartInfo = new ProcessStartInfo()
                        {
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
                }
                catch
                {
                    rceditExists = false;
                }

                if (!rceditExists)
                {
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
                        Arguments = $"\"{tmpOutputPath}\" --set-icon \"{(File.Exists(configIcon) ? configIcon : Path.Combine(tmpPath, configIcon))}\"",
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
            File.Move(tmpOutputPath, output, true);

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

        public static void CopyDirectory(string sourceDir, string destinationDir, bool overwrite = true)
        { // Copies a directory and its contents to a new location, optionally overwriting existing files
            if (!Directory.Exists(destinationDir)) Directory.CreateDirectory(destinationDir);

            foreach (var file in Directory.GetFiles(sourceDir))
                File.Copy(file, Path.Combine(destinationDir, Path.GetFileName(file)), overwrite); // Copy files from source to destination

            foreach (var directory in Directory.GetDirectories(sourceDir))
                CopyDirectory(directory, Path.Combine(destinationDir, Path.GetFileName(directory))); // Recursively copy directories from source to destination
        }

        public static void log(string message = "", bool isError = false, bool isGreen = false, bool messageBox = false)
        { // Logs a message to the console or GUI if the GUI is active
            if (GUI)
            {
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
            else
            {
                var originalColor = Console.ForegroundColor;
                if (isError) Console.ForegroundColor = ConsoleColor.Red;
                if (isGreen) Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] [HTML2EXE] {message}");
                if (isError || isGreen) Console.ForegroundColor = originalColor;
            }

            if (messageBox)
            {
                if (isError) MessageBox.Show($"Error: {message}", "HTML2EXE Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else MessageBox.Show(message, $"HTML2EXE 2.0 v{(IsBigBuild ? $"{CurrentVersion} (BIG)" : CurrentVersion)}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
