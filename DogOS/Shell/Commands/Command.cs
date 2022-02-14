using System;
using System.Collections.Generic;

namespace DogOS.Shell.Commands
{
    public class Command
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Category Category { get; private set; }

        public Command(string name, string description, Category category)
        {
            Name = name;
            Description = description;
            Category = category;
        }

        public virtual void Execute()
        {
            return;
        }

        public virtual void Execute(List<string> args)
        {
            return;
        }

        public virtual void Help()
        {
            Console.WriteLine("There is no help infomation available for this command.");
        }
    }
}