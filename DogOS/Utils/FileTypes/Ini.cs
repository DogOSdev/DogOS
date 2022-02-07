using System;
using System.IO;

namespace DogOS.Utils.FileTypes
{
    public static class Ini
    {
        // From: https://stackoverflow.com/a/55752753/13617487
        public static string ReadFile(string section, string key, string ini_file, string default_value = "")
        {
            string[] lines = ini_file.Split("\n");

            string curr_section = "";

            foreach(string line in lines)
            {
                if(line.StartsWith("[") && line.EndsWith("]"))
                {
                    curr_section = line;
                }
                else if (curr_section.Equals($"[{section}]"))
                {
                    string[] line_parts = line.Split(new[] { '=' }, 2);

                    if(line_parts.Length >= 1 && line_parts[0] == key)
                    {
                        return line_parts.Length >= 2
                            ? line_parts[1]
                            : null;
                    }
                }
            }

            return default_value;
        }
    }
}