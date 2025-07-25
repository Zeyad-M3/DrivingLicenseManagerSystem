using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3tr
{
    public partial class frmGlobalCurrentUserInfo : Form
    {
        string _userName;
        string _userPassword;
        public frmGlobalCurrentUserInfo(string userName, string userPassword)
        {
            InitializeComponent();
            _userName = userName;
            _userPassword = userPassword;
            LoadUserInfo();
        }
        private void LoadUserInfo()
        {
            // هنا يمكنك تحميل معلومات المستخدم وعرضها في النموذج
            label3.Text = _userName;
            label4.Text = _userPassword;
            // يمكنك إضافة المزيد من المعلومات حسب الحاجة
        }

        private void frmGlobalCurrentUserInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
