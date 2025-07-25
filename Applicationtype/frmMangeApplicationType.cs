using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _3tr.Applicationtype;
using ContactsBusinessLayer; // Assuming this namespace contains the clsApplication class

namespace _3tr
{
    public partial class frmMangeApplicationType : Form
    {
        public frmMangeApplicationType()
        {
            InitializeComponent();
            LodeAllApplicationTypes();
            CountOfDatatable();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        void CountOfDatatable()
        {
            int count = dataGridView1.Rows.Count;
            CountOfDatatable countOfDatatable = new CountOfDatatable(count);
            countOfDatatable.Dock = DockStyle.Bottom;
            this.Controls.Add(countOfDatatable);


        }

        public void LodeAllApplicationTypes()
        {
            dataGridView1.DataSource = ApplicationType.GetAllApplicationTypes();
            // Count the number of rows in the DataGridView and display it
            this.Size = new Size(500, 450); // Set the size of the form


        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void etToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType frmUpdateApplicationType = new frmUpdateApplicationType((int)dataGridView1.CurrentRow.Cells[0].Value);
            frmUpdateApplicationType.ShowDialog();
            LodeAllApplicationTypes(); // Reload the data after update



        }
    }
}
