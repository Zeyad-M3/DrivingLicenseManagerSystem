using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsBusinessLayer;

namespace _3tr.Applications
{
    public partial class frmIssueDrivingLicenseForTheFirstTime : Form
    {
        public int IdofApplication { get; set; }
        public frmIssueDrivingLicenseForTheFirstTime(int numIdApplication)
        {
            IdofApplication = numIdApplication;
            InitializeComponent();
            LoadUserControls();
        }

        private void LoadUserControls()
        {
            ShowApplication userControl = new ShowApplication(IdofApplication);

            // تحديد موقع التحكم في الفورم
            userControl.Location = new System.Drawing.Point(10, 150); // يمكنك تعديل الموقع حسب الحاجة

            // إضافة التحكم إلى الفورم
            this.Controls.Add(userControl);

            DrivingLicenseApplicationInfo usercontrolfordriving = new DrivingLicenseApplicationInfo(IdofApplication);
            usercontrolfordriving.Location = new System.Drawing.Point(10, 30);
            this.Controls.Add(usercontrolfordriving);
        }

        private void IssueDrivingLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {

        }

        public void CheckTestValidation()
        {
            clsTest test = new clsTest { ApplicationId = IdofApplication };

            bool isValid = clsTest.ValidateTestBeforeIssue(test);

            if (isValid)
            {
                MessageBox.Show("Success", "Validation Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine($"Validation successful for ApplicationId {IdofApplication}: All 3 tests passed.");
                // هنا يمكنك إضافة الكود الذي يقوم بإصدار رخصة القيادة
                clsLicense license = new clsLicense
                {
                    // Replace this line:
                    // LicenseNumber = "DL" + IdofApplication.ToString("D6"), // توليد رقم رخصة فريد

                    // With this line:
                    PersonId = clsApplication.GetApplicationById(IdofApplication).PersonID, // تعيين معرف الشخص من التطبيق
                    LicenseNumber = IdofApplication.ToString(), // استخدم رقم الطلب كرقم الرخصة (int)
                   
                    IssueStatus= "Issued New", // حالة الإصدار
                    IssueDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddYears(5) ,// تعيين تاريخ انتهاء الصلاحية بعد 5 سنوات
                    // إضافة الرخصة إلى قاعدة البيانات
                };
              
                // With this line:
                if (license.Save(license))
                {
                    // License issued successfully + id of the new license
                    MessageBox.Show($"License issued successfully with ID: {license.LicenseId}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to issue license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("U have to pass all the 3 tests", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine($"Validation failed for ApplicationId {IdofApplication}: Not all 3 tests passed.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckTestValidation();
        }
    }
}
