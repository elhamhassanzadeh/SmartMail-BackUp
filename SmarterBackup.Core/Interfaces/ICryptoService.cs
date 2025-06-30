using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarterBackup.Core.Interfaces
{
    public interface ICryptoService
    {
        Task EncryptDirectoryAsync(string sourceDir, string destinationFile,string password);
        Task DecryptZipAsync(string zipPath, string outputPath, string password);
    }
}
