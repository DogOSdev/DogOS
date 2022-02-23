using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell.Commands.General
{
    public class SHA256Command : Command
    {
        public SHA256Command() : base("sha256", "Hashes a string.", CommandCategory.General)
        {
        }

        public override CommandResult Execute(List<string> args)
        {
            if (args.Count <= 0) return CommandResult.Failure(new Types.Errors.NotEnoughArguments(
                "Not enough arguments given. Please specify a string."
            ));

            var str = new StringBuilder();

            foreach (var arg in args)
            {
                str.Append($"{arg} ");
            }

            str.Remove(str.Length - 1, 1);

            string hash = Utils.Security.Sha256.hash(str.ToString());

            Console.WriteLine(hash);

            return CommandResult.Success();
        }

        public override void Help()
        {
            Console.WriteLine(Description);

            Console.WriteLine("\tsha256 -h || Display's help infomation.");
            Console.WriteLine($"\tsha256 [string] || {Description}");
        }
    }
}