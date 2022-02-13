using System;
using System.IO;

namespace DogOS.Utils.FileTypes
{
    public static class Ini
    {
        // From: https://stackoverflow.com/a/55752753/13617487
        public static string ReadFile(string SECTION, string KEY, string PATH, string DEFAULT_VALUE = "")
        {
            // read all lines from the file
            string[] READER_LINES = File.ReadAllLines(PATH);

            // we are going to capture the value of a "section" line here, so we can 
            // test to see if subsequent lines belong to the section we are
            // looking for
            string CURRENT_SECTION = "";

            // itterate all the lines until we find the section and key we want
            foreach (string READER_LINE in READER_LINES)
            {
                // detect if the line is a [SECTION_NAME] and capture it as the current section
                if (READER_LINE.StartsWith("[") && READER_LINE.EndsWith("]"))
                {
                    CURRENT_SECTION = READER_LINE;
                }
                else if (CURRENT_SECTION.Equals($"[{SECTION}]"))
                {
                    // The current line is not a section header

                    // The current section is the section we are looking for, so lets process 
                    // the lines within it

                    // now lets split the current line into a key/value pair using = as the delimitor
                    string[] lineParts = READER_LINE.Split(new[] { '=' }, 2);

                    // test if part 1 of the line matches the KEY we are looking for
                    if (lineParts.Length >= 1 && lineParts[0] == KEY)
                    {
                        // we have found the key.
                        // now return part 2 of the line as the value, or DEFAULT_VALUE if the split 
                        // operation above could not find a part 2 to add to the list
                        return lineParts.Length >= 2
                            ? lineParts[1]
                            : DEFAULT_VALUE;
                    }
                }
            }

            // we have not found a match, so return the default value instead
            return DEFAULT_VALUE;
        }
    }
}