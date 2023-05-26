using DelicousWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DelicousWebApp.DAL
{
    public class DelicousDbContext:IdentityDbContext<AppUser>
    {
        public DelicousDbContext(DbContextOptions<DelicousDbContext> options):base(options)
        {

        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
