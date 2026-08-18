namespace HTML2EXE_2
{
    partial class BrowseDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowseDialog));
            selectFileBtn = new Button();
            openFileDialog1 = new OpenFileDialog();
            selectFolderBtn = new Button();
            label1 = new Label();
            folderBrowserDialog1 = new FolderBrowserDialog();
            noFileBtn = new Button();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            headerPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            headerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            headerPanel.Cursor = Cursors.Hand;
            headerPanel.Location = new Point(12, 12);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(342, 65);
            headerPanel.TabIndex = 8;
            headerPanel.Click += HeaderPanel_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(65, 65);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = false;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.Location = new Point(72, 12);
            label1.Name = "label1";
            label1.Size = new Size(270, 26);
            label1.TabIndex = 4;
            label1.Text = "HTML2EXE 2.0";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(72, 42);
            label2.Name = "label2";
            label2.Size = new Size(127, 15);
            label2.TabIndex = 7;
            label2.Text = "Copyright © 2025 Jgc7";

            // Add controls to header panel
            headerPanel.Controls.Add(label2);
            headerPanel.Controls.Add(label1);
            headerPanel.Controls.Add(pictureBox1);

            // 
            // selectFileBtn
            // 
            selectFileBtn.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right));
            selectFileBtn.Location = new Point(12, 85);
            selectFileBtn.Name = "selectFileBtn";
            selectFileBtn.Size = new Size(342, 30);
            selectFileBtn.TabIndex = 1;
            selectFileBtn.Text = "Select File";
            selectFileBtn.UseVisualStyleBackColor = true;
            selectFileBtn.Click += selectFileBtn_Click;
            // 
            // selectFolderBtn
            // 
            selectFolderBtn.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right));
            selectFolderBtn.Location = new Point(12, 121);
            selectFolderBtn.Name = "selectFolderBtn";
            selectFolderBtn.Size = new Size(342, 30);
            selectFolderBtn.TabIndex = 3;
            selectFolderBtn.Text = "Select Folder";
            selectFolderBtn.UseVisualStyleBackColor = true;
            selectFolderBtn.Click += selectFolderBtn_Click;
            // 
            // noFileBtn
            // 
            noFileBtn.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right));
            noFileBtn.Location = new Point(12, 157);
            noFileBtn.Name = "noFileBtn";
            noFileBtn.Size = new Size(342, 30);
            noFileBtn.TabIndex = 5;
            noFileBtn.Text = "Continue without a file";
            noFileBtn.UseVisualStyleBackColor = true;
            noFileBtn.Click += noFileBtn_Click_1;

            // 
            // openFileDialog1
            // 
            openFileDialog1.AddToRecent = false;
            openFileDialog1.DefaultExt = "html";
            openFileDialog1.Filter = "HTML files|*.html;*.htm|All file types|*.*";
            openFileDialog1.OkRequiresInteraction = true;
            openFileDialog1.Title = "Select your html file";
            // 
            // folderBrowserDialog1
            // 
            folderBrowserDialog1.AddToRecent = false;
            folderBrowserDialog1.Description = "Select your html folder";
            folderBrowserDialog1.OkRequiresInteraction = true;
            folderBrowserDialog1.ShowNewFolderButton = false;

            // 
            // BrowseDialog
            // 
            AcceptButton = selectFileBtn;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(366, 200);
            ControlBox = true;
            Controls.Add(noFileBtn);
            Controls.Add(selectFolderBtn);
            Controls.Add(selectFileBtn);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.Sizable;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = true;
            Name = "BrowseDialog";
            Text = "HTML2EXE 2.0";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button selectFileBtn;
        private OpenFileDialog openFileDialog1;
        private Button selectFolderBtn;
        private Label label1;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button noFileBtn;
        private PictureBox pictureBox1;
        private Label label2;
        private Panel headerPanel;
    }
}