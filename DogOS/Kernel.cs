using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace DogOS
{
    public class Kernel : Sys.Kernel
    {
        #region globals

        public static string os_name = "DogOS";
        public string version = "0.0.1";
        public static bool running = false;
        public static bool inGUI = false;
        public GUI.Desktop desktop;
        public static System.Drawing.Color background = System.Drawing.Color.White;

        #endregion

        protected override void BeforeRun()
        {
            running = true;
        }

        protected override void Run()
        {
            if(!inGUI)
            {
                Shell.Shell.Run();
            }
        }
    }
}
