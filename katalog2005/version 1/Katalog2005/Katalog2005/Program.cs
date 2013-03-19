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
        public static void Main()
        {
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