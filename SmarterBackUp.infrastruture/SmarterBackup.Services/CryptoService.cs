using ICSharpCode.SharpZipLib.Zip;
using SmarterBackup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZipFile = System.IO.Compression.ZipFile;

namespace SmarterBackUp.infrastruture.SmarterBackup.Services
{
    public class CryptoService : ICryptoService
    {


        public async Task EncryptDirectoryAsync(string sourcePath, string outputZipPath, string password)
        {
            // بررسی رمز
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Not Empty Password.", nameof(password));

            // ساخت نام فایل zip موقت در Temp
            var tempZip = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".zip");

            try
            {
                // ایجاد فایل فشرده از مسیر مبدا
                ZipFile.CreateFromDirectory(sourcePath, tempZip, CompressionLevel.Optimal, false);

                // خواندن محتویات فایل zip موقت
                byte[] zipBytes = await File.ReadAllBytesAsync(tempZip);

                // رمزگذاری با AES
                using var aes = Aes.Create();
                using var sha256 = SHA256.Create();
                aes.Key = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                aes.GenerateIV();

                using var outputFileStream = new FileStream(outputZipPath, FileMode.Create, FileAccess.Write);
                outputFileStream.Write(aes.IV, 0, aes.IV.Length); // ابتدا IV را بنویس

                using var cryptoStream = new CryptoStream(outputFileStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                await cryptoStream.WriteAsync(zipBytes, 0, zipBytes.Length);
            }
            finally
            {
                // حالا که همه Streamها بسته شدن، فایل tempZip رو پاک می‌کنیم
                if (File.Exists(tempZip))
                {
                    try
                    {
                        File.Delete(tempZip);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠️ it cannot remove TempFile: {ex.Message}");
                    }
                }
            }
        }

        public async Task DecryptZipAsync(string zipPath, string outputPath, string password)
        {
            try
            {
                Console.WriteLine("🧪 Source Path: " + zipPath);
                Console.WriteLine("📁 If exists file:" + File.Exists(zipPath));

                if (!File.Exists(zipPath))
                    throw new FileNotFoundException("Backup File not found");

                using FileStream inputFileStream = new FileStream(zipPath, FileMode.Open, FileAccess.Read);

                // خواندن IV
                byte[] iv = new byte[16];
                int bytesRead = await inputFileStream.ReadAsync(iv, 0, 16);

                if (bytesRead != 16)
                    throw new InvalidDataException("Invalid encrypted file format.");

                // کلید از روی پسورد ساخته می‌شود
                using var sha256 = SHA256.Create();
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                using var aes = Aes.Create();
                aes.Key = key;
                aes.IV = iv;

                using var cryptoStream = new CryptoStream(inputFileStream, aes.CreateDecryptor(), CryptoStreamMode.Read);

                // استخراج فایل ZIP از stream رمزگشایی‌شده
                using var memoryStream = new MemoryStream();
                await cryptoStream.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                using var archive = new ZipArchive(memoryStream, ZipArchiveMode.Read);

                if (!Directory.Exists(outputPath))
                    Directory.CreateDirectory(outputPath);

                archive.ExtractToDirectory(outputPath, overwriteFiles: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [DecryptZipAsync] {ex.GetType().Name} - {ex.Message}");
                throw;
            }
        }
    }
}
