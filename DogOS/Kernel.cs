using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;
using IL2CPU.API.Attribs;
using Cosmos.HAL;

namespace DogOS
{
    public class Kernel : Sys.Kernel
    {
        #region globals

        public static string os_name = "DogOS";
        public string version = "0.0.1";
        public static bool running = false;
        public GUI.Desktop desktop;
        public static System.Drawing.Color background = System.Drawing.Color.White;

        #endregion

        protected override void BeforeRun()
        {
            try
            {
                desktop = new GUI.Desktop(640, 480, System.Drawing.Color.Blue);
                Devices.MouseManager.AddMouseEvent(desktop);
                var win1 = new GUI.Window(desktop, 0, 0, 30, 20, System.Drawing.Color.Red);
                desktop.AddChild(win1);

                running = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected override void Run()
        {
        }
    }
}
