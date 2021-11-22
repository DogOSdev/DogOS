using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand() : base("help", "Get infomation about a command.") { }

        public override void Execute()
        {
            for (int i = 0; i < Shell.commands.Count; i++)
            {
                Console.WriteLine($"{Shell.commands[i].Name} : {Shell.commands[i].Description}");
            }
        }

        public override void Execute(List<string> args)
        {
            var cmd_name = args[0].ToLower();
            for (int i = 0; i < Shell.commands.Count; i++)
            {
                var cmd = Shell.commands[i];

                if(cmd_name == cmd.Name)
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
