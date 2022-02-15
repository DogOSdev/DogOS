using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DogOS.Shell.Commands.Filesystem
{
    public class ChangeDirectory : Command
    {
        public ChangeDirectory() : base("cd", "Change the active directory.") { }

        public override void Execute()
        {
            Console.WriteLine($"{Kernel.drive}{Kernel.dir}");
        }

        public override void Execute(List<string> args)
        {
            var backslash_dir = args[0].Replace('/', '\\');
            if(backslash_dir == "..")
            {
                if($"{Kernel.drive}{Kernel.dir}" != $"{Kernel.drive}\\")
                {
                    string res = @"\";
                    string[] split_dir = Kernel.dir.Split(@"\", StringSplitOptions.RemoveEmptyEntries);

                    for (var i = 0; i < split_dir.Length; ++i)
                    {
                        if (i < split_dir.Length - 1)
                        {
                            res += $@"{split_dir[i]}\";
                        }
                    }

                    Kernel.dir = res;
                    return;
                }
            }

            if(Directory.Exists($"{Kernel.drive}{Kernel.dir}{backslash_dir}\\"))
            {
                Kernel.dir = $"{Kernel.dir}{backslash_dir}\\";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Directory '{Kernel.drive}{Kernel.dir}{backslash_dir}\\' does not exist.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"cd || {Description}");
        }
    }
}
