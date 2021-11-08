using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;
using IL2CPU.API.Attribs;
using Cosmos.HAL;

namespace DogOS
{
    public class CustomMouseEvent : Events.MouseEvent
    {
        public override void OnMouseState(Sys.MouseState state)
        {
            switch (state)
            {
                case Sys.MouseState.None:
                    Kernel.background = System.Drawing.Color.Black;
                    break;
                case Sys.MouseState.Left:
                    Kernel.background = System.Drawing.Color.Red;
                    break;
                case Sys.MouseState.Right:
                    Kernel.background = System.Drawing.Color.Yellow;
                    break;
                case Sys.MouseState.Middle:
                    Kernel.background = System.Drawing.Color.Green;
                    break;
                case Sys.MouseState.FourthButton:
                    Kernel.background = System.Drawing.Color.Blue;
                    break;
                case Sys.MouseState.FifthButton:
                    Kernel.background = System.Drawing.Color.Purple;
                    break;
                default:
                    break;
            }
        }

        public override void OnMouseMove(int x, int y)
        {
            // Sys.Global.mDebugger.Send($"Mouse Move x: {x} y: {y}");
        }
    }

    public class Kernel : Sys.Kernel
    {
        #region globals

        public static string os_name = "DogOS";
        public string version = "0.0.1";
        public static bool running = false;
        public GUI.DisplayDriver desktop;
        public static System.Drawing.Color background = System.Drawing.Color.White;

        #endregion

        protected override void BeforeRun()
        {
            try
            {
                Devices.MouseManager.ScreenWidth = 640;
                Devices.MouseManager.ScreenHeight = 480;
                Devices.MouseManager.AddMouseEvent(new CustomMouseEvent());
                desktop = new GUI.BufferedDisplayDriver(640, 480);

                running = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected override void Run()
        {
            desktop.Clear(background);
            desktop.Draw();
        }
    }
}
