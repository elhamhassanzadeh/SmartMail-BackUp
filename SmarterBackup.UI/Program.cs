using System;
using System.Windows.Forms;

namespace SmarterBackup.UI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var userService = new UserService();
            var users = userService.LoadUsers();

            if (users.Count == 0)
            {
                
                Application.Run(new RegisterForm());
            }
            else
            {

                Application.Run(new LoginForm());
            }
        }
    }
}
