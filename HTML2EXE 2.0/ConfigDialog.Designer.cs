namespace HTML2EXE_2._0
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
            openFileDialog1 = new OpenFileDialog();
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
            SuspendLayout();
            // 
            // urlLabel
            // 
            urlLabel.AutoSize = true;
            urlLabel.Location = new Point(13, 40);
            urlLabel.Name = "urlLabel";
            urlLabel.Size = new Size(93, 15);
            urlLabel.TabIndex = 0;
            urlLabel.Text = "URL (advanced):";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(188, 28);
            label2.TabIndex = 1;
            label2.Text = "Additional options";
            // 
            // urlTextBox
            // 
            urlTextBox.Location = new Point(106, 37);
            urlTextBox.Name = "urlTextBox";
            urlTextBox.PlaceholderText = "webfiles/index.html";
            urlTextBox.Size = new Size(267, 23);
            urlTextBox.TabIndex = 2;
            urlTextBox.TextChanged += textBox1_TextChanged;
            // 
            // titleTextBox
            // 
            titleTextBox.Location = new Point(106, 64);
            titleTextBox.Name = "titleTextBox";
            titleTextBox.PlaceholderText = "HTML2EXE";
            titleTextBox.Size = new Size(267, 23);
            titleTextBox.TabIndex = 4;
            titleTextBox.TextChanged += textBox2_TextChanged;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(13, 67);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(32, 15);
            titleLabel.TabIndex = 3;
            titleLabel.Text = "Title:";
            titleLabel.Click += label3_Click;
            // 
            // iconLabel
            // 
            iconLabel.AutoSize = true;
            iconLabel.Location = new Point(13, 93);
            iconLabel.Name = "iconLabel";
            iconLabel.Size = new Size(33, 15);
            iconLabel.TabIndex = 5;
            iconLabel.Text = "Icon:";
            // 
            // iconBtn
            // 
            iconBtn.Location = new Point(106, 89);
            iconBtn.Name = "iconBtn";
            iconBtn.Size = new Size(75, 23);
            iconBtn.TabIndex = 6;
            iconBtn.Text = "Select";
            iconBtn.UseVisualStyleBackColor = true;
            iconBtn.Click += iconBtn_Click;
            // 
            // removeIconBtn
            // 
            removeIconBtn.Location = new Point(298, 89);
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
            iconPathLabel.Location = new Point(187, 93);
            iconPathLabel.Name = "iconPathLabel";
            iconPathLabel.Size = new Size(49, 15);
            iconPathLabel.TabIndex = 8;
            iconPathLabel.Text = "No icon";
            // 
            // openFileDialog1
            // 
            openFileDialog1.AddToRecent = false;
            openFileDialog1.Filter = "Icon Files|*.ico";
            openFileDialog1.Title = "Select icon";
            // 
            // contextMenu
            // 
            contextMenu.AutoSize = true;
            contextMenu.Location = new Point(12, 199);
            contextMenu.Name = "contextMenu";
            contextMenu.Size = new Size(138, 19);
            contextMenu.TabIndex = 9;
            contextMenu.Text = "Enable context menu";
            contextMenu.UseVisualStyleBackColor = true;
            // 
            // devTools
            // 
            devTools.AutoSize = true;
            devTools.Location = new Point(156, 199);
            devTools.Name = "devTools";
            devTools.Size = new Size(145, 19);
            devTools.TabIndex = 10;
            devTools.Text = "Enable developer tools";
            devTools.UseVisualStyleBackColor = true;
            // 
            // maximized
            // 
            maximized.AutoSize = true;
            maximized.Location = new Point(13, 224);
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
            resizable.Location = new Point(156, 224);
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
            controlBox.Location = new Point(12, 249);
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
            minimizable.Location = new Point(156, 249);
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
            maximizable.Location = new Point(156, 274);
            maximizable.Name = "maximizable";
            maximizable.Size = new Size(93, 19);
            maximizable.TabIndex = 15;
            maximizable.Text = "Maximizable";
            maximizable.UseVisualStyleBackColor = true;
            // 
            // fullscreen
            // 
            fullscreen.AutoSize = true;
            fullscreen.Location = new Point(13, 274);
            fullscreen.Name = "fullscreen";
            fullscreen.Size = new Size(109, 19);
            fullscreen.TabIndex = 16;
            fullscreen.Text = "Force fullscreen";
            fullscreen.UseVisualStyleBackColor = true;
            // 
            // alwaysOnTop
            // 
            alwaysOnTop.AutoSize = true;
            alwaysOnTop.Location = new Point(12, 299);
            alwaysOnTop.Name = "alwaysOnTop";
            alwaysOnTop.Size = new Size(101, 19);
            alwaysOnTop.TabIndex = 17;
            alwaysOnTop.Text = "Always on top";
            alwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // zoomControl
            // 
            zoomControl.AutoSize = true;
            zoomControl.Location = new Point(156, 299);
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
            showInTaskbar.Location = new Point(13, 324);
            showInTaskbar.Name = "showInTaskbar";
            showInTaskbar.Size = new Size(109, 19);
            showInTaskbar.TabIndex = 19;
            showInTaskbar.Text = "Show in taskbar";
            showInTaskbar.UseVisualStyleBackColor = true;
            // 
            // widthTextBox
            // 
            widthTextBox.Location = new Point(106, 114);
            widthTextBox.Name = "widthTextBox";
            widthTextBox.PlaceholderText = "800";
            widthTextBox.Size = new Size(267, 23);
            widthTextBox.TabIndex = 21;
            widthTextBox.TextChanged += textBox3_TextChanged;
            // 
            // widthLabel
            // 
            widthLabel.AutoSize = true;
            widthLabel.Location = new Point(12, 118);
            widthLabel.Name = "widthLabel";
            widthLabel.Size = new Size(87, 15);
            widthLabel.TabIndex = 20;
            widthLabel.Text = "Window width:";
            widthLabel.Click += label6_Click;
            // 
            // heightTextBox
            // 
            heightTextBox.Location = new Point(106, 143);
            heightTextBox.Name = "heightTextBox";
            heightTextBox.PlaceholderText = "600";
            heightTextBox.Size = new Size(267, 23);
            heightTextBox.TabIndex = 23;
            // 
            // heightLabel
            // 
            heightLabel.AutoSize = true;
            heightLabel.Location = new Point(12, 146);
            heightLabel.Name = "heightLabel";
            heightLabel.Size = new Size(91, 15);
            heightLabel.TabIndex = 22;
            heightLabel.Text = "Window height:";
            // 
            // okBtn
            // 
            okBtn.Location = new Point(297, 349);
            okBtn.Name = "okBtn";
            okBtn.Size = new Size(75, 23);
            okBtn.TabIndex = 24;
            okBtn.Text = "Build";
            okBtn.UseVisualStyleBackColor = true;
            okBtn.Click += okBtn_Click;
            // 
            // extraCmdTextBox
            // 
            extraCmdTextBox.Location = new Point(106, 170);
            extraCmdTextBox.Name = "extraCmdTextBox";
            extraCmdTextBox.PlaceholderText = "call start.bat";
            extraCmdTextBox.Size = new Size(267, 23);
            extraCmdTextBox.TabIndex = 27;
            // 
            // extraCmdLabel
            // 
            extraCmdLabel.AutoSize = true;
            extraCmdLabel.Location = new Point(13, 173);
            extraCmdLabel.Name = "extraCmdLabel";
            extraCmdLabel.Size = new Size(94, 15);
            extraCmdLabel.TabIndex = 26;
            extraCmdLabel.Text = "Extra command:";
            // 
            // blockClose
            // 
            blockClose.AutoSize = true;
            blockClose.Location = new Point(156, 324);
            blockClose.Name = "blockClose";
            blockClose.Size = new Size(105, 19);
            blockClose.TabIndex = 30;
            blockClose.Text = "Disable closing";
            blockClose.UseVisualStyleBackColor = true;
            // 
            // ConfigDialog
            // 
            AcceptButton = okBtn;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 381);
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
        private OpenFileDialog openFileDialog1;
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
    }
}