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
    public partial class Frmusers : Form
    {
        public Frmusers()
        {
            InitializeComponent();
            _RefreshContactsList();
            filterContactsList();

        }

        void _RefreshContactsList()
        {
            // add all users to data 
            dataGridView1.DataSource = clsUsers.GetAllUsers();
            CountOfDatatable(); // Count the number of rows in the DataGridView and display it

            this.Size = new Size(800, 700); // Set the size of the form
            // size is fixed
            this.MinimumSize = new Size(800, 700); // Set the minimum size of the form
            this.MaximumSize = new Size(800, 700); // Set the maximum size of the form

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void filterContactsList()
        {
            comboBox1.Items.Add("All Users");
            comboBox1.Items.Add("UserId");
            comboBox1.Items.Add("UserName");
            comboBox1.Items.Add("Role");
            comboBox1.SelectedIndex = 0; // اختيار افتراضي
            textBox1.Text = string.Empty; // Clear the filter text box
            
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                _RefreshContactsList();
                return;
            }
            string filterText = textBox1.Text.Trim();
            string selectedFilter = comboBox1.SelectedItem.ToString();
            List<clsUsers> filteredUsers = new List<clsUsers>();
            switch (selectedFilter)
            {
                case "UserId":
                    if (int.TryParse(filterText, out int userId))
                    {
                        var user = clsUsers.GetUserByID(userId);
                        if (user != null)
                        {
                            filteredUsers.Add(user);
                        }
                    }
                    break;
                case "UserName":
                    filteredUsers = clsUsers.GetUsersByUserName(filterText);
                    break;
                case "Role":
                    filteredUsers = clsUsers.GetUsersByRole(filterText);
                    break;
                case "All Users":
                    filteredUsers = clsUsers.GetAllUsers();
                    break;
            }
            dataGridView1.DataSource = filteredUsers;
            if (dataGridView1.DataSource == null || ((List<clsUsers>)dataGridView1.DataSource).Count == 0)
            {
                MessageBox.Show("No users found matching the filter criteria.");
            }


        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddEitUser frmAddEitUser = new FrmAddEitUser(-1); // -1 indicates a new user
            frmAddEitUser.ShowDialog();
            _RefreshContactsList(); // Refresh the list after adding a new user
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.");
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FrmAddEitUser frmAddEitUser = new FrmAddEitUser(-1); // -1 indicates a new user
            frmAddEitUser.ShowDialog();
            _RefreshContactsList(); // Refresh the list after adding a new user
        }

        private void test_Click(object sender, EventArgs e)
        {
            FrmAddEitUser user = new FrmAddEitUser((int)dataGridView1.CurrentRow.Cells[0].Value);
            user.ShowDialog();
            _RefreshContactsList(); // Refresh the list after updating a user

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete contact [" + dataGridView1.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsUsers.DeleteUser((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Contact Deleted Successfully.");
                    _RefreshContactsList();
                }

                else
                    MessageBox.Show("Contact is not deleted.");

            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            FrmAddEitUser user = new FrmAddEitUser((int)dataGridView1.CurrentRow.Cells[0].Value);
            user.ShowDialog();
            _RefreshContactsList(); // Refresh the list after updating a user


        }
        void CountOfDatatable()
        {
            int count = dataGridView1.Rows.Count;
            CountOfDatatable countOfDatatable = new CountOfDatatable(count);
            countOfDatatable.Dock = DockStyle.Bottom;
            this.Controls.Add(countOfDatatable);
        }

        private void Frmusers_Load(object sender, EventArgs e)
        {
           
        }
    }
}
