using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBusinessLayer;
using System.Windows.Forms;

namespace _3tr.Applications
{
    public partial class frmRenewTheLicense : Form
    {
        int licenseId;
        public frmRenewTheLicense(int licenseID)
        {
            licenseId = licenseID;
            InitializeComponent();
            LoadDateForTheNewLicenseRenewApplication();
        }

        void LoadDateForTheNewLicenseRenewApplication()
        {
            clsApplication application = new clsApplication();
            application.LicenseClassID = licenseId;
            // Fix: parse LicenseNumber (string) to int for ApplicationType
            if (int.TryParse(clsLicense.GetLicenseById(licenseId).LicenseNumber, out int licenseNumber))
            {
                application.ApplicationType = 3;
            }
            else
            {
                // Handle parse failure, e.g., set to 0 or throw exception as appropriate
                application.ApplicationType = 3;
            }
            application.Status = "Pending";
            application.ApplicationDate = DateTime.Now;
            application.PaymentDate = DateTime.Now;
            application.ApplicationFee = 5;
            application.PersonID = clsLicense.GetLicenseById(licenseId).PersonId;

            if (clsApplication.AddApplication(application))
            {

                clsLicense license = new clsLicense
                {
                    LicenseId = licenseId,
                    PersonId = clsLicense.GetLicenseById(licenseId).PersonId,
                    LicenseNumber = GenerateUniqueLicenseNumber(), // Fixed: assign result of method directly
                    ApplicationType = "Renew",
                    PhotoPath = clsLicense.GetLicenseById(licenseId).PhotoPath,
                    IssueDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddYears(5), // Assuming a 5-year renewal
                    Conditions = "Renewed",
                    IssueStatus = "Pending"
                };
                if (clsLicense.AddNewLicense(license))
                {
                    MessageBox.Show("License renewal successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label18.Text= application.Id.ToString();
                    label17.Text = application.ApplicationDate.ToString("dd/MM/yyyy");
                    label16.Text = license.IssueDate.ToString("dd/MM/yyyy");
                    label11.Text=license.ExpiryDate.ToString("dd/MM/yyyy");



                }
                else
                {
                    MessageBox.Show("Fail to renewal The License");
                }
            }
            else
            {
                MessageBox.Show("Failed to submit the license renewal application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Fix: Add GenerateUniqueLicenseNumber method
        private string GenerateUniqueLicenseNumber()
        {
            // Example: generate a unique license number using current timestamp and licenseId
            return $"LIC-{licenseId}-{DateTime.Now.Ticks}";
        }

        private void frmRenewTheLicense_Load(object sender, EventArgs e)
        {

        }
    }
}
