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
    public partial class frmVerify : MetroFramework.Forms.MetroForm
    {
        public string cs = @"Data Source=soterias_vault.db;Version=3;New=True;Compress=True;";
        public static string currentUser = frmMain.UserInformation.CurrentLoggedInUser;
        public static long currentUserID = frmMain.UserInformation.CurrentLoggedInUserID;
        public static long currentUserRole = frmMain.UserInformation.CurrentLoggedInUserRole;
        public static string AppVersion = frmMain.UserInformation.AppVersion;

        public frmVerify()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (validateUser(metroTextBox1.Text))
            {
                this.DialogResult = DialogResult.OK;
                //this.Close();
            }
            else
            {
                MetroMessageBox.Show(this, "Please check your password", "Verification Failed!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private bool validateUser(string password)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(cs))
                {
                    con.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM sot_users WHERE sot_user_id=@user_id", con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", currentUserID);
                        using (SQLiteDataReader readdata = cmd.ExecuteReader())
                        {
                            string hash = null;

                            while (readdata.Read())
                            {
                                hash = readdata["sot_user_password"].ToString();
                            }

                            if (PasswordHash.ScryptHashStringVerify(hash, password))
                            {
                                return true;
                            }
                            return false;
                        }
                    }
                }
            }
            catch (Exception )
            {
                return false;
            }
        }
    }
}
