using System;
using System.Collections.Generic;
using System.IO;

namespace DogOS.Shell.Commands.Filesystem
{
    public class DirectoryCommand : Command
    {
        public DirectoryCommand() : base("dir", "List the current directory", CommandCategory.Filesystem)
        {
        }

        private CommandResult GetDirectoryContents(string curr_dir)
        {
            if (Directory.Exists(curr_dir))
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
                return CommandResult.Success();
            }

            return CommandResult.Failure(new Types.Errors.DoesNotExist(
                $"Directory '{curr_dir}'"
            ));
        }

        public override CommandResult Execute()
        {
            return GetDirectoryContents($"{Kernel.drive}{Kernel.dir}");
        }

        public override CommandResult Execute(List<string> args)
        {
            return Execute();
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"\tdir || {Description}");
        }
    }
}