using System;
using System.Text;

namespace DogOS.Users
{
    class User
    {
        private string username;
        private string password;
        public User(string username)
        {
            this.username = username;
            this.password = null;
        }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string GetUsername()
        {
            // TODO: Properties?
            // or whatever the {get; set; } is.
            return username;
        }

        public bool VerifyPassword(string needle)
        {
            // TODO: Implement a timing-safe verification.
            // Unless if one isn't really needed for something this simple?

            string hashed_needle = Utils.Security.Sha256.hash(needle);

            return hashed_needle == password;
        }
    }
}