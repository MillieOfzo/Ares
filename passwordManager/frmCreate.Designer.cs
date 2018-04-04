namespace soteriasVault
{
    partial class frmCreate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreate));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.btn_Submit = new MetroFramework.Controls.MetroButton();
            this.txt_new_Password = new MetroFramework.Controls.MetroTextBox();
            this.txt_new_UserName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(59, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(6, 314);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(36, 19);
            this.metroLabel3.TabIndex = 10;
            this.metroLabel3.Text = "Ver: ";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // btn_Submit
            // 
            this.btn_Submit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Submit.Location = new System.Drawing.Point(23, 265);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(223, 31);
            this.btn_Submit.TabIndex = 9;
            this.btn_Submit.Text = "Create admin";
            this.btn_Submit.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_Submit.UseSelectable = true;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // txt_new_Password
            // 
            // 
            // 
            // 
            this.txt_new_Password.CustomButton.Image = null;
            this.txt_new_Password.CustomButton.Location = new System.Drawing.Point(201, 1);
            this.txt_new_Password.CustomButton.Name = "";
            this.txt_new_Password.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_new_Password.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_new_Password.CustomButton.TabIndex = 1;
            this.txt_new_Password.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_new_Password.CustomButton.UseSelectable = true;
            this.txt_new_Password.CustomButton.Visible = false;
            this.txt_new_Password.Lines = new string[0];
            this.txt_new_Password.Location = new System.Drawing.Point(23, 236);
            this.txt_new_Password.MaxLength = 32767;
            this.txt_new_Password.Name = "txt_new_Password";
            this.txt_new_Password.PasswordChar = '*';
            this.txt_new_Password.PromptText = "Password";
            this.txt_new_Password.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_new_Password.SelectedText = "";
            this.txt_new_Password.SelectionLength = 0;
            this.txt_new_Password.SelectionStart = 0;
            this.txt_new_Password.ShortcutsEnabled = true;
            this.txt_new_Password.Size = new System.Drawing.Size(223, 23);
            this.txt_new_Password.TabIndex = 8;
            this.txt_new_Password.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txt_new_Password.UseSelectable = true;
            this.txt_new_Password.WaterMark = "Password";
            this.txt_new_Password.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_new_Password.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txt_new_UserName
            // 
            this.txt_new_UserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            // 
            // 
            // 
            this.txt_new_UserName.CustomButton.Image = null;
            this.txt_new_UserName.CustomButton.Location = new System.Drawing.Point(201, 1);
            this.txt_new_UserName.CustomButton.Name = "";
            this.txt_new_UserName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_new_UserName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_new_UserName.CustomButton.TabIndex = 1;
            this.txt_new_UserName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_new_UserName.CustomButton.UseSelectable = true;
            this.txt_new_UserName.CustomButton.Visible = false;
            this.txt_new_UserName.Lines = new string[0];
            this.txt_new_UserName.Location = new System.Drawing.Point(23, 207);
            this.txt_new_UserName.MaxLength = 32767;
            this.txt_new_UserName.Name = "txt_new_UserName";
            this.txt_new_UserName.PasswordChar = '\0';
            this.txt_new_UserName.PromptText = "Username";
            this.txt_new_UserName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_new_UserName.SelectedText = "";
            this.txt_new_UserName.SelectionLength = 0;
            this.txt_new_UserName.SelectionStart = 0;
            this.txt_new_UserName.ShortcutsEnabled = true;
            this.txt_new_UserName.Size = new System.Drawing.Size(223, 23);
            this.txt_new_UserName.TabIndex = 7;
            this.txt_new_UserName.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txt_new_UserName.UseSelectable = true;
            this.txt_new_UserName.WaterMark = "Username";
            this.txt_new_UserName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_new_UserName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 175);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(221, 19);
            this.metroLabel1.TabIndex = 12;
            this.metroLabel1.Text = "No users present, create your admin";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // frmCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 341);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.btn_Submit);
            this.Controls.Add(this.txt_new_Password);
            this.Controls.Add(this.txt_new_UserName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCreate";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroButton btn_Submit;
        private MetroFramework.Controls.MetroTextBox txt_new_Password;
        private MetroFramework.Controls.MetroTextBox txt_new_UserName;
        private MetroFramework.Controls.MetroLabel metroLabel1;
    }
}