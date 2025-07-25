using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 using ContactsBusinessLayer;


namespace _3tr.Applications
{
    public partial class frmRenewLicenseApplication : Form
    {
        public frmRenewLicenseApplication()
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
                var PersonInfo= clsPersonBusiness.GetPersonById(driver.PersonId);
               
                var ClassInfo = clsLicense.GetLicenseById(driver.LicenseID);
                

                if (driver == null)
                {
                    MessageBox.Show("No driver found with the provided ID.");
                    return;
                }
                if (ClassInfo != null)
                {
                    
                    label12.Text =ClassInfo.LicenseId.ToString();
                    label16.Text= ClassInfo.IssueDate.ToString("dd/MM/yyyy");
                    label17.Text = ClassInfo.ExpiryDate.ToString("dd/MM/yyyy");
                    label13.Text = driver.FullName;
                    label15.Text = driver.NationalNO;
                    label14.Text = driver.LicenseID.ToString();
                    label18.Text = driver.Active_Licenses ? "Active" : "Inactive";
                    label19.Text = PersonInfo.DateOfBirth.ToString("dd/MM/yyyy");
                    label20.Text = driver.DriverId.ToString();
                    if(PersonInfo.Photo != null && PersonInfo.Photo != "")
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


        void CheckIfLicenseIsExpired()
        {
            var filterValue = textBox1.Text.Trim();
            int.TryParse(filterValue, out int driverId);
            if (driverId <= 0)
            {
                MessageBox.Show("Please enter a valid driver ID.");
                return;
            }
            var driver = Driver.GetDriverById(driverId);
            
             
            var ClassInfo = clsLicense.GetLicenseById(driver.LicenseID);


            if (ClassInfo.ExpiryDate < DateTime.Now.Date)
            {
               frmRenewTheLicense frm = new frmRenewTheLicense(ClassInfo.LicenseId);
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    MessageBox.Show("License renewed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDateFortheSerch(); // Reload the data after renewal
                }
            }
            else
            {
                MessageBox.Show("You can't renew the license because it’s not expired. Expiry Date is: " + ClassInfo.ExpiryDate.ToString("dd/MM/yyyy"), "Renewal Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void frmRenewLicenseApplication_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoadDateFortheSerch();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckIfLicenseIsExpired();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
