using System;
using System.Collections.Generic;

namespace DogOS.Shell.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand() : base("help", "Get infomation about a command.", CommandCategory.General)
        {
        }

        public Dictionary<CommandCategory, List<Command>> Organize()
        {
            var dict = new Dictionary<CommandCategory, List<Command>>();
            dict[CommandCategory.General] = new List<Command>();
            dict[CommandCategory.Filesystem] = new List<Command>();

            foreach(var command in Shell.commands)
            {
                dict[command.Category].Add(command);
            }

            return dict;
        }

        public override void Execute()
        {
            var dict = Organize();
            
            foreach(var key in dict.Keys)
            {
                Console.WriteLine($"=== {key} ===\n");
                foreach(var command in dict[key])
                {
                    Console.WriteLine($"{command.Name} : {command.Description}");
                }
                Console.WriteLine();
            }
        }

        public override void Execute(List<string> args)
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