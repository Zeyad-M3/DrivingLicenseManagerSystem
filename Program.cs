using _3tr.Applications;
using _3tr.Applications.Driving_Licenses;
using _3tr.Applications.Driving_Licenses.Manage_Applications;
using _3tr.Applicationtype;
using _3tr.DetainLicenses;
using _3tr.DrivingLicense;
using _3tr.Manage_Test_Types;
using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3tr
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
            // if MainApp close close all application and the frmLogin
            Application.ApplicationExit += (sender, e) =>
            {
                Application.Exit();
            };



        }
    }
}
