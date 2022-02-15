using System;
using System.Collections.Generic;

namespace DogOS.Shell.Commands
{
    public class Command
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public CommandCategory Category { get; private set; }

        public Command(string name, string description, CommandCategory category)
        {
            Name = name;
            Description = description;
            Category = category;
        }

        public virtual CommandResult Execute()
        {
            return CommandResult.Success();
        }

        public virtual CommandResult Execute(List<string> args)
        {
            return CommandResult.Success();
        }

        public virtual void Help()
        {
            Console.WriteLine("There is no help infomation available for this command.");
        }
    }
}