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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PemesananTiket
{
    public partial class Penumpang : Form
    {
        public Penumpang()
        {
            InitializeComponent();
            tampildata();
        }

        SqlConnection conn = Properti.conn;
        int clickCell = -1;

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Penumpang", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();

            dt.Columns.Add("Tanggal lahir", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                DateTime original = (DateTime)row["tanggal_lahir"];
                string format = original.ToString("yyyy-MM-dd");

                row["Tanggal lahir"] = format;
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["tanggal_lahir"].Visible = false;
        }

        private void Penumpang_Load(object sender, EventArgs e)
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
                else if (radioButton1.Checked == false && radioButton2.Checked == false)
                {
                    MessageBox.Show("Pilih Jenis Kelamin terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } else if(!Properti.Number(textBox4.Text))
                {
                    MessageBox.Show("Nomor telepon hanya boleh diisi angka", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    var konfirmasi = MessageBox.Show("Apakah anda yakin ingin menambahkan data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (konfirmasi == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Penumpang VALUES(@username,@password,@nama_penumpang,@alamat_penumpang,@tanggal_lahir,@jenis_kelamin,@telepon)", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@username", textBox1.Text);
                        cmd.Parameters.AddWithValue("@password", Properti.enkripsi(textBox2.Text));
                        cmd.Parameters.AddWithValue("@nama_penumpang", textBox3.Text);
                        cmd.Parameters.AddWithValue("@alamat_penumpang", richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@tanggal_lahir", dateTimePicker1.Value);
                        if(radioButton1.Checked)
                        {
                            cmd.Parameters.AddWithValue("@jenis_kelamin", "LAKI-LAKI");
                        } else if(radioButton2.Checked)
                        {
                            cmd.Parameters.AddWithValue("@jenis_kelamin", "PEREMPUAN");
                        }
                        cmd.Parameters.AddWithValue("@telepon", textBox4.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        tampildata();
                        MessageBox.Show("Data berhasil ditambahkan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            richTextBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (radioButton1.Checked == false && radioButton2.Checked == false)
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
                    var konfirmasi = MessageBox.Show("Apakah anda yakin ingin mengubah data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (konfirmasi == DialogResult.Yes)
                    {
                        var row = dataGridView1.CurrentRow;
                        int id_penumpang = Convert.ToInt32(row.Cells["id_penumpang"].Value.ToString());
                        SqlCommand cmd = new SqlCommand("UPDATE Penumpang SET username = @username , password = @password, nama_penumpang = @nama_penumpang, alamat_penumpang = @alamat_penumpang,tanggal_lahir = @tanggal_lahir, jenis_kelamin = @jenis_kelamin, telepon = @telepon WHERE id_penumpang = @id", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", id_penumpang);
                        cmd.Parameters.AddWithValue("@username", textBox1.Text);
                        cmd.Parameters.AddWithValue("@password", Properti.enkripsi(textBox2.Text));
                        cmd.Parameters.AddWithValue("@nama_penumpang", textBox3.Text);
                        cmd.Parameters.AddWithValue("@alamat_penumpang", richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@tanggal_lahir", dateTimePicker1.Value);
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
                        tampildata();
                        MessageBox.Show("Data berhasil diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin menghapus data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {
                var row = dataGridView1.CurrentRow;
                int id_penumpang = Convert.ToInt32(row.Cells["id_penumpang"].Value.ToString());
                SqlCommand cmd = new SqlCommand("DELETE Penumpang WHERE id_penumpang = @id", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id_penumpang);
                cmd.ExecuteNonQuery();
                conn.Close();
                tampildata();
                MessageBox.Show("Data berhasil dihapus", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.CurrentRow;
            string jk = row.Cells["jenis_kelamin"].Value.ToString();
            if(jk == "LAKI-LAKI")
            {
                radioButton1.Checked = true;
            } else if(jk == "PEREMPUAN")
            {
                radioButton2.Checked = true;
            }
            clickCell = e.RowIndex;
            textBox1.Text = dataGridView1.CurrentRow.Cells["username"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["password"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["nama_penumpang"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["telepon"].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells["alamat_penumpang"].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["tanggal_lahir"].Value.ToString());
            
            
        }
    }
}
