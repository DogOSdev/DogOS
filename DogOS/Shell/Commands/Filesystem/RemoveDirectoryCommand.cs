using System;
using System.Collections.Generic;
using System.IO;

namespace DogOS.Shell.Commands.Filesystem
{
    class RemoveDirectoryCommand : Command
    {
        public RemoveDirectoryCommand() : base("rmdir", "Remove a directory", CommandCategory.Filesystem) { }

        public override CommandResult Execute()
        {
            return CommandResult.Failure(new Types.Errors.NotEnoughArguments(
                "No directory was specified."
            ));
        }

        public override CommandResult Execute(List<string> args)
        {
            if(Directory.Exists($"{Kernel.drive}{Kernel.dir}{args[0]}"))
            {
                try
                {
                    Directory.Delete($"{Kernel.drive}{Kernel.dir}{args[0]}");
                    return CommandResult.Success();
                }
                catch(Exception e)
                {
                    return CommandResult.Failure(new Types.Errors.UnknownError(
                        e.ToString()
                    ));
                }
            }
            else
            {
                return CommandResult.Failure(new Types.Errors.DoesNotExist(
                    $"Directory '{Kernel.drive}{Kernel.dir}{args[0]}'."
                ));
            }
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"rmdir [directory] || {Description}");
        }
    }
}