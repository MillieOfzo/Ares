using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Sodium;

namespace passwordManager
{
    public partial class frmHome : MetroFramework.Forms.MetroForm
    {
        //Connection String
        public string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Roelof\Documents\C#\PasswordManager\passwordManager\passwordManager\Database1.mdf;Integrated Security=True;";
        private DataGridView dataGridView1 = new DataGridView();
        private BindingSource bindingSource1 = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();

        public string currentUser = frmMain.UserInformation.CurrentLoggedInUser;
        public string currentUserRole = frmMain.UserInformation.CurrentLoggedInUserRole;

        public frmHome()
        {
            InitializeComponent();
            metroLabel3.Text += currentUser;
            if (currentUserRole != "1")
            {
                metroTabControl1.TabPages.Remove(metroTabPage1);
            }
            BindGridUsers();
            BindGridPasswords();
        }

        private void btn_LogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain fl = new frmMain();
            fl.Show();
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (txt_add_username.Text == "" || txt_add_password.Text == "")
            {
                MessageBox.Show("Please provide UserName and Password");
                return;
            }
            try
            {
                //Create SqlConnection
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_login (UserName,Password,is_admin) VALUES (@username,@password,@is_admin)", con);
                //SqlDataReader myReader;

                //this will produce a 512 byte hash
                var hash = PasswordHash.ScryptHashString(txt_add_password.Text, PasswordHash.Strength.Medium);
                int is_admin;
                if (metroCheckBox1.Checked)
                {
                     is_admin = 1;
                }
                else
                {
                     is_admin = 0;
                }

                cmd.Parameters.AddWithValue("@username", txt_add_username.Text);
                cmd.Parameters.AddWithValue("@password", hash);
                cmd.Parameters.AddWithValue("@is_admin", is_admin);
                con.Open();
                int i = cmd.ExecuteNonQuery();

                con.Close();

                if (i != 0)
                {
                    MessageBox.Show("Data Saved");
                    BindGridUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGridUsers()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_login", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            metroGrid1.DataSource = dt;
                        }
                    }
                }
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (txt_name.Text == "" || txt_password.Text == "")
            {
                MessageBox.Show("Please provide Name and a password");
                return;
            }
            try
            {
                //Create SqlConnection
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_list (list_name,list_password,list_key,list_nonce) VALUES (@list_name,@list_password,@list_key,@list_nonce)", con);

                var list_nonce = SecretBox.GenerateNonce(); //24 byte nonce
                var list_key = SecretBox.GenerateKey(); //32 byte key
                var message = txt_password.Text;

                //encrypt the message
                var ciphertext = SecretBox.Create(message, list_nonce, list_key);

                cmd.Parameters.AddWithValue("@list_name", txt_name.Text);
                cmd.Parameters.AddWithValue("@list_password", ciphertext);
                cmd.Parameters.AddWithValue("@list_key", list_key);
                cmd.Parameters.AddWithValue("@list_nonce", list_nonce);

                Console.WriteLine();
                Console.WriteLine("Original data: " + message);
                Console.WriteLine("Encrypting and writing to disk...");

                con.Open();
                int i = cmd.ExecuteNonQuery();

                con.Close();

                if (i != 0)
                {
                    MessageBox.Show("Password Saved");
                    BindGridPasswords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BindGridPasswords()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_list", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            //Set AutoGenerateColumns False
                            metroGrid2.AutoGenerateColumns = false;

                            //Set Columns Count
                            metroGrid2.ColumnCount = 2;

                            //Add Columns
                            metroGrid2.Columns[0].Name = "Id";
                            metroGrid2.Columns[0].HeaderText = "Row ID";
                            metroGrid2.Columns[0].DataPropertyName = "Id";

                            metroGrid2.Columns[1].Name = "CustomerId";
                            metroGrid2.Columns[1].HeaderText = "Name";
                            metroGrid2.Columns[1].DataPropertyName = "list_name";

                            metroGrid2.DataSource = dt;
                        }
                    }
                }
            }
        }

        private void metroGrid2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string id_text = metroGrid2.Rows[e.RowIndex].Cells["Id"].Value.ToString();

            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_list WHERE Id = @row_id", con);
            cmd.Parameters.AddWithValue("@row_id", id_text);

            con.Open();
            SqlDataReader readdata = cmd.ExecuteReader();

            while (readdata.Read())
            {
                string text_name = readdata["list_name"].ToString();
                byte[] text_password = (byte[])readdata["list_password"];
                byte[] text_nonce = (byte[])readdata["list_nonce"];
                byte[] text_ke = (byte[])readdata["list_key"];
                byte[] message2 = SecretBox.Open(text_password, text_nonce, text_key);

                string msg = Encoding.UTF8.GetString(message2);
                MessageBox.Show("Password for: "+ text_name + " is: "+ msg);
            }
            
            
            
        }
    }
}
