
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using SmarterBackup.Core.Interfaces;
using SmarterBackup.Core.Models;
using SmarterBackup.Presentation;
using SmarterBackup.Presentation.configuration;
using SmarterBackUp.infrastruture.SmarterBackup.Services;

var builder = Host.CreateApplicationBuilder(args);

// ⬇️ Load config from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configuration = builder.Configuration;

// ⬇️ Bind configuration sections
var appSettings = configuration.Get<AppSettings>();
var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();

// ⬇️ Register config as Singleton
builder.Services.AddSingleton(appSettings);
builder.Services.AddSingleton(emailSettings);

// ⬇️ Register Services
builder.Services.AddScoped<IBackupService, BackupManager>();
builder.Services.AddScoped<ICryptoService, CryptoService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();

// ⚠️ برای تست، ایمیل رو غیرفعال کن؛ بعداً EmailService جایگزین شه
//builder.Services.AddScoped<IEmailService, EmptyEmailService>();
builder.Services.AddScoped<IEmailService, EmailService>();

var host = builder.Build();

var backupService = host.Services.GetRequiredService<IBackupService>();
var logger = host.Services.GetRequiredService<ILoggerService>();
var crypto = host.Services.GetRequiredService<ICryptoService>();

// ⬇️ اجرای بکاپ دستی
foreach (var task in appSettings.BackupTasks)
{
    Console.WriteLine($"Running Execution Backup: {task.Name}");
    var result = await backupService.RunBackupAsync(task);

    if (result.Success)
        Console.WriteLine("✅ Success");
    else
        Console.WriteLine($"❌ Error: {result.ErrorMessage}");
}

// ⬇️ راه‌اندازی زمان‌بندی
var scheduler = new BackupScheduler(backupService, logger);
var cts = new CancellationTokenSource();
Console.WriteLine("🟢 Scheduled Execution In Backup ... (Ctrl+C برای خروج)");

// برای تست، فعلاً زمان‌بندی رو غیر فعال کن
// scheduler.Start(appSettings.BackupTasks, cts.Token);
// await Task.Delay(Timeout.Infinite, cts.Token);

// ⬇️ تست ریستور
Console.WriteLine("🔄 Do you want to restore backup (y/n)");
var input = Console.ReadLine();
if (input?.ToLower() == "y")
{
    Console.Write("Source Path: ");
    var zipPath = Console.ReadLine();

    Console.Write("Destination Path: ");
    var outputPath = Console.ReadLine();

    Console.Write("🔑 Enter Password: ");
    var password = Console.ReadLine();

    try
    {
        await crypto.DecryptZipAsync(zipPath!, outputPath!, password!);
        Console.WriteLine("✅ Restore Succeeded.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error: {ex.Message}");
    }
}
