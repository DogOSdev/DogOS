using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell.Commands
{
    public class SHA256Command : Command
    {
        public SHA256Command() : base("sha256", "Hashes a string.") { }

        public override void Execute(List<string> args)
        {
            if(args.Count <= 0) return;

            var str = new StringBuilder();

            foreach (var arg in args)
            {
                str.Append($"{arg} ");
            }

            str.Remove(str.Length - 1, 1);

            string hash = Utils.Security.Sha256.hash(str.ToString());

            Console.WriteLine(hash);
            Cosmos.System.Global.mDebugger.Send(hash);
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine("\tsha256 -h || Display's help infomation.");
            Console.WriteLine($"\tsha256 [string] || {Description}");
        }
    }
}
