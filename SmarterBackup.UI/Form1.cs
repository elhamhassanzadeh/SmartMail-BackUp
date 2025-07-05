using Newtonsoft.Json;
using SmarterBackup.Core.Interfaces;
using SmarterBackup.Core.Models;
using SmarterBackup.UI.Models;
using SmarterBackUp.infrastruture.SmarterBackup.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmarterBackup.UI
{
    public partial class Form1 : Form
    {
        private ILoggerService _logger;
        private IEmailService _email;
        private ICryptoService _crypto;
        private BackupManager _backupManager;

        private string sourcePath;
        private string destinationPath;
        private string zipFilePath;
        private string restorePath;

        private System.Windows.Forms.Timer _backupTimer;
        private readonly string _userConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "userconfig.json");
        private readonly string _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "backup.log");
        private UserConfig _userConfig = new();

        public Form1()
        {
            InitializeComponent();

            _logger = new LoggerService();
            _crypto = new CryptoService();


            LoadUserConfig();

            _email = new EmailService(_userConfig.EmailSettings ?? new EmailSettings());
            _backupManager = new BackupManager(_logger, _email, _crypto);

            WireUpEvents();

            StartScheduledBackup();
        }

        private void WireUpEvents()
        {
            btnSelectSource.Click += BtnSelectSource_Click;
            btnSelectDestination.Click += BtnSelectDestination_Click;
            btnStartBackup.Click += BtnStartBackup_Click;
            btnToggleSchedule.Click += BtnToggleSchedule_Click;
            btnSaveSettings.Click += BtnSaveSettings_Click;

            btnSelectZip.Click += BtnSelectZip_Click;
            btnSelectRestorePath.Click += BtnSelectRestorePath_Click;
            btnStartRestore.Click += BtnStartRestore_Click;

            btnLoadLogs.Click += BtnLoadLogs_Click;

            chkEncrypt.CheckedChanged += (s, e) => SaveUserConfig();
            txtPassword.TextChanged += (s, e) => SaveUserConfig();
            numInterval.ValueChanged += (s, e) => SaveUserConfig();

            btnSaveEmailSettings.Click += BtnSaveEmailSettings_Click;
            
            btnTestEmail.Click += BtnTestEmail_Click;
        }

        private void LoadUserConfig()
        {
            if (!File.Exists(_userConfigPath))
                return;

            try
            {
                var json = File.ReadAllText(_userConfigPath);
                _userConfig = JsonConvert.DeserializeObject<UserConfig>(json) ?? new UserConfig();
                ApplyUserConfigToUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ خطا در بارگذاری تنظیمات: " + ex.Message);
            }
        }

        private void ApplyUserConfigToUI()
        {
            if (_userConfig.EmailSettings != null)
            {
                txtSenderEmail.Text = _userConfig.EmailSettings.SenderEmail;
                txtReceiverEmail.Text = _userConfig.EmailSettings.ReceiverEmail;
                txtSmtpServer.Text = _userConfig.EmailSettings.SmtpServer;
                numSmtpPort.Value = _userConfig.EmailSettings.Port;
                chkEnableSsl.Checked = _userConfig.EmailSettings.EnableSsl;
                txtSenderPassword.Text = _userConfig.EmailSettings.SenderPassword;
            }

            txtSource.Text = _userConfig.SourcePath;
            txtDestination.Text = _userConfig.DestinationPath;
            chkEncrypt.Checked = _userConfig.UseEncryption;
            txtPassword.Text = _userConfig.EncryptionPassword;
            numInterval.Value = (decimal)(_userConfig.IntervalMinutes ?? 1);
        }

        private void SaveUserConfig()
        {
            _userConfig.SourcePath = txtSource.Text;
            _userConfig.DestinationPath = txtDestination.Text;
            _userConfig.UseEncryption = chkEncrypt.Checked;
            _userConfig.EncryptionPassword = txtPassword.Text;
            _userConfig.IntervalMinutes = (double?)numInterval.Value;

            _userConfig.EmailSettings = new EmailSettings
            {
                SenderEmail = txtSenderEmail.Text,
                ReceiverEmail = txtReceiverEmail.Text,
                SmtpServer = txtSmtpServer.Text,
                Port = (int)numSmtpPort.Value,
                EnableSsl = chkEnableSsl.Checked,
                SenderPassword = txtSenderPassword.Text
            };

            try
            {
                var json = JsonConvert.SerializeObject(_userConfig, Formatting.Indented);
                File.WriteAllText(_userConfigPath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ خطا در ذخیره تنظیمات: " + ex.Message);
            }
        }

        private void StartScheduledBackup()
        {
            if (string.IsNullOrEmpty(_userConfig.SourcePath) || string.IsNullOrEmpty(_userConfig.DestinationPath))
            {
                lblBackupStatus.Text = "⚠️ تنظیمات ناقص برای شروع زمان‌بندی";
                return;
            }

            var intervalMinutes = _userConfig.IntervalMinutes ?? 1.0;

            _backupTimer = new System.Windows.Forms.Timer
            {
                Interval = (int)(intervalMinutes * 60 * 1000)
            };

            _backupTimer.Tick += async (s, e) =>
            {
                lblBackupStatus.Text = $"🕒 Scheduled Backup started at {DateTime.Now:HH:mm:ss}";
                _logger.Log("🕒 Scheduled backup triggered.");

                try
                {
                    var task = new BackupTask
                    {
                        Name = "UserBackup",
                        SourcePath = _userConfig.SourcePath,
                        DestinationPath = _userConfig.DestinationPath,
                        UseEncryption = _userConfig.UseEncryption,
                        EncryptionPassword = _userConfig.EncryptionPassword,
                        IntervalMinutes = _userConfig.IntervalMinutes
                    };

                    await _backupManager.RunBackupAsync(task);

                    lblBackupStatus.Text = $"✅ Backup completed at {DateTime.Now:HH:mm:ss}";
                    _logger.Log("✅ Scheduled backup completed successfully.");
                }
                catch (Exception ex)
                {
                    lblBackupStatus.Text = $"❌ Backup failed: {ex.Message}";
                    _logger.Log($"❌ Scheduled backup failed: {ex}");
                }

                var nextRun = DateTime.Now.AddMinutes(intervalMinutes);
                lblNextRun.Text = $"Next run: {nextRun:HH:mm:ss}";
            };

            _backupTimer.Start();
            lblNextRun.Text = $"Next run: {DateTime.Now.AddMinutes(intervalMinutes):HH:mm:ss}";
            lblBackupStatus.Text = "🟢 Scheduled backup timer started.";
        }

        private async void BtnStartBackup_Click(object sender, EventArgs e)
        {
            lblBackupStatus.Text = "Backup in progress...";

            var task = new BackupTask
            {
                Name = "UserBackup",
                SourcePath = txtSource.Text,
                DestinationPath = txtDestination.Text,
                UseEncryption = chkEncrypt.Checked,
                EncryptionPassword = txtPassword.Text,
                IntervalMinutes = (double?)numInterval.Value
            };

            try
            {
                await _backupManager.RunBackupAsync(task);
                lblBackupStatus.Text = "✅ Backup completed successfully.";
            }
            catch (Exception ex)
            {
                lblBackupStatus.Text = $"❌ Error: {ex.Message}";
            }
        }

        private async void BtnStartRestore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(zipFilePath) || string.IsNullOrWhiteSpace(restorePath))
            {
                MessageBox.Show("لطفا فایل ZIP و مسیر بازیابی را انتخاب کنید.");
                return;
            }

            lblRestoreStatus.Text = "Restoring...";

            try
            {
                bool isEncrypted = zipFilePath.EndsWith(".enc.zip", StringComparison.OrdinalIgnoreCase);
                string password = txtRestorePassword.Text;

                await _backupManager.RestoreAsync(zipFilePath, restorePath, password, isEncrypted);

                lblRestoreStatus.Text = "✅ Restore completed successfully.";
            }
            catch (Exception ex)
            {
                lblRestoreStatus.Text = $"❌ Error: {ex.Message}";
            }
        }

        private void BtnToggleSchedule_Click(object sender, EventArgs e)
        {
            if (_backupTimer == null) return;

            if (_backupTimer.Enabled)
            {
                _backupTimer.Stop();
                btnToggleSchedule.Text = "▶️ Start Schedule";
                lblBackupStatus.Text = "⏸ Schedule Paused.";
            }
            else
            {
                _backupTimer.Start();
                btnToggleSchedule.Text = "⏹ Stop Schedule";
                lblBackupStatus.Text = "🟢 Schedule Running.";
            }
        }

        private void BtnSelectSource_Click(object sender, EventArgs e)
        {
            var selectedPath = OpenFolderDialog();
            if (!string.IsNullOrEmpty(selectedPath))
            {
                sourcePath = selectedPath;
                txtSource.Text = sourcePath;
                lblBackupStatus.Text = $"Selected source: {sourcePath}";
                SaveUserConfig();
            }
        }

        private void BtnSelectDestination_Click(object sender, EventArgs e)
        {
            var selectedPath = OpenFolderDialog();
            if (!string.IsNullOrEmpty(selectedPath))
            {
                destinationPath = selectedPath;
                txtDestination.Text = destinationPath;
                lblBackupStatus.Text = $"Selected destination: {destinationPath}";
                SaveUserConfig();
            }
        }

        private void BtnSelectZip_Click(object sender, EventArgs e)
        {
            var selectedFile = OpenFileDialog("ZIP files (*.zip)|*.zip");
            if (!string.IsNullOrEmpty(selectedFile))
            {
                zipFilePath = selectedFile;
                lblRestoreStatus.Text = $"Selected ZIP: {zipFilePath}";
            }
        }

        private void BtnSelectRestorePath_Click(object sender, EventArgs e)
        {
            var selectedPath = OpenFolderDialog();
            if (!string.IsNullOrEmpty(selectedPath))
            {
                restorePath = selectedPath;
                lblRestoreStatus.Text = $"Restore path: {restorePath}";
            }
        }

        private string OpenFolderDialog()
        {
            using var dialog = new FolderBrowserDialog();
            return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedPath : null;
        }

        private string OpenFileDialog(string filter)
        {
            using var dialog = new OpenFileDialog();
            dialog.Filter = filter;
            return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
        }

        private async void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            SaveUserConfig();
            MessageBox.Show("تنظیمات ذخیره شد.");
        }

        private async void BtnSaveEmailSettings_Click(object sender, EventArgs e)
        {
            SaveUserConfig();
            MessageBox.Show("تنظیمات ایمیل ذخیره شد.");
        }

        private async void BtnTestEmail_Click(object sender, EventArgs e)
        {
            try
            {
                var settings = new EmailSettings
                {
                    SenderEmail = txtSenderEmail.Text,
                    ReceiverEmail = txtReceiverEmail.Text,
                    SmtpServer = txtSmtpServer.Text,
                    Port = (int)numSmtpPort.Value,
                    EnableSsl = chkEnableSsl.Checked,
                    SenderPassword = txtSenderPassword.Text
                };

                var emailService = new EmailService(settings);

                await emailService.SendReportAsync(new EmailReport
                {
                    TaskName = "تست ارسال",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    SourcePath = "C:\\Test\\Source",
                    DestinationPath = "C:\\Test\\Backup",
                    UseEncryption = false,
                    Success = true,
                    ErrorMessage = ""
                });

                MessageBox.Show("✅ ایمیل تست با موفقیت ارسال شد.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ خطا در ارسال ایمیل: {ex.Message}");
            }
        }


        private void BtnLoadLogs_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(_logFilePath))
                {
                    txtLogOutput.Text = "⚠️ فایل لاگ وجود ندارد.";
                    return;
                }

                var selectedDate = dtLogDate.Value.Date; // فقط تاریخ (بدون زمان)
                var lines = File.ReadAllLines(_logFilePath);

                var filtered = lines
                    .Where(line =>
                    {
                        // انتظار داریم تاریخ در قالب: [yyyy-MM-dd HH:mm:ss]
                        var match = System.Text.RegularExpressions.Regex.Match(line, @"\[(\d{4}-\d{2}-\d{2})");
                        if (!match.Success)
                            return false;

                        if (DateTime.TryParse(match.Groups[1].Value, out var logDate))
                            return logDate.Date == selectedDate;

                        return false;
                    })
                    .ToList();

                txtLogOutput.Text = filtered.Any()
                    ? string.Join(Environment.NewLine, filtered)
                    : "⚠️ هیچ لاگی در این تاریخ یافت نشد.";
            }
            catch (Exception ex)
            {
                txtLogOutput.Text = $"❌ خطا در بارگذاری لاگ: {ex.Message}";
            }
        }

    }
}








