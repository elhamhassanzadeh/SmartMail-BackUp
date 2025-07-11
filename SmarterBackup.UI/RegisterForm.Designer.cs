using Guna.UI2.WinForms;

namespace SmarterBackup.UI
{
    partial class RegisterForm
    {
        private Guna2TextBox txtUsername;
        private Guna2TextBox txtPassword;
        private Guna2TextBox txtConfirm;
        private Guna2Button btnRegister;
        private Guna2HtmlLabel lblTitle;

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            txtUsername = new Guna2TextBox();
            txtPassword = new Guna2TextBox();
            txtConfirm = new Guna2TextBox();
            btnRegister = new Guna2Button();
            lblTitle = new Guna2HtmlLabel();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.BorderRadius = 10;
            txtUsername.CustomizableEdges = customizableEdges1;
            txtUsername.DefaultText = "";
            txtUsername.Font = new Font("Segoe UI", 9F);
            txtUsername.Location = new Point(70, 70);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "نام کاربری";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtUsername.Size = new Size(250, 40);
            txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.BorderRadius = 10;
            txtPassword.CustomizableEdges = customizableEdges3;
            txtPassword.DefaultText = "";
            txtPassword.Font = new Font("Segoe UI", 9F);
            txtPassword.Location = new Point(70, 120);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.PlaceholderText = "رمز عبور";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtPassword.Size = new Size(250, 40);
            txtPassword.TabIndex = 2;
            // 
            // txtConfirm
            // 
            txtConfirm.BorderRadius = 10;
            txtConfirm.CustomizableEdges = customizableEdges5;
            txtConfirm.DefaultText = "";
            txtConfirm.Font = new Font("Segoe UI", 9F);
            txtConfirm.Location = new Point(70, 170);
            txtConfirm.Name = "txtConfirm";
            txtConfirm.PasswordChar = '●';
            txtConfirm.PlaceholderText = "تکرار رمز عبور";
            txtConfirm.SelectedText = "";
            txtConfirm.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtConfirm.Size = new Size(250, 40);
            txtConfirm.TabIndex = 3;
            // 
            // btnRegister
            // 
            btnRegister.BorderRadius = 15;
            btnRegister.CustomizableEdges = customizableEdges7;
            btnRegister.FillColor = Color.MediumSlateBlue;
            btnRegister.Font = new Font("Segoe UI", 9F);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(120, 230);
            btnRegister.Name = "btnRegister";
            btnRegister.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnRegister.Size = new Size(150, 45);
            btnRegister.TabIndex = 4;
            btnRegister.Text = "ثبت‌نام";
            btnRegister.Click += btnRegister_Click;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.MediumSlateBlue;
            lblTitle.Location = new Point(173, 21);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(32, 27);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ثبت";
            // 
            // RegisterForm
            // 
            ClientSize = new Size(400, 320);
            Controls.Add(lblTitle);
            Controls.Add(txtUsername);
            Controls.Add(txtPassword);
            Controls.Add(txtConfirm);
            Controls.Add(btnRegister);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ثبت‌نام";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
