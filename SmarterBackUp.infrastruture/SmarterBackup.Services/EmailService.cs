using MailKit.Net.Smtp;
using MimeKit;
using SmarterBackup.Core.Interfaces;
using SmarterBackup.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


namespace SmarterBackUp.infrastruture.SmarterBackup.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(EmailSettings settings)
        {
            _settings = settings;
        }

        public async Task SendEmailAsync(string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Backup System", _settings.SenderEmail));
            message.To.Add(new MailboxAddress("Admin", _settings.ReceiverEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.SmtpServer, _settings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_settings.SenderEmail, _settings.SenderPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public async Task SendReportAsync(EmailReport report)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"📌 Task: {report.TaskName}");
            sb.AppendLine($"⏱ Started: {report.StartTime}");
            sb.AppendLine($"✅ Finished: {report.EndTime}");
            sb.AppendLine($"📂 Source: {report.SourcePath}");
            sb.AppendLine($"📦 Destination: {report.DestinationPath}");
            sb.AppendLine($"🔐 Encryption: {(report.UseEncryption ? "Enabled" : "Disabled")}");
            sb.AppendLine($"✔️ Success: {report.Success}");
            if (!report.Success && !string.IsNullOrWhiteSpace(report.ErrorMessage))
                sb.AppendLine($"❌ Error: {report.ErrorMessage}");

            var subject = $"[Backup Report] - {report.TaskName} - {(report.Success ? "✅ Success" : "❌ Failed")}";
            await SendEmailAsync(subject, sb.ToString());
        }

        public async Task SendReportAsync(BackupResult result)
        {
            var subject = $"Backup Report: {result.TaskName}";
            var body = result.Success
                ? $"✅ Backup succeeded at {result.EndTime}"
                : $"❌ Backup failed at {result.EndTime}\nError: {result.ErrorMessage}";

            await SendEmailAsync(subject, body);
        }

        public async Task SendErrorAsync(string subject, string body)
        {
            await SendEmailAsync($"❗ Error: {subject}", body);
        }
    }
}
