using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace DogOS
{
    public enum ErrorTypes
    {
        Null = -1,
        UnknownCommand,
        RequiredArgument
    }

    public class Kernel : Sys.Kernel
    {
        #region globals

        public static string os_name = "DogOS";
        public string version = null;
        public static string filesys = @"0:\";
        public IniFile system_ini;
        public Sys.FileSystem.CosmosVFS fs;
       
        #endregion

        protected override void BeforeRun()
        {
            // Init the filesystem
            this.fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(this.fs);

            // Check if 'system.ini' exists. If not, create it.
            if (!File.Exists(filesys + "system.ini"))
            {
                Console.WriteLine("'system.ini' does not exist. Creating one.");
                Utils.WriteToFile(filesys, "system.ini", "[INFO]\nOS_VER=0.0.1\nOS_DIRECTORY=0:\\dogos");
            }

            version = IniFile.Read(filesys + "system.ini", "INFO", "OS_VER");

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
            Utils.ResetConsole();
        }

        protected override void Run()
        {
            Console.Write("\n");
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
                    Console.WriteLine(Utils.ReadFile(filesys, input_array[1]));
                    break;

                case "clear":
                    Console.Clear();
                    break;

                case "shutdown":
                    Sys.Power.Shutdown();
                    break;

                case "rm":
                    try
                    {
                        File.Delete(filesys + input_array[1]);
                    }
                    catch (Exception e)
                    {
                        Utils.OutputError(e);
                    }
                    break;

                default:
                    Utils.OutputError(input_array[0], ErrorTypes.UnknownCommand);
                    break;
            }
        }
    }
}
