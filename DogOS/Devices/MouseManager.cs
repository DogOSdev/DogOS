using System.Collections.Generic;

namespace DogOS.Devices
{
    // This is heavily based on Cosmos's MouseManager,
    // except with the added functionality of MouseEvents.

    // I would make Cosmos's MouseManager as the derived class,
    // except it's static so I cant.
    public static class MouseManager // : Cosmos.System.MouseManager (Error CS0713)
    {
        private static List<Cosmos.HAL.MouseBase> mouse_list = new List<Cosmos.HAL.MouseBase>();
        public static List<Events.IMouseEvent> mouse_events = new List<Events.IMouseEvent>();

        public static uint last_x;
        public static uint last_y;
        public static Cosmos.System.MouseState last_state;
        private static uint screen_width;
        private static uint screen_height;

        public static float MouseSensitivity = 1.0f;

        public static uint ScreenWidth
        {
            get => screen_width;
            set
            {
                screen_width = value;

                if (last_x >= screen_width)
                {
                    last_x = screen_width - 1;
                }
            }
        }

        public static uint ScreenHeight
        {
            get => screen_height;
            set
            {
                screen_height = value;

                if (last_y >= screen_height)
                {
                    last_y = screen_height - 1;
                }
            }
        }

        static MouseManager()
        {
            foreach (var mouse in Cosmos.HAL.Global.GetMouseDevices())
            {
                AddMouse(mouse);
            }
        }

        public static void HandleMouse(int delta_x, int delta_y, int mouse_state, int scroll_wheel)
        {
            var state = (Cosmos.System.MouseState)mouse_state;
            uint realx, realy;

            int x = (int)(last_x + MouseSensitivity * delta_x);
            int y = (int)(last_y + MouseSensitivity * delta_y);

            if (x <= 0)
            {
                realx = 0;
            }
            else if (x >= ScreenWidth)
            {
                realx = ScreenWidth - 1;
            }
            else
            {
                realx = (uint)x;
            }

            if (y <= 0)
            {
                realy = 0;
            }
            else if (y >= ScreenHeight)
            {
                realy = ScreenHeight - 1;
            }
            else
            {
                realy = (uint)y;
            }

            foreach (var mouse_event in mouse_events)
            {
                mouse_event.OnMouseEventState((int)realx, (int)realy, last_state, state);
                mouse_event.OnMouseEventMove((int)last_x, (int)last_y, (int)realx, (int)realy);
            }
            last_state = state;
            last_x = realx;
            last_y = realy;
        }

        public static void AddMouseEvent(Events.IMouseEvent mouse_event)
        {
            if (mouse_events.Contains(mouse_event))
            {
                return;
            }
            mouse_events.Add(mouse_event);
        }

        public static void RemoveMouseEvent(Events.IMouseEvent mouse_event)
        {
            if (mouse_events.Contains(mouse_event))
            {
                mouse_events.Remove(mouse_event);
            }
            return;
        }

        private static void AddMouse(Cosmos.HAL.MouseBase mouse)
        {
            mouse.OnMouseChanged = HandleMouse;
            mouse_list.Add(mouse);
        }
    }
}