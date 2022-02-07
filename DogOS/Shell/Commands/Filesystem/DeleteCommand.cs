using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DogOS.Shell.Commands.Filesystem
{
    public class DeleteCommand : Command
    {
        public DeleteCommand() : base("del", "Delete a file or directory.") { }

        public override void Execute()
        {
            Console.WriteLine("Execute del");
        }

        public override void Execute(List<string> args)
        {
            Execute();
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine("\tdel [file]");
            Console.WriteLine("\tdel [directory]");
        }
    }
}