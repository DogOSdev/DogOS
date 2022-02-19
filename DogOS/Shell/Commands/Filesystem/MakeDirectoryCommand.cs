using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DogOS.Shell.Commands.Filesystem
{
    public class MakeDirectoryCommand : Command
    {
        public MakeDirectoryCommand() : base("mkdir", "Create a empty directory.", CommandCategory.Filesystem) { }

        public override CommandResult Execute()
        {
            return CommandResult.Failure(new Types.Errors.NotEnoughArguments(
                "A directory name was not specified."
            ));
        }


        public override CommandResult Execute(List<string> args)
        {
            if(Directory.Exists($"{Kernel.drive}{Kernel.dir}{args[0]}"))
            {
                return CommandResult.Failure(new Types.Errors.AlreadyExists(
                    $"Directory {args[0]}"
                ));
            }

            try
            {
                Directory.CreateDirectory($"{Kernel.drive}{Kernel.dir}{args[0]}");
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Failure(new Types.Errors.UnknownError(
                    e.ToString()
                ));
            }
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"\tmkdir [file_name] || {Description}");
        }
    }
}