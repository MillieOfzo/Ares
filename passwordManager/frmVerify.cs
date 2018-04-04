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
    public partial class frmVerify : MetroFramework.Forms.MetroForm
    {
        public static string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;";
        public static string currentUser = frmMain.UserInformation.CurrentLoggedInUser;
        public static int currentUserID = frmMain.UserInformation.CurrentLoggedInUserID;
        public static int currentUserRole = frmMain.UserInformation.CurrentLoggedInUserRole;
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
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("Select * from tbl_login where Id=@user_id", con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", currentUserID);
                        using (SqlDataReader readdata = cmd.ExecuteReader())
                        {
                            string hash = null;

                            while (readdata.Read())
                            {
                                hash = readdata["Password"].ToString();
                            }

                            con.Close();
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
