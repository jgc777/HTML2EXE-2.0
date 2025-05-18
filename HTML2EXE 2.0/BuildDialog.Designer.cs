namespace HTML2EXE_2
{
    partial class BuildDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildDialog));
            logTextBox = new RichTextBox();
            copyBtn = new Button();
            buildBtn = new Button();
            SuspendLayout();
            // 
            // logTextBox
            // 
            logTextBox.Location = new Point(12, 12);
            logTextBox.Name = "logTextBox";
            logTextBox.ReadOnly = true;
            logTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            logTextBox.Size = new Size(460, 208);
            logTextBox.TabIndex = 0;
            logTextBox.Text = "";
            // 
            // copyBtn
            // 
            copyBtn.Location = new Point(397, 226);
            copyBtn.Name = "copyBtn";
            copyBtn.Size = new Size(75, 23);
            copyBtn.TabIndex = 2;
            copyBtn.Text = "Copy Log";
            copyBtn.UseVisualStyleBackColor = true;
            copyBtn.Click += copyBtn_Click;
            // 
            // BuildDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 261);
            Controls.Add(copyBtn);
            Controls.Add(logTextBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BuildDialog";
            Text = "HTML2EXE 2.0";
            Load += BuildDialog_Load;
            ResumeLayout(false);
        }

        #endregion

        public RichTextBox logTextBox;
        private Button buildBtn;
        private Button copyBtn;
    }
}