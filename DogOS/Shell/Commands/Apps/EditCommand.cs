using System;
using System.Collections.Generic;

namespace DogOS.Shell.Commands.Apps
{
    class EditCommand : Command
    {
        public EditCommand() : base("edit", "Edit a file", CommandCategory.General) { }

        public override CommandResult Execute()
        {
            return CommandResult.Failure(new Types.Errors.NotEnoughArguments(
                "No file provided to edit."
            ));
        }

        public override CommandResult Execute(List<string> args)
        {
            var editor = new DogOS.Shell.Apps.Editor(args[0]);
            editor.Run();

            return CommandResult.Success();
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"\tedit [file] || {Description}");
        }
    }
}
