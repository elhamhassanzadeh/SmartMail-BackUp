using SmarterBackup.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarterBackup.Core.Interfaces
{
    public interface ILoggerService
    {
        void Log(string message);
        void LogReult(BackupResult results);
    }
}
