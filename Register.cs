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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        SqlConnection conn = Properti.conn;

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }


        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            richTextBox1.Text = "";
            dateTimePicker2.Value = DateTime.Now;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(groupBox1.Controls))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (radioButton1.Checked == false && radioButton2.Checked == false)
                {
                    MessageBox.Show("Pilih Jenis Kelamin terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (!Properti.Number(textBox4.Text))
                {
                    MessageBox.Show("Nomor telepon hanya boleh diisi angka", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    var konfirmasi = MessageBox.Show("Apakah anda yakin ingin registrasi ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (konfirmasi == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Penumpang VALUES(@username,@password,@nama_penumpang,@alamat_penumpang,@tanggal_lahir,@jenis_kelamin,@telepon)", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@username", textBox1.Text);
                        cmd.Parameters.AddWithValue("@password", Properti.enkripsi(textBox2.Text));
                        cmd.Parameters.AddWithValue("@nama_penumpang", textBox3.Text);
                        cmd.Parameters.AddWithValue("@alamat_penumpang", richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@tanggal_lahir", dateTimePicker2.Value);
                        if (radioButton1.Checked)
                        {
                            cmd.Parameters.AddWithValue("@jenis_kelamin", "LAKI-LAKI");
                        }
                        else if (radioButton2.Checked)
                        {
                            cmd.Parameters.AddWithValue("@jenis_kelamin", "PEREMPUAN");
                        }
                        cmd.Parameters.AddWithValue("@telepon", textBox4.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Kamu Berhasil Registrasi", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
