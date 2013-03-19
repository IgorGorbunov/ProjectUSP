using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NXOpen;
using NXOpen.UF;
using NXOpen.Utilities;
using NXOpenUI;
using NXOpen.Assemblies;
using NXOpen.Preferences;

namespace Katalog2005
{
    static class Program
    {
        public static void Main(string[] args)
        {
          /*  MessageBox.Show(args[0]);
            MessageBox.Show(args[1]);
            MessageBox.Show(args[2]);
            MessageBox.Show(args[3]);*/

       /*     string connectionString = "";
            connectionString += "user id=" + System.Environment.GetEnvironmentVariable("KTPP_DB_USER");
           // connectionString += ";data source=" + findCorrectServerName(System.Environment.GetEnvironmentVariable("KTPP_DB_SERVER"));
            connectionString += ";password=" + System.Environment.GetEnvironmentVariable("KTPP_DB_PASSWORD");

            MessageBox.Show(connectionString.ToString());*/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Katalog());
                        
        }
        
        public static int GetUnloadOption(string dummy)
        {
            return UFConstants.UF_UNLOAD_IMMEDIATELY;
        }
    }
}