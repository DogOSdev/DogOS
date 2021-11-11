using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Sys = Cosmos.System;

namespace DogOS.GUI
{
    public class Widget
    {
        protected Widget parent;
        protected int x;
        protected int y;
        protected int w;
        protected int h;
        protected Color color;
        protected bool focusable = true;

        #region Public

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
                parent.GetFocus(this);
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
            return this.x <= x && x < this.x + w && this.y <= y && y < this.y + h;
        }

        public void Draw(GUI.DisplayDriver display_driver)
        {
            int X = 0;
            int Y = 0;
            ModelToScreen(ref X, ref Y);
            display_driver.Rectangle(X, Y, w, h, color, true);
        }

        public void OnMouseDown(int x, int y, Sys.MouseState button)
        {
            if(focusable)
                GetFocus(this);
        }

        public void OnMouseUp(int x, int y, Sys.MouseState button) { return; }

        public void OnMouseMove(int old_x, int old_y, int x, int y) { return; }

        #endregion

        #region Mouse Event Handler

        public void OnMouseState(int x, int y, Sys.MouseState old_state, Sys.MouseState state)
        {
            // Button has been pressed, enter that method.
            if(old_state == Sys.MouseState.None && state != Sys.MouseState.None)
            {
                OnMouseDown(x, y, state);
                return;
            }

            // Button has been released, enter that method
            if(old_state != Sys.MouseState.None && state == Sys.MouseState.None)
            {
                OnMouseUp(x, y, old_state);
            }
        }
        
        #endregion
    }
}
