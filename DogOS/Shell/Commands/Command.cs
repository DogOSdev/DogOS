using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell.Commands
{
    public abstract class Command
    {
        public string Name
        {
            get => name;
        }
        
        public string Description
        {
            get => description;
        }

        protected string name;
        protected string description;

        public Command(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public abstract int Execute(List<string> args);
    }
}
