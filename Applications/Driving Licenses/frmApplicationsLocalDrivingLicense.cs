using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _3tr.Applications.Driving_Licenses
{ 
    public partial class frmApplicationsLocalDrivingLicense : Form
    {
        public int _NumOfLicenseClass;

        public frmApplicationsLocalDrivingLicense()
        {
            InitializeComponent();
            label9.Text = DateTime.Now.ToShortDateString();
            filterForLicenseClass();
        }


        private void filterForLicenseClass()
        {
            comboBox1.Items.Add("Class 1 - Small Motorcycle License");
            comboBox1.Items.Add("Class 2 - Heavy Motorcycle License");
            comboBox1.Items.Add("Class 3 - Standard Car License");
            comboBox1.Items.Add("Class 4 - Commercial License (Taxi/Limousine)");
            comboBox1.Items.Add("Class 5 - Agricultural Vehicle License");
            comboBox1.Items.Add("Class 6 - Small/Medium Bus License");
            comboBox1.Items.Add("Class 7 - Heavy Truck License");
            comboBox1.SelectedIndex = 0; // اختيار افتراضي

            
        }


        void SwitchForComboBoxChoseToNumOfLicenseClass()
        {
            switch (comboBox1.SelectedItem?.ToString())
            {
                case "Class 1 - Small Motorcycle License":
                    _NumOfLicenseClass = 1;
                    return;
                case "Class 2 - Heavy Motorcycle License":
                    _NumOfLicenseClass = 2;
                    return;
                case "Class 3 - Standard Car License":
                    _NumOfLicenseClass = 3;
                    return;
                case "Class 4 - Commercial License (Taxi/Limousine)":
                    _NumOfLicenseClass = 4;
                    return;
                case "Class 5 - Agricultural Vehicle License":
                    _NumOfLicenseClass = 5;
                    return;
                case "Class 6 - Small/Medium Bus License":
                    _NumOfLicenseClass = 6;
                    return;
                case "Class 7 - Heavy Truck License":
                    _NumOfLicenseClass = 7;
                    return;
                default:
                    _NumOfLicenseClass = 0; // قيمة افتراضية إذا لم يتم اختيار عنصر صالح
                    return;
            }
        }
        private void SaveTheNewApplication()
        {
            SwitchForComboBoxChoseToNumOfLicenseClass();
            if (controlForPeopleAndFindby1.idForthePerson < 0)
            {
                MessageBox.Show("The Person Have Be Found. ");
                return;
                
            }
            clsApplication NewApplication = new clsApplication
            {
                //Id = 0,
                PersonID = controlForPeopleAndFindby1.idForthePerson,
                ApplicationDate = DateTime.Now,
                ApplicationType = 1,
                Status = "New",
                ApplicationFee = 5,
                PaymentDate = DateTime.Now,
                LicenseClassID = _NumOfLicenseClass


            };
            bool suceed = false;

            // if(clsApplication.GetApplicationById(NewApplication.Id))
            // if person have class 3 and new dont let person make new application the person can have one order for the class but if defrant class the person can appliy
            // تحقق من الطلبات الحالية للشخص
            List<clsApplication> existingApplications = clsApplication.GetApplicationsByPersonID(NewApplication.PersonID);
            bool hasActiveApplicationForSameClass = false;

            foreach (var app in existingApplications)
            {
                if (app.Status != "Completed" && app.Status != "Rejected" && app.LicenseClassID == NewApplication.LicenseClassID)
                {
                    hasActiveApplicationForSameClass = true;
                    break;
                }
            }

            if (hasActiveApplicationForSameClass)
            {
                MessageBox.Show("You already have an active application for this license class. Only one application per class is allowed.", "Application Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (clsApplication.AddApplication(NewApplication))
                {
                    suceed = true;
                    MessageBox.Show("Application Add Succeed.");
                    labelIdApplication.Text = NewApplication.Id.ToString();
                    return;
                }
                else
                {
                    MessageBox.Show("Application Not Add it fail.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


        }

        private void controlForPeopleAndFindby1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SaveTheNewApplication();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // if controlForPeopleAndFindby1 have value go to next page 
            tabControl1.SelectedTab = tabPage2;
                button1.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
