using Real_Estate_Management.Business;
using Real_Estate_Management.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Real_Estate_Management
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*
            User user = new User();
            user.IsAdmin = true;
            Application.Run(new Profile(user));
            */
            Application.Run(new Login());
        }
    }
}
