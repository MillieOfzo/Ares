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
    public partial class frmCreate : MetroFramework.Forms.MetroForm   
    {
        public string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Roelof\Documents\C#\PasswordManager\passwordManager\passwordManager\Database1.mdf;Integrated Security=True;";
        public static string currentUser = frmMain.UserInformation.CurrentLoggedInUser;
        public static int currentUserRole = frmMain.UserInformation.CurrentLoggedInUserRole;
        public static int currentUserID = frmMain.UserInformation.CurrentLoggedInUserID;
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
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO tbl_login (UserName,Password,is_admin) VALUES (@username,@password,@is_admin)", con))
                    {

                        //this will produce a 512 byte hash
                        var hash = PasswordHash.ScryptHashString(txt_new_Password.Text, PasswordHash.Strength.Medium);

                        cmd.Parameters.AddWithValue("@username", txt_new_UserName.Text);
                        cmd.Parameters.AddWithValue("@password", hash);
                        cmd.Parameters.AddWithValue("@is_admin", 1);
            
                        int i = cmd.ExecuteNonQuery();

                        if (i != 0)
                        {
                            MetroMessageBox.Show(this, "User created", "Success", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
