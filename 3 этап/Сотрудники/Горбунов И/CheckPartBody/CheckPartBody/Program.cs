using System;
using System.Windows.Forms;
using NXOpen;

namespace CheckPartBody
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SQLOracle.BuildConnectionString("ktc", "ktc", "BASEEOI");
            SqlOracle.BuildConnectionString("ktc", "ktc", "BASEEOI");
            Application.Run(new Form1());
        }
        public static int GetUnloadOption(string arg)
        // ReSharper restore UnusedParameter.Global
        // ReSharper restore UnusedMember.Global
        {
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
            return Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
            // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
        }
    }
}