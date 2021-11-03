using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;
using IL2CPU.API.Attribs;
using Cosmos.HAL;

namespace DogOS
{
    public enum ErrorTypes
    {
        Null = -1,
        UnknownCommand,
        RequiredArgument
    }

    public class Kernel : Sys.Kernel
    {
        #region globals

        public static string os_name = "DogOS";
        public string version = "0.0.1";
        public static bool running = false;
        public GUI.DisplayDriver desktop;

        #endregion

        protected override void BeforeRun()
        {
            try
            {
                desktop = new GUI.DisplayDriver();
                running = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected override void Run()
        {   
            desktop.Pixel(5, 5, System.Drawing.Color.Black, 5);
        }
    }
}
