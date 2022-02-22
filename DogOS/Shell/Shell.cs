using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell
{
    public static class Shell
    {
        public static bool echo_on = true;
        public static string Prefix = "$os_name $drive$path>";
        public static List<Commands.Command> commands = new List<Commands.Command>();

        static Shell()
        {
            commands.Add(new Commands.General.EchoCommand());
            commands.Add(new Commands.General.SHA256Command());
            commands.Add(new Commands.General.ShutdownCommand());
            commands.Add(new Commands.General.ClearCommand());
            commands.Add(new Commands.General.HelpCommand());

            commands.Add(new Commands.Filesystem.DirectoryCommand());
            commands.Add(new Commands.Filesystem.MakeFileCommand());
            commands.Add(new Commands.Filesystem.ReadCommand());
            commands.Add(new Commands.Filesystem.ChangeDirectory());
            commands.Add(new Commands.Filesystem.RemoveFileCommand());
            commands.Add(new Commands.Filesystem.MakeDirectoryCommand());
            commands.Add(new Commands.Filesystem.RemoveDirectoryCommand());

            commands.Add(new Commands.Apps.EditCommand());
        }

        public static string FormatPrefix()
        {
            return Prefix.Replace("$os_name", Kernel.os_name)
                .Replace("$drive", Kernel.drive)
                .Replace("$path", Kernel.dir);
        }

        // https://stackoverflow.com/a/59638742/13617487
        // Once I implement Doggoscript (which is a old interpreted language I made), this function won't really be needed.
        public static List<string> ParseInput(string input)
        {
            var args = new List<string>();
            var current_arg = new StringBuilder();
            var in_quotes = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '"')
                {
                    if (in_quotes)
                    {
                        args.Add(current_arg.ToString());
                        current_arg = new StringBuilder();
                        in_quotes = false;
                    }
                    else
                    {
                        in_quotes = true;
                    }
                }
                else if (input[i] == ' ')
                {
                    if (in_quotes)
                    {
                        current_arg.Append(input[i]);
                    }
                    else if (current_arg.Length > 0)
                    {
                        args.Add(current_arg.ToString());
                        current_arg = new StringBuilder();
                    }
                }
                else
                {
                    current_arg.Append(input[i]);
                }
            }

            if (current_arg.Length > 0) args.Add(current_arg.ToString());

            return args;
        }

        public static void Run()
        {
            if (echo_on) Console.Write(FormatPrefix());

            var input = Console.ReadLine();

            if (input.Length <= 0 || string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine();
                return;
            }

            ExecuteCommand(input);
            if (echo_on) Console.Write("\n");
            Cosmos.Core.Memory.Heap.Collect();
        }

        public static void ExecuteCommand(string input)
        {
            List<string> args = ParseInput(input);
            string name = args[0].ToLower();
            args.RemoveAt(0);

            for (int i = 0; i < commands.Count; i++)
            {
                var command = commands[i];

                if (name == command.Name)
                {
                    if (args.Count == 0)
                    {
                        var cmd_res = command.Execute();

                        if (cmd_res.IsError())
                        {
                            cmd_res.Error.Write();
                        }

                        return;
                    }
                    else
                    {
                        if (args[0] == "-h" || args[0] == "--help")
                        {
                            command.Help();
                            return;
                        }
                        else
                        {
                            var cmd_res = command.Execute(args);

                            if (cmd_res.IsError())
                            {
                                cmd_res.Error.Write();
                            }
                            return;
                        }
                    }
                }
            }
            new Types.Errors.DoesNotExist($"Command '{name}'").Write();
        }
    }
}