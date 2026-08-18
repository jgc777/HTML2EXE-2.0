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

            // ===== HEADER (Title) =====
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 12);
            label2.Name = "label2";
            label2.Size = new Size(188, 26);
            label2.TabIndex = 1;
            label2.Text = "Additional options";

            // ===== SECTION 1: URL & TITLE (Top rows) =====
            urlLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            urlLabel.AutoSize = true;
            urlLabel.Location = new Point(13, 53);
            urlLabel.Name = "urlLabel";
            urlLabel.Size = new Size(76, 15);
            urlLabel.TabIndex = 0;
            urlLabel.Text = "Custom URL:";

            urlTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            urlTextBox.Location = new Point(114, 50);
            urlTextBox.Name = "urlTextBox";
            urlTextBox.PlaceholderText = "webfiles/index.html";
            urlTextBox.Size = new Size(267, 23);
            urlTextBox.TabIndex = 2;

            titleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(13, 82);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(32, 15);
            titleLabel.TabIndex = 3;
            titleLabel.Text = "Title:";

            titleTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            titleTextBox.Location = new Point(114, 79);
            titleTextBox.Name = "titleTextBox";
            titleTextBox.PlaceholderText = "HTML2EXE";
            titleTextBox.Size = new Size(267, 23);
            titleTextBox.TabIndex = 4;

            // ===== SECTION 2: ICON (Row 3) =====
            iconLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            iconLabel.AutoSize = true;
            iconLabel.Location = new Point(13, 111);
            iconLabel.Name = "iconLabel";
            iconLabel.Size = new Size(33, 15);
            iconLabel.TabIndex = 5;
            iconLabel.Text = "Icon:";

            iconBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            iconBtn.Location = new Point(114, 107);
            iconBtn.Name = "iconBtn";
            iconBtn.Size = new Size(75, 23);
            iconBtn.TabIndex = 6;
            iconBtn.Text = "Select";
            iconBtn.UseVisualStyleBackColor = true;
            iconBtn.Click += iconBtn_Click;

            removeIconBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            removeIconBtn.Location = new Point(195, 107);
            removeIconBtn.Name = "removeIconBtn";
            removeIconBtn.Size = new Size(75, 23);
            removeIconBtn.TabIndex = 7;
            removeIconBtn.Text = "Remove";
            removeIconBtn.UseVisualStyleBackColor = true;
            removeIconBtn.Visible = false;
            removeIconBtn.Click += removeIconBtn_Click;

            iconPathLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            iconPathLabel.AutoSize = true;
            iconPathLabel.Location = new Point(276, 111);
            iconPathLabel.Name = "iconPathLabel";
            iconPathLabel.Size = new Size(49, 15);
            iconPathLabel.TabIndex = 8;
            iconPathLabel.Text = "No icon";

            iconOpener.AddToRecent = false;
            iconOpener.Filter = "Icon Files|*.ico";
            iconOpener.Title = "Select icon";

            // ===== SECTION 3: DIMENSIONS (Row 4) =====
            widthLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            widthLabel.AutoSize = true;
            widthLabel.Location = new Point(13, 139);
            widthLabel.Name = "widthLabel";
            widthLabel.Size = new Size(44, 15);
            widthLabel.TabIndex = 9;
            widthLabel.Text = "Width:";

            widthTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            widthTextBox.Location = new Point(114, 136);
            widthTextBox.Name = "widthTextBox";
            widthTextBox.PlaceholderText = "800";
            widthTextBox.Size = new Size(75, 23);
            widthTextBox.TabIndex = 10;

            heightLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            heightLabel.AutoSize = true;
            heightLabel.Location = new Point(200, 139);
            heightLabel.Name = "heightLabel";
            heightLabel.Size = new Size(50, 15);
            heightLabel.TabIndex = 11;
            heightLabel.Text = "Height:";

            heightTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            heightTextBox.Location = new Point(256, 136);
            heightTextBox.Name = "heightTextBox";
            heightTextBox.PlaceholderText = "600";
            heightTextBox.Size = new Size(75, 23);
            heightTextBox.TabIndex = 12;

            // ===== SECTION 4: CHECKBOXES (Rows 5-9, 2 columns) =====
            contextMenu.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            contextMenu.AutoSize = true;
            contextMenu.Location = new Point(13, 168);
            contextMenu.Name = "contextMenu";
            contextMenu.Size = new Size(138, 19);
            contextMenu.TabIndex = 13;
            contextMenu.Text = "Enable context menu";
            contextMenu.UseVisualStyleBackColor = true;

            devTools.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            devTools.AutoSize = true;
            devTools.Location = new Point(200, 168);
            devTools.Name = "devTools";
            devTools.Size = new Size(145, 19);
            devTools.TabIndex = 14;
            devTools.Text = "Enable developer tools";
            devTools.UseVisualStyleBackColor = true;

            maximized.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            maximized.AutoSize = true;
            maximized.Location = new Point(13, 193);
            maximized.Name = "maximized";
            maximized.Size = new Size(84, 19);
            maximized.TabIndex = 15;
            maximized.Text = "Maximized";
            maximized.UseVisualStyleBackColor = true;

            resizable.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            resizable.AutoSize = true;
            resizable.Checked = true;
            resizable.CheckState = CheckState.Checked;
            resizable.Location = new Point(200, 193);
            resizable.Name = "resizable";
            resizable.Size = new Size(74, 19);
            resizable.TabIndex = 16;
            resizable.Text = "Resizable";
            resizable.UseVisualStyleBackColor = true;

            controlBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            controlBox.AutoSize = true;
            controlBox.Checked = true;
            controlBox.CheckState = CheckState.Checked;
            controlBox.Location = new Point(13, 218);
            controlBox.Name = "controlBox";
            controlBox.Size = new Size(146, 19);
            controlBox.TabIndex = 17;
            controlBox.Text = "Show window controls";
            controlBox.UseVisualStyleBackColor = true;

            minimizable.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            minimizable.AutoSize = true;
            minimizable.Checked = true;
            minimizable.CheckState = CheckState.Checked;
            minimizable.Location = new Point(200, 218);
            minimizable.Name = "minimizable";
            minimizable.Size = new Size(91, 19);
            minimizable.TabIndex = 18;
            minimizable.Text = "Minimizable";
            minimizable.UseVisualStyleBackColor = true;

            maximizable.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            maximizable.AutoSize = true;
            maximizable.Checked = true;
            maximizable.CheckState = CheckState.Checked;
            maximizable.Location = new Point(200, 243);
            maximizable.Name = "maximizable";
            maximizable.Size = new Size(93, 19);
            maximizable.TabIndex = 19;
            maximizable.Text = "Maximizable";
            maximizable.UseVisualStyleBackColor = true;

            fullscreen.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            fullscreen.AutoSize = true;
            fullscreen.Location = new Point(13, 243);
            fullscreen.Name = "fullscreen";
            fullscreen.Size = new Size(109, 19);
            fullscreen.TabIndex = 20;
            fullscreen.Text = "Force fullscreen";
            fullscreen.UseVisualStyleBackColor = true;

            alwaysOnTop.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            alwaysOnTop.AutoSize = true;
            alwaysOnTop.Location = new Point(13, 268);
            alwaysOnTop.Name = "alwaysOnTop";
            alwaysOnTop.Size = new Size(101, 19);
            alwaysOnTop.TabIndex = 21;
            alwaysOnTop.Text = "Always on top";
            alwaysOnTop.UseVisualStyleBackColor = true;

            zoomControl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            zoomControl.AutoSize = true;
            zoomControl.Location = new Point(200, 268);
            zoomControl.Name = "zoomControl";
            zoomControl.Size = new Size(135, 19);
            zoomControl.TabIndex = 22;
            zoomControl.Text = "Enable zoom control";
            zoomControl.UseVisualStyleBackColor = true;

            showInTaskbar.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            showInTaskbar.AutoSize = true;
            showInTaskbar.Checked = true;
            showInTaskbar.CheckState = CheckState.Checked;
            showInTaskbar.Location = new Point(13, 293);
            showInTaskbar.Name = "showInTaskbar";
            showInTaskbar.Size = new Size(109, 19);
            showInTaskbar.TabIndex = 23;
            showInTaskbar.Text = "Show in taskbar";
            showInTaskbar.UseVisualStyleBackColor = true;

            blockClose.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            blockClose.AutoSize = true;
            blockClose.Location = new Point(200, 293);
            blockClose.Name = "blockClose";
            blockClose.Size = new Size(122, 19);
            blockClose.TabIndex = 24;
            blockClose.Text = "Block close event";
            blockClose.UseVisualStyleBackColor = true;

            // ===== SECTION 5: EXTRA CMD & WEBVIEW =====
            extraCmdLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            extraCmdLabel.AutoSize = true;
            extraCmdLabel.Location = new Point(13, 327);
            extraCmdLabel.Name = "extraCmdLabel";
            extraCmdLabel.Size = new Size(98, 15);
            extraCmdLabel.TabIndex = 25;
            extraCmdLabel.Text = "Extra command:";

            extraCmdTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            extraCmdTextBox.Location = new Point(114, 324);
            extraCmdTextBox.Name = "extraCmdTextBox";
            extraCmdTextBox.PlaceholderText = ";";
            extraCmdTextBox.Size = new Size(267, 23);
            extraCmdTextBox.TabIndex = 26;

            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(13, 355);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 34;
            label1.Text = "Webview source:";

            webviewBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            webviewBtn.Location = new Point(114, 351);
            webviewBtn.Name = "webviewBtn";
            webviewBtn.Size = new Size(75, 23);
            webviewBtn.TabIndex = 35;
            webviewBtn.Text = "Select";
            webviewBtn.UseVisualStyleBackColor = true;
            webviewBtn.Click += webviewBtn_Click;

            removeWebviewBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            removeWebviewBtn.Location = new Point(195, 351);
            removeWebviewBtn.Name = "removeWebviewBtn";
            removeWebviewBtn.Size = new Size(75, 23);
            removeWebviewBtn.TabIndex = 37;
            removeWebviewBtn.Text = "Remove";
            removeWebviewBtn.UseVisualStyleBackColor = true;
            removeWebviewBtn.Visible = false;
            removeWebviewBtn.Click += removeWebviewBtn_Click;

            webviewPathLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            webviewPathLabel.AutoSize = true;
            webviewPathLabel.Location = new Point(276, 355);
            webviewPathLabel.Name = "webviewPathLabel";
            webviewPathLabel.Size = new Size(66, 15);
            webviewPathLabel.TabIndex = 36;
            webviewPathLabel.Text = "Use default";

            webviewOpener.AddToRecent = false;
            webviewOpener.DefaultExt = "zip";
            webviewOpener.Filter = "HTML2EXE Webview ZIP|*.zip|All file types|*.*";

            // ===== SECTION 6: CHECKBOXES (continued) =====
            includeNETbox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            includeNETbox.AutoSize = true;
            includeNETbox.Location = new Point(13, 383);
            includeNETbox.Name = "includeNETbox";
            includeNETbox.Size = new Size(185, 19);
            includeNETbox.TabIndex = 27;
            includeNETbox.Text = "Include .NET Runtime (Big)";
            includeNETbox.UseVisualStyleBackColor = true;

            jsonOpener.AddToRecent = false;
            jsonOpener.DefaultExt = "json";
            jsonOpener.Filter = "JSON File|*.json";

            jsonSaver.Filter = "JSON File|*.json";

            // ===== SECTION 7: BUTTONS (Bottom) =====
            okBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okBtn.Location = new Point(321, 420);
            okBtn.Name = "okBtn";
            okBtn.Size = new Size(75, 23);
            okBtn.TabIndex = 28;
            okBtn.Text = "Build";
            okBtn.UseVisualStyleBackColor = true;
            okBtn.Click += okBtn_Click;

            saveConfigBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            saveConfigBtn.Location = new Point(13, 420);
            saveConfigBtn.Name = "saveConfigBtn";
            saveConfigBtn.Size = new Size(109, 23);
            saveConfigBtn.TabIndex = 29;
            saveConfigBtn.Text = "Save Config";
            saveConfigBtn.UseVisualStyleBackColor = true;
            saveConfigBtn.Click += saveConfigBtnClick;

            // ===== FORM SETTINGS =====
            AcceptButton = okBtn;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(406, 460);
            Controls.Add(saveConfigBtn);
            Controls.Add(okBtn);
            Controls.Add(includeNETbox);
            Controls.Add(removeWebviewBtn);
            Controls.Add(webviewPathLabel);
            Controls.Add(webviewBtn);
            Controls.Add(label1);
            Controls.Add(extraCmdTextBox);
            Controls.Add(extraCmdLabel);
            Controls.Add(blockClose);
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
            Controls.Add(heightTextBox);
            Controls.Add(heightLabel);
            Controls.Add(widthTextBox);
            Controls.Add(widthLabel);
            Controls.Add(iconPathLabel);
            Controls.Add(removeIconBtn);
            Controls.Add(iconBtn);
            Controls.Add(iconLabel);
            Controls.Add(titleTextBox);
            Controls.Add(titleLabel);
            Controls.Add(urlTextBox);
            Controls.Add(label2);
            Controls.Add(urlLabel);
            ControlBox = true;
            FormBorderStyle = FormBorderStyle.Sizable;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = true;
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
