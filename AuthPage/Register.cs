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
    public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();
            textBoxPassword.UseSystemPasswordChar = true;
            textBoxConfirmPassword.UseSystemPasswordChar = true;
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            // Registration new user 
            
            string username = textBoxUserName.Text;
            string password = textBoxPassword.Text;
            string confirmPassword = textBoxConfirmPassword.Text;
            var user = clsUsers.GetUsersByUserName(username).FirstOrDefault();
            if (user != null)
            {
                MessageBox.Show("Username already exists. Please choose a different username.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password cannot be empty.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create an instance of clsUsers to call the Save method
            var newUser = new clsUsers
            {
                UserName = username,
                UserPassword = password,
                Role = "User", // Default role
                Description = "New user registration"
            };

            if (newUser.Save(newUser))
            {
                MessageBox.Show("Registration successful! You can now log in.", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

              

            }
            else
            {
                MessageBox.Show("An error occurred while saving the user. Please try again.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void labelShowPassword_Click(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = !textBoxPassword.UseSystemPasswordChar;
            textBoxConfirmPassword.UseSystemPasswordChar = !textBoxConfirmPassword.UseSystemPasswordChar;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
