using ContactsBusinessLayer;
using System;
using System.Windows.Forms;

namespace _3tr
{
    public partial class FrmAddEitUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _UserID;

        public FrmAddEitUser(int UserID)
        {
            _UserID = UserID;
            if (UserID == -1)
            {
                _Mode = enMode.AddNew; // وضع الإضافة الجديدة
            }
            else
            {
                _Mode = enMode.Update; // وضع التحديث
            }
            InitializeComponent();
        }

        private void FrmAddEitUser_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update && _UserID > 0)
            {
                clsUsers existingUser = clsUsers.GetUserByID(_UserID);
                if (existingUser != null)
                {
                    textBox1.Enabled = false;
                    textBox1.Text = existingUser.UserId.ToString();
                    textBox2.Text = existingUser.UserName;
                    textBoxRole.Text = existingUser.Role;
                    textBoxDescription.Text = existingUser.Description;
                    textBox3.Text = existingUser.UserPassword;
                }
                else
                {
                    MessageBox.Show("User not found.");
                    _Mode = enMode.AddNew; // إذا ما لقيتش المستخدم، انقل لـ AddNew
                }
            }

            else if (_Mode == enMode.AddNew)
            {
                textBox1.Enabled = false;
                textBox1.Text = "Auto"; // إشارة إن UserId هيتولد تلقائيًا
                textBox2.Text = "";
                textBoxRole.Text = "";
                textBoxDescription.Text = "";
                textBox3.Text = "";
            }
        }

        private void ModeForApp(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                SaveUser();
            }
            else
            {
                UpdateUser();
            }
        }

        private void UpdateUser()
        {
            try
            {
                clsUsers existingUser = clsUsers.GetUserByID(_UserID);

                if (existingUser != null)
                {
                    // تحديث الكائن بالبيانات المعدلة من الحقول
                    existingUser.UserName = textBox2.Text;
                    existingUser.Role = textBoxRole.Text;
                    existingUser.Description = textBoxDescription.Text;
                    existingUser.UserPassword = textBox3.Text; // تأكد من تشفير كلمة المرور إذا لزم

                    // استدعاء Save بتمرير الكائن المحدث
                    if (existingUser.Save(existingUser))
                    {
                        MessageBox.Show("User updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Error updating user. Check constraints or data.");
                    }
                }
                else
                {
                    MessageBox.Show("User not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}");
            }
        }

        private void SaveUser()
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text)||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBoxRole.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return; // الخروج إذا كانت الحقول فارغة
            }

            clsUsers newUser = new clsUsers
            {

                UserId = 0, // صفر للسماح بتوليد المفتاح التلقائي
                UserName = textBox2.Text,
                UserPassword = textBox3.Text,
                Description = textBoxDescription.Text,
                Role = textBoxRole.Text
            };
            if (newUser.Save(newUser))
            {
                MessageBox.Show("User saved successfully!");
                _UserID = newUser.UserId; // تحديث _UserID بالقيمة الجديدة
                _Mode = enMode.Update; // انقل لـ Update بعد الإضافة
                textBox1.Text = _UserID.ToString(); // عرض UserId الجديد
            }
            else
            {
                MessageBox.Show("Error saving user.");
            }
        }

        private void pictureBoxSave_Click(object sender, EventArgs e)
        {
            ModeForApp(sender, e);
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}