using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Sys = Cosmos.System;

namespace DogOS.GUI.Components
{
    public abstract class Button : DogOS.GUI.Widget
    {
        protected new bool focusable = false;
        public Button(Widget parent, int x, int y, int w, int h) : base(parent, x, y, w, h, Color.Gray) { }

        public abstract void OnButtonPress();

        public new void OnMouseDown(int x, int y, Sys.MouseState button)
        {
            if(button == Sys.MouseState.Left)
            {
                OnButtonPress();
            }
            base.OnMouseDown(x, y, button);
        }
    }
}