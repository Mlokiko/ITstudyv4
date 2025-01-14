using ITstudyv4.Models;
using Microsoft.AspNetCore.Identity;

namespace ITstudyv4.Data
{
    public static class SeedData
    {
        public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ForumUser>>();

            // Role
            var roles = new[] { "Admin", "Moderator", "Użytkownik" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Tworzenie konta admina
            var adminEmail = "admin@example.com";
            var adminPassword = "Admin@123";
            var adminUserName = "admin";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ForumUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true, // nie bawimy się w potwierdzanie maila, ale może zostać
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
