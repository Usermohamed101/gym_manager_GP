using Microsoft.AspNetCore.Identity;

namespace GymManagmentSystem.DataBase
{
    public class User:IdentityUser
    {


        public string FirstName { get; set; }
        public string SecName { get; set; }
        public string? Address { get; set; }
        public string? ProfileImageUrl { get; set; }

    }
}
