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

namespace _3tr.Applications
{
    public partial class frmReplacementForLostOrdamagedLicense : Form
    {
        public frmReplacementForLostOrdamagedLicense()
        {
            InitializeComponent();
        }

        void LoadDateFortheSerch()
        {
            string filterValue = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(filterValue))
            {
                MessageBox.Show("Please enter a filter value.");
                return;
            }

            if (int.TryParse(filterValue, out int driverId))
            {
                var driver = Driver.GetDriverById(driverId);
                if (driver == null)
                {
                    MessageBox.Show("No driver found with the provided ID.");
                    return;
                }
                var PersonInfo = clsPersonBusiness.GetPersonById(driver.PersonId);

                var ClassInfo = clsLicense.GetLicenseById(driver.LicenseID);


                if (driver == null)
                {
                    MessageBox.Show("No driver found with the provided ID.");
                    return;
                }
                if (ClassInfo != null)
                {

                    label12.Text = ClassInfo.LicenseId.ToString();
                    label16.Text = ClassInfo.IssueDate.ToString("dd/MM/yyyy");
                    label17.Text = ClassInfo.ExpiryDate.ToString("dd/MM/yyyy");
                    label13.Text = driver.FullName;
                    label15.Text = driver.NationalNO;
                    label14.Text = driver.LicenseID.ToString();
                    label18.Text = driver.Active_Licenses ? "Active" : "Inactive";
                    label19.Text = PersonInfo.DateOfBirth.ToString("dd/MM/yyyy");
                    label20.Text = driver.DriverId.ToString();
                    if (PersonInfo.Photo != null && PersonInfo.Photo != "")
                    {
                        pictureBox11.ImageLocation = ClassInfo.PhotoPath;
                    }
                    else
                    {
                        pictureBox11.Image = null; // Clear the image if no path is provided
                        pictureBox11.Visible = false; // Hide the picture box if no image is available
                    }
                }
                else
                {
                    MessageBox.Show("No driver found with the provided ID.");
                }
            }
            else
            {
                MessageBox.Show("No driver found with the provided ID.");

            }
        }

        // Issue Replacement License
        private void IssueReplacementLicense()
        {
            string filterValue = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(filterValue))
            {
                MessageBox.Show("Please enter a filter value.");
                return;
            }
            if (int.TryParse(filterValue, out int driverId))
            {
                var driver = Driver.GetDriverById(driverId);
                if (driver == null)
                {
                    MessageBox.Show("No driver found with the provided ID.");
                    return;
                }
                // Assuming you have a method to issue a replacement license
                bool isIssued = clsLicense.IssueReplacementLicense(driver.LicenseID);
                if (isIssued)
                {
                    MessageBox.Show("Replacement license issued successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to issue replacement license.");
                }
            }
            else
            {
                MessageBox.Show("Invalid driver ID format.");
            }
        }

        private void frmReplacementForLostOrdamagedLicense_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoadDateFortheSerch();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IssueReplacementLicense();
        }
    }
}
