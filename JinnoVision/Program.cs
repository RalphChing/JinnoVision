using JinnoVision.App.Interfaces;
using JinnoVision.App.Services;
using JinnoVision.Domain.Interfaces;
using JinnoVision.Forms;
using JinnoVision.Infrastructure.Data;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace JinnoVision
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            string connectionString = @"Server=localhost;Database=JINNO_DB_TESTCOMP1;Trusted_Connection=True;";
            
            var factory = new DbConnectionFactory(connectionString);
            IUserRepository userRepository = new UserRepository(factory);
            IAuthService authService = new AuthService(userRepository);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm(authService));
        }
    }
}
