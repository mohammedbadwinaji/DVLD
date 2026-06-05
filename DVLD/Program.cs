using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DVLD.Forms.Users;

namespace DVLD
{
    internal static class Program
    {
        // Handler for UI Form Exceptions
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            _HandleCriticalException(e.Exception);
        }

        // Handler for Background / Non-UI Thread Exceptions
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                _HandleCriticalException(ex);
            }
        }

        // Central method to generate your HTML log and notify the user
        private static void _HandleCriticalException(Exception ex)
        {
            string reportPath = Logger.LogExceptionToHtml(ex);

            MessageBox.Show($"A critical error occurred. A detailed crash report has been saved to:\n\n{reportPath}",
                            "Application Crash", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // For severe crashes, cleanly close the application process
            Application.Exit();
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                // 1. Catch exceptions thrown on the main UI thread (e.g., inside form button clicks)
                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

                // 2. Set the routing policy so Windows Forms always triggers our handler
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                // 3. Catch exceptions thrown on background threads (e.g., tasks, timers)
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // 4. Run the application normally (the standard try-catch here is now your final safety net)
            try
            {

                bool keepRunning = true;

                while (keepRunning)
                {
                    // Create a clean instance of the Login Form
                    frmLogin loginForm = new frmLogin();

                    // ShowDialog blocks execution here until loginForm is closed or hidden
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        // If login succeeded, create and run the Main Form
                        frmMain mainForm = new frmMain();

                        // Application.Run links the thread to mainForm. 
                        // When mainForm closes, execution drops down past this line.
                        Application.Run(mainForm);

                        // Check if the user clicked "Logout" vs just hitting the 'X' button
                        // If they just closed the main window to exit, we break the loop.
                        if (mainForm.IsLoggedOut == false)
                        {
                            keepRunning = false;
                        }
                    }
                    else
                    {
                        // User closed the login form without successfully logging in
                        keepRunning = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _HandleCriticalException(ex);
            }
                
            
           


        }
    }


}
