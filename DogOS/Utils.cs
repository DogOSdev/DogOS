using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DogOS
{
    class Utils
    {
        public static void ResetConsole()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void OutputError(string error, ErrorTypes error_type = ErrorTypes.Null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            switch (error_type)
            {
                case ErrorTypes.UnknownCommand:
                    Console.Write("Unknown Command");
                    break;
                case ErrorTypes.RequiredArgument:
                    Console.Write("Required argument was not passed");
                    break;
                case ErrorTypes.Null: break;
            }

            ResetConsole();

            Console.Write(": " + error);
            Console.Write("\n");
        }

        public static void OutputError(Exception error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("'" + error.GetType().Name + "' was raised. More details are below.");
            ResetConsole();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Message: ");
            Console.Write(error.Message + "\n");
            ResetConsole();
        }

        public static void CreateFile(string file_path, string file_name)
        {
            try
            {
                File.Create(file_path + file_name);
            }
            catch (Exception e)
            {
                OutputError(e);
            }
        }

        public static string ReadFile(string file_path, string file_name)
        {
            try
            {
                return File.ReadAllText(file_path + file_name);
            }
            catch (Exception e)
            {
                OutputError(e);
                return null;
            }
        }

        public static void WriteToFile(string file_path, string file_name, string file_content)
        {
            if(!File.Exists(file_path + file_name))
            {
                CreateFile(file_path, file_name);
            }
            File.WriteAllText(file_path + file_name, file_content);
        }
    }
}
