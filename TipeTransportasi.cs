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
    public partial class TipeTransportasi : Form
    {
        public TipeTransportasi()
        {
            InitializeComponent();
            tampildata();
        }

        private void tampildata()
        {

            SqlCommand cmd = new SqlCommand("SELECT * FROM Tipe_Transportasi", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        SqlConnection conn = Properti.conn;
        int clickcell = -1;

        private void TipeTransportasi_Load(object sender, EventArgs e)
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
                    var konfirmasi = MessageBox.Show("Apakah anda yakin ingin menambahkan data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (konfirmasi == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Tipe_Transportasi VALUES(@nama_tipe,@keterangan)", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@nama_tipe", textBox1.Text);
                        cmd.Parameters.AddWithValue("@keterangan", richTextBox1.Text);
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
            richTextBox1.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clickcell = e.RowIndex;
            textBox1.Text = dataGridView1.CurrentRow.Cells["nama_tipe"].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells["keterangan"].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (clickcell == -1)
                {
                    MessageBox.Show("Pilih baris yang ingin diubah", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    var konfirmasi = MessageBox.Show("Apakah anda yakin ingin merubah data ini", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (konfirmasi == DialogResult.Yes)
                    {
                        var row = dataGridView1.CurrentRow;
                        int id_tipe = Convert.ToInt32(row.Cells["id_tipe_transportasi"].Value.ToString());
                        SqlCommand cmd = new SqlCommand("UPDATE Tipe_Transportasi SET nama_tipe = @nama_tipe, keterangan = @keterangan WHERE id_tipe_transportasi = @id", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", id_tipe);
                        cmd.Parameters.AddWithValue("@nama_tipe", textBox1.Text);
                        cmd.Parameters.AddWithValue("@keterangan", richTextBox1.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        tampildata();
                        MessageBox.Show("Data berhasil dirubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (clickcell == -1)
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
                    int id_tipe = Convert.ToInt32(row.Cells["id_tipe_transportasi"].Value.ToString());
                    SqlCommand cmd = new SqlCommand("DELETE FROM Tipe_transportasi WHERE id_tipe_transportasi = @id", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", id_tipe);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    tampildata();
                    MessageBox.Show("Data berhasil dihapus", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();

                }
            }
        }
    }
}
