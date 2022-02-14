using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DogOS.Shell.Commands.Filesystem
{
    public class DirectoryCommand : Command
    {
        public DirectoryCommand() : base("dir", "List the current directory")
        {
        }

        private void GetDirectoryContents(string curr_dir)
        {
            if (curr_dir != $"{Kernel.drive}\\")
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("..");

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }

            foreach (var dir in Directory.GetDirectories(curr_dir))
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(dir);

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("/");
            }

            foreach (var file in Directory.GetFiles(curr_dir))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(file);

                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public override void Execute()
        {
            GetDirectoryContents($"{Kernel.drive}{Kernel.dir}");
        }

        public override void Execute(List<string> args)
        {
            Execute();
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"\tdir || {Description}");
        }
    }
}