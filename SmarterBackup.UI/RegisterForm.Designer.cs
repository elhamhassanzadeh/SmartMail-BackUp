namespace SmarterBackup.UI
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblConfirm;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtConfirm;
        private Button btnRegister;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.lblConfirm = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.txtConfirm = new TextBox();
            this.btnRegister = new Button();

            // 
            // lblUsername
            // 
            this.lblUsername.Text = "نام کاربری:";
            this.lblUsername.Location = new System.Drawing.Point(30, 30);
            this.lblUsername.AutoSize = true;

            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(130, 30);
            this.txtUsername.Width = 150;

            // 
            // lblPassword
            // 
            this.lblPassword.Text = "رمز عبور:";
            this.lblPassword.Location = new System.Drawing.Point(30, 70);
            this.lblPassword.AutoSize = true;

            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(130, 70);
            this.txtPassword.Width = 150;
            this.txtPassword.PasswordChar = '●';

            // 
            // lblConfirm
            // 
            this.lblConfirm.Text = "تکرار رمز:";
            this.lblConfirm.Location = new System.Drawing.Point(30, 110);
            this.lblConfirm.AutoSize = true;

            // 
            // txtConfirm
            // 
            this.txtConfirm.Location = new System.Drawing.Point(130, 110);
            this.txtConfirm.Width = 150;
            this.txtConfirm.PasswordChar = '●';

            // 
            // btnRegister
            // 
            this.btnRegister.Text = "ثبت‌نام";
            this.btnRegister.Location = new System.Drawing.Point(130, 150);
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            // 
            // RegisterForm
            // 
            this.ClientSize = new System.Drawing.Size(350, 220);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.btnRegister);
            this.Name = "RegisterForm";
            this.Text = "ثبت‌نام کاربر جدید";
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
