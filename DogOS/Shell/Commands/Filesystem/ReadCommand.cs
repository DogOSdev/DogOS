using System;
using System.Collections.Generic;
using System.IO;

namespace DogOS.Shell.Commands.Filesystem
{
    public class ReadCommand : Command
    {
        public ReadCommand() : base("read", "Read a file.", CommandCategory.Filesystem) { }

        public override CommandResult Execute()
        {
            return CommandResult.Failure(new Types.Errors.NotEnoughArguments(
                "A file has not been given."
            ));
        }

        public override CommandResult Execute(List<string> args)
        {
            if(File.Exists($"{Kernel.drive}{Kernel.dir}{args[0]}"))
            {
                try
                {
                    foreach (var line in File.ReadAllLines($"{Kernel.drive}{Kernel.dir}{args[0]}"))
                    {
                        Console.WriteLine(line);
                    }
                    
                    return CommandResult.Success();
                }
                catch (Exception e)
                {
                    // TODO: Get the type of error and raise the right error.
                    return CommandResult.Failure(new Types.Errors.UnknownError(
                        e.ToString()
                    ));
                }
            }
            else
            {
                return CommandResult.Failure(new Types.Errors.DoesNotExist(
                    $"File '{Kernel.drive}{Kernel.dir}{args[0]}'"
                ));
            }
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"\tread [file] || {Description}");
        }
    }
}
