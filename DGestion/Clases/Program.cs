using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGestion
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (FirstInstance) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmLogIn());
            }
            else {
                MessageBox.Show("El sistema ya se está ejecutando en este terminal.");
                Application.Exit();
            }
        }

        private static bool FirstInstance {
            get {
                bool created;
                string name = Assembly.GetEntryAssembly().FullName;
                // created will be True if the current thread creates and owns the mutex.
                // Otherwise created will be False if a previous instance already exists.
                Mutex mutex = new Mutex(true, name, out created);
                return created;
            }
        }
    }
}
