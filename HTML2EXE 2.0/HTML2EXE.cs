﻿using System.IO.Compression;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Windows.Forms;

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
        private static readonly string LatestJsonUrl = "https://github.com/jgc777/HTML2EXE-2.0/releases/latest/download/latest.json";
        public static string webview = "https://github.com/jgc777/HTML2EXE-2.0/releases/latest/download/webview.zip";
        public static string webview_big = "https://github.com/jgc777/HTML2EXE-2.0/releases/latest/download/webview-big.zip";
        public static string? webviewURL;

        public static readonly string CurrentVersion = "999"; // Updated by GitHub at build
        private static readonly string TempFilePath = Path.Combine(Path.GetTempPath(), "HTML2EXE-latest.exe");
        public static readonly string tmpPath = Path.Combine(Path.GetTempPath(), "HTML2EXE");
        public static bool GUI = false;
        private static string guiFlag = Path.Combine(tmpPath, "gui.flag");
        public static BrowseDialog? browseDialog;

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                ApplicationConfiguration.Initialize();
                Application.EnableVisualStyles();
                Console.Title = "HTML2EXE 2.0 v" + (IsBigBuild ? CurrentVersion + " (BIG)" : CurrentVersion);

                if (update) CheckForUpdatesAsync(args).Wait();
                if (Directory.Exists(tmpPath)) Directory.Delete(tmpPath, true);
                Directory.CreateDirectory(tmpPath);
                Directory.CreateDirectory(Path.Combine(tmpPath, "webfiles"));

                if (args.Length > 0 && (args[0] == "-h" || args[0] == "--help" || args[0] == "/?" || args[0] == "/help")) {
                    Console.WriteLine("Opening web documentation...");
                    Process.Start(new ProcessStartInfo {
                        FileName = "https://jgc777.github.io/HTML2EXE-2.0/",
                        UseShellExecute = true
                    });
                }
                else if (args.Length > 0)
                {
                    string? htmlPath = null;
                    bool directory = false;
                    string arg0Expanded = Environment.ExpandEnvironmentVariables(args[0]);

                    // Define html/directory path and copy
                    if (File.Exists(arg0Expanded)) htmlPath = arg0Expanded;
                    else if (File.Exists(Path.Combine(Environment.CurrentDirectory, args[0]))) htmlPath = Path.Combine(Environment.CurrentDirectory, args[0]);
                    else if (Directory.Exists(arg0Expanded)) {
                        htmlPath = arg0Expanded;
                        directory = true;
                    }
                    else if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, args[0]))) {
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
                        if (File.Exists(Environment.ExpandEnvironmentVariables(args[2]))) {
                            JsonNode config = JsonNode.Parse(File.ReadAllText(Environment.ExpandEnvironmentVariables(args[2]))) ?? new JsonObject();
                            string? configIcon = config["icon"]?.ToString();
                            string? iconPath = null;
                            if (!string.IsNullOrEmpty(configIcon))
                            {
                                string expandedIcon = Environment.ExpandEnvironmentVariables(configIcon);
                                if (!string.IsNullOrEmpty(expandedIcon) && File.Exists(expandedIcon))
                                    iconPath = expandedIcon;
                                else if (!string.IsNullOrEmpty(configIcon) && File.Exists(Path.Combine(Environment.CurrentDirectory, configIcon)))
                                    iconPath = Path.Combine(Environment.CurrentDirectory, configIcon);
                            }
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
                            File.WriteAllText(Path.Combine(tmpPath, "config.json"), config.ToString()); // Save the config file
                        }
                        else Console.WriteLine("Config file not found: " + args[2], true);
                    }

                    // Define output path
                    string output = Path.Combine(Environment.CurrentDirectory, "out.exe");
                    string configJsonPath = Path.Combine(tmpPath, "config.json");
                    if (File.Exists(configJsonPath))
                    {
                        JsonNode configNode = JsonNode.Parse(File.ReadAllText(configJsonPath)) ?? new JsonObject();
                        if (configNode["title"] != null)
                            output = Path.Combine(Environment.CurrentDirectory, configNode["title"] + ".exe");
                        webviewURL = (configNode["include_runtime"]?.GetValue<bool>() ?? IsBigBuild) ? webview_big : webview; // Set the webview URL based on the config or the build type
                    }
                    else webviewURL = IsBigBuild ? webview_big : webview; // Set the webview URL based on the build type
                    if (args.Length >= 2 && args[1]!=null) output = args[1];
                    if (Directory.GetParent(output) is DirectoryInfo parentDirectory && !Directory.Exists(parentDirectory.ToString()))
                        Directory.CreateDirectory(parentDirectory.ToString());
                    
                    // Build the executable
                    build(output);
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
                log("Error: " + ex.Message, true, false, GUI);
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
                log("Checking for updates...");
                using HttpClient client = new HttpClient();
                string json = await client.GetStringAsync(LatestJsonUrl);

                using JsonDocument doc = JsonDocument.Parse(json);
                int latestVersion = doc.RootElement.GetProperty("version").GetInt32();
                string? downloadUrl = IsBigBuild ? doc.RootElement.GetProperty("url_big").GetString() : doc.RootElement.GetProperty("url").GetString();

                if ((latestVersion > Int32.Parse(CurrentVersion)) && doc.RootElement.GetProperty("update").GetBoolean() && !string.IsNullOrEmpty(downloadUrl))
                {
                    log($"New version available ({CurrentVersion} --> {latestVersion}). Updating...", false, true);
                    byte[] data = await client.GetByteArrayAsync(downloadUrl);
                    await File.WriteAllBytesAsync(TempFilePath, data);
                    string? argsString = null;
                    if (args.Length > 0) argsString = "\"" + string.Join("\" \"", args) + "\"";
                    Process update = new Process();
                    update.StartInfo = new ProcessStartInfo
                    {
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
                    while (!(update.HasExited || File.Exists(guiFlag)))
                    {
                        await Task.Delay(100);
                    }
                    if (File.Exists(guiFlag)) File.Delete(guiFlag);
                    Environment.Exit(0);
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
            JsonNode config = (File.Exists(configPath)) ? JsonNode.Parse(File.ReadAllText(configPath)) ?? new JsonObject() : new JsonObject();
            bool hasConfig = File.Exists(configPath);
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
CompanyName=" + (config["title"]?.ToString() ?? "Jgc7") + @"
ProductName=" + (config["title"]?.ToString() ?? "HTML2EXE - Set title to change") + @"
LegalCopyright=Copyright " + DateTime.Now.Year + " " + (config["title"]?.ToString() ?? "Jgc7") + @"
[Strings]
FileDesc=" + (config["title"]?.ToString() ?? "HTML2EXE 2.0") + @"
InstallPrompt=
DisplayLicense=
FinishMessage=
TargetName=out.exe
FriendlyName=" + (config["title"]?.ToString() ?? "HTML2EXE 2.0") + @"
AppLaunched=.\Webview.exe
PostInstallCmd=cmd /c del /f /q Webview.exe.WebView2
AdminQuietInstCmd=
UserQuietInstCmd=
FILE0=""Webview.exe""
FILE1=""WebView2Loader.dll""
FILE2=""webfiles.zip""" + (hasConfig ? "\nFILE3=\"config.json\"" : "") + @"
[SourceFiles]
SourceFiles0=.\ 
[SourceFiles0]
%FILE0%=
%FILE1%=
%FILE2%=" + (hasConfig ? "\n%FILE3%=" : "");
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
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "iexpress",
                WorkingDirectory = tmpPath,
                Arguments = "/Q /N " + iexpressConfigPath,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0) throw new Exception("Building failed with exit code: " + process.ExitCode);
            log("IExpress finished with code " + process.ExitCode);
            string? configIcon = config?["icon"]?.ToString();
            if (!string.IsNullOrEmpty(configIcon))
            {
                bool rceditExists = false;
                try {
                    Process checkRcedit = new Process();
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
                catch
                {
                    rceditExists = false;
                }

                if (!rceditExists)
                {
                    log("Installing rcedit...");
                    Process rceditwinget = new Process();
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

                log("Adding icon...");
                using Process rcedit = new Process();
                rcedit.StartInfo = new ProcessStartInfo()
                {
                    FileName = "rcedit",
                    Arguments = "\"" + Path.Combine(tmpPath, "out.exe") + "\" --set-icon \"" + (File.Exists(configIcon) ? configIcon : Path.Combine(tmpPath, configIcon)) + "\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                rcedit.Start();
                rcedit.WaitForExit();
                if (rcedit.ExitCode != 0) log("rcedit failed with exit code " + rcedit.ExitCode, true);
            }
            log("Cleaning up...");
            File.Move(Path.Combine(tmpPath, "out.exe"), output, true);
            Directory.Delete(tmpPath, true);
            log("Finished building!", false, true, GUI);
            log("Output: " + output, false, true);
            if (GUI)
            {
                browseDialog?.configDialog.buildDialog.Close();
                Process.Start("explorer.exe", "/select, \"" + output + "\"");
            }
        }

        public static void log(string message = "", bool isError = false, bool isGreen = false, bool messageBox = false)
        {
            if (GUI)
            {
                if (browseDialog is BrowseDialog && browseDialog.configDialog.buildDialog.logTextBox is RichTextBox)
                {
                    if (isError) browseDialog.configDialog.buildDialog.logTextBox.ForeColor = Color.Red;
                    if (isGreen) browseDialog.configDialog.buildDialog.logTextBox.ForeColor = Color.Green;
                    browseDialog.configDialog.buildDialog.logTextBox.Text += "[" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "] " + message + Environment.NewLine;
                }
                else throw new Exception("logTextBox not found");
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
                else MessageBox.Show(message, "HTML2EXE 2.0 v" + (IsBigBuild ? CurrentVersion + " (BIG)" : CurrentVersion), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
