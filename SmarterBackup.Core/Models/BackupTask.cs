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
        public bool UseEncryption { set; get; }
        public string? EncryptionPassword { get; set; }
        public string? ScheduleCorn { get; set; }// هر شب ساعت 12   
        public double? IntervalMinutes { get; set; }// مثلا 60 برای یک ساعت

    }
}
