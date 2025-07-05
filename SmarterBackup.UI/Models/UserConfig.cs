using SmarterBackup.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarterBackup.UI.Models
{
    public class UserConfig
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public bool UseEncryption { get; set; }
        public string EncryptionPassword { get; set; }
        public double? IntervalMinutes { get; set; }

        public EmailSettings EmailSettings { get; set; } = new EmailSettings(); 
    }
}
