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
    public partial class Petugas : Form
    {
        public Petugas()
        {
            InitializeComponent();
            tampildata();

            SqlCommand cmd = new SqlCommand("SELECT id_level,nama_level FROM Level",conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "nama_level";
            comboBox1.ValueMember = "id_level";

            comboBox1.SelectedIndex = -1;
            conn.Close();

        }

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Petugas", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        SqlConnection conn = Properti.conn;
        int clickCell = -1;

        private void Petugas_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } else if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Pilih Level terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } else 
                {
                    var konfirmasi = MessageBox.Show("Apakah anda yakin ingin menambahkan data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(konfirmasi ==DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Petugas VALUES(@username,@password,@nama_petugas,@id_level)", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@username", textBox1.Text);
                        cmd.Parameters.AddWithValue("@password", Properti.enkripsi(textBox2.Text));
                        cmd.Parameters.AddWithValue("@nama_petugas", textBox3.Text);
                        cmd.Parameters.AddWithValue("@id_level", comboBox1.SelectedValue);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        tampildata();
                        MessageBox.Show("Data berhasil ditambahkan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if(clickCell == -1)
                {
                    MessageBox.Show("Pilih baris yang ingin diubah", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } else if(comboBox1.SelectedIndex == 1)
                {
                    MessageBox.Show("Pilih Level terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    var konfirmasi = MessageBox.Show("Apakah anda yakin ingin mengubah data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (konfirmasi == DialogResult.Yes)
                    {
                        var row = dataGridView1.CurrentRow;
                        int id_petugas = Convert.ToInt32(row.Cells["id_petugas"].Value.ToString());
                        SqlCommand cmd = new SqlCommand("UPDATE Petugas SET username = @username , password = @password, nama_petugas = @nama_petugas, id_level = @id_level WHERE id_petugas = @id", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", id_petugas);
                        cmd.Parameters.AddWithValue("@username", textBox1.Text);
                        cmd.Parameters.AddWithValue("@password", Properti.enkripsi(textBox2.Text));
                        cmd.Parameters.AddWithValue("@nama_petugas", textBox3.Text);
                        cmd.Parameters.AddWithValue("@id_level", comboBox1.SelectedValue);
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
            try
            {

                if (clickCell == -1)
                {
                    MessageBox.Show("Pilih baris yang ingin diubah", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    var konfirmasi = MessageBox.Show("Apakah anda yakin ingin menghapus data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (konfirmasi == DialogResult.Yes)
                    {
                        var row = dataGridView1.CurrentRow;
                        int id_petugas = Convert.ToInt32(row.Cells["id_petugas"].Value.ToString());
                        SqlCommand cmd = new SqlCommand("DELETE Petugas WHERE id_petugas = @id", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", id_petugas);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        tampildata();
                        MessageBox.Show("Data berhasil dihapus", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clickCell = e.RowIndex;
            textBox1.Text = dataGridView1.CurrentRow.Cells["username"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["password"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["nama_petugas"].Value.ToString();
            comboBox1.SelectedValue = dataGridView1.CurrentRow.Cells["id_level"].Value.ToString();
        }
    }
}
