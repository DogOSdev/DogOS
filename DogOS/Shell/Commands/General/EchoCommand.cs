using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell.Commands.General
{
    public class EchoCommand : Command
    {
        public EchoCommand() : base("echo", "Echo a message, or toggle echo.", CommandCategory.General)
        {
        }

        public override CommandResult Execute()
        {
            if (Shell.echo_on)
                Console.WriteLine("Echo is enabled.");
            else
                Console.WriteLine("Echo is disabled.");

            return CommandResult.Success();
        }

        public override CommandResult Execute(List<string> args)
        {
            if (args[0] != "-t")
            {
                var str = new StringBuilder();

                foreach (var arg in args)
                {
                    str.Append($"{arg} ");
                }
                Console.WriteLine(str);
                return CommandResult.Success();
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
                return CommandResult.Failure(new Types.Errors.InvalidOption(
                    $"'{args[1]}' is not a valid option. Valid options are 'ON' or 'OFF'."
                ));
            }

            return CommandResult.Success();
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine("\techo -h || Display's help message.");
            Console.WriteLine($"\techo [message]* || {Description}");
            Console.WriteLine($"\techo \"[message]\" || {Description}");
            Console.WriteLine($"\techo -t (ON | OFF) || Turns echo on or off");
            Console.WriteLine("\techo || Displays the echo setting.");
        }
    }
}