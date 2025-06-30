using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarterBackup.Core.Models
{
    public class BackupResult
    {
        public string TaskName { get; set; } = string.Empty;
        public DateTime StartTime    { get; set; }
        public DateTime EndTime { get; set; }
        public bool Success { get; set; }
        public DateTime Timestamp { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
