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
    public partial class frmDetainLicense : Form
    {
        public frmDetainLicense()
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

            if (int.TryParse(filterValue, out int LicenseID))
            {
                var License = clsLicense.GetLicenseById(LicenseID);
                if (License == null)
                {
                    MessageBox.Show("No License found with the provided ID.");
                    return;
                }
                var driver = Driver.GetDriverByLicenseId(License.LicenseId);
                if (driver == null)
                {
                    MessageBox.Show("No driver found with the provided License ID.");
                    return;
                }
                var PersonInfo = clsPersonBusiness.GetPersonById(License.PersonId);



                if (LicenseID == null)
                {
                    MessageBox.Show("No License found with the provided ID.");
                    return;
                }
                if (License != null)
                {

                    label12.Text = License.LicenseId.ToString();
                    label16.Text = License.IssueDate.ToString("dd/MM/yyyy");
                    label17.Text = License.ExpiryDate.ToString("dd/MM/yyyy");
                    label13.Text = driver.FullName;
                    label15.Text = driver.NationalNO;
                    label14.Text = driver.LicenseID.ToString();
                    label18.Text = driver.Active_Licenses ? "Active" : "Inactive";
                    label19.Text = PersonInfo.DateOfBirth.ToString("dd/MM/yyyy");
                    label20.Text = driver.DriverId.ToString();
                    if (PersonInfo.Photo != null && PersonInfo.Photo != "")
                    {
                        pictureBox11.ImageLocation = License.PhotoPath;
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

        // detain license  
        void detainlicense()
        {

            string filterValue = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(filterValue))
            {
                MessageBox.Show("Please enter a filter value.");
                return;
            }

            if (int.TryParse(filterValue, out int LicenseID))
            {
                var License = clsLicense.GetLicenseById(LicenseID);
                clsLicense license = new clsLicense()
                {
                    LicenseId = License.LicenseId,
                    IssueDate = License.IssueDate,
                    LicenseNumber = License.LicenseNumber,
                    ApplicationType = License.ApplicationType,
                    ExpiryDate = License.ExpiryDate,
                    PhotoPath= License.PhotoPath,
                    PersonId= License.PersonId,
                    Conditions = textBox2.Text, // Fixed: assign string directly
                    IssueStatus = "Detained"
                };
                bool result = clsLicense.UpdateLicense(license);
                if (result)
                {
                    MessageBox.Show("License has been detained successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDateFortheSerch(); // Reload the data after detaining
                }
                else
                {
                    MessageBox.Show("Failed to detain the license. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void frmDetainLicense_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoadDateFortheSerch();
        }

       


        private void button1_Click(object sender, EventArgs e)
        {
            detainlicense();
        }
    }
}
