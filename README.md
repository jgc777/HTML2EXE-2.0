<div align="center">
<img src="icon.png" width="150"><br>
<h1>HTML2EXE 2.0</h1>
</div>

![.NET](https://img.shields.io/badge/.NET-5C2D91?logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/C%23-%23239120.svg?logoColor=white)
![Converter](https://img.shields.io/badge/Converter-gray)
![HTML5](https://img.shields.io/badge/Webview%202-%23E34F26.svg?logo=html5&logoColor=white)
[![MIT License](https://img.shields.io/badge/License-MIT-blue)](./LICENSE)
[![Workflow](https://github.com/jgc777/HTML2EXE-2.0/actions/workflows/publish.yml/badge.svg)](https://github.com/jgc777/HTML2EXE-2.0/actions/workflows/publish.yml)

A rewrite of [HTML2EXE](https://jgc777.github.io/HTML2EXE) which takes much less size and it's more customizable.

Note that there are some good alternatives such as [Electron](https://www.electronjs.org/docs/latest/), [NW.js](https://nwjs.readthedocs.io/en/latest/) or [Tauri](https://v2.tauri.app/start/). Due to iexpress limitations, this program only works in Windows.

## Requirements
- Windows 10 or later

## Features
- Convert HTML files (really any chromium-compatible files) and folders to EXE files
- Use internet webpages (Use the URL option)
- Custom icon, title, windows command & more
- Very small program size (except for the version with .NET included)
- Graphical user interface (GUI)
- Command line interface (CLI)
- Quick and easy to use and distribute

## How to use it
- [Download the program](https://github.com/jgc777/HTML2EXE-2.0/releases/latest/download/HTML2EXE.exe) from the [latest release](https://github.com/jgc777/HTML2EXE-2.0/releases/latest/).
- Drag and drop your HTML file or folder to the program.

You can also open the program to see the GUI menu or use it from the command line.

### GUI (Graphical User Interface)
- Open the program and a GUI will show up with all the options.
- You can select the HTML file or folder you want to convert (with index.html) or just continue with an empty folder.
- The program will show a new window with many options and the build button.
- Press that button and the program will start compiling, and it will show a "Finished Building!" or a "Error" with the compiler log.

### CLI (Command Line Interface)
```
HTML2EXE HTML OUTPUT CONFIG WEBVIEW

HTML: Path to the HTML file or folder (with a .html file, preferably index.html) you want to convert.
OUTPUT (optional): Output EXE file path (defaults to "out.exe")
CONFIG (optional): Path to config.json file. If you don't specify it, the program will use default values.
WEBVIEW (optional): Path to a local webview.zip file. If you don't specify it, the program will download the latest default webview.
```

The program will show the log in the terminal.

## Config.json
It's a JSON file which defines some webview settings. It can be loaded in the CLI and configuration GUI. The configuration GUI creates it for you.

| Option | Type | Description |
|---|---|---|
| `maximized` | boolean | If the window should be automatically maximized. |
| `resizable` | boolean | If the window should be resizable. |
| `control_box` | boolean | If the window should have a control box (minimize, maximize and close buttons). |
| `minimizable` | boolean | If the window should be minimizable. |
| `maximizable` | boolean | If the window should be maximizable. |
| `fullscreen` | boolean | If the window should be fullscreen by default. |
| `always_on_top` | boolean | If the window should be always on top. |
| `zoomcontrol` | boolean | If the zoom control should be enabled. |
| `show_in_taskbar` | boolean | If the window should be shown in the taskbar. |
| `width` | integer | The width of the window. |
| `height` | integer | The height of the window. |
| `url` | string | The URL to load. |
| `title` | string | The title of the window and data folder. |
| `icon` | string | The icon of the window. |
| `context_menu` | boolean | If the context menu should be enabled. |
| `dev_tools` | boolean | If developer tools should be enabled. |
| `block_close` | boolean | If the window should be blocked from closing. |
| `additional_cmd` | string | Additional command to run in the webfiles folder after starting the window (in a hidden cmd.exe window, to start batch files use call to stay hidden or start to create a window). |

`include_runtime` and `webview` are only used by the HTML2EXE compiler, not in the final webview app. `icon` is a full path only during compilation.

## Contributing
Use GitHub pull requests, discussions or issues to report problems, suggest features, etc. It will be appreciated.

## License
This project is licensed under the MIT License, see the [LICENSE](LICENSE) file for details.

## Credits
- [Webview 2](https://developer.microsoft.com/es-es/microsoft-edge/webview2) & [IExpress](https://es.wikipedia.org/wiki/IExpress): Microsoft
- [rcedit](https://github.com/electron/rcedit): Electron
- [Icon](https://www.onlinewebfonts.com/icon/454233): Online WebFonts
