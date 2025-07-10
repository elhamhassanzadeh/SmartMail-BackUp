using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                this.Hide(); // فرم لاگین رو مخفی کن
                var mainForm = new Form1();
                mainForm.ShowDialog(); // فرم اصلی را باز کن
                this.Close(); // بعد از بسته شدن فرم اصلی، لاگین هم بسته بشه
            }
            else
            {
                MessageBox.Show("نام کاربری یا رمز عبور اشتباه است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
