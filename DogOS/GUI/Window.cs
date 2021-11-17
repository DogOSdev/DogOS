using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Sys = Cosmos.System;

namespace DogOS.GUI
{
    public class Window : CompositeWidget
    {
        protected bool dragging = false;

        public Window(Widget parent, int x, int y, int w, int h, Color color) : base(parent, x, y, w, h, color) {}

        public new void OnMouseDown(int x, int y, Sys.MouseState button)
        {
            base.OnMouseDown(x, y, button);
            dragging = button == Sys.MouseState.Left;
        }

        public new void OnMouseUp(int x, int y, Sys.MouseState button)
        {
            dragging = false;
            base.OnMouseUp(x, y, button);
        }

        public new void OnMouseMove(int old_x, int old_y, int x, int y)
        {
            if(dragging)
            {
                this.x += x - old_x;
                this.y += y - old_y;
            }
            base.OnMouseMove(old_x, old_y, x, y);
        }
    }
}
