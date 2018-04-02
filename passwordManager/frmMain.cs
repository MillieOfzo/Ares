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

namespace passwordManager
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

        }

        internal class UserInformation
        {
            public static string CurrentLoggedInUser
            {
                get;
                set;
            }
            public static string CurrentLoggedInUserRole
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
                
                //Create SqlConnection
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("Select * from tbl_Login where UserName=@username", con);
                cmd.Parameters.AddWithValue("@username", txt_UserName.Text);
                //cmd.Parameters.AddWithValue("@password", txt_Password.Text);
                con.Open();
                //SqlDataAdapter adapt = new SqlDataAdapter(cmd);

                SqlDataReader readdata = cmd.ExecuteReader();
                string is_admin = null;
                string username = null;
                string hash = null;

                while (readdata.Read())
                {
                    is_admin = readdata["is_admin"].ToString();
                    username = readdata["UserName"].ToString();
                    hash = readdata["Password"].ToString();
                }


                //DataSet ds = new DataSet();
                //adapt.Fill(ds);
                //int count = ds.Tables[0].Rows.Count;
                //If count is equal to 1, than show frmMain form
                con.Close();
                if (PasswordHash.ScryptHashStringVerify(hash, txt_Password.Text))
                {
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}
