using Bar_Rating.Data;
using Bar_Rating.Models;
using Microsoft.AspNetCore.Identity;

namespace Bar_Rating.Seeding
{
    public class AdministratorSeeder : ISeeder
    {
        private const string AdminUsername = "admin";
        private const string AdminPassword = "Test_123";

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var role = await roleManager.FindByNameAsync(GlobalConstants.AdminRoleName);

            if (role == null)
            {
                await new RolesSeeder().SeedAsync(dbContext, serviceProvider);
            }

            if (dbContext.Users.Any(x => x.UserName == AdminUsername))
            {
                return;
            }

            var user = new ApplicationUser()
            {
                UserName = AdminUsername,
                FirstName = "Admin",
                LastName = "Adminov",
            };

            var result = await userManager.CreateAsync(user, AdminPassword);

            await userManager.AddToRoleAsync(user, role.Name);
        }
    }
}
