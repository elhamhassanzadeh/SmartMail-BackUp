using SmarterBackup.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarterBackup.Presentation.configuration
{
    
    public class AppSettings
    {
        public List<BackupTask> BackupTasks { get; set; } = new();
    }
}
