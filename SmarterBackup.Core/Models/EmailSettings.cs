﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarterBackup.Core.Models
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }=string.Empty;
        public int Port { get; set; }
        public string SenderEmail { get; set; } = string.Empty;
        public string SenderPassword { get; set; } = string.Empty;
        public string ReceiverEmail { get; set; } = string.Empty;
        public bool EnableSsl { set; get; }
    }
}
