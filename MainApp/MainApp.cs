using _3tr.Applications;
using _3tr.Applications.Driving_Licenses;
using _3tr.Applications.Driving_Licenses.Manage_Applications;
using _3tr.DetainLicenses;
using _3tr.DrivingLicense;
using _3tr.Manage_Test_Types;
using _3tr.Users;
using ContactsBusinessLayer; // Assuming this namespace contains the clsApplication class
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
    public partial class MainApp : Form
    {
        string userName;
        string userPassword;
        public MainApp(string userName, string userPassword)
        {
            InitializeComponent();
            timeDate();
            this.userName = userName;
            this.userPassword = userPassword;
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

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
            this.Hide();
           
            
        }

        private void manageApplicationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmMangeApplicationType frmMangeApplicationType = new frmMangeApplicationType();
            frmMangeApplicationType.ShowDialog();

        }

        private void localLiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApplicationsLocalDrivingLicense frm = new frmApplicationsLocalDrivingLicense();
            frm.ShowDialog();  

        }

        private void testToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmManageApplicationsForLocal frmManageApplicationsForLocal = new frmManageApplicationsForLocal();
            frmManageApplicationsForLocal.ShowDialog();
        }

        private void testToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmManageApplicationsForInternational frmManageApplicationsForInternational = new frmManageApplicationsForInternational();
            frmManageApplicationsForInternational.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApplicationsForInternationalDrivingLicense frm = new frmApplicationsForInternationalDrivingLicense();
            frm.ShowDialog();
        }

        private void mangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frmManageTestTypes = new frmManageTestTypes();
            frmManageTestTypes.ShowDialog();
        }

        private void toolStripButton3_ButtonClick(object sender, EventArgs e)
        {
            frmMangeDrivers frmMangeDrivers = new frmMangeDrivers();
            frmMangeDrivers.ShowDialog();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmRenewLicenseApplication frmRenewLicenseApplication = new frmRenewLicenseApplication();
            frmRenewLicenseApplication.ShowDialog(); 
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmReplacementForLostOrdamagedLicense frmReplacementForLostOrdamagedLicense = new frmReplacementForLostOrdamagedLicense();
            frmReplacementForLostOrdamagedLicense.ShowDialog();
        }

        private void testToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmDetainLicense license = new frmDetainLicense();
            license.ShowDialog();
        }

        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmManageDetainedLicenses frmManageDetainedLicenses = new frmManageDetainedLicenses();
            frmManageDetainedLicenses.ShowDialog();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense license = new frmReleaseDetainedLicense();
            license.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense license = new frmReleaseDetainedLicense();
            license.ShowDialog();
        }

        private void test1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePass frmChangePass = new frmChangePass(userPassword, userName);
            frmChangePass.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmGlobalCurrentUserInfo frmGlobalCurrentUserInfo = new frmGlobalCurrentUserInfo(userName , userPassword);
            frmGlobalCurrentUserInfo.ShowDialog();
        }
    }
}
