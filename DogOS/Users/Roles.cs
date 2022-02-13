namespace DogOS.Users
{
    public enum Roles
    {
        System = 0,
        Admin = 1,
        User = 2,
        Guest = 3
    }

    public static class RolesUtil
    {
        public static string RoleToString(Roles role)
        {
            switch (role)
            {
                case Roles.System:
                    return "System";
                    break;
                case Roles.Admin:
                    return "Admin";
                    break;
                case Roles.User:
                    return "User";
                    break;
                case Roles.Guest:
                    return "Guest";
                    break;
                default:
                    return "Unknown";
                    break;
            }
        }
    }
}