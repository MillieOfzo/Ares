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
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;

namespace soteriasVault
{
    public partial class frmHome : MetroFramework.Forms.MetroForm
    {
        //Connection String
        public string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;";
        private DataGridView dataGridView1 = new DataGridView();
        private BindingSource bindingSource1 = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();

        public static string currentUser = frmMain.UserInformation.CurrentLoggedInUser;
        public static int currentUserRole = frmMain.UserInformation.CurrentLoggedInUserRole;
        public static int currentUserID = frmMain.UserInformation.CurrentLoggedInUserID;
        public static string AppVersion = frmMain.UserInformation.AppVersion;

        private static string user_vault = currentUser+"_vault.dat";

        public frmHome()
        {
            InitializeComponent();
            metroLabel3.Text += currentUser;
            metroLabel6.Text += AppVersion;
            if (currentUserRole != 1)
            {
                metroTabControl1.TabPages.Remove(metroTabPage1);
            }
            BindGridUsers();
            BindGridPasswords();
            checkVault();
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
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO tbl_login (UserName,Password,is_admin) VALUES (@username,@password,@is_admin)", con))
                    {

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

                        cmd.Parameters.AddWithValue("@username", txt_add_username.Text.ToLower());
                        cmd.Parameters.AddWithValue("@password", hash);
                        cmd.Parameters.AddWithValue("@is_admin", is_admin);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();

                        if (i != 0)
                        {
                            MetroMessageBox.Show(this, "User Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            BindGridUsers();
                        }
                    }
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
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_login", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        // Clear the ListView control
                        metroListView2.Items.Clear();

                        metroListView2.Scrollable = true;

                        metroListView2.View = View.Details;

                        metroListView2.Columns.Add("ID");
                        metroListView2.Columns.Add("Name");
                        metroListView2.Columns.Add("Admin");
                        while (reader.Read())
                        {
                            var item = new ListViewItem();
                            item.Text = reader["Id"].ToString();
                            item.SubItems.Add(reader["UserName"].ToString());
                            item.SubItems.Add(reader["is_admin"].ToString());
                            metroListView2.Items.Add(item);
                        }
                        //metroListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        //metroListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    }
                }
            }
        }

        private void RefreshGridUsers()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_login", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        // Clear the ListView control
                        metroListView2.Items.Clear();

                        metroListView2.Scrollable = true;

                        metroListView2.View = View.Details;

