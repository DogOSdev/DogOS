using System;
using System.Collections.Generic;

namespace DogOS.Shell.Commands
{
    public class ShutdownCommand : Command
    {
        public ShutdownCommand() : base("shutdown", "Shuts down your computer.", DogOS.Shell.Category.General)
        {
        }

        public override void Execute()
        {
            Cosmos.System.Power.Shutdown();
        }

        public override void Execute(List<string> args)
        {
            if (args.Contains("-r") || args.Contains("--restart"))
            {
                Cosmos.System.Power.Reboot();
                return;
            }
            // Ignore any commands given.
            Execute();
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