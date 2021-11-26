using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell.Commands
{
    public class EchoCommand : Command
    {
        public EchoCommand() : base("echo", "Echo a message, or toggle echo.") { }

        public override void Execute()
        {
            Console.WriteLine("h");
        }

        public override void Execute(List<string> args)
        {
            var str = new StringBuilder();

            foreach (var arg in args)
            {
                str.Append($"{arg} ");
            }
            Console.WriteLine(str);
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine("\techo -h || Display's help message.");
            Console.WriteLine($"\techo [message] || {Description}");
            Console.WriteLine($"\techo -t (ON | OFF) || Turns echo on or off");
            Console.WriteLine("echo || Displays the echo setting.");
        }
    }
}
