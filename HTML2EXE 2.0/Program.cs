using System.IO.Compression;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace HTML2EXE_2._0
{
    internal static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        private static readonly bool update = true; // Set to false to disable update check
        private static readonly string LatestJsonUrl = "https://raw.githubusercontent.com/jgc777/HTML2EXE-2.0/refs/heads/master/latest.json";
        private static readonly string webviewURL = "https://github.com/jgc777/HTML2EXE-2.0/releases/latest/download/webview.zip";
        public static readonly string CurrentVersion = "999"; // Updated by GitHub at build
        private static readonly string TempFilePath = Path.Combine(Path.GetTempPath(), "HTML2EXE-latest.exe");
        public static readonly string tmpPath = Path.Combine(Path.GetTempPath(), "HTML2EXE");
        public static bool GUI = false;
        public static BrowseDialog browseDialog;

        [STAThread]
        static void Main(string[] args)
        {
            try {
                Console.Title = "HTML2EXE 2.0 v" + CurrentVersion;
                if (update) CheckForUpdatesAsync(args).Wait();
                if (Directory.Exists(tmpPath)) Directory.Delete(tmpPath, true);
                Directory.CreateDirectory(tmpPath);
                Directory.CreateDirectory(Path.Combine(tmpPath, "webfiles"));
                if (args.Length > 0 && (args[0] == "-h" || args[0] == "--help" || args[0] == "/?" || args[0] == "/help"))
                {
                    Console.WriteLine("Opening web documentation...");
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://jgc777.github.io/HTML2EXE-2.0/",
                        UseShellExecute = true
                    });
                }
                else if (args.Length > 0)
                {
                    string htmlPath = null;
                    bool directory = false;
                    // Define html/directory path and copy
                    if (File.Exists(args[0])) htmlPath = args[0];
                    else if (File.Exists(Path.Combine(Environment.CurrentDirectory, args[0]))) htmlPath = Path.Combine(Environment.CurrentDirectory, args[0]);
                    else if (Directory.Exists(args[0]))
                    {
                        htmlPath = args[0];
                        directory = true;
                    }
                    else if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, args[0])))
                    {
                        htmlPath = Path.Combine(Environment.CurrentDirectory, args[0]);
                        directory = true;
                    }
                    else
                    {
                        Console.WriteLine("File/Folder not found: " + args[0]);
                        throw new Exception("File/Folder not found: " + args[0]);
                    }
                    if (directory) new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(htmlPath, Path.Combine(tmpPath, "webfiles"), true);
                    else File.Copy(htmlPath, Path.Combine(tmpPath, "webfiles", Path.GetFileName(htmlPath)), true);
                    
                    // Copy config icon and modify config file
                    if (args.Length >= 3)
                    {
                        if (File.Exists(args[2])) {
                            JsonNode config = JsonNode.Parse(File.ReadAllText(args[2]));
                            string configIcon = config["icon"]?.ToString();
                            string iconPath = null;
                            if (File.Exists(Environment.ExpandEnvironmentVariables(configIcon))) iconPath = Environment.ExpandEnvironmentVariables(configIcon);
                            if (File.Exists(Path.Combine(Environment.CurrentDirectory, configIcon))) iconPath = Path.Combine(Environment.CurrentDirectory, configIcon);
                            config["icon"] = Path.Combine("webfiles", Path.GetFileName(iconPath));
                            File.Copy(iconPath, Path.Combine(Path.GetTempPath(), "HTML2EXE", "webfiles", Path.GetFileName(iconPath)), true); // Copy the icon to the webfiles directory
                            File.WriteAllText(Path.Combine(tmpPath, "config.json"), config.ToString()); // Save the config file
                        }
                        else Console.WriteLine("Config file not found: " + args[2], true);
                    }

                    // Ensure config.json exists
                    if (!File.Exists(Path.Combine(tmpPath, "config.json"))) File.WriteAllText(Path.Combine(tmpPath, "config.json"), "{}");

                    // Define output path
                    string output = Path.Combine(Environment.CurrentDirectory, "out.exe");
                    if (JsonNode.Parse(File.ReadAllText(Path.Combine(tmpPath, "config.json")))["title"] != null) output = Path.Combine(Environment.CurrentDirectory, JsonNode.Parse(File.ReadAllText(Path.Combine(tmpPath, "config.json")))["title"] + ".exe");
                    if (args.Length >= 2) output = args[1];
                    if (!Directory.Exists(Directory.GetParent(output)?.ToString())) Directory.CreateDirectory(Directory.GetParent(output).ToString());

                    // Build the executable
                    build(output);
                }
                else
                {
                    GUI = true;
                    Console.WriteLine("No arguments provided, starting GUI...");
                    FreeConsole();
                    ApplicationConfiguration.Initialize();
                    Application.EnableVisualStyles();
                    browseDialog = new BrowseDialog();
                    Application.Run(browseDialog);
                }
            }
            catch (Exception ex)
            {
                log("Error: " + ex.Message, true, false, true);
            }
            finally
            {
                if (Directory.Exists(tmpPath)) Directory.Delete(tmpPath, true);
            }
        }

        public static async Task CheckForUpdatesAsync(string[] args)
        {
            try
            {
                using HttpClient client = new HttpClient();
                string json = await client.GetStringAsync(LatestJsonUrl);

                using JsonDocument doc = JsonDocument.Parse(json);
                int latestVersion = doc.RootElement.GetProperty("version").GetInt32();
                string downloadUrl = doc.RootElement.GetProperty("url").GetString();

                if (latestVersion > Int32.Parse(CurrentVersion))
                {
                    log($"New version available ({CurrentVersion} --> {latestVersion}). Updating...", false, true, GUI);
                    byte[] data = await client.GetByteArrayAsync(downloadUrl);
                    await File.WriteAllBytesAsync(TempFilePath, data);
                    string argsString = null;
                    if (args.Length > 0) argsString = "\"" + string.Join("\" \"", args) + "\"";
                    Process.Start(new ProcessStartInfo {
                        FileName = TempFilePath,
                        Arguments = argsString,
                        UseShellExecute = true,
                        WorkingDirectory = Environment.CurrentDirectory
                    });
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                log($"Error searching for updates: {ex.Message}", true, false, true);
            }
        }

        public static void build(string output)
        {
            try
            {
                string tmpPath = Path.Combine(Path.GetTempPath(), "HTML2EXE");
                Directory.CreateDirectory(tmpPath);
                if (!Directory.Exists(Path.Combine(tmpPath, "webfiles"))) Directory.CreateDirectory(Path.Combine(tmpPath, "webfiles"));
                string configTitle = JsonNode.Parse(File.ReadAllText(Path.Combine(tmpPath, "config.json")))["title"]?.ToString() ?? null;
                string iexpressConfig = @"[Version]
Class=IEXPRESS
SEDVersion=3
[Options]
PackagePurpose=InstallApp
ShowInstallProgramWindow=1
HideExtractAnimation=0
UseLongFileName=1
InsideCompressed=0
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
Internalname=" + Path.GetFileName(output) + @"
OriginalFilename=" + Path.GetFileName(output) + @"
FileDescription=%FileDesc%
CompanyName=" + (configTitle ?? "Jgc7") + @"
ProductName=" + (configTitle ?? "HTML2EXE 2.0") + @"
LegalCopyright=Copyright
[Strings]
FileDesc=" + (configTitle ?? "HTML2EXE 2.0") + @"
InstallPrompt=
DisplayLicense=
FinishMessage=
TargetName=out.exe
FriendlyName=" + (configTitle ?? "HTML2EXE 2.0") + @"
AppLaunched=Webview
PostInstallCmd=cmd /c del /f /q Webview.exe.WebView2
AdminQuietInstCmd=
UserQuietInstCmd=
FILE0=""Webview.exe""
FILE1=""WebView2Loader.dll""
FILE2=""webfiles.zip""
FILE3=""config.json""
[SourceFiles]
SourceFiles0=.\
[SourceFiles0]
%FILE0%=
%FILE1%=
%FILE2%=
%FILE3%=
";
                string iexpressConfigPath = Path.Combine(tmpPath, "HTML2EXE.sed");
                log("Started building");
                log("Downloading webview.zip...");
                using (HttpClient client = new HttpClient())
                {
                    var response = client.GetAsync(webviewURL).Result;
                    response.EnsureSuccessStatusCode();
                    var fileBytes = response.Content.ReadAsByteArrayAsync().Result;
                    File.WriteAllBytes(Path.Combine(tmpPath, "webview.zip"), fileBytes);
                    string tempZipPath = Path.Combine(tmpPath, "webview.zip");
                    log("Extracting webview.zip...");
                    ZipFile.ExtractToDirectory(tempZipPath, tmpPath);
                    File.Delete(tempZipPath);
                }
                log("Compressing web files...");
                ZipFile.CreateFromDirectory(Path.Combine(tmpPath, "webfiles"), Path.Combine(tmpPath, "webfiles.zip"), CompressionLevel.SmallestSize, true);
                log("Building...");
                File.WriteAllText(iexpressConfigPath, iexpressConfig);
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "iexpress";
                    process.StartInfo.WorkingDirectory = tmpPath;
                    process.StartInfo.Arguments = "/Q /N " + iexpressConfigPath;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false; process.Start();
                    process.WaitForExit();
                    if (process.ExitCode != 0) throw new Exception("Building failed with exit code: " + process.ExitCode);
                }
                string configIcon = JsonNode.Parse(File.ReadAllText(Path.Combine(tmpPath, "config.json")))["icon"]?.ToString();
                if (!string.IsNullOrEmpty(configIcon)) {
                    log("Installing rcedit (to add the icon)...");
                    Process rceditwinget = new Process();
                    rceditwinget.StartInfo = new ProcessStartInfo() {
                        FileName = "winget",
                        Arguments = "install rcedit",
                        CreateNoWindow = true
                    };
                    rceditwinget.Start();
                    rceditwinget.WaitForExit();

                    log("Adding icon...");
                    using Process rcedit = new Process();
                    rcedit.StartInfo = new ProcessStartInfo() {
                        FileName = "rcedit",
                        Arguments = "\"" + Path.Combine(tmpPath, "out.exe") + "\" --set-icon \"" + (File.Exists(configIcon) ? configIcon : Path.Combine(tmpPath, configIcon)) + "\"",
                        CreateNoWindow = true
                    };
                    rcedit.Start();
                    rcedit.WaitForExit();
                    if (rcedit.ExitCode != 0) log("rcedit failed with exit code: " + rcedit.ExitCode, true);
                }
                log("Cleaning up...");
                File.Move(Path.Combine(tmpPath, "out.exe"), output, true);
                Directory.Delete(tmpPath, true);
                log("Finished building!", false, true, GUI);
                log("Output: " + output, false, true);
                if (GUI) browseDialog.configDialog.buildDialog.Close();
                Process.Start("explorer.exe", "/select, \"" + output + "\"");
            }
            catch (Exception ex)
            {
                log("Build error: " + ex.Message, true, false, true);
                log("Cleaning temporary directory and closing...");
                Directory.Delete(tmpPath, true);
            }
        }

        public static void log(string message, bool isError = false, bool isGreen = false, bool messageBox = false)
        {
            if (GUI)
            {
                if (isError) browseDialog.configDialog.buildDialog.logTextBox.ForeColor = Color.Red;
                if (isGreen) browseDialog.configDialog.buildDialog.logTextBox.ForeColor = Color.Green;
                browseDialog.configDialog.buildDialog.logTextBox.Text += "\n[" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "] " + message;
            }
            else
            {
                if (isError) Console.ForegroundColor = ConsoleColor.Red;
                if (isGreen) Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + message);
                if (isError || isGreen) Console.ForegroundColor = ConsoleColor.White;
            }
            if (messageBox) {
                if (isError) MessageBox.Show("Error: " + message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
