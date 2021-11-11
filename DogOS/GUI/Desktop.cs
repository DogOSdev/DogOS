using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Sys = Cosmos.System;

namespace DogOS.GUI
{
    public class Desktop : CompositeWidget, Events.IMouseEvent
    {
        public int mouse_x;
        public int mouse_y;

        public Desktop(int w, int h, Color color) : base(null, 0, 0, w, h, color)
        {
            mouse_x = w / 2;
            mouse_y = h / 2;
            Devices.MouseManager.ScreenWidth = (uint)w;
            Devices.MouseManager.ScreenHeight = (uint)h;
        }

        public new void Draw(GUI.DisplayDriver display_driver)
        {
            base.Draw(display_driver);

            for (int i = 0; i < 4; i++)
            {
                display_driver.Pixel(mouse_x - i, mouse_y, Color.White);
                display_driver.Pixel(mouse_x + i, mouse_y, Color.White);
                display_driver.Pixel(mouse_x, mouse_y - i, Color.White);
                display_driver.Pixel(mouse_x, mouse_y + i, Color.White);
            }
        }

        public new void OnMouseMove(int old_x, int old_y, int x, int y)
        {
            base.OnMouseMove(old_x, old_y, x, y);
            mouse_x = x;
            mouse_y = y;
        }
    }
}
