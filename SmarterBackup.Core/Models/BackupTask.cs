using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarterBackup.Core.Models
{
    public class BackupTask
    {
        public string Name { get; set; } = string.Empty;
        public string SourcePath { get; set; }= string.Empty;
        public string DestinationPath { get; set; } = string.Empty;
        public bool UseEncription { set; get; }= true;
        public string EncryptionPassword { get; set; }
        public string? ScheduleCron { get; set; }// هر شب ساعت 12   
        public int? IntervalMinutes { get; set; }// مثلا 60 برای یک ساعت

    }
}
