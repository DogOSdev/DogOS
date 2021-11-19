using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell
{
    public static class CommandList
    {
        public static List<Commands.Command> commands = new List<Commands.Command>();

        static CommandList()
        {
            commands.Add(new Commands.EchoCommand());
        }
    }
}
