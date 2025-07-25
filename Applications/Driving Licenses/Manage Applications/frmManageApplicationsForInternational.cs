using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsBusinessLayer;

namespace _3tr.Applications.Driving_Licenses.Manage_Applications
{
    public partial class frmManageApplicationsForInternational : Form
    {
        public frmManageApplicationsForInternational()
        {
            InitializeComponent();
            LoadDataFordataGridView();
        }

        void LoadDataFordataGridView()
        {
            dataGridView1.DataSource = clsInternationalLicense.GetAll();



        }

        private void frmManageApplicationsForInternational_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmApplicationsForInternationalDrivingLicense frm = new frmApplicationsForInternationalDrivingLicense();
            frm.ShowDialog();
        }
    }
}
