using SmarterBackup.Core.Interfaces;
using SmarterBackup.Core.Models;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace SmarterBackUp.infrastruture.SmarterBackup.Services
{
    public class BackupManager : IBackupService
    {
        private readonly ILoggerService _logger;
        private readonly IEmailService _email;
        private readonly ICryptoService _crypto;

        public BackupManager(ILoggerService logger, IEmailService email, ICryptoService crypto)
        {
            _logger = logger;
            _email = email;
            _crypto = crypto;
        }

        public async Task<BackupResult> RunBackupAsync(BackupTask task)
        {
            var start = DateTime.Now;
            var zipPath = GetBackupZipPath(task);

            var result = new BackupResult
            {
                TaskName = task.Name,
                Timestamp = start,
                Success = false
            };

            try
            {
                LogBackupStart(task, zipPath);

                // اگر فایل وجود دارد، حذفش کن
                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }

                if (task.UseEncryption)
                {
                    if (string.IsNullOrWhiteSpace(task.EncryptionPassword))
                        throw new ArgumentException("رمز عبور رمزگذاری نمی‌تواند خالی باشد.");

                    Console.WriteLine("🔐 Encryption enabled.");
                    await _crypto.EncryptDirectoryAsync(task.SourcePath, zipPath, task.EncryptionPassword);
                }
                else
                {
                    Console.WriteLine("📦 Compression without encryption.");
                    ZipFile.CreateFromDirectory(task.SourcePath, zipPath, CompressionLevel.Optimal, false);
                }

                Console.WriteLine($"✅ Backup completed: {zipPath}");
                result.Success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Backup failed: {ex.Message}");
                result.ErrorMessage = ex.Message;
            }
            finally
            {
                result.Timestamp = DateTime.Now;
                var report = BuildReport(task, start, result);
                await _email.SendReportAsync(report);
            }

            return result;
        }

        public async Task RestoreAsync(string zipPath, string destinationPath, string password, bool isEncrypted)
        {
            try
            {
                if (isEncrypted)
                {
                    if (string.IsNullOrWhiteSpace(password))
                        throw new ArgumentException("برای فایل رمزگذاری شده، پسورد نمی‌تواند خالی باشد.");

                    Console.WriteLine("🔐 Decrypting and restoring...");
                    await _crypto.DecryptZipAsync(zipPath, destinationPath, password);
                }
                else
                {
                    Console.WriteLine("📦 Extracting without decryption...");
                    ZipFile.ExtractToDirectory(zipPath, destinationPath);
                }

                Console.WriteLine("✅ Restore completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Restore failed: {ex.Message}");
                throw;
            }
        }

        private string GetBackupZipPath(BackupTask task)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var fileName = task.UseEncryption
                ? $"{task.Name}_{timestamp}.enc.zip"
                : $"{task.Name}_{timestamp}.zip";

            return Path.Combine(task.DestinationPath, fileName);
        }

        private void LogBackupStart(BackupTask task, string zipPath)
        {
            _logger.Log($"📁 Source: {task.SourcePath}");
            _logger.Log($"📦 ZIP Target: {zipPath}");
        }

        private EmailReport BuildReport(BackupTask task, DateTime start, BackupResult result)
        {
            return new EmailReport
            {
                TaskName = task.Name,
                StartTime = start,
                EndTime = DateTime.Now,
                SourcePath = task.SourcePath,
                DestinationPath = task.DestinationPath,
                UseEncryption = task.UseEncryption,
                Success = result.Success,
                ErrorMessage = result.ErrorMessage
            };
        }
    }
}
