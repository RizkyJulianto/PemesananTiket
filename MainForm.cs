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
    public partial class MainForm : Form
    {
        public MainForm(int level)
        {
            InitializeComponent();
            if (level == 1) {

                lOGINToolStripMenuItem.Enabled = false;
                rUTEToolStripMenuItem.Enabled = false;
                tRANSPORTASIToolStripMenuItem.Enabled = false;
                pENUMPANGToolStripMenuItem.Enabled = false;
            } else if (level == 2)
            { 
                lOGINToolStripMenuItem.Enabled = false;
            }
        }

        private void rUTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rute rute = new Rute();
            rute.ShowDialog();
        }

        private void tRANSPORTASIToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void tIPETRANSPORTASIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipeTransportasi t = new TipeTransportasi();
            t.ShowDialog();
        }

        private void dATAPENUMPANGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Penumpang p = new Penumpang();
            p.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void pILIHTRANSPORTASIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transportasi t = new Transportasi();
            t.ShowDialog();
        }

        private void dATAPETUGASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Petugas p = new Petugas();
            p.ShowDialog();
        }

        private void pENUMPANGToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lOGOUYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin logout ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(konfirmasi  == DialogResult.Yes)
            {
                TampilanDepan l = new TampilanDepan();
                l.Show();
                this.Hide();
            }
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin keluar ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void dATAPEMESANANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataPemesanan d = new DataPemesanan();
            d.ShowDialog();

        }

        private void rEPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report r = new Report();
            r.ShowDialog();
        }
    }
}
