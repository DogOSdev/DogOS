using System;
using System.Collections.Generic;
using System.Text;

namespace DogOS.Utils
{
    public class CommandLine
    {
        // Utility from the Aura OS. Thanks!
        // https://stackoverflow.com/questions/59638467/parsing-command-line-args-with-quotes
        public static List<string> ParseCommandLine(string cmd_line)
        {
            var args = new List<string>();
            if (string.IsNullOrWhiteSpace(cmd_line)) return args;

            var current_arg = new StringBuilder();
            bool in_quoted_arg = false;

            for (int i = 0; i < cmd_line.Length; i++)
            {
                if (cmd_line[i] == '"')
                {
                    if (in_quoted_arg)
                    {
                        args.Add(current_arg.ToString());
                        current_arg = new StringBuilder();
                        in_quoted_arg = false;
                    }
                    else
                    {
                        in_quoted_arg = true;
                    }
                }
                else if (cmd_line[i] == ' ')
                {
                    if (in_quoted_arg)
                    {
                        current_arg.Append(cmd_line[i]);
                    }
                    else if (current_arg.Length > 0)
                    {
                        args.Add(current_arg.ToString());
                        current_arg = new StringBuilder();
                    }
                }
                else
                {
                    current_arg.Append(cmd_line[i]);
                }
            }

            if (current_arg.Length > 0) args.Add(current_arg.ToString());

            return args;
        }
    }
}
