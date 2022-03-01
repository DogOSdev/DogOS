using System;
using System.Collections.Generic;
using System.Drawing;
using Cosmos.System;
using Cosmos.System.Graphics;

namespace DogOS.GUI
{
    public abstract class CompositeWidget : Widget
    {
        private List<Widget> children = new List<Widget>();
        private Widget focused_child = null;

        public CompositeWidget(Widget parent, int x, int y, int w, int h, Color color) : base(parent, x, y, w, h, color) { }

        public new void GetFocus(Widget widget)
        {
            focused_child = widget;
            if(parent != null)
                parent.GetFocus(widget);
        }

        public void AddChild(Widget child)
        {
            children.Add(child);
        }

        public new void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            foreach (var child in children)
            {
                child.Draw(canvas);
            }
        }

        public new void OnMouseDown(int x, int y, MouseState button)
        {
            foreach (var child in children)
            {
                if(child.ContainsCoordinate(x - this.x, y - this.y))
                {
                    child.OnMouseDown(x - this.x, y - this.y, button);
                    break;
                }
            }
        }

        public new void OnMouseUp(int x, int y, MouseState button)
        {
            foreach (var child in children)
            {
                if (child.ContainsCoordinate(x - this.x, y - this.y))
                {
                    child.OnMouseUp(x - this.x, y - this.y, button);
                    break;
                }
            }
        }

        public new void OnMouseMove(int old_x, int old_y, int x, int y)
        {
            int first_child = -1;

            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].ContainsCoordinate(old_x - this.x, old_y - this.y))
                {
                    children[i].OnMouseMove(old_x - this.x, old_y - this.y, x - this.x, y - this.y);
                    first_child = i;
                    break;
                }
            }

            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].ContainsCoordinate(x - this.x, y - this.y))
                {
                    if (first_child != i)
                        children[i].OnMouseMove(old_x - this.x, old_y - this.y, x - this.x, y - this.y);
                    break;
                }
            }
        }
    }
}
