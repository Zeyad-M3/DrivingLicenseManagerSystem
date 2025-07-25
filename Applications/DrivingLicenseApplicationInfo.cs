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
using static System.Net.Mime.MediaTypeNames;

namespace _3tr.Applications
{
    public partial class DrivingLicenseApplicationInfo : UserControl
    {
        public int IdofApplication { get; set; }
        public DrivingLicenseApplicationInfo(int numIdApplication)
        {
            IdofApplication = numIdApplication;
            InitializeComponent();
            LoadDataforthecontrol();
        }


        void LoadDataforthecontrol()
        {


      
            var application = clsApplication.GetApplicationById(IdofApplication);
            var testApplicationpass = clsTest.GetTestsByApplicationId(IdofApplication);

            if (application != null)
            {
                label5.Text = application.LicenseClassID.ToString();
                // if pass in 1 and 2 and 3 in testtypeid this in the label3 for the application and the result = "Passed"
                if (testApplicationpass.Count > 0)
                {
                    label3.Text = string.Join(", ", testApplicationpass.Select(t => t.TestTypeId.ToString()));
                }
                else
                {
                    label3.Text = "No Tests Found";
                }







            }


            else
            {
                MessageBox.Show("Application Not Found");
            }

            

        }
        private void DrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
