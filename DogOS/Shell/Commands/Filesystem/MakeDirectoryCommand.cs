using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DogOS.Shell.Commands.Filesystem
{
    public class MakeDirectoryCommand : Command
    {
        public MakeDirectoryCommand() : base("mkdir", "Creates a directory.") { }

        public override void Execute()
        {
            Console.WriteLine("Execute mkdir.");
        }

        public override void Execute(List<string> args)
        {
            Execute();
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"\tmkdir || {Description}");
        }
    }
}