using System;
using System.Collections.Generic;
using System.Drawing;
using Cosmos.System;
using Cosmos.System.Graphics;

namespace DogOS.GUI
{
    public class Desktop : CompositeWidget, Events.IMouseEvent
    {
        public int mouse_x;
        public int mouse_y;

        public Canvas canvas;

        public Desktop(int w, int h, Color color) : base(null, 0, 0, w, h, color)
        {
            mouse_x = w / 2;
            mouse_y = h / 2;

            Devices.MouseManager.ScreenWidth = (uint)w;
            Devices.MouseManager.ScreenHeight = (uint)h;
            Devices.MouseManager.AddMouseEvent(this);
            Devices.MouseManager.last_x = (uint) mouse_x;
            Devices.MouseManager.last_y = (uint) mouse_y;
            canvas = new VBECanvas(new Mode(w, h, ColorDepth.ColorDepth32));
        }

        public void Draw()
        {
            base.Draw(canvas);
            Pen pen = new Pen(Kernel.mc);

            for (int i = 0; i < 4; i++)
            {
                canvas.DrawPoint(pen, mouse_x - i, mouse_y);
                canvas.DrawPoint(pen, mouse_x + i, mouse_y);
                canvas.DrawPoint(pen, mouse_x, mouse_y - i);
                canvas.DrawPoint(pen, mouse_x, mouse_y + i);
            }

            canvas.Display();
        }

        public new void OnMouseDown(int x, int y, MouseState button)
        {
            base.OnMouseDown(x, y, button);
        }

        public new void OnMouseUp(int x, int y, MouseState button)
        {
            base.OnMouseUp(x, y, button);
        }

        public new void OnMouseMove(int old_x, int old_y, int x, int y)
        {
            base.OnMouseMove(old_x, old_y, x, y);
            mouse_x = x;
            mouse_y = y;
        }

        #region IMouseEvent

        public void OnMouseEventState(int x, int y, MouseState old_state, MouseState state)
        {
            OnMouseState(x, y, old_state, state);
        }

        public void OnMouseEventMove(int old_x, int old_y, int x, int y)
        {
            OnMouseMove(old_x, old_y, x, y);
        }

        #endregion
    }
}
