
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using SmarterBackup.Core.Interfaces;
//using SmarterBackup.Core.Models;
//using SmarterBackup.Presentation.configuration;
//using SmarterBackUp.infrastruture.SmarterBackup.Services;

//var builder = Host.CreateApplicationBuilder(args);

//// Load config//به خاطر حذف appsettings.json از بک اند
////builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
////var configuration = builder.Configuration;

////var appSettings = configuration.Get<AppSettings>();
////var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();

////builder.Services.AddSingleton(appSettings);
////builder.Services.AddSingleton(emailSettings);
//builder.Services.AddScoped<IBackupService, BackupManager>();
//builder.Services.AddScoped<ICryptoService, CryptoService>();
//builder.Services.AddScoped<ILoggerService, LoggerService>();
//builder.Services.AddScoped<IEmailService, EmailService>();

//var host = builder.Build();

//var backupService = host.Services.GetRequiredService<IBackupService>();
//var logger = host.Services.GetRequiredService<ILoggerService>();
//var crypto = host.Services.GetRequiredService<ICryptoService>();

//// ۱. اول از کاربر بپرس چی میخواد
//Console.WriteLine("🚀 Welcome! What do you want to do?");
//Console.WriteLine("1) Backup");
//Console.WriteLine("2) Restore");
//Console.Write("Enter choice (1 or 2): ");

//var choice = Console.ReadLine()?.Trim();

//if (choice == "2")
//{
//    // ریستور
//    Console.Write("📁 Enter ZIP path: ");
//    var zipPath = Console.ReadLine()?.Trim();

//    Console.Write("📂 Enter Destination path: ");
//    var outputPath = Console.ReadLine()?.Trim();

//    Console.Write("🔑 Do you want to decrypt? Enter password or leave empty: ");
//    var password = Console.ReadLine()?.Trim();

//    if (string.IsNullOrWhiteSpace(zipPath) || string.IsNullOrWhiteSpace(outputPath))
//    {
//        Console.WriteLine("❌ ZIP path and Destination path cannot be empty.");
//        return;
//    }

//    bool isEncrypted = !string.IsNullOrEmpty(password);

//    try
//    {
//        await backupService.RestoreAsync(zipPath!, outputPath!, password!, isEncrypted);
//       // Console.WriteLine("✅ Restore completed successfully.");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"❌ Restore failed: {ex.Message}");
//    }
//}
//else if (choice == "1")
//{
//    // بکاپ زمانبندی
//    //foreach (var task in appSettings.BackupTasks)
//    {
//        Console.WriteLine($"⚙️ Backup task: {task.Name}");
//        Console.Write("🔐 Do you want to encrypt this backup? (y/n): ");
//        var encChoice = Console.ReadLine()?.Trim().ToLower();

//        if (encChoice == "y")
//        {
//            Console.Write("🔑 Enter encryption password: ");
//            var pwd = Console.ReadLine()?.Trim();

//            if (string.IsNullOrEmpty(pwd))
//            {
//                Console.WriteLine("❌ Encryption password cannot be empty. Skipping encryption.");
//                task.UseEncryption = false;
//                task.EncryptionPassword = "";
//            }
//            else
//            {
//                task.UseEncryption = true;
//                task.EncryptionPassword = pwd;
//            }
//        }
//        else
//        {
//            task.UseEncryption = false;
//            task.EncryptionPassword = "";
//        }
//    }

//    var cts = new CancellationTokenSource();
//    Console.WriteLine("🟢 Scheduled Execution In Backup ... (Ctrl+C to exit)");

//    var scheduler = new BackupScheduler(backupService, logger);
//    scheduler.Start(appSettings.BackupTasks, cts.Token);

//    await Task.Delay(Timeout.Infinite, cts.Token);
//}
//else
//{
//    Console.WriteLine("❌ Invalid choice. Exiting.");
//}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmarterBackup.Core.Interfaces;
using SmarterBackup.Core.Models;
using SmarterBackup.Presentation.configuration;
using SmarterBackUp.infrastruture.SmarterBackup.Services;

