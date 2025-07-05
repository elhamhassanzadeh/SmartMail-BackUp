
using System.Windows.Forms;

namespace SmarterBackup.UI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // تب‌ها
            mainTabs = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage(); // تنظیمات ایمیل

            // تب 1 - بکاپ
            txtSource = new TextBox();
            txtDestination = new TextBox();
            btnSelectSource = new Button();
            btnSelectDestination = new Button();
            chkEncrypt = new CheckBox();
            txtPassword = new TextBox();
            numInterval = new NumericUpDown();
            btnStartBackup = new Button();
            lblBackupStatus = new Label();
            lblNextRun = new Label();
            btnToggleSchedule = new Button();
            btnSaveSettings = new Button();

            // تب 2 - ریستور
            btnSelectZip = new Button();
            btnSelectRestorePath = new Button();
            txtRestorePassword = new TextBox();
            lblRestoreStatus = new Label();
            btnStartRestore = new Button();

            // تب 3 - لاگ‌ها
            dtLogDate = new DateTimePicker();
            txtLogOutput = new TextBox();
            btnLoadLogs = new Button();

            // تب 4 - تنظیمات ایمیل
           
            txtSenderEmail = new TextBox();
            txtReceiverEmail = new TextBox();
            txtSmtpServer = new TextBox();
            numSmtpPort = new NumericUpDown();
            numSmtpPort.Minimum = 1;
            numSmtpPort.Maximum = 65535;
            numSmtpPort.Value = 587;
            
            chkEnableSsl = new CheckBox();
            txtSenderPassword = new TextBox();
            btnSaveEmailSettings = new Button();
            btnTestEmail = new Button();

            // تنظیمات تب‌ها
            mainTabs.Dock = DockStyle.Fill;
            tabPage1.Text = "بکاپ";
            tabPage2.Text = "بازیابی";
            tabPage3.Text = "لاگ‌ها";
            tabPage4.Text = "تنظیمات ایمیل";

            // اضافه کردن تب‌ها
            mainTabs.Controls.Add(tabPage1);
            mainTabs.Controls.Add(tabPage2);
            mainTabs.Controls.Add(tabPage3);
            mainTabs.Controls.Add(tabPage4);

            // ===== تب 1: بکاپ =====
            txtSource.Location = new System.Drawing.Point(20, 20);
            txtSource.Size = new System.Drawing.Size(250, 23);
            btnSelectSource.Location = new System.Drawing.Point(280, 20);
            btnSelectSource.Text = "انتخاب مبدا";

            txtDestination.Location = new System.Drawing.Point(20, 60);
            txtDestination.Size = new System.Drawing.Size(250, 23);
            btnSelectDestination.Location = new System.Drawing.Point(280, 60);
            btnSelectDestination.Text = "انتخاب مقصد";

            chkEncrypt.Location = new System.Drawing.Point(20, 100);
            chkEncrypt.Text = "رمزگذاری فایل";

            txtPassword.Location = new System.Drawing.Point(20, 130);
            txtPassword.Size = new System.Drawing.Size(250, 23);
            txtPassword.PasswordChar = '*';

            numInterval.Location = new System.Drawing.Point(20, 160);
            numInterval.Minimum = 1;
            numInterval.Maximum = 1440;
            numInterval.Value = 1;

            btnStartBackup.Location = new System.Drawing.Point(20, 200);
            btnStartBackup.Text = "شروع بکاپ";

            btnToggleSchedule.Location = new System.Drawing.Point(130, 200);
            btnToggleSchedule.Text = "⏸ زمان‌بندی";

            btnSaveSettings.Location = new System.Drawing.Point(250, 200);
            btnSaveSettings.Text = "ذخیره تنظیمات";

            lblBackupStatus.Location = new System.Drawing.Point(20, 240);
            lblBackupStatus.Size = new System.Drawing.Size(400, 20);
            lblBackupStatus.Text = "وضعیت: آماده";

            lblNextRun.Location = new System.Drawing.Point(20, 270);
            lblNextRun.Size = new System.Drawing.Size(400, 20);

            tabPage1.Controls.AddRange(new Control[] {
                txtSource, btnSelectSource, txtDestination, btnSelectDestination,
                chkEncrypt, txtPassword, numInterval, btnStartBackup,
                btnToggleSchedule, btnSaveSettings, lblBackupStatus, lblNextRun
            });

            // ===== تب 2: ریستور =====
            btnSelectZip.Location = new System.Drawing.Point(20, 20);
            btnSelectZip.Text = "انتخاب فایل ZIP";

            btnSelectRestorePath.Location = new System.Drawing.Point(20, 60);
            btnSelectRestorePath.Text = "مسیر بازیابی";

            txtRestorePassword.Location = new System.Drawing.Point(20, 100);
            txtRestorePassword.Size = new System.Drawing.Size(250, 23);
            txtRestorePassword.PasswordChar = '*';

            btnStartRestore.Location = new System.Drawing.Point(20, 140);
            btnStartRestore.Text = "شروع بازیابی";

            lblRestoreStatus.Location = new System.Drawing.Point(20, 180);
            lblRestoreStatus.Size = new System.Drawing.Size(400, 20);

            tabPage2.Controls.AddRange(new Control[] {
                btnSelectZip, btnSelectRestorePath, txtRestorePassword,
                btnStartRestore, lblRestoreStatus
            });

            // ===== تب 3: لاگ‌ها =====
            dtLogDate.Location = new System.Drawing.Point(20, 20);
            dtLogDate.Size = new System.Drawing.Size(200, 23);

            btnLoadLogs.Location = new System.Drawing.Point(240, 20);
            btnLoadLogs.Text = "بارگذاری لاگ‌ها";

            txtLogOutput.Location = new System.Drawing.Point(20, 60);
            txtLogOutput.Size = new System.Drawing.Size(740, 300);
            txtLogOutput.Multiline = true;
            txtLogOutput.ScrollBars = ScrollBars.Vertical;

            tabPage3.Controls.AddRange(new Control[] {
                dtLogDate, btnLoadLogs, txtLogOutput
            });

            // ===== تب 4: تنظیمات ایمیل =====
            AddLabeled(tabPage4, "ایمیل فرستنده:", txtSenderEmail, 20);
            AddLabeled(tabPage4, "ایمیل گیرنده:", txtReceiverEmail, 60);
            AddLabeled(tabPage4, "SMTP Server:", txtSmtpServer, 100);
            AddLabeled(tabPage4, "پورت:", numSmtpPort, 140);

            chkEnableSsl.Location = new System.Drawing.Point(150, 180);
            chkEnableSsl.Text = "استفاده از SSL";
            tabPage4.Controls.Add(chkEnableSsl);

            AddLabeled(tabPage4, "رمز ایمیل:", txtSenderPassword, 220);
            txtSenderPassword.PasswordChar = '*';

            btnSaveEmailSettings.Location = new System.Drawing.Point(150, 260);
            btnSaveEmailSettings.Text = "💾 ذخیره تنظیمات ایمیل";

            btnTestEmail.Location = new System.Drawing.Point(300, 260);
            btnTestEmail.Text = "📧 تست ارسال ایمیل";

            tabPage4.Controls.Add(btnSaveEmailSettings);
            tabPage4.Controls.Add(btnTestEmail);

            // نهایی کردن فرم
            this.Controls.Add(mainTabs);
            this.Text = "SmarterBackup";
            this.ClientSize = new System.Drawing.Size(800, 600);
        }

        private void AddLabeled(Control parent, string labelText, Control input, int top)
        {
            var label = new Label();
            label.Text = labelText;
            label.Location = new System.Drawing.Point(20, top);
            label.AutoSize = true;

            input.Location = new System.Drawing.Point(150, top);
            input.Size = new System.Drawing.Size(250, 23);

            parent.Controls.Add(label);
            parent.Controls.Add(input);
        }

        #endregion

        private TabControl mainTabs;
        private TabPage tabPage1, tabPage2, tabPage3, tabPage4;

        private TextBox txtSource, txtDestination, txtPassword, txtRestorePassword;
        private TextBox txtSenderEmail, txtReceiverEmail, txtSmtpServer, txtSenderPassword;
        private TextBox txtLogOutput;

        private Button btnSelectSource, btnSelectDestination, btnStartBackup;
        private Button btnToggleSchedule, btnSaveSettings, btnSelectZip;
        private Button btnSelectRestorePath, btnStartRestore, btnLoadLogs;
        private Button btnSaveEmailSettings, btnTestEmail;

        private Label lblBackupStatus, lblNextRun, lblRestoreStatus;

        private CheckBox chkEncrypt, chkEnableSsl;

        private NumericUpDown numInterval, numSmtpPort;

        private DateTimePicker dtLogDate;
    }
}
