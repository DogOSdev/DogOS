using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DogOS.Shell.Commands.Filesystem
{
    public class ChangeDirectoryCommand : Command
    {
        public ChangeDirectoryCommand() : base("cd", "change the current directory")
        {
        }

        private StringBuilder keep_sys_text()
        { return new StringBuilder(); }

        private string[] keep_sys_io()
        { return Directory.GetDirectories(@"0:\"); }

        public override void Execute()
        {
            Console.WriteLine("Change Directory Command.");
        }

        public override void Execute(List<string> args)
        {
            Execute();
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"\tcd || {Description}");
        }
    }
}