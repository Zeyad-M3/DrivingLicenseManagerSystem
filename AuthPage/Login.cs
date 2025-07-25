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
            LoadCredentials();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void PerformLogin()
        {
            string userName = textBoxUserName.Text.Trim();
            string password = textBoxPassword.Text;
            SaveCredentials(textBoxUserName.Text, textBoxPassword.Text); // حفظ بيانات الاعتماد الافتراضية
            // التحقق من صحة اسم المستخدم وكلمة المرور
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // load and write in the login the user and password if the user check the remember me checkbox
           




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

                MainApp mainApp = new MainApp( userName , password );
                 
                mainApp.Show();

                this.FindForm().Hide();
                // if MainApp close close all application and the frmLogin
                mainApp.FormClosed += (s, args) => Application.Exit();




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

        // save the user name and password in file if the user check the remember me checkbox
        void SaveCredentials(string userName, string password)
        {
            string filePath = "E:\\repos\\19 Project course\\3tr\\app materya\\credentials.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(userName);
                writer.WriteLine(password);
            }

        }
        // load the user name and password from file if the user check the remember me checkbox
        void LoadCredentials()
        {
            string filePath = "E:\\repos\\19 Project course\\3tr\\app materya\\credentials.txt";
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    textBoxUserName.Text = reader.ReadLine();
                    textBoxPassword.Text = reader.ReadLine();
                }
            }
        }


        private void labelRemember_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxRemember_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
