using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3tr.Applications
{
    public partial class frmVisionTestAppointments : Form
    {
        public int IdofApplication { get; set; }
        public string TypeOfTest { get; set; }
        public frmVisionTestAppointments(int numIdApplication,string Typeoftest)
        {
            IdofApplication = numIdApplication;
            TypeOfTest= Typeoftest;
            InitializeComponent();
            LoadUserControl ();
        }
        private void LoadUserControl()
        {

            if(TypeOfTest == "Vision Test Appointments")
            {
                label1.Text = "Vision Test Appointments";
            }
            else if (TypeOfTest == "Written Test Appointments")
            {
                label1.Text = "Written Test Appointments";
                pictureBox1.Visible = false; // إخفاء PictureBox إذا كان نوع الاختبار هو Hearing Test
            }
            else if (TypeOfTest == "Driving Test Appointments")
            {
                label1.Text = "Driving Test Appointments";
                pictureBox1.Visible = false; // إخفاء PictureBox إذا كان نوع الاختبار هو Hearing Test

            }
            else
            {
                this.Text = "Unknown Test Type";

            }
            // إنشاء مثيل لـ UserControl مع تمرير IdofApplication
            ShowApplication userControl = new ShowApplication(IdofApplication);

            // تحديد موقع التحكم في الفورم
            userControl.Location = new System.Drawing.Point(10, 200); // يمكنك تعديل الموقع حسب الحاجة

            // إضافة التحكم إلى الفورم
            this.Controls.Add(userControl);
            DrivingLicenseApplicationInfo usercontrolfordriving = new DrivingLicenseApplicationInfo(IdofApplication);
            usercontrolfordriving.Location= new System.Drawing.Point(10,100);
            this.Controls.Add (usercontrolfordriving);  
        }


        private void frmVisionTestAppointments_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmScheduleTest frmScheduleTest = new frmScheduleTest(IdofApplication, TypeOfTest);
            frmScheduleTest.ShowDialog();
        }
    }
}
