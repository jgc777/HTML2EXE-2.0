namespace HTML2EXE_2
{
    partial class ConfigDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigDialog));
            urlLabel = new Label();
            label2 = new Label();
            urlTextBox = new TextBox();
            titleTextBox = new TextBox();
            titleLabel = new Label();
            iconLabel = new Label();
            iconBtn = new Button();
            removeIconBtn = new Button();
            iconPathLabel = new Label();
            iconOpener = new OpenFileDialog();
            contextMenu = new CheckBox();
            devTools = new CheckBox();
            maximized = new CheckBox();
            resizable = new CheckBox();
            controlBox = new CheckBox();
            minimizable = new CheckBox();
            maximizable = new CheckBox();
            fullscreen = new CheckBox();
            alwaysOnTop = new CheckBox();
            zoomControl = new CheckBox();
            showInTaskbar = new CheckBox();
            widthTextBox = new TextBox();
            widthLabel = new Label();
            heightTextBox = new TextBox();
            heightLabel = new Label();
            okBtn = new Button();
            extraCmdTextBox = new TextBox();
            extraCmdLabel = new Label();
            blockClose = new CheckBox();
            jsonOpener = new OpenFileDialog();
            saveConfigBtn = new Button();
            includeNETbox = new CheckBox();
            jsonSaver = new SaveFileDialog();
            label1 = new Label();
            webviewBtn = new Button();
            webviewOpener = new OpenFileDialog();
            webviewPathLabel = new Label();
            removeWebviewBtn = new Button();
            SuspendLayout();
            // 
            // urlLabel
            // 
            urlLabel.AutoSize = true;
            urlLabel.Location = new Point(13, 40);
            urlLabel.Name = "urlLabel";
            urlLabel.Size = new Size(76, 15);
            urlLabel.TabIndex = 0;
            urlLabel.Text = "Custom URL:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 6);
            label2.Name = "label2";
            label2.Size = new Size(188, 28);
            label2.TabIndex = 1;
            label2.Text = "Additional options";
            // 
            // urlTextBox
            // 
            urlTextBox.Location = new Point(114, 37);
            urlTextBox.Name = "urlTextBox";
            urlTextBox.PlaceholderText = "webfiles/index.html";
            urlTextBox.Size = new Size(259, 23);
            urlTextBox.TabIndex = 2;
            // 
            // titleTextBox
            // 
            titleTextBox.Location = new Point(114, 66);
            titleTextBox.Name = "titleTextBox";
            titleTextBox.PlaceholderText = "HTML2EXE";
            titleTextBox.Size = new Size(259, 23);
            titleTextBox.TabIndex = 4;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(13, 69);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(32, 15);
            titleLabel.TabIndex = 3;
            titleLabel.Text = "Title:";
            // 
            // iconLabel
            // 
            iconLabel.AutoSize = true;
            iconLabel.Location = new Point(13, 128);
            iconLabel.Name = "iconLabel";
            iconLabel.Size = new Size(33, 15);
            iconLabel.TabIndex = 5;
            iconLabel.Text = "Icon:";
            // 
            // iconBtn
            // 
            iconBtn.Location = new Point(114, 124);
            iconBtn.Name = "iconBtn";
            iconBtn.Size = new Size(75, 23);
            iconBtn.TabIndex = 6;
            iconBtn.Text = "Select";
            iconBtn.UseVisualStyleBackColor = true;
            iconBtn.Click += iconBtn_Click;
            // 
            // removeIconBtn
            // 
            removeIconBtn.Location = new Point(298, 124);
            removeIconBtn.Name = "removeIconBtn";
            removeIconBtn.Size = new Size(75, 23);
            removeIconBtn.TabIndex = 7;
            removeIconBtn.Text = "Remove";
            removeIconBtn.UseVisualStyleBackColor = true;
            removeIconBtn.Visible = false;
            removeIconBtn.Click += removeIconBtn_Click;
            // 
            // iconPathLabel
            // 
            iconPathLabel.AutoSize = true;
            iconPathLabel.Location = new Point(195, 128);
            iconPathLabel.Name = "iconPathLabel";
            iconPathLabel.Size = new Size(49, 15);
            iconPathLabel.TabIndex = 8;
            iconPathLabel.Text = "No icon";
            // 
            // iconOpener
            // 
            iconOpener.AddToRecent = false;
            iconOpener.Filter = "Icon Files|*.ico";
            iconOpener.Title = "Select icon";
            // 
            // contextMenu
            // 
            contextMenu.AutoSize = true;
            contextMenu.Location = new Point(11, 211);
            contextMenu.Name = "contextMenu";
            contextMenu.Size = new Size(138, 19);
            contextMenu.TabIndex = 9;
            contextMenu.Text = "Enable context menu";
            contextMenu.UseVisualStyleBackColor = true;
            // 
            // devTools
            // 
            devTools.AutoSize = true;
            devTools.Location = new Point(156, 211);
            devTools.Name = "devTools";
            devTools.Size = new Size(145, 19);
            devTools.TabIndex = 10;
            devTools.Text = "Enable developer tools";
            devTools.UseVisualStyleBackColor = true;
            // 
            // maximized
            // 
            maximized.AutoSize = true;
            maximized.Location = new Point(12, 236);
            maximized.Name = "maximized";
            maximized.Size = new Size(84, 19);
            maximized.TabIndex = 11;
            maximized.Text = "Maximized";
            maximized.UseVisualStyleBackColor = true;
            // 
            // resizable
            // 
            resizable.AutoSize = true;
            resizable.Checked = true;
            resizable.CheckState = CheckState.Checked;
            resizable.Location = new Point(156, 236);
            resizable.Name = "resizable";
            resizable.Size = new Size(74, 19);
            resizable.TabIndex = 12;
            resizable.Text = "Resizable";
            resizable.UseVisualStyleBackColor = true;
            // 
            // controlBox
            // 
            controlBox.AutoSize = true;
            controlBox.Checked = true;
            controlBox.CheckState = CheckState.Checked;
            controlBox.Location = new Point(11, 261);
            controlBox.Name = "controlBox";
            controlBox.Size = new Size(146, 19);
            controlBox.TabIndex = 13;
            controlBox.Text = "Show window controls";
            controlBox.UseVisualStyleBackColor = true;
            // 
            // minimizable
            // 
            minimizable.AutoSize = true;
            minimizable.Checked = true;
            minimizable.CheckState = CheckState.Checked;
            minimizable.Location = new Point(156, 261);
            minimizable.Name = "minimizable";
            minimizable.Size = new Size(91, 19);
            minimizable.TabIndex = 14;
            minimizable.Text = "Minimizable";
            minimizable.UseVisualStyleBackColor = true;
            // 
            // maximizable
            // 
            maximizable.AutoSize = true;
            maximizable.Checked = true;
            maximizable.CheckState = CheckState.Checked;
            maximizable.Location = new Point(156, 286);
            maximizable.Name = "maximizable";
            maximizable.Size = new Size(93, 19);
            maximizable.TabIndex = 15;
            maximizable.Text = "Maximizable";
            maximizable.UseVisualStyleBackColor = true;
            // 
            // fullscreen
            // 
            fullscreen.AutoSize = true;
            fullscreen.Location = new Point(11, 286);
            fullscreen.Name = "fullscreen";
            fullscreen.Size = new Size(109, 19);
            fullscreen.TabIndex = 16;
            fullscreen.Text = "Force fullscreen";
            fullscreen.UseVisualStyleBackColor = true;
            // 
            // alwaysOnTop
            // 
            alwaysOnTop.AutoSize = true;
            alwaysOnTop.Location = new Point(11, 311);
            alwaysOnTop.Name = "alwaysOnTop";
            alwaysOnTop.Size = new Size(101, 19);
            alwaysOnTop.TabIndex = 17;
            alwaysOnTop.Text = "Always on top";
            alwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // zoomControl
            // 
            zoomControl.AutoSize = true;
            zoomControl.Location = new Point(156, 311);
            zoomControl.Name = "zoomControl";
            zoomControl.Size = new Size(135, 19);
            zoomControl.TabIndex = 18;
            zoomControl.Text = "Enable zoom control";
            zoomControl.UseVisualStyleBackColor = true;
            // 
            // showInTaskbar
            // 
            showInTaskbar.AutoSize = true;
            showInTaskbar.Checked = true;
            showInTaskbar.CheckState = CheckState.Checked;
            showInTaskbar.Location = new Point(11, 336);
            showInTaskbar.Name = "showInTaskbar";
            showInTaskbar.Size = new Size(109, 19);
            showInTaskbar.TabIndex = 19;
            showInTaskbar.Text = "Show in taskbar";
            showInTaskbar.UseVisualStyleBackColor = true;
            // 
            // widthTextBox
            // 
            widthTextBox.Location = new Point(114, 95);
            widthTextBox.Name = "widthTextBox";
            widthTextBox.PlaceholderText = "800";
            widthTextBox.Size = new Size(85, 23);
            widthTextBox.TabIndex = 21;
            // 
            // widthLabel
            // 
            widthLabel.AutoSize = true;
            widthLabel.Location = new Point(13, 98);
            widthLabel.Name = "widthLabel";
            widthLabel.Size = new Size(87, 15);
            widthLabel.TabIndex = 20;
            widthLabel.Text = "Window width:";
            // 
            // heightTextBox
            // 
            heightTextBox.Location = new Point(287, 95);
            heightTextBox.Name = "heightTextBox";
            heightTextBox.PlaceholderText = "600";
            heightTextBox.Size = new Size(85, 23);
            heightTextBox.TabIndex = 23;
            // 
            // heightLabel
            // 
            heightLabel.AutoSize = true;
            heightLabel.Location = new Point(215, 98);
            heightLabel.Name = "heightLabel";
            heightLabel.Size = new Size(46, 15);
            heightLabel.TabIndex = 22;
            heightLabel.Text = "Height:";
            // 
            // okBtn
            // 
            okBtn.Location = new Point(11, 390);
            okBtn.Name = "okBtn";
            okBtn.Size = new Size(362, 24);
            okBtn.TabIndex = 24;
            okBtn.Text = "Build";
            okBtn.UseVisualStyleBackColor = true;
            okBtn.Click += okBtn_Click;
            // 
            // extraCmdTextBox
            // 
            extraCmdTextBox.Location = new Point(114, 182);
            extraCmdTextBox.Name = "extraCmdTextBox";
            extraCmdTextBox.PlaceholderText = "call start.bat";
            extraCmdTextBox.Size = new Size(259, 23);
            extraCmdTextBox.TabIndex = 27;
            // 
            // extraCmdLabel
            // 
            extraCmdLabel.AutoSize = true;
            extraCmdLabel.Location = new Point(11, 185);
            extraCmdLabel.Name = "extraCmdLabel";
            extraCmdLabel.Size = new Size(94, 15);
            extraCmdLabel.TabIndex = 26;
            extraCmdLabel.Text = "Extra command:";
            // 
            // blockClose
            // 
            blockClose.AutoSize = true;
            blockClose.Location = new Point(156, 336);
            blockClose.Name = "blockClose";
            blockClose.Size = new Size(105, 19);
            blockClose.TabIndex = 30;
            blockClose.Text = "Disable closing";
            blockClose.UseVisualStyleBackColor = true;
            // 
            // jsonOpener
            // 
            jsonOpener.FileName = "config.json";
            jsonOpener.Filter = "Json files|*.json";
            // 
            // saveConfigBtn
            // 
            saveConfigBtn.Location = new Point(156, 358);
            saveConfigBtn.Name = "saveConfigBtn";
            saveConfigBtn.Size = new Size(216, 23);
            saveConfigBtn.TabIndex = 32;
            saveConfigBtn.Text = "Export Config";
            saveConfigBtn.UseVisualStyleBackColor = true;
            saveConfigBtn.Click += saveConfigBtnClick;
            // 
            // includeNETbox
            // 
            includeNETbox.AutoSize = true;
            includeNETbox.Location = new Point(11, 361);
            includeNETbox.Name = "includeNETbox";
            includeNETbox.Size = new Size(137, 19);
            includeNETbox.TabIndex = 33;
            includeNETbox.Text = "Include .NET runtime";
            includeNETbox.UseVisualStyleBackColor = true;
            // 
            // jsonSaver
            // 
            jsonSaver.AddToRecent = false;
            jsonSaver.DefaultExt = "json";
            jsonSaver.FileName = "config.json";
            jsonSaver.Filter = "JSON File|*.json";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 157);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 34;
            label1.Text = "Webview source:";
            // 
            // webviewBtn
            // 
            webviewBtn.Location = new Point(114, 153);
            webviewBtn.Name = "webviewBtn";
            webviewBtn.Size = new Size(75, 23);
            webviewBtn.TabIndex = 35;
            webviewBtn.Text = "Select";
            webviewBtn.UseVisualStyleBackColor = true;
            webviewBtn.Click += webviewBtn_Click;
            // 
            // webviewOpener
            // 
            webviewOpener.AddToRecent = false;
            webviewOpener.DefaultExt = "zip";
            webviewOpener.FileName = "webview.zip";
            webviewOpener.Filter = "HTML2EXE Webview ZIP|*.zip|All file types|*.*";
            // 
            // webviewPathLabel
            // 
            webviewPathLabel.AutoSize = true;
            webviewPathLabel.Location = new Point(195, 157);
            webviewPathLabel.Name = "webviewPathLabel";
            webviewPathLabel.Size = new Size(66, 15);
            webviewPathLabel.TabIndex = 36;
            webviewPathLabel.Text = "Use default";
            // 
            // removeWebviewBtn
            // 
            removeWebviewBtn.Location = new Point(297, 153);
            removeWebviewBtn.Name = "removeWebviewBtn";
            removeWebviewBtn.Size = new Size(75, 23);
            removeWebviewBtn.TabIndex = 37;
            removeWebviewBtn.Text = "Remove";
            removeWebviewBtn.UseVisualStyleBackColor = true;
            removeWebviewBtn.Visible = false;
            removeWebviewBtn.Click += removeWebviewBtn_Click;
            // 
            // ConfigDialog
            // 
            AcceptButton = okBtn;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 424);
            Controls.Add(removeWebviewBtn);
            Controls.Add(webviewPathLabel);
            Controls.Add(webviewBtn);
            Controls.Add(label1);
            Controls.Add(includeNETbox);
            Controls.Add(saveConfigBtn);
            Controls.Add(blockClose);
            Controls.Add(extraCmdTextBox);
            Controls.Add(extraCmdLabel);
            Controls.Add(okBtn);
            Controls.Add(heightTextBox);
            Controls.Add(heightLabel);
            Controls.Add(widthTextBox);
            Controls.Add(widthLabel);
            Controls.Add(showInTaskbar);
            Controls.Add(zoomControl);
            Controls.Add(alwaysOnTop);
            Controls.Add(fullscreen);
            Controls.Add(maximizable);
            Controls.Add(minimizable);
            Controls.Add(controlBox);
            Controls.Add(resizable);
            Controls.Add(maximized);
            Controls.Add(devTools);
            Controls.Add(contextMenu);
            Controls.Add(iconPathLabel);
            Controls.Add(removeIconBtn);
            Controls.Add(iconBtn);
            Controls.Add(iconLabel);
            Controls.Add(titleTextBox);
            Controls.Add(titleLabel);
            Controls.Add(urlTextBox);
            Controls.Add(label2);
            Controls.Add(urlLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfigDialog";
            Text = "HTML2EXE 2.0";
            Load += ConfigDialog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label urlLabel;
        private Label label2;
        private TextBox urlTextBox;
        private TextBox titleTextBox;
        private Label titleLabel;
        private Label iconLabel;
        private Button iconBtn;
        private Button removeIconBtn;
        private Label iconPathLabel;
        private OpenFileDialog iconOpener;
        private CheckBox contextMenu;
        private CheckBox devTools;
        private CheckBox maximized;
        private CheckBox resizable;
        private CheckBox controlBox;
        private CheckBox minimizable;
        private CheckBox maximizable;
        private CheckBox fullscreen;
        private CheckBox alwaysOnTop;
        private CheckBox zoomControl;
        private CheckBox showInTaskbar;
        private TextBox widthTextBox;
        private Label widthLabel;
        private TextBox heightTextBox;
        private Label heightLabel;
        private Button okBtn;
        private TextBox extraCmdTextBox;
        private Label extraCmdLabel;
        private CheckBox blockClose;
        private OpenFileDialog jsonOpener;
        private Button saveConfigBtn;
        private CheckBox includeNETbox;
        private SaveFileDialog jsonSaver;
        private Label label1;
        private Button webviewBtn;
        private OpenFileDialog webviewOpener;
        private Label webviewPathLabel;
        private Button removeWebviewBtn;
    }
}