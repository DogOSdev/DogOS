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

        #endregion

        protected override void BeforeRun()
        {
            Console.WriteLine($"{os_name} v{version}");
            Console.WriteLine("(c) 2021 TaromaruYuki and Contributers.\n");
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
