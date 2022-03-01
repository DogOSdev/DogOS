using Sys = Cosmos.System;

namespace DogOS.Events
{
    public interface IMouseEvent
    {
        void OnMouseEventState(int x, int y, Sys.MouseState old_state, Sys.MouseState state);
        void OnMouseEventMove(int old_x, int old_y, int x, int y);
    }
}