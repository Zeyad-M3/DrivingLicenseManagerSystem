using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsBusinessLayer; // Assuming this namespace contains the clsTestTypeBusiness class

namespace _3tr.Manage_Test_Types
{
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
            LodeAllTestTypes();
            CountOfDatatable();
        }
        void CountOfDatatable()
        {
            int count = dataGridView1.Rows.Count;
            CountOfDatatable countOfDatatable = new CountOfDatatable(count);
            countOfDatatable.Dock = DockStyle.Bottom;
            this.Controls.Add(countOfDatatable);
        }
        public void LodeAllTestTypes()
        {
            dataGridView1.DataSource = clsTestTypeBusiness.GetAllTestTypes();
            // Coulom size
            dataGridView1.Columns[1].Width = 160; 
            dataGridView1.Columns[2].Width = 550;
            


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmEditTestType frmEditTestType = new frmEditTestType((int)dataGridView1.CurrentRow.Cells[0].Value);
            frmEditTestType.ShowDialog();
            LodeAllTestTypes(); // Reload the data after update
        }
    }
}
