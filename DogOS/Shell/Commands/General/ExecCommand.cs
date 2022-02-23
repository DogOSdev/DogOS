using System;
using System.Collections.Generic;
using System.IO;

namespace DogOS.Shell.Commands.General
{
    public class ExecCommand : Command
    {
        public ExecCommand() : base("exec", "Execute a .dog file", CommandCategory.General) { }

        public override CommandResult Execute()
        {
            return CommandResult.Failure(new Types.Errors.NotEnoughArguments(
                "File not specified." 
            ));
        }

        public override CommandResult Execute(List<string> args)
        {
            if(!args[0].EndsWith(".dog"))
            {
                return CommandResult.Failure(new Types.Errors.InvalidOption(
                    $"File {args[0]} is not a script." 
                ));
            }

            List<string> lines = new List<string>();
            try
            {
                foreach (var line in File.ReadAllText($"{Kernel.drive}{Kernel.dir}{args[0]}").Split("\n"))
                {
                    lines.Add(line);
                }
            }
            catch(Exception e)
            {
                return CommandResult.Failure(new Types.Errors.UnknownError(
                    $"Exception {e}" 
                ));
            }

            if(lines.Count == 0)
            {
                Console.WriteLine("Script is empty.");
                return CommandResult.Success();
            }

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];

                if (line.Length <= 0 || string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
                    continue;

                if (Shell.echo_on)
                {
                    Console.WriteLine($"{Shell.FormatPrefix()}{line}");
                }

                var cmd_res = Shell.ExecuteCommand(line);

                if(cmd_res.IsError())
                {
                    Console.WriteLine($"Error in script {args[0]} at line {i + 1}. Stopping.");

                    return cmd_res;
                }

                Console.WriteLine();
            }

            return CommandResult.Success();
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine($"\texec [file_name] || {Description}");
        }
    }
}
