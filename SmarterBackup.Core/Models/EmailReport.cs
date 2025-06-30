using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarterBackup.Core.Models
{
   public class EmailReport
    {
        public string TaskName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SourcePath {  get; set; }= string.Empty;
        public string DestinationPath { get; set; } = string.Empty;
        public bool UseEncryption {  get; set; }
        public bool Success { get; set; }
        public string ? ErrorMessage {  get; set; }
    }
}
