namespace soteriasVault
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
            this.txt_Password = new MetroFramework.Controls.MetroTextBox();
            this.btn_Submit = new MetroFramework.Controls.MetroButton();
            this.txt_UserName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_Password
            // 
            // 
            // 
            // 
            this.txt_Password.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.txt_Password.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode")));
            this.txt_Password.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location")));
            this.txt_Password.CustomButton.Name = "";
            this.txt_Password.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size")));
            this.txt_Password.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_Password.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex")));
            this.txt_Password.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_Password.CustomButton.UseSelectable = true;
            this.txt_Password.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible")));
            this.txt_Password.Lines = new string[0];
            resources.ApplyResources(this.txt_Password, "txt_Password");
            this.txt_Password.MaxLength = 50;
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.PromptText = "Password";
            this.txt_Password.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_Password.SelectedText = "";
            this.txt_Password.SelectionLength = 0;
            this.txt_Password.SelectionStart = 0;
            this.txt_Password.ShortcutsEnabled = true;
            this.txt_Password.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txt_Password.UseSelectable = true;
            this.txt_Password.WaterMark = "Password";
            this.txt_Password.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_Password.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btn_Submit
            // 
            this.btn_Submit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btn_Submit, "btn_Submit");
            this.btn_Submit.Name = "btn_Submit";
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
            this.txt_UserName.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.txt_UserName.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode1")));
            this.txt_UserName.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location1")));
            this.txt_UserName.CustomButton.Name = "";
            this.txt_UserName.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size1")));
            this.txt_UserName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_UserName.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex1")));
            this.txt_UserName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_UserName.CustomButton.UseSelectable = true;
            this.txt_UserName.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible1")));
            this.txt_UserName.Lines = new string[0];
            resources.ApplyResources(this.txt_UserName, "txt_UserName");
            this.txt_UserName.MaxLength = 50;
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.PasswordChar = '\0';
            this.txt_UserName.PromptText = "Username";
            this.txt_UserName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_UserName.SelectedText = "";
            this.txt_UserName.SelectionLength = 0;
            this.txt_UserName.SelectionStart = 0;
            this.txt_UserName.ShortcutsEnabled = true;
            this.txt_UserName.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txt_UserName.UseSelectable = true;
            this.txt_UserName.WaterMark = "Username";
            this.txt_UserName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_UserName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel3
            // 
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Small;
            resources.ApplyResources(this.metroLabel3, "metroLabel3");
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // frmMain
            // 
            this.AcceptButton = this.btn_Submit;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.btn_Submit);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.txt_UserName);
            this.DisplayHeader = false;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Resizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox txt_Password;
        private MetroFramework.Controls.MetroButton btn_Submit;
        private MetroFramework.Controls.MetroTextBox txt_UserName;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}