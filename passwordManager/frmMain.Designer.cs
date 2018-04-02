namespace passwordManager
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.txt_Password = new MetroFramework.Controls.MetroTextBox();
            this.btn_Submit = new MetroFramework.Controls.MetroButton();
            this.txt_UserName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(28, 183);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(68, 19);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "Username";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(28, 212);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(64, 19);
            this.metroLabel2.TabIndex = 2;
            this.metroLabel2.Text = "Password";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txt_Password
            // 
            // 
            // 
            // 
            this.txt_Password.CustomButton.Image = null;
            this.txt_Password.CustomButton.Location = new System.Drawing.Point(233, 1);
            this.txt_Password.CustomButton.Name = "";
            this.txt_Password.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_Password.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_Password.CustomButton.TabIndex = 1;
            this.txt_Password.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_Password.CustomButton.UseSelectable = true;
            this.txt_Password.CustomButton.Visible = false;
            this.txt_Password.Lines = new string[0];
            this.txt_Password.Location = new System.Drawing.Point(147, 208);
            this.txt_Password.MaxLength = 32767;
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_Password.SelectedText = "";
            this.txt_Password.SelectionLength = 0;
            this.txt_Password.SelectionStart = 0;
            this.txt_Password.ShortcutsEnabled = true;
            this.txt_Password.Size = new System.Drawing.Size(255, 23);
            this.txt_Password.TabIndex = 3;
            this.txt_Password.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txt_Password.UseSelectable = true;
            this.txt_Password.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_Password.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btn_Submit
            // 
            this.btn_Submit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Submit.Location = new System.Drawing.Point(147, 237);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(111, 31);
            this.btn_Submit.TabIndex = 4;
            this.btn_Submit.Text = "Login";
            this.btn_Submit.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_Submit.UseSelectable = true;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // txt_UserName
            // 
            this.txt_UserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            // 
            // 
            // 
            this.txt_UserName.CustomButton.Image = null;
            this.txt_UserName.CustomButton.Location = new System.Drawing.Point(233, 1);
            this.txt_UserName.CustomButton.Name = "";
            this.txt_UserName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_UserName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_UserName.CustomButton.TabIndex = 1;
            this.txt_UserName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_UserName.CustomButton.UseSelectable = true;
            this.txt_UserName.CustomButton.Visible = false;
            this.txt_UserName.Lines = new string[0];
            this.txt_UserName.Location = new System.Drawing.Point(147, 179);
            this.txt_UserName.MaxLength = 32767;
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.PasswordChar = '\0';
            this.txt_UserName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_UserName.SelectedText = "";
            this.txt_UserName.SelectionLength = 0;
            this.txt_UserName.SelectionStart = 0;
            this.txt_UserName.ShortcutsEnabled = true;
            this.txt_UserName.Size = new System.Drawing.Size(255, 23);
            this.txt_UserName.TabIndex = 0;
            this.txt_UserName.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txt_UserName.UseSelectable = true;
            this.txt_UserName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_UserName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(7, 265);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(36, 19);
            this.metroLabel3.TabIndex = 5;
            this.metroLabel3.Text = "Ver: ";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(156, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Your vault is hidden here";
            this.notifyIcon1.BalloonTipTitle = "SoteriaVault";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "SoteriaVault";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.btn_Submit;
            this.ClientSize = new System.Drawing.Size(461, 290);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.btn_Submit);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.txt_UserName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox txt_Password;
        private MetroFramework.Controls.MetroButton btn_Submit;
        private MetroFramework.Controls.MetroTextBox txt_UserName;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}