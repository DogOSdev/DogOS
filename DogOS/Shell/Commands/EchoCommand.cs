using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell.Commands
{
    public class EchoCommand : Command
    {
        public EchoCommand() : base("echo", "Echo input to the screen") { }

        public override int Execute(List<string> args)
        {
            Console.WriteLine(string.Join(" ", args));
            return 0;
        }
    }
}
