using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsBusinessLayer; // Assuming this namespace contains the clsUsers class

namespace _3tr.Users
{
    public partial class frmChangePass : Form
    {
        string Password;
        string UserName;
        public frmChangePass(string password, string userName)
        {
            InitializeComponent();
            Password = password;
            UserName = userName;
            LoadUserInfo();
        }
        void LoadUserInfo()
        {
            label3.Text = UserName;
            label4.Text = Password;
        }
        void ChangePassword()
        {
            string newPassword = textBox1.Text.Trim();
            string confirmPassword = textBox2.Text.Trim();
            
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please enter both new password and confirm password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Update the user's password in the database
            
            var user = clsUsers.GetUsersByUserName(UserName).FirstOrDefault();
            if (user == null)
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else    
            {
               

                var users = new clsUsers
                {
                    UserPassword = confirmPassword,
                    UserId = user.UserId,
                    UserName = user.UserName, // Use user.UserName instead of user.UserFullName
                    Role = user.Role,
                    Description = user.Description
                };

                clsUsers.Update(users);
                MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            ;

           
        }


        private void frmChangePass_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangePassword();
        }
    }
}
