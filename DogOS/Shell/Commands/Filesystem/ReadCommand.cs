using System;
using System.Collections.Generic;
using System.IO;

namespace DogOS.Shell.Commands.Filesystem
{
    public class ReadCommand : Command
    {
        public ReadCommand() : base("read", "Read a file.") { }

        public override void Execute()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERR: A file has not been given.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public override void Execute(List<string> args)
        {
            if(!File.Exists(args[0]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERR: File '{Shell.drive}:{Shell.dir}{args[0]}' does not exist.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            try
            {
                Console.WriteLine(File.ReadAllText($"{Shell.drive}:{Shell.dir}{args[0]}"));
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERR: {e}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"\tread [file] || {Description}");
        }
    }
}
