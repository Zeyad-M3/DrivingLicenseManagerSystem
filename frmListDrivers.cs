using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3tr
{
    public partial class frmListDrivers : Form
    {
        public frmListDrivers()
        {
            InitializeComponent();
        }

        // void _RefreshDriversList()
        //{
        //    // Add all drivers to data
        //    dataGridView1.DataSource = cls.clsDrivers.GetAllDrivers();
        //    // Set the size of the form
        //    this.Size = new Size(800, 700);
        //}

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
