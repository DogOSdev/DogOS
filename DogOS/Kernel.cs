using System;
using System.IO;
using Sys = Cosmos.System;

namespace DogOS
{
    public class Kernel : Sys.Kernel
    {
        #region globals

        public static string os_name = "DogOS";
        public static string codename;
        public static string build_type;
        public static string version;
        public static string temp_dir;
        public static string os_dir;
        public static Users.User curr_user;

        public static Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        public static Users.UserManager users = new Users.UserManager();

        #endregion globals

        protected override void BeforeRun()
        {
            Console.WriteLine("Registering Filesystem...");
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

            Console.WriteLine("Loading System Info.");
            string os_info = Utils.FileTypes.Ini.ReadFile("SYSTEM", "OS", "0:\\system.ini", "UNKNOWN|UNKNOWN|0.0.0");
            string[] os_info_split = os_info.Split("|");

            os_dir = Utils.FileTypes.Ini.ReadFile("SYSTEM", "DIR", "0:\\system.ini", "dogos");
            temp_dir = Utils.FileTypes.Ini.ReadFile("SYSTEM", "TEMP", "0:\\system.ini", $"{os_dir}\\temp");

            codename = os_info_split[0];
            build_type = os_info_split[1];
            version = os_info_split[2];

            Console.WriteLine("Loading users...");
            users.LoadUsers("0:\\users.ini");

            if(users.users.Count == 0)
            {
                Console.WriteLine("No users were loaded...");
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }

            Console.Clear();
            Console.Beep();

            if (users.users.Count == 0)
            {
                curr_user = new Users.User("Guest", Users.Roles.Guest);
            }
            else
            {
                ConsoleKeyInfo choice;

                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("Would you like to sign in? (y/n)");
                    choice = Console.ReadKey(true);

                    if(choice.Key == ConsoleKey.Y || choice.Key == ConsoleKey.N)
                        break;
                }

                if(choice.Key == ConsoleKey.N)
                {
                    curr_user = new Users.User("Guest", Users.Roles.Guest);
                }
                else
                {
                    while (true)
                    {
                        Console.Write("Username: ");
                        var username = Console.ReadLine();

                        Users.User found_user = null;
                        foreach (var user in users.users)
                        {
                            if (user.GetUsername() == username)
                            {
                                found_user = user;
                            }
                        }

                        if (found_user == null)
                        {
                            Console.WriteLine($"Username {username} does not exist!");
                            continue;
                        }


                        while (true)
                        {
                            Console.Write("Password: ");
                            string password = "";
                            ConsoleKeyInfo key_info;

                            do
                            {
                                key_info = Console.ReadKey(true);

                                if (key_info.Key != ConsoleKey.Backspace && key_info.Key != ConsoleKey.Enter)
                                {
                                    password += key_info.KeyChar;
                                    Console.Write("*");
                                }
                                else
                                {
                                    if (key_info.Key == ConsoleKey.Backspace && password.Length > 0)
                                    {
                                        password = password.Substring(0, (password.Length - 1));
                                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                        Console.Write(" ");
                                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                    }
                                }
                            }
                            while (key_info.Key != ConsoleKey.Enter);
                            Console.Write("\n");

                            if (found_user.VerifyPassword(password))
                            {
                                break;
                            }

                            Console.WriteLine("Invalid password.");
                        }

                        curr_user = found_user;
                        break;
                    }
                }
            }

            Console.Clear();
            Console.Beep(880, 100);
            Console.WriteLine($"{os_name} {codename} ({build_type}) v{version}");
            Console.WriteLine("(c) 2021 TaromaruYuki and Contributers.");
            Console.WriteLine($"Available heap memory: {Cosmos.Core.GCImplementation.GetAvailableRAM()}mb\n");
            Console.WriteLine($"Logged in as {curr_user.GetUsername()}");
        }

        protected override void Run()
        {
            Shell.Shell.Run();
        }
    }
}