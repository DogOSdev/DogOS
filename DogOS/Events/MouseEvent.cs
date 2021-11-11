using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace DogOS.Events
{
    public interface IMouseEvent
    {
        void OnMouseState(int x, int y, Sys.MouseState old_state, Sys.MouseState state);
        void OnMouseMove(int old_x, int old_y, int x, int y);
    }
}
