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

namespace _3tr.DrivingLicense
{
    public partial class frmDrivingLicense : Form
    {
        public int PersonId { get; set; }
        public frmDrivingLicense(int personId)
        {
            PersonId = personId;
            InitializeComponent();
            loadeDataFortheForm();

        }

        void loadeDataFortheForm()
        {
            // Fix: GetLicenseByPersonId returns a List<clsLicense>, not a single clsLicense.
            List<clsLicense> licenses = clsLicense.GetLicenseByPersonId(PersonId);
            clsLicense license = licenses != null && licenses.Count > 0 ? licenses[0] : null;

            clsPersonBusiness person = clsPersonBusiness.GetPersonById(PersonId);
            

            if (license != null)
            {
                label15.Text = license.IssueDate.ToLongDateString();
                label16.Text = license.ExpiryDate.ToLongDateString();
                label14.Text = license.LicenseId.ToString();
            }
            else
            {
                label15.Text = "";
                label16.Text = "";
                label14.Text = "";
            }

            if (person != null)
            {
                label17.Text = person.DateOfBirth.ToLongDateString();
                label13.Text = person.FullName;
                label19.Text = person.PersonId.ToString();
            }
            else
            {
                label17.Text = "";
                label13.Text = "";
                label19.Text = "";
            }

            // Set the photo if it exists
            if (!string.IsNullOrEmpty(person?.Photo) && System.IO.File.Exists(person.Photo))
            {
                pictureBox9.Image = Image.FromFile(person.Photo);
            }
            else
            {
                pictureBox9.Image = null; // or set a default image
            }
            //if person have a driving data show it in the form else make new driving data
            var drivers = Driver.GetDriversByPersonId(PersonId);
            if (drivers == null || (drivers != null && drivers.Count == 0))
            {
                Driver driver = new Driver();
                driver.NationalNO = person?.NationalID.ToString();
                driver.FullName = person?.FullName;
                driver.PersonId = person != null ? person.PersonId : 0;
                driver.Date = DateTime.Today.Date;
                driver.LicenseID = license != null ? license.LicenseId : 0;
                driver.Active_Licenses = true;

                if (Driver.AddNewDriver(driver))
                {
                    MessageBox.Show("Driver data saved successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to save driver data.");
                }
            }
            else
            {
                drivers = Driver.GetDriversByPersonId(PersonId);
                if (drivers != null && drivers.Count > 0)
                {
                    var driver = drivers[0]; // Assuming you want to display the first driver's data
                    label20.Text = driver.NationalNO;
                    label18.Text = driver.DriverId.ToString();
                    label21.Text = driver.Active_Licenses ? "Active" : "Inactive";
                    
                }
                else
                {
                    label18.Text = "";
                    label20.Text = "";
                    label21.Text = "";
                    
                }
            }


        }



        private void frmDrivingLicense_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
