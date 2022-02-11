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
        public static Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        public static UserManager users = new UserManager();
        public static User curr_user; 

        #endregion globals

        protected override void BeforeRun()
        {
            Console.WriteLine("Registering Filesystem...");
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

            Console.WriteLine("Loading users...");
            users.LoadUsers("0:\\users.ini");

            if(users.users.Count == 0)
            {
                Console.WriteLine("No users were loaded...");
                Console.WriteLine("Press any key to continue...");
                Console.Read();
            }

            Console.Clear();
            Console.Beep();
            Console.WriteLine($"{os_name} v{version}");
            Console.WriteLine("(c) 2021 TaromaruYuki and Contributers.");
            Console.WriteLine($"Available heap memory: {Cosmos.Core.GCImplementation.GetAvailableRAM()}mb\n");
            running = true;

            if(users.users.Count == 0)
            {
                curr_user = new Users.User("Guest", Roles.Guest);
            }
            else
            {
                while(true)
                {
                    Console.Write("Username: ");
                    var username = Console.ReadLine();

                    Users.User found_user = null;
                    foreach(var user in users.users)
                    {
                        if(user.GetUsername() == username)
                        {
                            found_user = user;
                        }
                    }

                    if(found_user == null)
                    {
                        Console.WriteLine($"Username {username} does not exist!");
                        continue;
                    }


                    while(true)
                    {
                        // TODO: Do not echo password when typing.
                        Console.Write("Password: ");
                        var password = Console.ReadLine();

                        if(found_user.VerifyPassword(password))
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

        protected override void Run()
        {
            if (!inGUI)
            {
                Shell.Shell.Run();
            }
        }
    }
}