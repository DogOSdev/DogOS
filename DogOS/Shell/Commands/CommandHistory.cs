using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell.Commands
{
    public class CommandHistory
    {
        public static int Index = 0;
        public static List<string> commands = new List<string>();

        public static void Add(string cmd)
        {
            commands.Add(cmd);
            Index = commands.Count - 1;
        }

        public static void ClearCurrentConsoleLine()
        {
            int current_line_cursor = Console.CursorTop;
            Console.SetCursorPosition(0, current_line_cursor);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, current_line_cursor);
        }
    }
}
