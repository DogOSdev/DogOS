using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DogOS.Shell.Apps
{
    public class Editor
    {
        string file;
        List<string> lines = new List<string>();
        bool running = true;
        bool file_exists = true;

        #region MetaData

        string version = "0.1";

        #endregion

        public Editor(string file)
        {
            this.file = file;
        }

        public void Run()
        {
            Read();
            Console.Clear();
            Header();

            while(running)
                GetInput();
        }

        private void Header()
        {
            Console.WriteLine($"=== Dogedit v{version} ===");
            Console.WriteLine("Type 'h' for help");
        }

        private void Read()
        {
            try
            {
                foreach (var line in File.ReadAllText($"{Kernel.drive}{Kernel.dir}{file}").Split("\n"))
                {
                    lines.Add(line);
                }
            }
            catch
            {
                file_exists = false;
            }
        }

        private string JoinLines()
        {
            var builder = new StringBuilder();

            for (var i = 0; i < lines.Count; ++i)
            {
                builder.Append(lines[i]);
                if(i < lines.Count - 1)
                    builder.Append("\n");
            }

            return builder.ToString();
        }

        private void Write()
        {
            //Console.WriteLine($"Saving to '{Kernel.drive}{Kernel.dir}{file}'");
            File.WriteAllText($"{Kernel.drive}{Kernel.dir}{file}", JoinLines());
        }

        private void PrintLines()
        {
            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {lines[i]}");
            }
        }

        private void AddLine()
        {
            Console.Write("Line text > ");
            lines.Add(Console.ReadLine());
        }

        private void EditLine()
        {
            int line_num;

            while(true)
            {
                Console.Write("Line? ");
                string line_num_str = Console.ReadLine();

                if(int.TryParse(line_num_str, out line_num))
                {
                    break;
                }
            }
            
            Console.Write("Line Text > ");
            string line = Console.ReadLine();
            lines[line_num - 1] = line;
        }

        private void DeleteLine()
        {
            int line_num;

            while (true)
            {
                Console.Write("Line? ");
                string line_num_str = Console.ReadLine();

                if (int.TryParse(line_num_str, out line_num))
                {
                    break;
                }
            }

            lines.RemoveAt(line_num - 1);
        }

        private void Help()
        {
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("p - Print lines");
            Console.WriteLine("w - Write to file");
            Console.WriteLine("a - Add line");
            Console.WriteLine("d - Delete line");
            Console.WriteLine("e - Edit line");
            Console.WriteLine("q - Quit");
        }

        private void GetInput()
        {
            Console.Write(file);
            if(!file_exists)
                Console.Write(" (NEW)");
            Console.Write(" > ");

            string cmd = Console.ReadLine();

            switch (cmd)
            {
                case "q":
                    running = false;
                    break;
                case "p":
                    PrintLines();
                    break;
                case "w":
                    Write();
                    break;
                case "a":
                    AddLine();
                    break;
                case "d":
                    DeleteLine();
                    break;
                case "e":
                    EditLine();
                    break;
                case "h":
                    Help();
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    break;
            }

            Console.WriteLine();
        }
    }
}
