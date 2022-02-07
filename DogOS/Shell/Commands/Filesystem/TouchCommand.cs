using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DogOS.Shell.Commands.Filesystem
{
    public class TouchCommand : Command
    {
        public TouchCommand() : base("touch", "Create a empty file.") { }

        public override void Execute()
        {
            Console.WriteLine("Execute touch.");
        }

        public override void Execute(List<string> args)
        {
            Execute();
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"\ttouch [file_name] || {Description}");
        }
    }
}