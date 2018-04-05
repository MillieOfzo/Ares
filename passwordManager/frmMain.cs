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
using Sodium;
using MetroFramework;
using System.Diagnostics;
using System.Data.SQLite;

namespace soteriasVault
{
    public partial class frmMain : MetroFramework.Forms.MetroForm   
    {
        //Connection String
        //public string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;";
        public string cs = @"Data Source=soterias_vault.db;Version=3;New=True;Compress=True;";
        public frmMain()
        {

            InitializeComponent();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
  
            UserInformation.AppVersion = version;

            metroLabel3.Text += version;
            checkUsers();
        }

        internal class UserInformation
        {
            public static long CurrentLoggedInUserID
            {
                get;
                set;
            }
            public static string CurrentLoggedInUser
            {
                get;
                set;
            }
            public static long CurrentLoggedInUserRole
            {
                get;
                set;
            }
            public static string AppVersion
            {
                get;
                set;
            }
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (txt_UserName.Text == "" || txt_Password.Text == "")
            {
                MetroMessageBox.Show(this, "Please provide UserName and Password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(cs))
                {
                    con.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM sot_users WHERE sot_user_name=@username", con))
                    {
                        //int rowCount = (int)cmd.ExecuteScalar();
                        //if (rowCount == 0)
                        //{
                        //    MetroMessageBox.Show(this, "No user found", "Login Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    return;
                        //}

                        cmd.Parameters.AddWithValue("@username", txt_UserName.Text.ToLower());
                        long is_admin =0;
                        long user_id = 0;
                        string username = null;
                        string hash = null;

                        using (SQLiteDataReader readdata = cmd.ExecuteReader())
                        {
                            while (readdata.Read())
                            {
                                is_admin = (long)readdata["sot_user_is_admin"];
                                user_id = (long)readdata["sot_user_id"];
                                username = readdata["sot_user_name"].ToString();
                                hash = readdata["sot_user_password"].ToString();
                            }
                        }

                        if (PasswordHash.ScryptHashStringVerify(hash, txt_Password.Text))
                        {
                            UserInformation.CurrentLoggedInUserID = user_id;
                            UserInformation.CurrentLoggedInUser = username;
                            UserInformation.CurrentLoggedInUserRole = is_admin;
                            //MessageBox.Show("Login Successful!");
                            this.Hide();
                            frmHome fm = new frmHome();
                            fm.Show();
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "Please check your username and password", "Login Failed!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine("Something went wrong during login!");
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        private void checkUsers()
        {
            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("SELECT 1 FROM sot_users", con))
                {
                    // Check if there are users in database
                    object rowCount = cmd.ExecuteScalar();
                    // If there are no users show create user form
                    if (rowCount == null)
                    {
                        frmCreate createAdmin = new frmCreate();
                        createAdmin.ShowDialog();
                        if (createAdmin.DialogResult == DialogResult.OK)
                        {
                            // i dunno
                        }
                    }
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
