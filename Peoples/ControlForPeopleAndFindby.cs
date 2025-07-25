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

namespace _3tr.Peoples
{
    public partial class ControlForPeopleAndFindby : UserControl
    {
        public int idForthePerson { get; set; }
        public ControlForPeopleAndFindby()
        {
            InitializeComponent();
            filterContactsList();
            
        }

        private void richTextBoxAddress_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            frmAddPerson frm = new frmAddPerson(-1);
            frm.ShowDialog();
        }

        private void filterContactsList()
        {
            comboBox1.Items.Add("PersonId");
            comboBox1.Items.Add("NationalID");
            comboBox1.SelectedIndex = 0;
            linkLabelEditPerson.Enabled = false;
        }

        private void ControlForPeopleAndFindby_Load(object sender, EventArgs e)
        {
        }

        private void buttonFilter_Click()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a filter value.");
                return;
            }

            string selectedFilter = comboBox1.SelectedItem.ToString();
            string filterValue = textBox1.Text.Trim();

            try
            {
                switch (selectedFilter)
                {
                    case "PersonId":
                        if (int.TryParse(filterValue, out int personId))
                        {
                            var person = clsPersonBusiness.GetPersonById(personId);
                            if (person != null)
                            {
                                idForthePerson = personId;
                                UpdateFields(person);
                                linkLabelEditPerson.Enabled = true; // Enable the edit link if a person is found

                                // Attach the event handler for the link click dynamically
                                linkLabelEditPerson.LinkClicked -= linkLabelEditPerson_LinkClicked; // Remove any existing handlers
                                linkLabelEditPerson.LinkClicked += linkLabelEditPerson_LinkClicked;
                            }
                            else
                            {
                                MessageBox.Show("No person found with ID: " + personId);

                                linkLabelEditPerson.Enabled = false; // Disable the edit link if no person is found        
                                linkLabelEditPerson.LinkClicked -= linkLabelEditPerson_LinkClicked; // Remove any existing handlers
                                ClearFields();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid numeric Person ID.");
                            ClearFields();
                        }
                        break;

                    case "NationalID":
                        var resultByNationalId = clsPersonBusiness.GetPersonByNationalID(filterValue);
                        if (resultByNationalId != null && resultByNationalId.Count > 0)
                        {
                            // Assuming the first result is used
                            UpdateFields(resultByNationalId.First());
                        }
                        else
                        {
                            MessageBox.Show("No person found with National ID: " + filterValue);
                            ClearFields();
                        }
                        break;
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("NullReferenceException: " + ex.Message);
                MessageBox.Show("Error: Data not found or invalid. Check the input.");
                ClearFields();
            }
            catch (FormatException ex)
            {
                Console.WriteLine("FormatException: " + ex.Message);
                MessageBox.Show("Error: Invalid data format.");
                ClearFields();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
                ClearFields();
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            buttonFilter_Click();
        }

        private void UpdateFields(clsPersonBusiness person)
        {
            // تعيين PersonId
            textBoxPersonID.Text = person.PersonId.ToString();

            // تعيين القيم الأخرى مع التحقق من null
            textBoxNationalID.Text = person.NationalID ?? string.Empty;
            textBoxFullName.Text = person.FullName ?? string.Empty;
            textBoxEmail.Text = person.Email ?? string.Empty;
            textBoxNationality.Text = person.Nationality ?? string.Empty;
            textBoxPhoneNumber.Text = person.Phone ?? string.Empty;

            

            // تعيين تاريخ الميلاد مع التحقق
            textBoxDateOfBirth.Text = person.DateOfBirth.ToString("yyyy-MM-dd");

            // تعيين العنوان
            richTextBoxAddress.Text = person.Address ?? string.Empty;

            // تعيين الصورة مع التحقق من المسار
            if (!string.IsNullOrEmpty(person.Photo) && System.IO.File.Exists(person.Photo))
            {
                pictureBox3.ImageLocation = person.Photo;
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pictureBox3.Image = null;
                pictureBox3.ImageLocation = string.Empty;
                Console.WriteLine("Invalid or missing photo path: " + (person.Photo ?? "null"));
            }
        }

        private void ClearFields()
        {
            textBoxPersonID.Text = string.Empty;
            textBoxNationalID.Text = string.Empty;
            textBoxFullName.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
            textBoxNationality.Text = string.Empty;
            textBoxPhoneNumber.Text = string.Empty;
            textBoxDateOfBirth.Text = string.Empty;
            richTextBoxAddress.Text = string.Empty;
            pictureBox3.Image = null;
            pictureBox3.ImageLocation = string.Empty;
        }

        private void linkLabelEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (int.TryParse(textBoxPersonID.Text, out int personId))
            {
                frmAddPerson frm = new frmAddPerson(personId);
                frm.ShowDialog();
            }
        }
    }
}