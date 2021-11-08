using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace DogOS.Events
{
    public abstract class MouseEvent
    {
        public abstract void OnMouseState(Sys.MouseState state);
        public abstract void OnMouseMove(int x, int y);
    }
}
