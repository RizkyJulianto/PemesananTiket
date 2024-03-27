using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PemesananTiket
{
    public partial class Pemesanan : Form
    {


        public Pemesanan()
        {
            InitializeComponent();
            
      


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



        }

        SqlConnection conn = Properti.conn;

       

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Pemesanan_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(Properti.validasi(this.Controls, textBox7))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                
                }else if (comboBox3.SelectedIndex == -1)
                {
                    MessageBox.Show("Pilih tujuan terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }else if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Pilih petugas terlebih dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SqlCommand tujuan = new SqlCommand("SELECT tujuan FROM Rute WHERE id_rute = @id_rute",conn);
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
                cmd.Parameters.AddWithValue("@id_penumpang", Properti.id_penumpang);
                cmd.Parameters.AddWithValue("@kode_kursi", textBox2.Text);
                cmd.Parameters.AddWithValue("@id_rute", comboBox3.SelectedValue);
                cmd.Parameters.AddWithValue("@tujuan", tujuantext);
                cmd.Parameters.AddWithValue("@tanggal_berangkat", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@jam_check_in",DateTime.Now);
                cmd.Parameters.AddWithValue("@jam_berangkat",dateTimePicker3.Value);
                cmd.Parameters.AddWithValue("@total_bayar",textBox7.Text);
                cmd.Parameters.AddWithValue("@id_petugas",comboBox1.SelectedValue);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Data berhasil ditambahkan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
             }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox3.SelectedIndex != -1) {

                SqlCommand cmd = new SqlCommand("SELECT harga FROM Rute WHERE id_rute = @id_rute", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@id_rute", comboBox3.SelectedValue);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    textBox7.Text = reader["harga"].ToString();
                }

                conn.Close();
            
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
              


        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin membatalkan pemesanan?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(konfirmasi == DialogResult.Yes) {
            
            clear();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime pemesanan = dateTimePicker1.Value;
            DateTime berangkat = dateTimePicker2.Value;

            TimeSpan selisih = berangkat - pemesanan;
            int lamahari = selisih.Days;
              

            if(lamahari > 3) {
                dateTimePicker2.Value = DateTime.Now;
                MessageBox.Show("Maksimal untuk melakukan booking tiket yaitu 3 hari setelah tanggal pemesanan ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            
            }
        }
    }
}
