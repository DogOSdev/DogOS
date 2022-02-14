using System;
using System.Collections.Generic;

namespace DogOS.Shell.Commands
{
    public class ClearCommand : Command
    {
        public ClearCommand() : base("clear", "Clears the screen", DogOS.Shell.Category.General)
        {
        }

        public override void Execute()
        {
            Console.Clear();
        }

        public override void Execute(List<string> args)
        {
            // Ignore any params given.
            Execute();
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"\tclear || {Description}");
        }
    }
}