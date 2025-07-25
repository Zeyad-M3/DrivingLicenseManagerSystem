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

namespace _3tr.Applications
{
    public partial class frmScheduleTest : Form
    {
        public int IdofApplication { get; set; }
        public string TestType { get; set; } 
        public frmScheduleTest(int numIdApplication, string _testType)
        {
            IdofApplication = numIdApplication;
            TestType = _testType;
            InitializeComponent(); LoadData();
           
        }

        void LoadData()
        {
            if(TestType== "Written Test Appointments")
            {
                label7.Text= "20";
                pictureBox1.Visible = false; // إخفاء PictureBox إذا كان نوع الاختبار هو Written Test
            }
            else if (TestType == "Vision Test Appointments")
            {
                label7.Text = "10";
            }
            else if (TestType == "Driving Test Appointments")
            {
                label7.Text = "30";
                pictureBox1.Visible = false; // إخفاء PictureBox إذا كان نوع الاختبار هو Driving Test
            }
           


            var application = clsApplication.GetApplicationById(IdofApplication);
            List<clsTest> testApplicationpass = clsTest.GetTestsByApplicationId(IdofApplication);
            if (application != null)
            {
                label10.Text = application.LicenseClassID.ToString();
                label9.Text = application.PersonID.ToString();
                foreach (clsTest test in testApplicationpass)
                {
                    if (label8 != null) // التأكد من وجود label8
                    {
                        if (test.RetryCount <= 0) // التحقق من القيمة <= 0 (بما أن null يصبح 0)
                        {
                            label8.Text = "0";
                        }
                        else
                        {
                            label8.Text = test.RetryCount.ToString();
                        }
                    }
                }
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TestType == "Vision Test Appointments")
            {
                clsTest clsTest = new clsTest
                {
                    ApplicationId = IdofApplication,
                    TestTypeId = 1,
                    TestDate = DateTime.TryParse(maskedTextBox1.Text, out DateTime dateValue) ? dateValue : DateTime.MinValue,
                    Score = comboBox1.Text == "Pass" ? (int)(0.6 * 100) : 0, // 60% من 100 كقيمة افتراضية إذا كان Pass، و0 إذا لم يكن
                    TestResult = comboBox1.Text,
                    RetryCount = null,
                    QuestionCount = 100, // تعيين قيمة افتراضية للأسئلة (غيرها إذا كان لديك قيمة مختلفة)
                    Description = string.Empty,
                    PersonId = null
                };

                bool success = false;
                if (clsTest.AddTest(clsTest))
                {
                    MessageBox.Show("Test Add It Succeeded.");
                    success = true;
                }
                else
                {
                    MessageBox.Show("Test Not Add It.");
                }
            }
            else if (TestType == "Written Test Appointments")
            {
                

                clsTest clsTest = new clsTest
                {
                    ApplicationId = IdofApplication,
                    TestTypeId = 2,
                    TestDate = DateTime.TryParse(maskedTextBox1.Text, out DateTime dateValue) ? dateValue : DateTime.MinValue,
                    Score = comboBox1.Text == "Pass" ? (int)(0.6 * 100) : 0, // 60% من 100 كقيمة افتراضية إذا كان Pass، و0 إذا لم يكن
                    TestResult = comboBox1.Text,
                    RetryCount = null,
                    QuestionCount = 100, // تعيين قيمة افتراضية للأسئلة (غيرها إذا كان لديك قيمة مختلفة)
                    Description = string.Empty,
                    PersonId = null
                };
                bool success = false;
                if (clsTest.AddTest(clsTest))
                {
                    MessageBox.Show("Test Add It Succeeded.");
                    success = true;
                }
                else
                {
                    MessageBox.Show("Test Not Add It.");
                }
            }
            else if (TestType == "Driving Test Appointments")
            {
                label7.Text=30.ToString();
                clsTest clsTest = new clsTest
                {
                    ApplicationId = IdofApplication,
                    TestTypeId = 3,
                    TestDate = DateTime.TryParse(maskedTextBox1.Text, out DateTime dateValue) ? dateValue : DateTime.MinValue,
                    Score = comboBox1.Text == "Pass" ? (int)(0.6 * 100) : 0, // 60% من 100 كقيمة افتراضية إذا كان Pass، و0 إذا لم يكن
                    TestResult = comboBox1.Text,
                    RetryCount = null,
                    QuestionCount = 100, // تعيين قيمة افتراضية للأسئلة (غيرها إذا كان لديك قيمة مختلفة)
                    Description = string.Empty,
                    PersonId = null
                };
                bool success = false;
                if (clsTest.AddTest(clsTest))
                {
                    MessageBox.Show("Test Add It Succeeded.");
                    success = true;
                }
                else
                {
                    MessageBox.Show("Test Not Add It.");
                }
            }



        }
    }
}
