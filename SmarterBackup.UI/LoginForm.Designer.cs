using Guna.UI2.WinForms;

namespace SmarterBackup.UI
{
    partial class LoginForm
    {
        private Guna2TextBox txtUsername;
        private Guna2TextBox txtPassword;
        private Guna2Button btnLogin;
        private Guna2HtmlLabel lblTitle;

        private void InitializeComponent()
        {
            this.txtUsername = new Guna2TextBox();
            this.txtPassword = new Guna2TextBox();
            this.btnLogin = new Guna2Button();
            this.lblTitle = new Guna2HtmlLabel();

            // 
            // lblTitle
            // 
            this.lblTitle.Text = " سیستم به ورود";
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.DodgerBlue;
            this.lblTitle.Location = new Point(120, 20);

            // 
            // txtUsername
            // 
            this.txtUsername.PlaceholderText = "نام کاربری";
            this.txtUsername.Location = new Point(70, 70);
            this.txtUsername.Size = new Size(250, 40);
            this.txtUsername.BorderRadius = 10;

            // 
            // txtPassword
            // 
            this.txtPassword.PlaceholderText = "رمز عبور";
            this.txtPassword.Location = new Point(70, 120);
            this.txtPassword.Size = new Size(250, 40);
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.BorderRadius = 10;

            // 
            // btnLogin
            // 
            this.btnLogin.Text = "ورود";
            this.btnLogin.Location = new Point(120, 180);
            this.btnLogin.Size = new Size(150, 45);
            this.btnLogin.FillColor = Color.DodgerBlue;
            this.btnLogin.BorderRadius = 15;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // 
            // LoginForm
            // 
            this.ClientSize = new Size(400, 270);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "ورود";
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
        }
    }
}
