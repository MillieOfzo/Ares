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
using System.Data.SqlClient;
using Sodium;
using MetroFramework;
using System.Diagnostics;

namespace soteriasVault
{
    public partial class frmMain : MetroFramework.Forms.MetroForm   
    {
        //Connection String
        public string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;";

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
            public static int CurrentLoggedInUserID
            {
                get;
                set;
            }
            public static string CurrentLoggedInUser
            {
                get;
                set;
            }
            public static int CurrentLoggedInUserRole
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
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("Select * from tbl_login where UserName=@username", con))
                    {
                        //int rowCount = (int)cmd.ExecuteScalar();
                        //if (rowCount == 0)
                        //{
                        //    MetroMessageBox.Show(this, "No user found", "Login Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    return;
                        //}

                        cmd.Parameters.AddWithValue("@username", txt_UserName.Text);
                        int is_admin = 0;
                        string username = null;
                        int username_id = 0;
                        string hash = null;

                        using (SqlDataReader readdata = cmd.ExecuteReader())
                        {
                            while (readdata.Read())
                            {
                                is_admin = (int)readdata["is_admin"];
                                username = readdata["UserName"].ToString();
                                username_id = (int)readdata["Id"];
                                hash = readdata["Password"].ToString();
                            }
                        }

                        if (PasswordHash.ScryptHashStringVerify(hash, txt_Password.Text))
                        {
                            UserInformation.CurrentLoggedInUserID = username_id;
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
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
                Console.WriteLine("Something went wrong!");
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
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_login", con))
                {
                    // Check if there are users in database
                    int rowCount = (int)cmd.ExecuteScalar();
                    // If there are no users show create user form
                    if (rowCount == 0)
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

        private void txt_Password_Click(object sender, EventArgs e)
        {

        }

        private void txt_UserName_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
