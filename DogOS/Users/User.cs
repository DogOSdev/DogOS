namespace DogOS.Users
{
    public class User
    {
        private string username;
        private string password;
        private Roles role;

        public User(string username, Roles role)
        {
            this.username = username;
            this.password = null;
            this.role = role;
        }

        public User(string username, Roles role, string password)
        {
            this.username = username;
            this.password = password;
            this.role = role;
        }

        public string GetUsername()
        {
            // TODO: Properties?
            // or whatever the { get; set; } is.
            return username;
        }

        public Roles GetRole()
        {
            return role;
        }

        public bool VerifyPassword(string needle)
        {
            // TODO: Implement a timing-safe verification.
            // Unless if one isn't really needed for something this simple?

            string hashed_needle = Utils.Security.Sha256.hash(needle).ToUpper();

            return hashed_needle == password.ToUpper();
        }

        public bool PasswordIsEmpty()
        {
            return this.password == null || this.password == "";
        }
    }
}