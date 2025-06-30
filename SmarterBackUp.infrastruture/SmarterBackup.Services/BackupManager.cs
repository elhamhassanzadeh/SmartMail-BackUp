//using SmarterBackup.Core.Interfaces;
//using SmarterBackup.Core.Models;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO.Compression;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//namespace SmarterBackUp.infrastruture.SmarterBackup.Services
//{
//    public class BackupManager : IBackupService
//    {

//        private readonly ILoggerService _logger;
//        private readonly IEmailService _email;
//        private readonly ICryptoService _crypto;


//        public BackupManager(ILoggerService logger, IEmailService email, ICryptoService crypto)
//        {
//            _logger = logger;
//            _email = email;
//            _crypto = crypto;
//        }


//        public async Task<BackupResult> RunBackupAsync(BackupTask task)
//        {


//            var start = DateTime.Now;

//            try
//            {
//                Console.WriteLine($"🔐 Password: {(task.EncryptionPassword ?? "NULL")}");
//                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
//                var zipPath = Path.Combine(task.DestinationPath, $"{task.Name}_{timestamp}.zip");

//                Console.WriteLine($"🟢 شروع بکاپ: {task.Name}");
//                Console.WriteLine($"📁 مسیر منبع: {task.SourcePath}");
//                Console.WriteLine($"📦 مسیر فایل ZIP: {zipPath}");

//                if (task.UseEncription)
//                {
//                    Console.WriteLine("🔐 رمزگذاری فعال است.");
//                    await _crypto.EncryptDirectoryAsync(task.SourcePath, zipPath, task.EncryptionPassword);
//                }
//                else
//                {
//                    Console.WriteLine("📦 فشرده‌سازی بدون رمز.");
//                    ZipFile.CreateFromDirectory(task.SourcePath, zipPath, CompressionLevel.Optimal, false);
//                }

//                Console.WriteLine($"✅ بکاپ با موفقیت انجام شد. مسیر فایل: {zipPath}");
//                return new BackupResult
//                {
//                    TaskName = task.Name,
//                    Success = true,
//                    Timestamp = DateTime.Now
//                };

//                var result = new BackupResult
//                {
//                    TaskName = task.Name,
//                    Success = false,
//                    Timestamp = DateTime.Now,

//                };
//                var report = BuildReport(task, start, result);
//                await _email.SendReportAsync(report);
//                await _email.SendEmailAsync("succed backup", "send emai");

//                return result;
//            }
//            catch (Exception ex) {
//                Console.WriteLine($"❌ خطا در بکاپ: {ex.Message}");
//                var result = new BackupResult
//                {
//                    TaskName = task.Name,
//                    Success = false,
//                    Timestamp = DateTime.Now,
//                    ErrorMessage = ex.Message
//                };


//                var report = BuildReport(task, start, result);
//                await _email.SendReportAsync(report);

//                return result;
//            }

//        }


//        private EmailReport BuildReport(BackupTask task, DateTime start, BackupResult result)
//        {
//            return new EmailReport
//            {
//                TaskName = task.Name,
//                StartTime = start,
//                EndTime = DateTime.Now,
//                SourcePath = task.SourcePath,
//                DestinationPath = task.DestinationPath,
//                UseEncryption = task.UseEncription,
//                Success = result.Success,
//                ErrorMessage = result.ErrorMessage
//            };
//        }


//    }

//}


using SmarterBackup.Core.Interfaces;
using SmarterBackup.Core.Models;
using System;
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
            var result = new BackupResult
            {
                TaskName = task.Name,
                Success = false,
                Timestamp = DateTime.Now
            };

            try
            {
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var zipPath = Path.Combine(task.DestinationPath, $"{task.Name}_{timestamp}.zip");

                Console.WriteLine($"🟢 شروع بکاپ: {task.Name}");
                Console.WriteLine($"📁 مسیر منبع: {task.SourcePath}");
                Console.WriteLine($"📦 مسیر فایل ZIP: {zipPath}");

                if (task.UseEncription)
                {
                    Console.WriteLine("🔐 رمزگذاری فعال است.");
                    await _crypto.EncryptDirectoryAsync(task.SourcePath, zipPath, task.EncryptionPassword);
                }
                else
                {
                    Console.WriteLine("📦 فشرده‌سازی بدون رمز.");
                    ZipFile.CreateFromDirectory(task.SourcePath, zipPath, CompressionLevel.Optimal, false);
                }

                Console.WriteLine($"✅ بکاپ با موفقیت انجام شد. مسیر فایل: {zipPath}");
                result.Success = true;
                result.Timestamp = DateTime.Now;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ خطا در بکاپ: {ex.Message}");
                result.Success = false;
                result.Timestamp = DateTime.Now;
                result.ErrorMessage = ex.Message;
            }

            var report = BuildReport(task, start, result);
            await _email.SendReportAsync(report);

            return result;
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
                UseEncryption = task.UseEncription,
                Success = result.Success,
                ErrorMessage = result.ErrorMessage
            };
        }
    }
}