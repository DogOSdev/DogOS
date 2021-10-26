using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace CosmosKernel1
{
    public enum ErrorTypes
    {
        Null = -1,
        UnknownCommand
    }

    public class Kernel : Sys.Kernel
    {
        #region globals

        public static string os_name = "DogOS";
        public static string version = "0.0.1";
        public static string filesys = @"0:\";
        public Sys.FileSystem.CosmosVFS fs;
       
        #endregion

        public void ResetConsole()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void OutputError(string error, ErrorTypes error_type = ErrorTypes.Null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            switch(error_type)
            {
                case ErrorTypes.UnknownCommand:
                    Console.Write("Unknown Command");
                    break;
                case ErrorTypes.Null: break;
            }

            ResetConsole();

            Console.Write(": " + error);
            Console.Write("\n");
        }

        public void OutputError(Exception error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("'" + error.GetType().Name + "' was raised. More details are below.");
            ResetConsole();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Message: ");
            Console.Write(error.Message + "\n");
            ResetConsole();
        }

        protected override void BeforeRun()
        {
            // Init the filesystem
            this.fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(this.fs);

            // Clear the console
            Console.Clear();

            // Create Header
            string welcome = "# " + os_name + " " + version + " #";
            string borders = new string('#', welcome.Length);
            Console.ForegroundColor = ConsoleColor.Green;
            // Write header to console
            Console.WriteLine(borders);
            Console.WriteLine(welcome);
            Console.WriteLine(borders);

            // Reset the console
            ResetConsole();
        }

        protected override void Run()
        {
            Console.Write(filesys + ">");
            var input = Console.ReadLine();

            // Clean the input string
            string[] input_array = input.Split(" ");

            switch(input_array[0])
            {
                case "help":
                    Console.WriteLine("help - Get all available commands.");
                    Console.WriteLine("echo - Echo a string of text onto the screen.");
                    Console.WriteLine("dir - Read the current directory.");
                    Console.WriteLine("cat - Read a file's contents.");
                    break;

                case "echo":
                    int i = input.IndexOf(" ") + 1;
                    string str = input.Substring(i);
                    Console.WriteLine(str);
                    break;

                case "dir":
                    var directory_list = Directory.GetFiles(filesys);
                    foreach(var file in directory_list)
                    {
                        Console.WriteLine(file);
                    }
                    break;

                case "cat":
                    try
                    {
                        Console.WriteLine(File.ReadAllText(filesys + input_array[1]));
                    }
                    catch (Exception e)
                    {
                        OutputError(e);
                    }
                    break;

                case "clear":
                    Console.Clear();
                    break;

                case "test":
                    break;

                default:
                    OutputError(input_array[0], ErrorTypes.UnknownCommand);
                    break;
            }
        }
    }
}
