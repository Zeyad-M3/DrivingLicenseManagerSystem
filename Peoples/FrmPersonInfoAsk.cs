using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ContactsBusinessLayer; // افتراض أن الطبقة موجودة

namespace _3tr
{
    public partial class frmAddPerson : Form
    {
        private string imagePath = string.Empty; // لتخزين مسار الصورة

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        int _PersonID;
        public frmAddPerson(int PersonID)
        {
            _PersonID = PersonID;
            if (PersonID == -1)
            {
                _Mode = enMode.AddNew; // وضع الإضافة الجديدة
            }
            else
            {
                _Mode = enMode.Update; // وضع التحديث
            }
            InitializeComponent();
        }

        private void frmAddPerson_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                UpdatePerson(); // استدعاء تحديث البيانات تلقائيًا عند فتح الفورم لو في وضع Update
            }
        }

        private void BtnUploadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All Files (*.*)|*.*";
                openFileDialog.Title = "Select an Image";
                openFileDialog.InitialDirectory = @"C:\Users\YourUsername\Pictures"; // عدل هذا لو أردت

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog.FileName;
                    pictureBox3.ImageLocation = imagePath; // عرض الصورة داخل PictureBox
                    MessageBox.Show($"Image selected: {imagePath}");
                }
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModeForApp(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                SavePerson(); // حفظ البيانات عند الضغط على زر الحفظ
            }
            else
            {
                UpdatePerson(); // تحديث البيانات عند الضغط على زر التحديث
            }
        }

        private void UpdatePerson()
        {
            clsPersonBusiness person = clsPersonBusiness.GetPersonById(_PersonID);
            if (person == null)
            {
                MessageBox.Show("Person not found.");
                this.Close();
                return;
            }

            // تعبئة الحقول بالبيانات الحالية
            textBoxPersonID.Text = person.PersonId.ToString();
            textBoxNationalID.Text = person.NationalID;
            textBoxFullName.Text = person.FullName;
            textBoxEmail.Text = person.Email;
            textBoxPhoneNumber.Text = person.Phone;
            textBoxNationality.Text = person.Nationality;
            textBoxDateOfBirth.Text = person.DateOfBirth.ToString("yyyy-MM-dd"); // تنسيق التاريخ
            richTextBoxAddress.Text = person.Address;
            pictureBox3.ImageLocation = person.Photo; // عرض الصورة الحالية
            imagePath = person.Photo; // حفظ مسار الصورة الحالية
        }

        private void SavePerson()
        {
            try
            {
                // التحقق من تعبئة الحقول المطلوبة
                if (string.IsNullOrWhiteSpace(textBoxPersonID.Text) ||
                    string.IsNullOrWhiteSpace(textBoxNationalID.Text) ||
                    string.IsNullOrWhiteSpace(textBoxFullName.Text) ||
                    string.IsNullOrWhiteSpace(textBoxEmail.Text) ||
                    string.IsNullOrWhiteSpace(textBoxPhoneNumber.Text) ||
                    string.IsNullOrWhiteSpace(textBoxDateOfBirth.Text) ||
                    string.IsNullOrWhiteSpace(richTextBoxAddress.Text))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return;
                }

                // تحويل Person ID إلى رقم
                if (!int.TryParse(textBoxPersonID.Text.Trim(), out int personId))
                {
                    MessageBox.Show("Person ID must be a valid number.");
                    return;
                }
                var existingPerson = clsPersonBusiness.GetPersonById(personId);
                if (existingPerson != null && _Mode == enMode.AddNew)
                {
                    MessageBox.Show("A person with this ID already exists.");
                    return;
                }
                else if (existingPerson != null && _Mode == enMode.Update && existingPerson.PersonId != personId)
                {
                    MessageBox.Show("Cannot update to a different Person ID.");
                    return;
                }

                string nationalId = textBoxNationalID.Text.Trim();
                if (string.IsNullOrEmpty(nationalId))
                {
                    MessageBox.Show("National ID cannot be empty!");
                    return;
                }

                var existingPersonWithNationalId = clsPersonBusiness.GetPersonByNationalID(nationalId);
                if (existingPersonWithNationalId != null && existingPersonWithNationalId.Count > 0) // التحقق من وجود عناصر
                {
                    if (_Mode == enMode.AddNew)
                    {
                        MessageBox.Show("A person with this National ID already exists.");
                        return;
                    }
                    else if (_Mode == enMode.Update)
                    {
                        var currentPerson = clsPersonBusiness.GetPersonById(personId);
                        if (currentPerson != null && currentPerson.NationalID != nationalId)
                        {
                            MessageBox.Show("A person with this National ID already exists.");
                            return;
                        }
                    }
                }

                string fullName = textBoxFullName.Text.Trim();
                string email = textBoxEmail.Text.Trim();
                if (!email.Contains("@") || !email.Contains("."))
                {
                    MessageBox.Show("Please enter a valid email address.");
                    errorProvider1.SetError(textBoxEmail, "Invalid email format.");
                    return;
                }

                string phoneNumber = textBoxPhoneNumber.Text.Trim();
                string nationality = textBoxNationality.Text.Trim();
                string address = richTextBoxAddress.Text.Trim();

                // التحقق من صحة تاريخ الميلاد
                if (!DateTime.TryParse(textBoxDateOfBirth.Text.Trim(), out DateTime dateOfBirth))
                {
                    MessageBox.Show("Invalid Date of Birth.");
                    return;
                }

                if (dateOfBirth > DateTime.Now)
                {
                    MessageBox.Show("Date of Birth cannot be in the future.");
                    return;
                }

                // التحقق من الصورة (اختياري)
                string photoPath = string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath) ? null : imagePath;

                // إنشاء الكائن لطبقة البيانات
                clsPersonBusiness personData = new clsPersonBusiness
                {
                    PersonId = personId,
                    NationalID = nationalId,
                    FullName = fullName,
                    Email = email,
                    Phone = phoneNumber,
                    Address = address,
                    Nationality = nationality,
                    DateOfBirth = dateOfBirth,
                    Photo = photoPath // تأكد إن PhotoPath في clsPerson يطابق هذا
                };

                // حفظ أو تحديث البيانات مباشرة
                bool success;
                if (_Mode == enMode.AddNew)
                {
                    clsPersonBusiness.AddPerson(personData); // افتراض إنها تعمل
                    success = true;
                }
                else if (_Mode == enMode.Update)
                {
                    success = clsPersonBusiness.UpdatePerson(personData);

                }
                else
                {
                    MessageBox.Show("Invalid mode.");
                    return;
                }

                if (success)
                {
                    MessageBox.Show(_Mode == enMode.AddNew ? "Person added successfully!" : "Person updated successfully!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to save person. Please try again. Check database constraints or logs.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving person: " + ex.Message);
            }
        }

        private void linkLabelSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BtnUploadImage_Click(sender, e); // لفتح نافذة اختيار الصورة
        }

        private void pictureBoxSave_Click(object sender, EventArgs e)
        {
            SavePerson(); // حفظ البيانات عند الضغط على زر الحفظ
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
        }

        private void textBoxPersonID_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
        }
    }
}