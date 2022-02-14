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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERR: Missing filename.");
            Console.ForegroundColor = ConsoleColor.White;
        }


        public override void Execute(List<string> args)
        {
            if(File.Exists($"{Kernel.drive}{Kernel.dir}{args[0]}"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERR: File exists.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            try
            {
                File.Create($"{Kernel.drive}{Kernel.dir}{args[0]}");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"\ttouch [file_name] || {Description}");
        }
    }
}