using SmarterBackup.Core.Interfaces;
using SmarterBackup.Core.Models;
using System;
using System.IO;

namespace SmarterBackUp.infrastruture.SmarterBackup.Services
{
    
    public class LoggerService : ILoggerService
    {
        private readonly string _logDirectory;
        private readonly string _logFilePath;

        public LoggerService()
        {
            _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            _logFilePath = Path.Combine(_logDirectory, "backup.log");

            if (!Directory.Exists(_logDirectory))
                Directory.CreateDirectory(_logDirectory);
        }

        public void Log(string message)
        {
            string timestamp = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]";
            File.AppendAllText(_logFilePath, $"{timestamp} {message}{Environment.NewLine}");
        }
    


        public void LogReult(BackupResult results)
        {
            throw new NotImplementedException();
        }
    }


}
