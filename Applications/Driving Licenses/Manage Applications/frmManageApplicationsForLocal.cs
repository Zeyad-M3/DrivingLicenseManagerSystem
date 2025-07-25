using _3tr.DrivingLicense;
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

namespace _3tr.Applications.Driving_Licenses
{
    public partial class frmManageApplicationsForLocal : Form
    {
        public frmManageApplicationsForLocal()
        {
            InitializeComponent();
            LoadAllApplications();
            filterContactsList();



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmApplicationsLocalDrivingLicense frmApplicationsLocalDrivingLicense = new frmApplicationsLocalDrivingLicense();
            frmApplicationsLocalDrivingLicense.ShowDialog();
        }
        void CountOfDatatable()
        {
            int count = dataGridView1.Rows.Count;
            CountOfDatatable countOfDatatable = new CountOfDatatable(count);
            countOfDatatable.Dock = DockStyle.Bottom;
            this.Controls.Add(countOfDatatable);


        }

        private void LoadAllApplications()
        {
           
            try
            {
                dataGridView1.DataSource = clsApplication.GetAllApplications();

                if (dataGridView1.DataSource == null)
                {
                    MessageBox.Show("No data available from GetAllPersons.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading persons: " + ex.Message);
            }
            

        }

        private void frmManageApplicationsForLocal_Load(object sender, EventArgs e)
        {

        }

        private void sechduleTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmVisionTestAppointments frm = new frmVisionTestAppointments((int)dataGridView1.CurrentRow.Cells[0].Value, "Vision Test Appointments");
            frm.ShowDialog();
        }
        
        

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            frmVisionTestAppointments frm = new frmVisionTestAppointments((int)dataGridView1.CurrentRow.Cells[0].Value, "Written Test Appointments");
            frm.ShowDialog();


        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

            frmVisionTestAppointments frm = new frmVisionTestAppointments((int)dataGridView1.CurrentRow.Cells[0].Value, "Driving Test Appointments");
            frm.ShowDialog();

        }


        private void buttonFilter_Click()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a filter value.");
                return;
            }

            string selectedFilter = comboBox1.SelectedItem.ToString();
            string filterValue = textBox1.Text.Trim();

            try
            {
                switch (selectedFilter)
                {
                    case "PersonID":
                        if (int.TryParse(filterValue, out int personId))
                        {
                            var applications = clsApplication.GetApplicationsByPersonID(personId);
                            if (applications != null && applications.Count > 0)
                            {
                                dataGridView1.DataSource = applications;
                            }
                            else
                            {
                                dataGridView1.DataSource = null;
                                MessageBox.Show("No applications found for Person ID: " + personId);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid numeric Person ID.");
                        }
                        break;
                      
                    case "ApplicationId":
                    if (int.TryParse(filterValue, out int applicationId))
                    {
                        var application = clsApplication.GetApplicationById(applicationId);

                        if (application != null)
                        {
                            // Show the single application in the DataGridView
                            dataGridView1.DataSource = new List<clsApplication> { application };
                        }
                        else
                        {
                            dataGridView1.DataSource = null;
                            MessageBox.Show("No application found with Application ID: " + filterValue);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid numeric Application ID.");
                    }
                    break;
                               

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering data: " + ex.Message);
            }
        }


        void DelateApplication()
        {
            bool succeeded = false;
            if (MessageBox.Show(Text = "Are you sure you want to delete this person? [" + dataGridView1.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            if (clsApplication.Delete((int)dataGridView1.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Application Delete Succeeded.");
                succeeded = true;
            }
            else
            {
                MessageBox.Show("Delete Fail."); succeeded = false;
            }
        }

        private void filterContactsList()
        {
            comboBox1.Items.Add("PersonID");
            comboBox1.Items.Add("ApplicationId");
            comboBox1.Items.Add("Status");
            comboBox1.Items.Add("LicenseClassID");
            comboBox1.Items.Add("Score");
            comboBox1.SelectedIndex = 0; // اختيار افتراضي
            textBox1.Text = string.Empty; // Clear the filter text box

            
            CountOfDatatable(); // Count the number of rows in the DataGridView and display it
        }
        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelateApplication();
            LoadAllApplications();

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDrivingLicenseForTheFirstTime frm = new frmIssueDrivingLicenseForTheFirstTime((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            buttonFilter_Click();
        }

        //private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frmEditApplication frm = new frmEditApplication(1);
        //    frm.ShowDialog();
        //}

        private void showApplicationDettToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowApplication frm = new frmShowApplication((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            int NumberForCancel = (int)dataGridView1.CurrentRow.Cells[0].Value;

            var application = clsApplication.GetApplicationById(NumberForCancel);
            if (application != null)
            {
                // عرض رسالة تأكيد
                DialogResult result = MessageBox.Show("Are you sure you want to cancel this application?",
                    "Confirm Cancellation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // تحديث حالة التطبيق
                    application.Status = "Cancelled";

                    // تحديث التطبيق في قاعدة البيانات
                    if (clsApplication.UpdateApplication(application))
                    {
                        MessageBox.Show("Application cancelled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to cancel application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
          
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if person has license or not 
            var licenses = clsLicense.GetLicenseByPersonId((int)dataGridView1.CurrentRow.Cells[1].Value);
            if (licenses == null || licenses.Count == 0)
            {
                MessageBox.Show("No license found for this person.");
            }
            else
            {
                
                frmDrivingLicense frm = new frmDrivingLicense((int)dataGridView1.CurrentRow.Cells[1].Value);
                frm.ShowDialog();
            }
        }
    }
}
