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
    public partial class Rute : Form
    {
        public Rute()
        {
            InitializeComponent();
            tampildata();
            SqlCommand cmd = new SqlCommand("SELECT id_transportasi FROM Transportasi", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "id_transportasi";
            comboBox1.ValueMember = "id_transportasi";
            comboBox1.SelectedIndex = -1;
            conn.Close();

        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = -1;

        }

        SqlConnection conn = Properti.conn;
        int clickCell = -1;

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Rute", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void Rute_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clickCell = e.RowIndex;
            textBox1.Text = dataGridView1.CurrentRow.Cells["tujuan"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["rute_awal"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["rute_akhir"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["harga"].Value.ToString();
            comboBox1.SelectedValue = dataGridView1.CurrentRow.Cells["id_transportasi"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } else if(!Properti.Number(textBox4.Text))
                {
                    MessageBox.Show("Inputan Harga harus angka", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Pilih  transportasi  terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } 
                else
                {
                    var konfirmasi = MessageBox.Show("Apakah anda yakin ingin menambahkan data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (konfirmasi == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Rute VALUES(@tujuan,@rute_awal,@rute_akhir,@harga,@id_transportasi)", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@tujuan", textBox1.Text);
                        cmd.Parameters.AddWithValue("@rute_awal", textBox2.Text);
                        cmd.Parameters.AddWithValue("@rute_akhir", textBox3.Text);
                        cmd.Parameters.AddWithValue("@harga", textBox4.Text);
                        cmd.Parameters.AddWithValue("@id_transportasi", comboBox1.SelectedValue);
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (clickCell == -1)
                {
                    MessageBox.Show("Pilih baris yang ingin diubah", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (!Properti.Number(textBox4.Text))
                {
                    MessageBox.Show("Inputan Harga harus angka", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                else if (comboBox1.SelectedIndex == 1)
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
                        int id_rute = Convert.ToInt32(row.Cells["id_rute"].Value.ToString());
                        SqlCommand cmd = new SqlCommand("UPDATE Rute SET tujuan= @tujuan, rute_awal = @rute_awal, rute_akhir = @rute_akhir, harga  = @harga, id_transportasi = @id_transportasi WHERE id_rute = @id", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", id_rute);
                        cmd.Parameters.AddWithValue("@tujuan", textBox1.Text);
                        cmd.Parameters.AddWithValue("@rute_awal", textBox2.Text);
                        cmd.Parameters.AddWithValue("@rute_akhir", textBox3.Text);
                        cmd.Parameters.AddWithValue("@harga", textBox4.Text);
                        cmd.Parameters.AddWithValue("@id_transportasi", comboBox1.SelectedValue);
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
                    int id_rute = Convert.ToInt32(row.Cells["id_rute"].Value.ToString());
                    SqlCommand cmd = new SqlCommand("DELETE FROM Rute WHERE id_rute = @id", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", id_rute);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    tampildata();
                    MessageBox.Show("Data berhasil diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
            }
        }
    }
}
