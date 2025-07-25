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

namespace _3tr
{
    public partial class frmPeople : Form
    {
        public frmPeople()
        {
            InitializeComponent();
            filterContactsList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            // يمكن إضافة منطق هنا (مثل عرض تفاصيل الشخص)
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void frmPeople_Load(object sender, EventArgs e) { }

        private void filterContactsList()
        {
            comboBox1.Items.Add("PersonId");
            comboBox1.Items.Add("NationalID");
            comboBox1.Items.Add("Nationality");
            comboBox1.SelectedIndex = 0; // اختيار افتراضي
            textBox1.Text = string.Empty; // Clear the filter text box
            LoadAllPersons();
            CountOfDatatable(); // Count the number of rows in the DataGridView and display it
            this.Size = new Size(1200, 700); // Set the size of the form
            // size is fixed
            this.MinimumSize = new Size(1200, 700); // Set the minimum size of the form
            this.MaximumSize = new Size(1200, 700); // Set the maximum size of the form
        }

        private void LoadAllPersons()
        {
            try
            {
                dataGridView1.DataSource = clsPersonBusiness.GetAllPersons();

                if (dataGridView1.DataSource == null)
                {
                    MessageBox.Show("No data available from GetAllPersons.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading persons: " + ex.Message);
            }
        }

        void CountOfDatatable()
        {
            int count = dataGridView1.Rows.Count;
            CountOfDatatable countOfDatatable = new CountOfDatatable(count);
            countOfDatatable.Dock = DockStyle.Bottom;
            this.Controls.Add(countOfDatatable);


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
                                dataGridView1.DataSource = new List<clsPersonBusiness> { person }; // تحويل الكائن إلى قائمة
                            }
                            else
                            {
                                dataGridView1.DataSource = null;
                                MessageBox.Show("No person found with ID: " + personId);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid numeric Person ID.");
                        }
                        break;

                    case "NationalID":
                        var resultByNationalId = clsPersonBusiness.GetPersonByNationalID(filterValue); // افتراض وجود هذه المنهجية
                        dataGridView1.DataSource = resultByNationalId;
                        if (resultByNationalId == null || resultByNationalId.Count == 0)
                        {
                            MessageBox.Show("No persons found with National ID: " + filterValue);
                        }
                        break;

                    case "Nationality":
                        var resultByNationality = clsPersonBusiness.GetPersonsByNationality(filterValue);
                        dataGridView1.DataSource = resultByNationality;
                        if (resultByNationality == null || resultByNationality.Count == 0)
                        {
                            MessageBox.Show("No persons found with Nationality: " + filterValue);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering data: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty; // مسح النص عند تغيير الخيار
            dataGridView1.DataSource = clsPersonBusiness.GetAllPersons(); // إعادة تحميل جميع الأشخاص
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // يمكن إضافة تصفية فورية هنا إذا أردت
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonFilter_Click();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmAddPerson frm = new frmAddPerson(-1);
            frm.ShowDialog();
        }

        private void test2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddPerson frm = new frmAddPerson(-1);
            frm.ShowDialog();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.");
        }

        private void honeCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.");

        }

        private void editeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddPerson frm = new frmAddPerson((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            LoadAllPersons(); // إعادة تحميل الأشخاص بعد التعديل
        }

        private void deletPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Text = "Are you sure you want to delete this person? [" + dataGridView1.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsPersonBusiness.Delete((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person deleted successfully.");
                    LoadAllPersons(); // إعادة تحميل الأشخاص بعد الحذف
                }
                else
                {
                    MessageBox.Show("Error deleting person.");
                }
            }
        }
    }
}