namespace HTML2EXE_2._0
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
            cancelBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // selectFileBtn
            // 
            selectFileBtn.Location = new Point(182, 9);
            selectFileBtn.Name = "selectFileBtn";
            selectFileBtn.Size = new Size(157, 30);
            selectFileBtn.TabIndex = 1;
            selectFileBtn.Text = "Select File";
            selectFileBtn.UseVisualStyleBackColor = true;
            selectFileBtn.Click += selectFileBtn_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.AddToRecent = false;
            openFileDialog1.DefaultExt = "html";
            openFileDialog1.Filter = "HTML files|*.html;*.htm|All file types|*.*";
            openFileDialog1.OkRequiresInteraction = true;
            openFileDialog1.Title = "Select your html file";
            // 
            // selectFolderBtn
            // 
            selectFolderBtn.Location = new Point(182, 45);
            selectFolderBtn.Name = "selectFolderBtn";
            selectFolderBtn.Size = new Size(157, 30);
            selectFolderBtn.TabIndex = 3;
            selectFolderBtn.Text = "Select Folder";
            selectFolderBtn.UseVisualStyleBackColor = true;
            selectFolderBtn.Click += selectFolderBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(164, 30);
            label1.TabIndex = 4;
            label1.Text = "HTML2EXE 2.0";
            // 
            // folderBrowserDialog1
            // 
            folderBrowserDialog1.AddToRecent = false;
            folderBrowserDialog1.Description = "Select your html folder";
            folderBrowserDialog1.OkRequiresInteraction = true;
            folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // noFileBtn
            // 
            noFileBtn.Location = new Point(182, 81);
            noFileBtn.Name = "noFileBtn";
            noFileBtn.Size = new Size(157, 30);
            noFileBtn.TabIndex = 5;
            noFileBtn.Text = "Continue without a file";
            noFileBtn.UseVisualStyleBackColor = true;
            noFileBtn.Click += noFileBtn_Click_1;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 57);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(165, 65);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 39);
            label2.Name = "label2";
            label2.Size = new Size(127, 15);
            label2.TabIndex = 7;
            label2.Text = "Copyright © 2025 Jgc7";
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(182, 117);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(157, 30);
            cancelBtn.TabIndex = 8;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += cancelBtn_Click_1;
            // 
            // BrowseDialog
            // 
            AcceptButton = selectFileBtn;
            AutoScaleMode = AutoScaleMode.None;
            CancelButton = cancelBtn;
            ClientSize = new Size(354, 156);
            ControlBox = false;
            Controls.Add(cancelBtn);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(noFileBtn);
            Controls.Add(label1);
            Controls.Add(selectFolderBtn);
            Controls.Add(selectFileBtn);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BrowseDialog";
            Text = "HTML2EXE 2.0";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private Button cancelBtn;
    }
}