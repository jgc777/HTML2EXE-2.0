# HTML2EXE 2.0
![.NET](https://img.shields.io/badge/.NET-5C2D91?logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?logo=csharp&logoColor=white)
![Converter](https://img.shields.io/badge/converter-gray)
![HTML5](https://img.shields.io/badge/html5-%23E34F26.svg?logo=html5&logoColor=white)
[![MIT License](https://img.shields.io/badge/license-MIT-blue)](./LICENSE)
[![Workflow](https://github.com/jgc777/HTML2EXE-2.0/actions/workflows/publish.yml/badge.svg)](https://github.com/jgc777/HTML2EXE-2.0/actions/workflows/publish.yml)

<p align="center"><img src="icon.png" width="150"></p>

A rewrite of [HTML2EXE](https://jgc777.github.io/HTML2EXE) which takes much less size and it's more customizable.

## Features
- Convert HTML files (really any chromium-compatible files) and folders to EXE files
- Use internet webpages (Use the URL option)
- Custom icon, title, windows command & more
- Very small program size (except for the version with .NET included)
- Graphical user interface (GUI)
- Command line interface (CLI)
- Quick and easy to use and distribute

## How to use it
First of all, [download the program](https://github.com/jgc777/HTML2EXE-2.0/releases/latest/download/HTML2EXE.exe) from the [latest release](https://github.com/jgc777/HTML2EXE-2.0/releases/latest/). Then, drag and drop the HTML file or folder you want to convert to the program. You can also open the program to see the menu or call it from the command line (read more down here).

## GUI (Graphical User Interface)
Just open the program and a GUI will show up with all the options. You can select the HTML file or folder you want to convert (with index.html) or just continue with an empty folder. The program will show a new window with many options and the build button. Press that button and the program will start compiling, and it will show a "Finished Building!" or a "Error" with the compiler log.

## CLI (Command Line Interface)
`HTML2EXE HTML OUTPUT CONFIG`

- HTML: The HTML file or folder you want to convert. If you select a folder, make sure it contains an index.html file.
- OUTPUT (optional): The output EXE file path. If you don't specify it, the program will create "out.exe".
- CONFIG (optional): The path to the config.json file. If you don't specify it, the program will use an empty config.json file.

The program will show the log in the terminal.

## Config.json
It's a JSON file which defines some webview settings. It can be loaded in the CLI and configuration GUI. The configuration GUI creates it for you.

| Option             | Type     | Description                                                                |
|--------------------|----------|----------------------------------------------------------------------------|
| `maximized`        | boolean  | If the window should be automatically maximized.                           |
| `resizable`        | boolean  | If the window should be resizable.                                         |
| `control_box`      | boolean  | If the window should have a control box (minimize, maximize and close buttons). |
| `minimizable`      | boolean  | If the window should be minimizable.                                       |
| `maximizable`      | boolean  | If the window should be maximizable.                                       |
| `fullscreen`       | boolean  | If the window should be fullscreen by default.                             |
| `always_on_top`    | boolean  | If the window should be always on top.                                     |
| `zoomcontrol`      | boolean  | If the zoom control should be enabled.                                     |
| `show_in_taskbar`  | boolean  | If the window should be shown in the taskbar.                              |
| `width`            | integer  | The width of the window.                                                   |
| `height`           | integer  | The height of the window.                                                  |
| `url`              | string   | The URL to load.                                                           |
| `title`            | string   | The title of the window and data folder.                                   |
| `icon`             | string   | The icon of the window.                                                    |
| `context_menu`     | boolean  | If the context menu should be enabled.                                     |
| `dev_tools`        | boolean  | If developer tools should be enabled.                                      |
| `block_close`      | boolean  | If the window should be blocked from closing.                              |
| `additional_cmd`   | string   | Additional command to run in the webfiles folder after starting the window (in a hidden cmd.exe window, to start batch files use call to stay hidden or start to create a window). |

Note that `include_runtime` is only used in the config for the HTML2EXE compiler (not in the webview). `icon` is a full path only during the compilation.

## Contributing
Use GitHub pull requests, discussions or issues to report problems, suggest features, etc. It will be appreciated.

## License
This project is licensed under the MIT License, see the [LICENSE](LICENSE) file for details.

## Credits
- [Webview 2](https://developer.microsoft.com/es-es/microsoft-edge/webview2) & [IExpress](https://es.wikipedia.org/wiki/IExpress): Microsoft
- [rcedit](https://github.com/electron/rcedit): Electron
- [Icon](https://www.onlinewebfonts.com/icon/454233): Online WebFonts
