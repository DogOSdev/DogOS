using System.Drawing;
using Cosmos.System;

namespace DogOS.GUI
{
    public class Window : CompositeWidget
    {
        private bool dragging;

        public Window(Widget parent, int x, int y, int w, int h, Color color) : base(parent, x, y, w, h, color) { }

        public new void OnMouseDown(int x, int y, MouseState button)
        {
            base.OnMouseDown(x, y, button);
            dragging = button == MouseState.Left;
        }

        public new void OnMouseUp(int x, int y, MouseState button)
        {
            base.OnMouseUp(x, y, button);
            dragging = false;
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
