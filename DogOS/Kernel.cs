using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;
using IL2CPU.API.Attribs;

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
        public static bool running = true;

        [ManifestResourceStream(ResourceName = "DogOS.en-US.ini")]
        public static byte[] lang_file_bytes;
        public static string lang_file;
        public static int lang_offset = 3;

        #endregion

        protected override void BeforeRun()
        {
            Shell.Commands.CommandManager.RegisterCommands();

            string b = "";
            for (int i = 0; i < lang_file_bytes.Length; i++)
            {
                if (i + lang_offset < lang_file_bytes.Length)
                    b += (char)lang_file_bytes[i + lang_offset];
            }
            lang_file = b;
        }

        protected override void Run()
        {
            while(running)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                Shell.Commands.CommandManager._CommandManager(input);
            }
        }
    }
}
