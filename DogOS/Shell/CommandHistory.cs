using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell
{
    static class CommandHistory
    {
        static private List<Commands.Command> command_history;
        static private int index = 0;

        public static Commands.Command GoBack(int amount=1)
        {
            if(index - amount >= 0)
            {
                index -= amount;
                return command_history[index];
            }
            return null;
        }

        public static Commands.Command GoForward(int amount = 1)
        {
            if(index + amount + 1 < command_history.Count)
            {
                index -= amount;
                return command_history[index];
            }
            return null;
        }

        public static Commands.Command GetCurrent()
        {
            return command_history[index];
        }

        public static void AddCommand(Commands.Command command)
        {
            command_history.Add(command);
        }
    }
}
