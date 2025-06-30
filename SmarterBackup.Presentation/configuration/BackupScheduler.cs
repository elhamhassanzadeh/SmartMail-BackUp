using SmarterBackup.Core.Interfaces;
using SmarterBackup.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarterBackup.Presentation.configuration
{
    public class BackupScheduler
    {
        private readonly IBackupService _backupService;
        private readonly ILoggerService _logger;

        public BackupScheduler(IBackupService backupService, ILoggerService logger)
        {
            _backupService = backupService;
            _logger = logger;
        }

        public void Start(List<BackupTask> tasks, CancellationToken cancellationToken)
        {
            foreach (var task in tasks)
            {
                if (task.IntervalMinutes.HasValue)
                {
                    _ = Task.Run(() => RunTaskAsync(task, cancellationToken));
                }
            }
        }

        //private async Task RunTaskAsync(BackupTask task, CancellationToken cancellationToken)
        //{
        //    while (!cancellationToken.IsCancellationRequested)
        //    {
        //        _logger.Log($"⏰ اجرای برنامه‌ریزی شده: {task.Name}");

        //        await _backupService.RunBackupAsync(task);

        //        await Task.Delay(TimeSpan.FromMinutes(task.IntervalMinutes!.Value), cancellationToken);
        //    }
        //}
        private async Task RunTaskAsync(BackupTask task, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextRun = now.AddMinutes(task.IntervalMinutes!.Value);

                _logger.Log($"⏰ Schedule Execution: {task.Name} in {now}");
                Console.WriteLine($"🔁 Execution '{task.Name}' in {now}, next execution in {nextRun}");

                await _backupService.RunBackupAsync(task);

                _logger.Log($"📅 next execution '{task.Name}' in {nextRun}");
                await Task.Delay(TimeSpan.FromMinutes(task.IntervalMinutes!.Value), cancellationToken);
            }
        }

    }
}
