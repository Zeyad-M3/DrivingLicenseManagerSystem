using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3tr.Applicationtype
{
    public partial class frmUpdateApplicationType : Form
    {
        private int _numofEdate;

        public frmUpdateApplicationType(int numofEdate)
        {
            _numofEdate = numofEdate;
            InitializeComponent();
        }

        private void FillThetextBox()
        {
            // استرجاع بيانات نوع التطبيق
            ApplicationType applicationTypes = ApplicationType.GetApplicationTypeById(_numofEdate);
            if (applicationTypes != null)
            {
           
                textBox1.Text = applicationTypes.ApplicationTypeId.ToString();
                textBox1.Enabled = false; // تعطيل مربع النص لـ ApplicationTypeId
                textBox2.Text = applicationTypes.ApplicationTypeName;
                textBox3.Text = applicationTypes.ApplicationTypefee.ToString();
            }
            else
            {
                MessageBox.Show("Error retrieving application type details.");
                this.Close(); // إغلاق الفورم إذا لم يتم العثور على البيانات
            }
        }

        private void SaveChanges()
        {
            // أخذ القيم الجديدة من الحقول
            int applicationTypeId = int.Parse(textBox1.Text);
            string applicationTypeName = textBox2.Text.Trim();
            decimal applicationTypeFee = string.IsNullOrEmpty(textBox3.Text) ? 0m : decimal.Parse(textBox3.Text);

            // التحقق من القيم
            if (string.IsNullOrEmpty(applicationTypeName))
            {
                MessageBox.Show("Error: ApplicationTypeName cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // إنشاء كائن التحديث
            ApplicationType applicationTypeForUpdate = new ApplicationType(applicationTypeId, applicationTypeName, applicationTypeFee)
            {
                ApplicationTypeId = applicationTypeId,
                ApplicationTypeName = applicationTypeName,
                ApplicationTypefee = applicationTypeFee
            };

            // تنفيذ التحديث
            List<ApplicationType> updatedResult = ApplicationType.UpdateApplicationType(applicationTypeForUpdate);
            if (updatedResult.Count > 0)
            {
                MessageBox.Show("Update successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillThetextBox(); // إعادة تحميل البيانات لعرض التحديث
                this.Close(); // إغلاق الفورم بعد التحديث الناجح
            }
            else
            {
                MessageBox.Show("Update failed. No changes applied.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            FillThetextBox(); // ملء الحقول عند تحميل الفورم
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close(); // إغلاق الفورم عند النقر على الصورة الأولى
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SaveChanges(); // تنفيذ التحديث عند النقر على الصورة الثانية
        }
    }
}