using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3tr.DetainLicenses
{
    public partial class frmManageDetainedLicenses : Form
    {
        public frmManageDetainedLicenses()
        {
            InitializeComponent();
            loadDataForDataGrid();
        }
        void loadDataForDataGrid()
        {
            dataGridView1.DataSource= clsLicense.GetAllLicenses();
        }
        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense license = new frmReleaseDetainedLicense();
            license.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmDetainLicense license = new frmDetainLicense();
            license.ShowDialog();
        }
    }
}
