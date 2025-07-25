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

namespace _3tr.Applications.Driving_Licenses
{
    public partial class frmApplicationsForInternationalDrivingLicense : Form
    {
        public frmApplicationsForInternationalDrivingLicense()
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
        private void frmApplicationsForInternationalDrivingLicense_Load(object sender, EventArgs e)
        {

        }
        void CheckIfClassoftheDriver3OrMore()
        {
            string filterValue = textBox1.Text.Trim();
            if (int.TryParse(filterValue, out int driverId))
            {
                var driver = Driver.GetDriverById(driverId);
                var applications = clsApplication.GetApplicationsByPersonID(driver.PersonId);
                // Count how many applications of type 3 or more exist
                int count = applications.Count(app => app.ApplicationType >= 3);
                if (count > 0)
                {
                    // If there are 3 or more applications, allow the driver to apply for an international driving license
                    var application = new clsApplication
                    {
                        PersonID = driver.PersonId,
                        ApplicationDate = DateTime.Now,
                        ApplicationType = 7, // Assuming 7 is the type for international driving license
                        Status = "Pending",
                        ApplicationFee = 51, // Set the application fee as needed
                        PaymentDate = DateTime.Now,
                        LicenseClassID = driver.LicenseID
                    };
                    if (application.Save())
                    {
                        MessageBox.Show("Application for International Driving License submitted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to submit the application. Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("The driver Can't apply for an international driving license Bcz U Don't Have License 3 or more.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckIfClassoftheDriver3OrMore();
            // make wait then open the frmShowApplication
            System.Threading.Thread.Sleep(100); 
            frmApplicationInfoForInternational frmApplicationInfoForInternational = new frmApplicationInfoForInternational(int.Parse(label14.Text));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoadDateFortheSerch();
        }
    }
}
