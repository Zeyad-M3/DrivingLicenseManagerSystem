using ContactsBusinessLayer;
using System;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using System.IO;


namespace _3tr
{
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            // إعداد كلمة المرور لإخفائها بشكل افتراضي

            textBoxPassword.UseSystemPasswordChar = true;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void PerformLogin()
        {
            string userName = textBoxUserName.Text.Trim();
            string password = textBoxPassword.Text;

            var user = clsUsers.GetUsersByUserName(userName)
                               .FirstOrDefault(u => u.UserPassword == password);

            if (user != null)
            {
                string filePath = @"E:\repos\19 Project course\3tr\app materya\voices\ElevenLabs_Text_to_Speech_audio.wav";

                if (File.Exists(filePath))
                {
                    using (SoundPlayer player = new SoundPlayer(filePath))
                    {
                        player.Play();
                    }

                }
                else
                {
                    MessageBox.Show("Voice file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                MainApp mainApp = new MainApp();
                mainApp.Show();

                this.FindForm().Hide();




                // يمكنك إضافة أي إجراءات إضافية هنا بعد تسجيل الدخول الناجح
            }
            else
            {
                MessageBox.Show("Login failed! Please check your username and password.",
                                "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void labelShowPassword_Click(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = !textBoxPassword.UseSystemPasswordChar;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // لا شيء هنا (لا تحاول تسجيل الدخول عند تحميل الصفحة)
        }

        private void labelRegister_Click(object sender, EventArgs e)
        {

            FrmRegister Register = new FrmRegister();
            Register.ShowDialog();





        }

        private void labelRemember_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxRemember_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRemember.Checked)
            {
                //code will be here from file not database 
            }
            else
            {
                //code will be here
            }
        }
    }
}
