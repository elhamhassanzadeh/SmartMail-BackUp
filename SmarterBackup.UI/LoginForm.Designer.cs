namespace SmarterBackup.UI
{
    partial class LoginForm
    {
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;

        private void InitializeComponent()
        {
            lblUsername = new Label();
            lblPassword = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.Location = new Point(30, 30);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(100, 23);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "نام کاربری:";
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(30, 70);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 23);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "رمز عبور:";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(136, 30);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(150, 23);
            txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(136, 67);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(150, 23);
            txtPassword.TabIndex = 3;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(120, 110);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(75, 23);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "ورود";
            btnLogin.Click += btnLogin_Click;
            // 
            // LoginForm
            // 
            ClientSize = new Size(320, 170);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ورود به برنامه";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}