using System.Drawing;
using Cosmos.System;
using Cosmos.System.Graphics;

namespace DogOS.GUI
{
    public abstract class Widget
    {
        protected Widget parent;
        protected int x;
        protected int y;
        protected int w;
        protected int h;
        protected Color color;
        protected bool focusable = true;

        public Widget(Widget parent, int x, int y, int w, int h, Color color)
        {
            this.parent = parent;
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.color = color;
        }

        public void GetFocus(Widget widget)
        {
            if(parent != null)
                parent.GetFocus(widget);
        }

        public void ModelToScreen(ref int x, ref int y)
        {
            if(parent != null)
                parent.ModelToScreen(ref x, ref y);

            x += this.x;
            y += this.y;
        }
        public bool ContainsCoordinate(int x, int y)
        {
            return this.x <= x && x < this.x + w
                && this.y <= y && y < this.y + h;
        }

        public void Draw(Canvas canvas)
        {
            int X = 0;
            int Y = 0;
            Pen pen = new Pen(color);

            ModelToScreen(ref X, ref Y);
            canvas.DrawFilledRectangle(pen, X, Y, w, h);
        }

        public void OnMouseDown(int x, int y, MouseState button)
        {
            if(focusable)
                GetFocus(this);
        }

        public void OnMouseUp(int x, int y, MouseState button) { }
        public void OnMouseMove(int old_x, int old_y, int x, int y) { }

        #region Mouse Event Handler

        public void OnMouseState(int x, int y, MouseState old_state, MouseState state)
        {
            // Button has been pressed, enter that method.
            if (old_state == MouseState.None && state != MouseState.None)
            {
                OnMouseDown(x, y, state);
                return;
            }

            // Button has been released, enter that method
            if (old_state != MouseState.None && state == MouseState.None)
            {
                OnMouseUp(x, y, old_state);
            }
        }

        #endregion
    }
}
