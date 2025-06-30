using SmarterBackup.Core.Interfaces;
using SmarterBackup.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarterBackUp.infrastruture.SmarterBackup.Services
{
    public class LoggerService : ILoggerService
    {

        private readonly string logFilePath = "backup-log.txt";

        public void Log(string message)
        {
            File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
        }

        public void LogResult(BackupResult result)
        {
            var msg = $"[{result.StartTime}] {result.TaskName} → Success: {result.Success}";
            if (!result.Success && !string.IsNullOrEmpty(result.ErrorMessage))
                msg += $" → Error: {result.ErrorMessage}";

            Log(msg);
        }

        public void LogReult(BackupResult results)
        {
            
            //throw new NotImplementedException();
        }
    }
}
