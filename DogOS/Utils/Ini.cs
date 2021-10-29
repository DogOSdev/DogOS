using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;


namespace DogOS.Utils
{
    public class Ini
    {
        // Source code to reading file is here
        // https://stackoverflow.com/a/55752753
        public static string Read(string file, string section, string key, string default_value=null)
        {
            string[] reader_lines = File.ReadAllLines(file);
            string current_section = "";

            foreach (string reader_line in reader_lines)
            {
                if(reader_line.StartsWith('[') && reader_line.EndsWith(']'))
                {
                    current_section = reader_line;
                }
                else if(current_section.Equals($"[{section}]"))
                {
                    string[] line_parts = reader_line.Split(new[] { '=' }, 2);

                    if(line_parts.Length >= 1 && line_parts[0] == key)
                    {
                        return line_parts.Length >= 2 ? line_parts[1] : default_value;
                    }
                }
            }

            return default_value;
        }
    }
}
