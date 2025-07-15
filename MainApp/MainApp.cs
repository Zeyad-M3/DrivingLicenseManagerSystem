using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsBusinessLayer; // Assuming this namespace contains the clsApplication class

namespace _3tr
{
    public partial class MainApp : Form
    {
        public MainApp()
        {
            InitializeComponent();
            timeDate();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //

        }


        private void button2_Click_1(object sender, EventArgs e)
        {
           //
        }

        private void login1_Load(object sender, EventArgs e)
        {

        }

        private void MainApp_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmtest frm = new frmtest();
            frm.ShowDialog();

        }

        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {

        }

        private void drivingLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
               
        }
        void timeDate()
        {
            toolStripLabel1.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_ButtonClick(object sender, EventArgs e)
        {
            frmPeople frmPeople = new frmPeople();
            frmPeople.ShowDialog();
        }

        private void toolStripButton4_ButtonClick(object sender, EventArgs e)
        {
            Frmusers frmusers = new Frmusers();
            frmusers.ShowDialog();
        }
    }
}
