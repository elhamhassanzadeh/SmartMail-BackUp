using SmarterBackup.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarterBackup.Core.Interfaces
{
    public interface IBackupService
    {
        Task<BackupResult> RunBackupAsync(BackupTask tasks);
        Task RestoreAsync(string zipPath, string destinationPath, string password, bool isEncrypted);
    }
}
