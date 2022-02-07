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
            if (Shell.dir != @"\")
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
            GetDirectoryContents($"{Shell.drive}:\\{Shell.dir}");
        }

        public override void Execute(List<string> args)
        {
            var tree = new StringBuilder();

            for (int i = 0; i < args.Count; i++)
            {
                var arg = args[i];
                tree.Append(arg);

                if (i < args.Count - 1)
                    tree.Append(@"\");
            }

            GetDirectoryContents($"{Shell.drive}:\\{Shell.dir}\\{tree.ToString()}");
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"\tdir || {Description}");
            Console.WriteLine($"\tdir [directories]* || Go down the directory tree specified.");
        }
    }
}