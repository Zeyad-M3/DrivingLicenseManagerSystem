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

namespace _3tr.DrivingLicense
{
    public partial class frmMangeDrivers : Form
    {
        public frmMangeDrivers()
        {
            InitializeComponent();
            LoadDrivers();
        }

        void LoadDrivers()
        {
            // Assuming DriversData.GetAllDrivers() returns a list of drivers
            var drivers = Driver.GetalltheDrivers();
            dataGridView1.DataSource = drivers;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
