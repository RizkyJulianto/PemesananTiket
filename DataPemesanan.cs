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
    public partial class DataPemesanan : Form
    {
        public DataPemesanan()
        {
            InitializeComponent();
            dateTimePicker3.Format = DateTimePickerFormat.Time;
            dateTimePicker3.ShowUpDown = true;


            SqlCommand petugas = new SqlCommand("SELECT id_petugas, nama_petugas FROM Petugas", conn);
            petugas.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(petugas);
            da1.Fill(dt1);
            conn.Close();

            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "nama_petugas";
            comboBox1.ValueMember = "id_petugas";
            comboBox1.SelectedIndex = -1;



            SqlCommand tujuan = new SqlCommand("SELECT id_rute, tujuan FROM Rute", conn);
            tujuan.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(tujuan);
            da2.Fill(dt2);
            comboBox3.DataSource = dt2;
            comboBox3.DisplayMember = "tujuan";
            comboBox3.ValueMember = "id_rute";
            comboBox3.SelectedIndex = -1;
            conn.Close();

            tampildata();
        }

        SqlConnection conn = Properti.conn;

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Pemesanan", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;

        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox7.Text = "";
            richTextBox1.Text = "";
            comboBox1.SelectedValue = -1;
            comboBox3.SelectedValue = -1;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;

        }

        private void DataPemesanan_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls, textBox7))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                } else if(!Properti.Number(textBox1.Text))
                {
                    MessageBox.Show("Kode pemesanan harus angka", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }else if(!Properti.Number(textBox2.Text))
                {
                    MessageBox.Show("Kode kursi harus angka", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (comboBox3.SelectedIndex == -1)
                {
                    MessageBox.Show("Pilih tujuan terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Pilih petugas terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SqlCommand tujuan = new SqlCommand("SELECT tujuan FROM Rute WHERE id_rute = @id_rute", conn);
                tujuan.CommandType = CommandType.Text;
                conn.Open();

                tujuan.Parameters.AddWithValue("@id_rute", comboBox3.SelectedValue);

                string tujuantext = tujuan.ExecuteScalar().ToString();
                conn.Close();


                SqlCommand cmd = new SqlCommand("INSERT INTO Pemesanan VALUES (@kodepemesanan,@tanggal_pesan,@tempat_pemesanan,@id_penumpang,@kode_kursi,@id_rute,@tujuan,@tanggal_berangkat,@jam_check_in,@jam_berangkat,@total_bayar,@id_petugas)", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@kodepemesanan", textBox1.Text);
                cmd.Parameters.AddWithValue("@tanggal_pesan", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@tempat_pemesanan", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@kode_kursi", textBox2.Text);
                cmd.Parameters.AddWithValue("@id_rute", comboBox3.SelectedValue);
                cmd.Parameters.AddWithValue("@tujuan", tujuantext);
                cmd.Parameters.AddWithValue("@tanggal_berangkat", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@jam_check_in", DateTime.Now);
                cmd.Parameters.AddWithValue("@jam_berangkat", dateTimePicker3.Value);
                cmd.Parameters.AddWithValue("@total_bayar", textBox7.Text);
                cmd.Parameters.AddWithValue("@id_petugas", comboBox1.SelectedValue);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Data berhasil ditambahkan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin membatalkan pemesanan?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {

                clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls, textBox7))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (!Properti.Number(textBox1.Text))
                {
                    MessageBox.Show("Kode pemesanan harus angka", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (!Properti.Number(textBox2.Text))
                {
                    MessageBox.Show("Kode kursi harus angka", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (comboBox3.SelectedIndex == -1)
                {
                    MessageBox.Show("Pilih tujuan terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Pilih petugas terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SqlCommand tujuan = new SqlCommand("SELECT tujuan FROM Rute WHERE id_rute = @id_rute", conn);
                tujuan.CommandType = CommandType.Text;
                conn.Open();

                tujuan.Parameters.AddWithValue("@id_rute", comboBox3.SelectedValue);

                string tujuantext = tujuan.ExecuteScalar().ToString();
                conn.Close();

                var row = dataGridView1.CurrentRow;
                int id_pemesanan = Convert.ToInt32(row.Cells["id_pemesanan"].Value.ToString());
                SqlCommand cmd = new SqlCommand("UPDATE Pemesanan SET  kode_pemesanan = @kodepemesanan, tanggal_pesan = @tanggal_pesan, tempat_pemesanan=@tempat_pemesanan,kode_kursi = @kode_kursi,id_rute= @id_rute,tujuan = @tujuan, tanggal_berangkat = @tanggal_berangkat, jam_check_in = @jam_check_in, jam_berangkat = @jam_berangkat, total_bayar = @total_bayar, id_petugas = @id_petugas WHERE id_pemesanan = @id", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@id", textBox1.Text);
                cmd.Parameters.AddWithValue("@kodepemesanan", textBox1.Text);
                cmd.Parameters.AddWithValue("@tanggal_pesan", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@tempat_pemesanan", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@kode_kursi", textBox2.Text);
                cmd.Parameters.AddWithValue("@id_rute", comboBox3.SelectedValue);
                cmd.Parameters.AddWithValue("@tujuan", tujuantext);
                cmd.Parameters.AddWithValue("@tanggal_berangkat", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@jam_check_in", DateTime.Now);
                cmd.Parameters.AddWithValue("@jam_berangkat", dateTimePicker3.Value);
                cmd.Parameters.AddWithValue("@total_bayar", textBox7.Text);
                cmd.Parameters.AddWithValue("@id_petugas", comboBox1.SelectedValue);
                cmd.ExecuteNonQuery();
                conn.Close();
                tampildata();
                MessageBox.Show("Data berhasil diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["kode_pemesanan"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["kode_kursi"].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells["tempat_pemesanan"].Value.ToString();
            comboBox3.SelectedValue = dataGridView1.CurrentRow.Cells["id_rute"].Value.ToString();
            comboBox1.SelectedValue = dataGridView1.CurrentRow.Cells["id_petugas"].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["tanggal_pesan"].Value.ToString());
            dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["tanggal_berangkat"].Value.ToString());
            dateTimePicker3.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["jam_berangkat"].Value.ToString());
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex != -1)
            {

                SqlCommand cmd = new SqlCommand("SELECT harga FROM Rute WHERE id_rute = @id_rute", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@id_rute", comboBox3.SelectedValue);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox7.Text = reader["harga"].ToString();
                }

                conn.Close();

            }
        }
    }
}
