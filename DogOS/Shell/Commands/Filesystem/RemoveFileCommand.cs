using System;
using System.Collections.Generic;
using System.IO;

namespace DogOS.Shell.Commands.Filesystem
{
    internal class RemoveFileCommand : Command
    {
        public RemoveFileCommand() : base("rmfile", "Remove a file", CommandCategory.Filesystem)
        {
        }

        public override CommandResult Execute()
        {
            return CommandResult.Failure(new Types.Errors.NotEnoughArguments(
                "No file was specified."
            ));
        }

        public override CommandResult Execute(List<string> args)
        {
            if (File.Exists($"{Kernel.drive}{Kernel.dir}{args[0]}"))
            {
                try
                {
                    File.Delete($"{Kernel.drive}{Kernel.dir}{args[0]}");
                    return CommandResult.Success();
                }
                catch (Exception e)
                {
                    return CommandResult.Failure(new Types.Errors.UnknownError(
                        e.ToString()
                    ));
                }
            }
            else
            {
                return CommandResult.Failure(new Types.Errors.DoesNotExist(
                    $"File '{Kernel.drive}{Kernel.dir}{args[0]}'."
                ));
            }
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"rmfile [file] || {Description}");
        }
    }
}