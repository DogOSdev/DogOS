using System;
using System.Collections.Generic;

namespace DogOS.Shell.Commands.General
{
    public class ClearCommand : Command
    {
        public ClearCommand() : base("clear", "Clears the screen", CommandCategory.General)
        {
        }

        public override CommandResult Execute()
        {
            Console.Clear();
            return CommandResult.Success();
        }

        public override CommandResult Execute(List<string> args)
        {
            // Ignore any params given.
            return Execute();
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"\tclear || {Description}");
        }
    }
}