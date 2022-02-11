using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DogOS.Users
{
    class UserManager
    {
        public List<User> users = new List<User>();

        public int LoadUsers(string user_ini_file)
        {
            // Remove the old users loaded, if loaded before
            users.Clear();

            // Read the file that was given
            string user_ini = "";

            // Get the user ini file
            try
            {
                user_ini = File.ReadAllText(user_ini_file);
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.ToString());
                Console.ForegroundColor = ConsoleColor.White;

                return -1;
            }

            // Get the user sections from the list of users
            var user_sections_str = Utils.FileTypes.Ini.ReadFile("LIST", "LIST", user_ini);
            var user_sections = user_sections_str.Split("|");

            // Loop through every user
            foreach(var user_key in user_sections)
            {
                var username = Utils.FileTypes.Ini.ReadFile(user_key.ToUpper(), "USERNAME", user_ini);
                var password = Utils.FileTypes.Ini.ReadFile(user_key.ToUpper(), "PASSWORD", user_ini);
                var role = Utils.Filetypes.Ini.ReadFile(user_key.ToUpper(), "ROLE", user_ini, 3);

                if(password == "")
                {
                    users.Add(new User(username, (Roles)role));
                }
                else
                {
                    users.Add(new User(username, (Roles)role, password));
                }
            }

            // Successfully loaded users
            return 0;
        }
    }
}