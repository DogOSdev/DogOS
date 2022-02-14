using System;
using System.Collections.Generic;
using System.IO;

namespace DogOS.Shell.Commands.Filesystem
{
    class RemoveFileCommand : Command
    {
        public RemoveFileCommand() : base("rmfile", "Remove a file", DogOS.Shell.Category.Filesystem) { }

        public override void Execute()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERR: No file was specified.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public override void Execute(List<string> args)
        {
            if(File.Exists($"{Kernel.drive}{Kernel.dir}{args[0]}"))
            {
                File.Delete($"{Kernel.drive}{Kernel.dir}{args[0]}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERR: File '{Kernel.drive}{Kernel.dir}{args[0]}' does not exist.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"rmfile [file] || {Description}");
        }
    }
}