var builder = Host.CreateApplicationBuilder(args);

// افزودن سرویس‌ها
builder.Services.AddScoped<IBackupService, BackupManager>();
builder.Services.AddScoped<ICryptoService, CryptoService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<IEmailService, EmailService>();

var host = builder.Build();

var backupService = host.Services.GetRequiredService<IBackupService>();
var logger = host.Services.GetRequiredService<ILoggerService>();
var crypto = host.Services.GetRequiredService<ICryptoService>();
var email = host.Services.GetRequiredService<IEmailService>();

Console.WriteLine("🚀 Welcome! What do you want to do?");
Console.WriteLine("1) Backup");
Console.WriteLine("2) Restore");
Console.Write("Enter choice (1 or 2): ");

var choice = Console.ReadLine()?.Trim();

if (choice == "2")
{
    // ریستور
    Console.Write("📁 Enter ZIP path: ");
    var zipPath = Console.ReadLine()?.Trim();

    Console.Write("📂 Enter Destination path: ");
    var outputPath = Console.ReadLine()?.Trim();

    Console.Write("🔑 Do you want to decrypt? Enter password or leave empty: ");
    var password = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(zipPath) || string.IsNullOrWhiteSpace(outputPath))
    {
        Console.WriteLine("❌ ZIP path and Destination path cannot be empty.");
        return;
    }

    bool isEncrypted = !string.IsNullOrEmpty(password);

    try
    {
        await backupService.RestoreAsync(zipPath!, outputPath!, password!, isEncrypted);
        Console.WriteLine("✅ Restore completed successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Restore failed: {ex.Message}");
    }
}
else if (choice == "1")
{
    // بکاپ دستی یا زمان‌بندی‌شده (تک‌تسکه)

    Console.Write("🔤 Enter Task Name: ");
    var taskName = Console.ReadLine()?.Trim() ?? "ManualBackup";

    Console.Write("📁 Enter Source Path: ");
    var source = Console.ReadLine()?.Trim();

    Console.Write("📂 Enter Destination Path: ");
    var dest = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(dest))
    {
        Console.WriteLine("❌ Source and Destination paths are required.");
        return;
    }

    Console.Write("🔐 Do you want to encrypt this backup? (y/n): ");
    var encChoice = Console.ReadLine()?.Trim().ToLower();

    bool useEncryption = encChoice == "y";
    string password = "";

    if (useEncryption)
    {
        Console.Write("🔑 Enter encryption password: ");
        password = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrEmpty(password))
        {
            Console.WriteLine("❌ Encryption password cannot be empty. Skipping encryption.");
            useEncryption = false;
        }
    }

    Console.Write("⏱️ Interval (in minutes, optional - just press Enter for one-time): ");
    var intervalInput = Console.ReadLine()?.Trim();
    double.TryParse(intervalInput, out var interval);
    interval = interval <= 0 ? 0 : interval;

    var task = new BackupTask
    {
        Name = taskName,
        SourcePath = source,
        DestinationPath = dest,
        UseEncryption = useEncryption,
        EncryptionPassword = password,
        IntervalMinutes = interval > 0 ? interval : null
    };

    if (interval > 0)
    {
        var cts = new CancellationTokenSource();
        Console.WriteLine("🟢 Scheduled Backup Started... (Ctrl+C to stop)");

        var scheduler = new BackupScheduler(backupService, logger);
        scheduler.Start(new List<BackupTask> { task }, cts.Token);

        await Task.Delay(Timeout.Infinite, cts.Token);
    }
    else
    {
        try
        {
            Console.WriteLine("⚙️ Running manual backup...");
            await backupService.RunBackupAsync(task);
            Console.WriteLine("✅ Backup completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Backup failed: {ex.Message}");
        }
    }
}
else
{
    Console.WriteLine("❌ Invalid choice. Exiting.");
}

