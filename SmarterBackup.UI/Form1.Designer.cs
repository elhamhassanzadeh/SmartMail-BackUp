

using System;
using System.Windows.Forms;
using Guna.UI2.WinForms;

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

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            mainTabs = new Guna2TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();

            mainTabs.Dock = DockStyle.Fill;
            mainTabs.ItemSize = new System.Drawing.Size(180, 40);
            mainTabs.TabButtonHoverState.FillColor = System.Drawing.Color.MediumSlateBlue;
            mainTabs.TabButtonSelectedState.FillColor = System.Drawing.Color.SlateBlue;
            mainTabs.TabMenuBackColor = System.Drawing.Color.White;
            mainTabs.Controls.Add(tabPage1);
            mainTabs.Controls.Add(tabPage2);
            mainTabs.Controls.Add(tabPage3);
            mainTabs.Controls.Add(tabPage4);

            tabPage1.Text = "بکاپ";
            tabPage2.Text = "بازیابی";
            tabPage3.Text = "لاگ‌ها";
            tabPage4.Text = "تنظیمات ایمیل";

            // === بکاپ ===
            txtSource = CreateTextBox(20, 20);
            btnSelectSource = CreateButton(280, 20, "انتخاب مبدا");

            txtDestination = CreateTextBox(20, 60);
            btnSelectDestination = CreateButton(280, 60, "انتخاب مقصد");

            chkEncrypt = CreateCheckbox(20, 100, "رمزگذاری فایل");

            txtPassword = CreateTextBox(20, 130);
            txtPassword.PasswordChar = '●';

            numInterval = CreateNumericUpDown(20, 160, 1, 1440, 1);

            btnStartBackup = CreateButton(20, 200, "شروع بکاپ");
            btnToggleSchedule = CreateButton(130, 200, "⏸ زمان‌بندی");
            btnSaveSettings = CreateButton(250, 200, "ذخیره تنظیمات");

            lblBackupStatus = CreateLabel(20, 240, "وضعیت: آماده");
            lblNextRun = CreateLabel(20, 270, "");

            tabPage1.Controls.AddRange(new Control[] {
                txtSource, btnSelectSource, txtDestination, btnSelectDestination,
                chkEncrypt, txtPassword, numInterval, btnStartBackup,
                btnToggleSchedule, btnSaveSettings, lblBackupStatus, lblNextRun
            });

            // === ریستور ===
            btnSelectZip = CreateButton(20, 20, "انتخاب فایل ZIP");
            btnSelectRestorePath = CreateButton(20, 60, "مسیر بازیابی");

            txtRestorePassword = CreateTextBox(20, 100);
            txtRestorePassword.PasswordChar = '●';

            btnStartRestore = CreateButton(20, 140, "شروع بازیابی");
            lblRestoreStatus = CreateLabel(20, 180, "");

            tabPage2.Controls.AddRange(new Control[] {
                btnSelectZip, btnSelectRestorePath, txtRestorePassword,
                btnStartRestore, lblRestoreStatus
            });

            // === لاگ‌ها ===
            dtLogDate = new Guna2DateTimePicker();
            dtLogDate.Location = new System.Drawing.Point(20, 20);
            dtLogDate.Size = new System.Drawing.Size(200, 36);

            btnLoadLogs = CreateButton(240, 20, "بارگذاری لاگ‌ها");

            txtLogOutput = new Guna2TextBox();
            txtLogOutput.Location = new System.Drawing.Point(20, 60);
            txtLogOutput.Size = new System.Drawing.Size(740, 300);
            txtLogOutput.Multiline = true;
            txtLogOutput.ScrollBars = ScrollBars.Vertical;
            txtLogOutput.ReadOnly = true;

            tabPage3.Controls.AddRange(new Control[] { dtLogDate, btnLoadLogs, txtLogOutput });

            // === تنظیمات ایمیل ===
            txtSenderEmail = CreateTextBox(150, 20);
            AddLabel(tabPage4, "ایمیل فرستنده:", 20);

            txtReceiverEmail = CreateTextBox(150, 60);
            AddLabel(tabPage4, "ایمیل گیرنده:", 60);

            txtSmtpServer = CreateTextBox(150, 100);
            AddLabel(tabPage4, "SMTP Server:", 100);

            numSmtpPort = CreateNumericUpDown(150, 140, 1, 65535, 587);
            AddLabel(tabPage4, "پورت:", 140);

            chkEnableSsl = CreateCheckbox(150, 180, "استفاده از SSL");

            txtSenderPassword = CreateTextBox(150, 220);
            txtSenderPassword.PasswordChar = '●';
            AddLabel(tabPage4, "رمز ایمیل:", 220);

            btnSaveEmailSettings = CreateButton(150, 260, "💾 ذخیره تنظیمات ایمیل");
            btnTestEmail = CreateButton(310, 260, "📧 تست ارسال ایمیل");

            tabPage4.Controls.AddRange(new Control[] {
                txtSenderEmail, txtReceiverEmail, txtSmtpServer, numSmtpPort,
                chkEnableSsl, txtSenderPassword, btnSaveEmailSettings, btnTestEmail
            });

            this.Controls.Add(mainTabs);
            this.Text = "SmarterBackup";
            this.ClientSize = new System.Drawing.Size(850, 600);
        }

        // ساخت کنترل‌ها
        private Guna2TextBox CreateTextBox(int x, int y)
        {
            var tb = new Guna2TextBox();
            tb.Location = new System.Drawing.Point(x, y);
            tb.Size = new System.Drawing.Size(250, 36);
            return tb;
        }

        private Guna2Button CreateButton(int x, int y, string text)
        {
            var btn = new Guna2Button();
            btn.Text = text;
            btn.Location = new System.Drawing.Point(x, y);
            btn.Size = new System.Drawing.Size(120, 36);
            return btn;
        }

        private Guna2CheckBox CreateCheckbox(int x, int y, string text)
        {
            var cb = new Guna2CheckBox();
            cb.Text = text;
            cb.Location = new System.Drawing.Point(x, y);
            cb.AutoSize = true;
            return cb;
        }

        private Guna2NumericUpDown CreateNumericUpDown(int x, int y, int min, int max, int val)
        {
            var num = new Guna2NumericUpDown();
            num.Location = new System.Drawing.Point(x, y);
            num.Size = new System.Drawing.Size(100, 36);
            num.Minimum = min;
            num.Maximum = max;
            num.Value = val;
            return num;
        }

        private Label CreateLabel(int x, int y, string text)
        {
            var lbl = new Label();
            lbl.Text = text;
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.AutoSize = true;
            return lbl;
        }

        private void AddLabel(Control parent, string text, int top)
        {
            var lbl = new Label();
            lbl.Text = text;
            lbl.Location = new System.Drawing.Point(20, top);
            lbl.AutoSize = true;
            parent.Controls.Add(lbl);
        }

        private Guna2TabControl mainTabs;
        private TabPage tabPage1, tabPage2, tabPage3, tabPage4;

        private Guna2TextBox txtSource, txtDestination, txtPassword, txtRestorePassword;
        private Guna2TextBox txtSenderEmail, txtReceiverEmail, txtSmtpServer, txtSenderPassword, txtLogOutput;

        private Guna2Button btnSelectSource, btnSelectDestination, btnStartBackup;
        private Guna2Button btnToggleSchedule, btnSaveSettings, btnSelectZip;
        private Guna2Button btnSelectRestorePath, btnStartRestore, btnLoadLogs;
        private Guna2Button btnSaveEmailSettings, btnTestEmail;

        private Guna2CheckBox chkEncrypt, chkEnableSsl;
        private Guna2NumericUpDown numInterval, numSmtpPort;
        private Guna2DateTimePicker dtLogDate;
        private Label lblBackupStatus, lblNextRun, lblRestoreStatus;
    }
}