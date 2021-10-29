using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell.Commands
{
    class CommandManager
    {
        public static List<BaseCommand> commands = new List<BaseCommand>();

        public static void RegisterCommands()
        {
            commands.Add(new Power.Reboot(new string[] { "reboot" }));
            commands.Add(new Power.Shutdown(new string[] { "shutdown" }));
        }

        public static void _CommandManager(string cmd)
        {
            CommandHistory.Add(cmd);

            if(cmd.Length <= 0)
            {
                Console.WriteLine();
                return;
            }

            #region Parse Command

            List<string> arguments = Utils.CommandLine.ParseCommandLine(cmd);

            string first_arg = arguments[0];

            if (arguments.Count > 0)
            {
                arguments.RemoveAt(0);
            }

            #endregion

            foreach (var command in commands)
            {
                if(command.ContainsCommand(first_arg))
                {
                    ReturnInfo res;

                    if(arguments.Count > 0 && (arguments[0] == "/help" || arguments[0] == "/h"))
                    {
                        ShowHelp(command);
                        res = new ReturnInfo(command, ReturnCode.Ok);
                    }
                    else
                    {
                        res = CheckCommand(command);

                        if(res.Code == ReturnCode.Ok)
                        {
                            if(arguments.Count == 0)
                            {
                                res = command.Execute();
                            }
                            else
                            {
                                res = command.Execute(arguments);
                            }
                        }
                    }
                    ProcessCommandResult(res);

                    return;
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Unknown Command.");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
        }

        private static void ShowHelp(BaseCommand command)
        {
            Console.WriteLine($"Description: {command.Description}.");
            Console.WriteLine();
            if(command.CommandValues.Length > 1)
            {
                Console.Write("Aliases: ");
                for (int i = 0; i < command.CommandValues.Length; i++)
                {
                    if (i != command.CommandValues.Length - 1)
                    {
                        Console.Write($"{command.CommandValues[i]}, ");
                    }
                    else
                    {
                        Console.Write(command.CommandValues[i]);
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            command.PrintHelp();
        }

        private static ReturnInfo CheckCommand(BaseCommand command)
        {
            return new ReturnInfo(command, ReturnCode.Ok);
        }

        private static void ProcessCommandResult(ReturnInfo res)
        {
            if(res.Code == ReturnCode.Argument)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The command argument's are not correct.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (res.Code == ReturnCode.Error)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error: {res.Info}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
