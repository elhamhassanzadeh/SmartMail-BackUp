using SmarterBackup.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarterBackup.Core.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string body); // متد عمومی برای ارسال ایمیل
        Task SendReportAsync(EmailReport report);         // گزارش بکاپ کامل
        Task SendErrorAsync(string subject, string body); // ایمیل هشدار یا خطا
    }
}
