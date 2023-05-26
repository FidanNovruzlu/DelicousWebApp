using Microsoft.AspNetCore.Identity;

namespace DelicousWebApp.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
    }
}
