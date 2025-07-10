using Newtonsoft.Json;
using SmarterBackup.UI.Models;
using System.Security.Cryptography;
using System.Text;

public class UserService
{
    private readonly string userFilePath = "users.json";

    public List<UserModel> LoadUsers()
    {
        if (!File.Exists(userFilePath))
        {
            
            return new List<UserModel>();
        }

        var json = File.ReadAllText(userFilePath);
        return JsonConvert.DeserializeObject<List<UserModel>>(json) ?? new List<UserModel>();
    }

    public void SaveUsers(List<UserModel> users)
    {
        var json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(userFilePath, json);
    }

    public bool RegisterUser(string username, string password)
    {
        var users = LoadUsers();
        if (users.Any(u => u.Username == username))
            return false; // کاربر تکراری

        users.Add(new UserModel
        {
            Username = username,
            PasswordHash = HashPassword(password)
        });

        SaveUsers(users);
        return true;
    }

    public bool Authenticate(string username, string password)
    {
        var users = LoadUsers();
        var hashedPassword = HashPassword(password);
        return users.Any(u => u.Username == username && u.PasswordHash == hashedPassword);
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
