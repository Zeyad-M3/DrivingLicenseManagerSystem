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

namespace _3tr.Applications
{
    public partial class frmApplicationInfoForInternational : Form
    {
        int IDOflicense;
        public frmApplicationInfoForInternational(int iDOflicense)
        {
            IDOflicense = iDOflicense;
            InitializeComponent();
            LoadDataForInternationalLicenses();


        }
        private void LoadDataForInternationalLicenses()
        {
            clsInternationalLicense NewInternationalLicense = new clsInternationalLicense();
            NewInternationalLicense.LicenseID = IDOflicense;
            NewInternationalLicense.IssueDate = DateTime.Now;
            NewInternationalLicense.ExpiryDate = DateTime.Now.AddYears(5); // Assuming a 5-year validity
            NewInternationalLicense.Status = "New";
            NewInternationalLicense.PreviousInternationalLicenseID = 1; // Assuming no previous license for new applications

            if(clsInternationalLicense.ExistsByLicenseID(IDOflicense))
            {
                MessageBox.Show("International License already exists for this license ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsInternationalLicense.AddNewInternationalLicense(NewInternationalLicense))
            {
                MessageBox.Show("International License application submitted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Failed to submit International License application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmApplicationInfoForInternational_Load(object sender, EventArgs e)
        {

        }
    }
}
