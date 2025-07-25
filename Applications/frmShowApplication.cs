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
    public partial class frmShowApplication : Form
    {
        public int IdofApplication { get; set; }
        public frmShowApplication(int numIdApplication)
        {
            IdofApplication = numIdApplication;
            InitializeComponent();
            LoadUserControl();


        }

        // class to fill all the text in the control
        private void LoadUserControl()
        {
            // إنشاء مثيل لـ UserControl مع تمرير IdofApplication
            ShowApplication userControl = new ShowApplication(IdofApplication);

            // تحديد موقع التحكم في الفورم
            userControl.Location = new System.Drawing.Point(10, 10); // يمكنك تعديل الموقع حسب الحاجة

            // إضافة التحكم إلى الفورم
            this.Controls.Add(userControl);
        }
    

       

        private void frmShowApplication_Load(object sender, EventArgs e)
        {

        }
    }
}
