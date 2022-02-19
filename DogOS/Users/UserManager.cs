using System;
using System.Collections.Generic;

namespace DogOS.Users
{
    public class UserManager
    {
        public List<User> users = new List<User>();

        public int LoadUsers(string user_ini)
        {
            // Remove the old users loaded, if loaded before
            users.Clear();

            // Get the user sections from the list of users
            var user_sections_str = Utils.FileTypes.Ini.ReadFile("LIST", "LIST", user_ini);
            var user_sections = user_sections_str.Split("|");

            // Loop through every user
            foreach (var user_key in user_sections)
            {
                var username = Utils.FileTypes.Ini.ReadFile(user_key.ToUpper(), "USERNAME", user_ini);
                var password = Utils.FileTypes.Ini.ReadFile(user_key.ToUpper(), "PASSWORD", user_ini);
                var role = Utils.FileTypes.Ini.ReadFile(user_key.ToUpper(), "ROLE", user_ini);

                int role_int;
                if (int.TryParse(role, out role_int))
                {
                    if (password == "")
                    {
                        users.Add(new User(username, (Roles)role_int));
                    }
                    else
                    {
                        users.Add(new User(username, (Roles)role_int, password));
                    }
                }
                else
                {
                    Console.WriteLine($"Failed to load user '{username}'. Role id is not a integer.");
                }
            }

            // Successfully loaded users
            return 0;
        }
    }
}