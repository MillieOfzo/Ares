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
using MetroFramework;

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
        public string AppVersion = frmMain.UserInformation.AppVersion;

        public frmHome()
        {
            InitializeComponent();
            metroLabel3.Text += currentUser;
            metroLabel6.Text += AppVersion;
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

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (txt_add_username.Text == "" || txt_add_password.Text == "")
            {
                MetroMessageBox.Show(this, "Please provide UserName and Password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MetroMessageBox.Show(this, "User Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Question);
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
                MetroMessageBox.Show(this, "Please provide a name and Password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                con.Open();
                int i = cmd.ExecuteNonQuery();

                con.Close();

                if (i != 0)
                {
                    MetroMessageBox.Show(this, "Password Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    RefreshGridPasswords();
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
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                            // Clear the ListView control
                            metroListView1.Items.Clear();

                            metroListView1.Scrollable = true;

                            metroListView1.View = View.Details;

                            metroListView1.Columns.Add("ID");
                            metroListView1.Columns.Add("Name");

                            while (reader.Read())
                            {
                                var item = new ListViewItem();
                                item.Text = reader["Id"].ToString();        // 1st column text
                                item.SubItems.Add(reader["list_name"].ToString());  // 2nd column text
                                metroListView1.Items.Add(item);
                            }
                        metroListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        metroListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    }
                    con.Close();
                }
            }
        }

        private void RefreshGridPasswords()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_list", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        // Clear the ListView control
                        metroListView1.Items.Clear();

                        // Display items in the ListView control
                        metroListView1.View = View.Details;

                        while (reader.Read())
                        {
                            var item = new ListViewItem();
                            item.Text = reader["Id"].ToString();        // 1st column text
                            item.SubItems.Add(reader["list_name"].ToString());  // 2nd column text
                            metroListView1.Items.Add(item);
                        }
                        metroListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        metroListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    }
                    con.Close();
                }
            }
        }

        private void metroListView1_DoubleClick(object sender, EventArgs e)
        {
            //string show = metroListView1.Items[0].SubItems[0].Text;
            string row_id = metroListView1.SelectedItems[0].SubItems[0].Text;

            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_list WHERE Id = @row_id", con);
            cmd.Parameters.AddWithValue("@row_id", row_id);

            con.Open();
            SqlDataReader readdata = cmd.ExecuteReader();

            while (readdata.Read())
            {
                string text_name = readdata["list_name"].ToString();
                byte[] text_password = (byte[])readdata["list_password"];
                byte[] text_nonce = (byte[])readdata["list_nonce"];
                byte[] text_key = (byte[])readdata["list_key"];
                byte[] message2 = SecretBox.Open(text_password, text_nonce, text_key);

                string msg = Encoding.UTF8.GetString(message2);

                if (MetroMessageBox.Show(this, "Password: \n" + msg+ "\n\n (Click Yes to copy)", text_name, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    Clipboard.SetText(msg);
                }
            }

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            RefreshGridPasswords();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void frmHome_Resize(object sender, EventArgs e)
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
 