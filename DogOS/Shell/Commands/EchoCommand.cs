using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell.Commands
{
    public class EchoCommand : Command
    {
        public EchoCommand() : base("echo", "Echo a message, or toggle echo.")
        {
        }

        public override void Execute()
        {
            if (Shell.echo_on)
                Console.WriteLine("Echo is enabled.");
            else
                Console.WriteLine("Echo is disabled.");
        }

        public override void Execute(List<string> args)
        {
            if (args[0] != "-t")
            {
                var str = new StringBuilder();

                foreach (var arg in args)
                {
                    str.Append($"{arg} ");
                }
                Console.WriteLine(str);
                return;
            }

            var lower = args[1].ToLower();

            if (lower == "on")
            {
                Shell.echo_on = true;
            }
            else if (lower == "off")
            {
                Shell.echo_on = false;
            }
            else
            {
                Console.WriteLine($"ERR: '{args[1]}' is not a valid option. Valid options are 'ON' or 'OFF'.");
            }
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine("\techo -h || Display's help message.");
            Console.WriteLine($"\techo [message] || {Description}");
            Console.WriteLine($"\techo -t (ON | OFF) || Turns echo on or off");
            Console.WriteLine("\techo || Displays the echo setting.");
        }
    }
}