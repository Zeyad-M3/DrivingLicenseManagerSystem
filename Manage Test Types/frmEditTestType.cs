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

namespace _3tr.Manage_Test_Types
{
    public partial class frmEditTestType : Form
    {
        private int _numofEdate;
        public frmEditTestType(int numofEdate)
        {
            _numofEdate = numofEdate;
            InitializeComponent();
            FillThetextBox();   
        }

        void FillThetextBox()
        {
            // font size for textBoxs = 12 bold
            textBox1.Font = new Font(textBox1.Font.FontFamily, 12, FontStyle.Bold);
            textBox2.Font = new Font(textBox2.Font.FontFamily, 12, FontStyle.Bold);
            textBox3.Font = new Font(textBox3.Font.FontFamily, 12, FontStyle.Bold);
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, 11, FontStyle.Bold);
            // استرجاع بيانات نوع الاختبار
            ContactsBusinessLayer.clsTestTypeBusiness testType = clsTestTypeBusiness.GetTestTypeById(_numofEdate);
            if (testType != null)
            {
                textBox1.Text = testType.TestTypeID.ToString();
                textBox1.Enabled = false; // تعطيل مربع النص لـ TestTypeID
                textBox2.Text = testType.TestTypeName;
                richTextBox1.Text = testType.TestDescription;
                textBox3.Text = testType.TestFees.ToString();
            }
            else
            {
                MessageBox.Show("Error retrieving test type details.");
                this.Close(); // إغلاق الفورم إذا لم يتم العثور على البيانات
            }
        }

        private void SaveChanges()
        {
            int testTypeId = int.Parse(textBox1.Text);
            string testTypeName = textBox2.Text.Trim();
            string testDescription = richTextBox1.Text.Trim();
            decimal testFees = string.IsNullOrEmpty(textBox3.Text) ? 0m : decimal.Parse(textBox3.Text);

            // التحقق من القيم
            if (string.IsNullOrEmpty(testTypeName))
            {
                MessageBox.Show("Error: TestTypeName cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(testDescription))
            {
                MessageBox.Show("Error: TestDescription cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (testFees < 0)
            {
                MessageBox.Show("Error: TestFees cannot be negative.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // إنشاء كائن التحديث
            clsTestTypeBusiness testTypeForUpdate = new clsTestTypeBusiness
            {
                TestTypeID = testTypeId,
                TestTypeName = testTypeName,
                TestDescription = testDescription,
                TestFees = testFees
            };
            // تنفيذ التحديث
            bool isUpdated = clsTestTypeBusiness.UpdateTestType(testTypeForUpdate);
            if (isUpdated)
            {
                MessageBox.Show("Test type updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // إغلاق الفورم بعد التحديث الناجح
            }
            else
            {
                MessageBox.Show("Error updating test type.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void frmEditTestType_Load(object sender, EventArgs e)
        {

            FillThetextBox();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }
    }
}
