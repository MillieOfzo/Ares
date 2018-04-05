using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Sodium;
using MetroFramework;
using System.Diagnostics;

namespace soteriasVault
{
    public partial class frmCreate : MetroFramework.Forms.MetroForm   
    {
        public string cs = @"Data Source=soterias_vault.db;Version=3;New=True;Compress=True;";
        public static string currentUser = frmMain.UserInformation.CurrentLoggedInUser;
        public static long currentUserRole = frmMain.UserInformation.CurrentLoggedInUserRole;
        public static long currentUserID = frmMain.UserInformation.CurrentLoggedInUserID;
        public static string AppVersion = frmMain.UserInformation.AppVersion;

        public frmCreate()
        {
            InitializeComponent();

            metroLabel3.Text += AppVersion;

        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (txt_new_UserName.Text == "" || txt_new_Password.Text == "")
            {
                MetroMessageBox.Show(this, "Please provide UserName and Password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(cs))
                {
                    con.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO sot_users (sot_user_name,sot_user_password,sot_user_is_admin) VALUES (@username,@password,@is_admin)", con))
                    {

                        //this will produce a 512 byte hash
                        var hash = PasswordHash.ScryptHashString(txt_new_Password.Text, PasswordHash.Strength.Medium);

                        cmd.Parameters.AddWithValue("@username", txt_new_UserName.Text.ToLower());
                        cmd.Parameters.AddWithValue("@password", hash);
                        // By default create admin user with no admin rights
                        cmd.Parameters.AddWithValue("@is_admin", 0);
            
                        int i = cmd.ExecuteNonQuery();

                        if (i != 0)
                        {
                            if (MetroMessageBox.Show(this, "User created", "Success", MessageBoxButtons.OK, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                            {
                                //this.DialogResult = DialogResult.OK;
                                this.Hide();
                                frmMain main = new frmMain();
                                main.Show();
                                //Application.Restart();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmCreate_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
