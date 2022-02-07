using System;
using Sys = Cosmos.System;

namespace DogOS
{
    public class Kernel : Sys.Kernel
    {
        #region globals

        public static string os_name = "DogOS";
        public string version = "0.0.1";
        public static bool running = false;
        public static bool inGUI = false;
        public Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();

        #endregion globals

        protected override void BeforeRun()
        {
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.Clear();
            Console.Beep();
            Console.WriteLine($"{os_name} v{version}");
            Console.WriteLine("(c) 2021 TaromaruYuki and Contributers.");
            Console.WriteLine($"Available heap memory: {Cosmos.Core.GCImplementation.GetAvailableRAM()}mb\n");
            running = true;
        }

        protected override void Run()
        {
            if (!inGUI)
            {
                Shell.Shell.Run();
            }
        }
    }
}