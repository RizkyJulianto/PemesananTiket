using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PemesananTiket
{
    public partial class MainFormPenumpang : Form
    {
        public MainFormPenumpang()
        {
            InitializeComponent();
            lOGINToolStripMenuItem.Enabled = false;
        }

        private void pEMESANANToolStripMenuItem_Click(object sender, EventArgs e)
        {


            Pemesanan p = new Pemesanan();
            p.ShowDialog();
        }

        private void lOGOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin logout?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(konfirmasi == DialogResult.Yes)
            {
                TampilanDepan p = new TampilanDepan();
                p.Show();
                this.Hide();
            }
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin keluar?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