                        while (reader.Read())
                        {
                            var item = new ListViewItem();
                            item.Text = reader["Id"].ToString();
                            item.SubItems.Add(reader["UserName"].ToString());
                            item.SubItems.Add(reader["is_admin"].ToString());
                            metroListView2.Items.Add(item);
                        }
                        //metroListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        //metroListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    }
                }
            }
        }

        private bool checkVault()
        {
            //Console.WriteLine(File.Exists(user_vault) ? "File exists." : "File does not exist.");
            // If vault file exists show delete and add buttons
            if (File.Exists(user_vault))
            {
                metroButton4.Hide();
                metroButton3.Show();
                metroButton2.Show();
                metroButton5.Show();
                metroButton6.Show();
                return true;
            }
            else
            {
                metroButton4.Show();
                metroButton3.Hide();
                metroButton2.Hide();
                metroButton5.Hide();
                metroButton6.Hide();
                return false;
            }
            
        } 

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (txt_name.Text == "" || txt_password.Text == "")
            {
                MetroMessageBox.Show(this, "Please provide a name and password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                frmVerify newForm = new frmVerify();
                newForm.ShowDialog();
                if (newForm.DialogResult == DialogResult.OK)
                {
                    byte[] decryptData = null;
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        con.Open();
                        // Get user secretkey
                        decryptData = decryptDataKey(cs, currentUserID);

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO tbl_list (list_name,list_password,list_nonce,user_id) VALUES (@list_name,@list_password,@list_nonce,@user_id)", con))
                        {
                            cmd.CommandType = CommandType.Text;

                                var list_nonce = SecretBox.GenerateNonce(); //24 byte nonce
                                var message = txt_password.Text;

                                // Encrypt password
                                var ciphertext = SecretBox.Create(message, list_nonce, decryptData);

                                cmd.Parameters.AddWithValue("@list_name", txt_name.Text);
                                cmd.Parameters.AddWithValue("@list_password", ciphertext);
                                cmd.Parameters.AddWithValue("@list_nonce", list_nonce);
                                cmd.Parameters.AddWithValue("@user_id", currentUserID);

                                int i = cmd.ExecuteNonQuery();

                                con.Close();

                                if (i != 0)
                                {
                                    MetroMessageBox.Show(this, "Password Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                    RefreshGridPasswords();
                                }

                            con.Close();
                        }

                    }
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
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_list WHERE user_id = @user_id", con))
                {
                    cmd.Parameters.AddWithValue("@user_id", currentUserID);
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
                            item.Text = reader["Id"].ToString();
                            item.SubItems.Add(reader["list_name"].ToString());
                            metroListView1.Items.Add(item);
                        }
                        //metroListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        //metroListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    }
                }
            }
        }

        private void RefreshGridPasswords()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_list WHERE user_id = @user_id", con))
                {
                    cmd.Parameters.AddWithValue("@user_id", currentUserID);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        // Clear the ListView control
                        metroListView1.Items.Clear();

                        metroListView1.Scrollable = true;

                        metroListView1.View = View.Details;

                        while (reader.Read())
                        {
                            var item = new ListViewItem();
                            item.Text = reader["Id"].ToString();
                            item.SubItems.Add(reader["list_name"].ToString());
                            metroListView1.Items.Add(item);
                        }
                        //metroListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        //metroListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    }
                }
            }
        }

        private void metroListView1_DoubleClick(object sender, EventArgs e)
        {
            //string show = metroListView1.Items[0].SubItems[0].Text;
            string row_id = metroListView1.SelectedItems[0].SubItems[0].Text;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_list WHERE Id = @row_id", con))
                {
                    cmd.Parameters.AddWithValue("@row_id", row_id);
                    using (SqlDataReader readdata = cmd.ExecuteReader())
                    {
                        // Get user secretkey
                        byte[] secret_key = decryptDataKey(cs, currentUserID);

                        while (readdata.Read())
                        {
                            string text_name = readdata["list_name"].ToString();
                            byte[] text_password = (byte[])readdata["list_password"];
                            byte[] text_nonce = (byte[])readdata["list_nonce"];
                            byte[] message2 = SecretBox.Open(text_password, text_nonce, secret_key);

                            string msg = Encoding.UTF8.GetString(message2);

                            if (MetroMessageBox.Show(this, "Password: \n" + msg + "\n\n (Click Yes to copy)", text_name, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                            {
                                Clipboard.SetText(msg);
                            }
                        }
                    }
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

        private void metroButton4_Click(object sender, EventArgs e)
        {
            ///////////////////////////////
            //
            // Secret key storage in vault file
            //
            ///////////////////////////////
            var list_key = SecretBox.GenerateKey(); //32 byte key

            // Create the original data to be encrypted
            byte[] toEncrypt = list_key;

            // Create a file.
            FileStream fStream = new FileStream(user_vault, FileMode.Create);

            // Create some random entropy.
            byte[] entropy = CreateRandomEntropy();

            Console.WriteLine();
            //Console.WriteLine("Original data: " + Encoding.UTF8.GetString(toEncrypt));
            Console.WriteLine("Encrypting and writing to vault...");

            // Encrypt a copy of the data to the stream.
            int bytesWritten = EncryptDataToStream(toEncrypt, entropy, DataProtectionScope.CurrentUser, fStream);
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE tbl_login SET user_entropy = @user_entropy, user_bytes =@user_bytes WHERE Id=@user_id ", con))
                {
                    cmd.Parameters.AddWithValue("@user_entropy", entropy);
                    cmd.Parameters.AddWithValue("@user_bytes", bytesWritten);
                    cmd.Parameters.AddWithValue("@user_id", currentUserID);

                    int i = cmd.ExecuteNonQuery();

                    if (i != 0)
                    {
                        fStream.Close();
                        File.Encrypt(user_vault);
                        checkVault();
                    }
                }
            }
        }

        public static byte[] decryptDataKey(string sql_conn, int user_id)
        {
            using (SqlConnection con = new SqlConnection(sql_conn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from tbl_login where Id=@user_id", con))
                {
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader readdata = cmd.ExecuteReader())
                    {
                        //string password = newForm.metroTextBox1.Text;
                        Console.WriteLine("Reading data from disk and decrypting...");
                        // Open the file.
                        File.Decrypt(user_vault);
                        using (FileStream fStream = new FileStream(user_vault, FileMode.Open))
                        {

                            byte[] user_entropy = null;
                            int user_bytes = 0;

                            while (readdata.Read())
                            {
                                user_entropy = (byte[])readdata["user_entropy"];
                                user_bytes = Convert.ToInt32(readdata["user_bytes"]);
                            }

                            // Read from the stream and decrypt the secret key.
                            byte[] decryptData = DecryptDataFromStream(user_entropy, DataProtectionScope.CurrentUser, fStream, user_bytes);

                            fStream.Close();
                            //Console.WriteLine("Decrypted data: " + Encoding.UTF8.GetString(decryptData));
                            con.Close();
                            return decryptData;
                        }
                    }                                 
                }
            }
        }

        public static byte[] CreateRandomEntropy()
        {
            // Create a byte array to hold the random value.
            byte[] entropy = new byte[16];

            // Create a new instance of the RNGCryptoServiceProvider.
            // Fill the array with a random value.
            new RNGCryptoServiceProvider().GetBytes(entropy);

            // Return the array.
            return entropy;


        }

        public static int EncryptDataToStream(byte[] Buffer, byte[] Entropy, DataProtectionScope Scope, Stream S)
        {
            if (Buffer == null)
                throw new ArgumentNullException("Buffer");
            if (Buffer.Length <= 0)
                throw new ArgumentException("Buffer");
            if (Entropy == null)
                throw new ArgumentNullException("Entropy");
            if (Entropy.Length <= 0)
                throw new ArgumentException("Entropy");
            if (S == null)
                throw new ArgumentNullException("S");

            int length = 0;

            // Encrypt the data in memory. The result is stored in the same same array as the original data.
            byte[] encryptedData = ProtectedData.Protect(Buffer, Entropy, Scope);

            // Write the encrypted data to a stream.
            if (S.CanWrite && encryptedData != null)
            {
                S.Write(encryptedData, 0, encryptedData.Length);

                length = encryptedData.Length;
            }

            // Return the length that was written to the stream. 
            return length;

        }

        public static byte[] DecryptDataFromStream(byte[] Entropy, DataProtectionScope Scope, Stream S, int Length)
        {
            if (S == null)
                throw new ArgumentNullException("S");
            if (Length <= 0)
                throw new ArgumentException("Length");
            if (Entropy == null)
                throw new ArgumentNullException("Entropy");
            if (Entropy.Length <= 0)
                throw new ArgumentException("Entropy");

            byte[] inBuffer = new byte[Length];
            byte[] outBuffer;

            // Read the encrypted data from a stream.
            if (S.CanRead)
            {
                S.Read(inBuffer, 0, Length);

                outBuffer = ProtectedData.Unprotect(inBuffer, Entropy, Scope);
            }
            else
            {
                throw new IOException("Could not read the stream.");
            }

            // Return the length that was written to the stream. 
            return outBuffer;

        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            frmVerify newForm = new frmVerify();
            newForm.ShowDialog();
            if (newForm.DialogResult == DialogResult.OK)
            {
                if (File.Exists(user_vault))
                {
                    if (MetroMessageBox.Show(this, "Are you sure you want to delete your vault?\n Everything in your vault wil be lost?", "Are you sure!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            using (SqlConnection con = new SqlConnection(cs))
                            {
                                con.Open();
                                using (SqlCommand cmd = new SqlCommand("DELETE FROM tbl_list WHERE user_id =@user_id", con))
                                {
                                    cmd.Parameters.AddWithValue("@user_id", currentUserID);

                                    int i = cmd.ExecuteNonQuery();

                                    if (i != 0)
                                    {

                                    }
                                }
                            }
                            // close this one
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            File.Delete(user_vault);
                            if (!File.Exists(user_vault))
                            {
                                if (MetroMessageBox.Show(this, "Restart now?", "File deleted", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    //Process.GetCurrentProcess().Kill();

                                    Application.Restart();

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
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            if (metroListView1.SelectedItems.Count > 0)
            {
                string row_id = metroListView1.SelectedItems[0].SubItems[0].Text;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM tbl_list WHERE Id=@row_id ", con))
                    {
                        frmVerify newForm = new frmVerify();
                        newForm.ShowDialog();
                        if (newForm.DialogResult == DialogResult.OK)
                        {
                            if (MetroMessageBox.Show(this, "Are you sure you want to delete this password", "Are you sure!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                            {
                                cmd.Parameters.AddWithValue("@row_id", row_id);

                                int i = cmd.ExecuteNonQuery();

                                if (i != 0)
                                {
                                    RefreshGridPasswords();
                                }
                            }
                        }
                    }
                }
            }

        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            if (metroListView2.SelectedItems.Count > 0 )
            {
                string row_id = metroListView2.SelectedItems[0].SubItems[0].Text;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM tbl_login WHERE Id=@row_id ", con))
                    {
                        frmVerify newForm = new frmVerify();
                        newForm.ShowDialog();
                        if (newForm.DialogResult == DialogResult.OK)
                        {
                            if (MetroMessageBox.Show(this, "Are you sure you want to delete this user", "Are you sure!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                            {
                                cmd.Parameters.AddWithValue("@row_id", row_id);

                                int i = cmd.ExecuteNonQuery();

                                if (i != 0)
                                {
                                    RefreshGridUsers();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void frmHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
 