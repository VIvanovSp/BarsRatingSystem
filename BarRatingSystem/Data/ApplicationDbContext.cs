using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BarRatingSystem.Models;

namespace BarRatingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BarRatingSystem.Models.Bar> Bars { get; set; } = default!;

        // Add additional DbSets if you have other entities
    }
}