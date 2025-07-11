using System;
using System.Windows.Forms;

namespace SmarterBackup.UI
{
    public partial class LoginForm : Form
    {
        private readonly UserService _userService;

        public LoginForm()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;

            if (_userService.Authenticate(username, password))
            {
                this.Hide();
                new Form1().ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("نام کاربری یا رمز عبور اشتباه است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
