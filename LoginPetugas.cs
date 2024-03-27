using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PemesananTiket
{
    public partial class LoginPetugas : Form
    {
        public LoginPetugas()
        {
            InitializeComponent();
        }

        SqlConnection conn = Properti.conn;

        private void LoginPetugas_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    SqlCommand user = new SqlCommand("SELECT COUNT (*) FROM Petugas WHERE username = @username", conn);
                    user.CommandType = CommandType.Text;
                    conn.Open();
                    user.Parameters.AddWithValue("@username", textBox1.Text);

                    int cariuser = (int)user.ExecuteScalar();
                    conn.Close();
                    if (cariuser == 0)
                    {
                        MessageBox.Show("Petugas tidak ditemukan", "Waning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    SqlCommand cmd = new SqlCommand("SELECT * FROM Petugas WHERE username = @username AND password = @password", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    cmd.Parameters.AddWithValue("@password", Properti.enkripsi(textBox2.Text));
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        int level = Convert.ToInt32(dr["id_level"]);
                        MainForm m = new MainForm(level);
                        m.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login gagal harap periksa kembali username dan password anda", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    conn.Close();



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            } else
            {
                textBox2.PasswordChar = '*';
            }
        }
    }
}
