using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell
{
    public static class Shell
    {
        public static bool echo_on = true;
        public static string Prefix = "{os_name}>";
        public static List<Commands.Command> commands = new List<Commands.Command>();

        static Shell()
        {
            commands.Add(new Commands.EchoCommand());
            commands.Add(new Commands.SHA256Command());
            commands.Add(new Commands.ShutdownCommand());
            commands.Add(new Commands.ClearCommand());
            commands.Add(new Commands.HelpCommand());
        }

        public static string FormatPrefix()
        {
            return Prefix.Replace("{os_name}", Kernel.os_name);
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
                if(input[i] == '"')
                {
                    if(in_quotes)
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
                else if(input[i] == ' ')
                {
                    if(in_quotes)
                    {
                        current_arg.Append(input[i]);
                    }
                    else if(current_arg.Length > 0)
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
                
            if(current_arg.Length > 0) args.Add(current_arg.ToString());

            return args;
        }

        public static void Run()
        {
            if(echo_on) Console.Write(Prefix.Replace("{os_name}", Kernel.os_name));

            var input = Console.ReadLine();

            ExecuteCommand(input);
            if(echo_on) Console.Write("\n");
        }

        public static void ExecuteCommand(string input)
        {
            List<string> args = ParseInput(input);
            string name = args[0].ToLower();
            args.RemoveAt(0);

            for (int i = 0; i < commands.Count; i++)
            {
                var command = commands[i];

                if(name == command.Name)
                {
                    if(args.Count == 0)
                    {
                        command.Execute();
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
                            command.Execute(args);
                            return;
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERR: Command '{name}' does not exist.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
