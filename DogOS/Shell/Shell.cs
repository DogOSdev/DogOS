using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Shell
{
    public static class Shell
    {
        public static string Prefix = "{os_name}> ";

        public static string GetFormattedPrefix()
        {
            string pre = Prefix;
            return pre.Replace("{os_name}", Kernel.os_name);
        }

        public static void Run()
        {
            Console.Write(GetFormattedPrefix());

            string input = Console.ReadLine();

            RunCommand(input);
        }

        public static List<string> InputToArgs(string input)
        {
            var t = input.Split(" ");
            var l = new List<string>();

            foreach (var i in t)
            {
                l.Add(i);
            }
            return l;
        }

        public static void RunCommand(string input)
        {
            var split_input = InputToArgs(input);
            var name = split_input[0].ToLower();
            split_input.RemoveAt(0);

            foreach (var item in CommandList.commands)
            {
                if(name == item.Name)
                {
                    item.Execute(split_input);
                }
            }
        }
    }
}
