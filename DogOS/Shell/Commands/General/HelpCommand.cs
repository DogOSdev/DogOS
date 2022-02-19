using System;
using System.Collections.Generic;

namespace DogOS.Shell.Commands.General
{
    public class HelpCommand : Command
    {
        public HelpCommand() : base("help", "Get infomation about a command.", CommandCategory.General)
        {
        }

        public List<List<Command>> Organize()
        {
            var final_list = new List<List<Command>>();
            var general_list = new List<Command>();
            var filesystem_list = new List<Command>();

            foreach (var command in Shell.commands)
            {
                switch (command.Category)
                {
                    case CommandCategory.General:
                        general_list.Add(command);
                        break;

                    case CommandCategory.Filesystem:
                        filesystem_list.Add(command);
                        break;
                }
            }

            final_list.Add(general_list);
            final_list.Add(filesystem_list);

            return final_list;
        }

        public string CategoryToString(CommandCategory category)
        {
            switch (category)
            {
                case CommandCategory.General:
                    return "General";
                    break;

                case CommandCategory.Filesystem:
                    return "File System";
                    break;

                default:
                    return "Unknown";
                    break;
            }
        }

        public override CommandResult Execute()
        {
            var commands = Organize();

            foreach (var list in commands)
            {
                Console.WriteLine($"=== {CategoryToString(list[0].Category)} ===");
                foreach (var command in list)
                {
                    Console.WriteLine($"{command.Name}: {command.Description}");
                }
                Console.WriteLine();
            }

            return CommandResult.Success();
        }

        public override CommandResult Execute(List<string> args)
        {
            var cmd_name = args[0].ToLower();
            for (int i = 0; i < Shell.commands.Count; i++)
            {
                var cmd = Shell.commands[i];

                if (cmd_name == cmd.Name)
                {
                    cmd.Help();
                }
            }

            return CommandResult.Success();
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine("\t help help | help -h : Get help about the help command. But, why?");
            Console.WriteLine($"\t help [command] : {Description}");
            Console.WriteLine("\thelp : List all commands available.");
        }
    }
}