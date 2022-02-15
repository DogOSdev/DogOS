using System;
using System.Collections.Generic;

namespace DogOS.Shell.Commands.General
{
    public class ShutdownCommand : Command
    {
        public ShutdownCommand() : base("shutdown", "Shuts down your computer.", CommandCategory.General)
        {
        }

        public override CommandResult Execute()
        {
            Cosmos.System.Power.Shutdown();
            return CommandResult.Success();
        }

        public override CommandResult Execute(List<string> args)
        {
            if (args.Contains("-r") || args.Contains("--restart"))
            {
                Cosmos.System.Power.Reboot();
                return CommandResult.Success();
            }
            // Ignore any commands given.
            return Execute();
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine("\tshutdown -h || Display's help message.");
            Console.WriteLine("\tshutdown -r || Restart's the system.");
            Console.WriteLine($"\tshutdown || {Description}");
        }
    }
}