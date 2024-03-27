using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PemesananTiket
{
    public partial class Transportasi : Form
    {
        public Transportasi()
        {
            InitializeComponent();
            tampildata();
            SqlCommand cmd = new SqlCommand("SELECT id_tipe_transportasi, nama_tipe FROM Tipe_Transportasi", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "nama_tipe";
            comboBox1.ValueMember = "id_tipe_transportasi";
            comboBox1.SelectedIndex = -1;

            conn.Close();
            numericUpDown1.Value = 0;
        }

        SqlConnection conn = Properti.conn;

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Transportasi", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void Transportasi_Load(object sender, EventArgs e)
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
                else if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Pilih Tipe transportasi  terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } else if(numericUpDown1.Value == 0)
                {
                    MessageBox.Show("Tambah jumlah kursi terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    var konfirmasi = MessageBox.Show("Apakah anda yakin ingin menambahkan data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (konfirmasi == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Transportasi VALUES(@kode,@jumlah_kursi,@keterangan,@id_tipe_transportasi)", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@kode", textBox1.Text);
                        cmd.Parameters.AddWithValue("@jumlah_kursi", numericUpDown1.Value);
                        cmd.Parameters.AddWithValue("@keterangan", richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@id_tipe_transportasi", comboBox1.SelectedValue);
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
        int clickCell = -1;


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
                else if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Pilih Level terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (numericUpDown1.Value == 0)
                {
                    MessageBox.Show("Tambah jumlah kursi terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    var konfirmasi = MessageBox.Show("Apakah anda yakin ingin mengubah data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (konfirmasi == DialogResult.Yes)
                    {
                        var row = dataGridView1.CurrentRow;
                        int id_transportasi = Convert.ToInt32(row.Cells["id_transportasi"].Value.ToString());
                        SqlCommand cmd = new SqlCommand("UPDATE Transportasi SET kode = @kode, jumlah_kursi = @jumlah_kursi, keterangan = @keterangan, id_tipe_transportasi = @id_tipe_transportasi WHERE id_transportasi = @id", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", id_transportasi);
                        cmd.Parameters.AddWithValue("@kode", textBox1.Text);
                        cmd.Parameters.AddWithValue("@jumlah_kursi", numericUpDown1.Value);
                        cmd.Parameters.AddWithValue("@keterangan", richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@id_tipe_transportasi", comboBox1.SelectedValue);
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
                MessageBox.Show("Pilih baris yang ingin dihapus", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else
            {
                var konfirmasi = MessageBox.Show("Apakah anda yakin ingin mengubah data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (konfirmasi == DialogResult.Yes)
                {
                    var row = dataGridView1.CurrentRow;
                    int id_transportasi = Convert.ToInt32(row.Cells["id_transportasi"].Value.ToString());
                    SqlCommand cmd = new SqlCommand("DELETE FROM Transportasi WHERE id_transportasi = @id", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", id_transportasi);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    tampildata();
                    MessageBox.Show("Data berhasil dihapus", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
            }
        
    }

        private void clear()
        {
            textBox1.Text = "";
            numericUpDown1.Value = 0;
            richTextBox1.Text = "";
            comboBox1.SelectedIndex = -1;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clickCell = e.RowIndex;
            textBox1.Text = dataGridView1.CurrentRow.Cells["kode"].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells["keterangan"].Value.ToString();
            comboBox1.SelectedValue = dataGridView1.CurrentRow.Cells["id_tipe_transportasi"].Value.ToString();
            numericUpDown1.Value = Convert.ToInt32(dataGridView1.CurrentRow.Cells["jumlah_kursi"].Value.ToString());
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox2.Text;
            if(!string.IsNullOrEmpty(keyword)) {


                SqlCommand cmd = new SqlCommand("SELECT * FROM Transportasi WHERE kode LIKE @keyword OR keterangan LIKE @keyword", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                DataTable dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();
                if(dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                } else
                {
                    MessageBox.Show("Data tidak dapat ditemkan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tampildata();

                }

            } else
            {
                tampildata();
            }


        }
    }
}
