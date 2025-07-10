using System;
using System.Windows.Forms;

namespace SmarterBackup.UI
{
    public partial class RegisterForm : Form
    {
        private readonly UserService _userService;

        public RegisterForm()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;
            var confirm = txtConfirm.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("نام کاربری و رمز عبور نمی‌تواند خالی باشد");
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("رمز عبور با تکرار آن مطابقت ندارد");
                return;
            }

            if (_userService.RegisterUser(username, password))
            {
                MessageBox.Show("ثبت‌نام موفق بود. اکنون وارد شوید.");
                this.Hide();
                new LoginForm().ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("این نام کاربری قبلاً وجود دارد");
            }
        }
    }
}
