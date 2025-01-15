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
            var adminUserName = "Admin";
            var adminImage = "https://media4.giphy.com/media/v1.Y2lkPTc5MGI3NjExZGM5cnNmcmVoZW5hd2lzdXF0eGl2eG5jc3M5MTZ4dmlqanNhMW10dSZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/4ZaVOvh0M3q7c6YuE2/giphy.gif";
            var adminBio = "Adminować trzeba potrafić";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ForumUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    ProfilePictureURL = adminImage,
                    Bio = adminBio
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Tworzenie konta moderatora
            var ModeratorEmail = "moderator@example.com";
            var ModeratorPassword = "Moderator@123";
            var ModeratorUserName = "Moderator";
            var ModeratorImage = "https://static.scientificamerican.com/sciam/cache/file/2AE14CDD-1265-470C-9B15F49024186C10_source.jpg";
            var ModeratorBio = "Discord to za mało";

            if (await userManager.FindByEmailAsync(ModeratorEmail) == null)
            {
                var Moderator = new ForumUser
                {
                    UserName = ModeratorUserName,
                    Email = ModeratorEmail,
                    ProfilePictureURL = ModeratorImage,
                    Bio = ModeratorBio
                };

                var result = await userManager.CreateAsync(Moderator, ModeratorPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(Moderator, "Moderator");
                }
            }

            // Tworzenie konta użytkownika
            var uzytkownikEmail = "uzytkownik@example.com";
            var uzytkownikPassword = "uzytkownik@123";
            var uzytkownikUserName = "Uzytkownik";
            var uzytkownikImage = "https://i.cbc.ca/1.7046192.1701492097!/fileImage/httpImage/image.jpg_gen/derivatives/16x9_940/african-wild-cat.jpg";
            var uzytkownikBio = "Co to za strona?";

            if (await userManager.FindByEmailAsync(uzytkownikEmail) == null)
            {
                var uzytkownik = new ForumUser
                {
                    UserName = uzytkownikUserName,
                    Email = uzytkownikEmail,
                    ProfilePictureURL = uzytkownikImage,
                    Bio = uzytkownikBio

                };

                var result = await userManager.CreateAsync(uzytkownik, uzytkownikPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(uzytkownik, "Użytkownik");
                }
            }

            // Tworzenie konta użytkownika2
            var uzytkownikEmail2 = "Kacper@google.com";
            var uzytkownikPassword2 = "Kacper@123";
            var uzytkownikUserName2 = "Kacper";
            var uzytkownikImage2 = "https://static.vecteezy.com/system/resources/thumbnails/022/963/918/small_2x/ai-generative-cute-cat-isolated-on-solid-background-photo.jpg";
            var uzytkownikBio2 = "";

            if (await userManager.FindByEmailAsync(uzytkownikEmail2) == null)
            {
                var uzytkownik2 = new ForumUser
                {
                    UserName = uzytkownikUserName2,
                    Email = uzytkownikEmail2,
                    ProfilePictureURL = uzytkownikImage2,
                    Bio = uzytkownikBio2

                };

                var result = await userManager.CreateAsync(uzytkownik2, uzytkownikPassword2);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(uzytkownik2, "Użytkownik");
                }
            }

            // Tworzenie konta użytkownika3
            var uzytkownikEmail3 = "Blazej@wp.pl";
            var uzytkownikPassword3 = "blazej@123";
            var uzytkownikUserName3 = "Baran";
            var uzytkownikImage3 = "https://images.freeimages.com/images/large-previews/ab9/confident-black-cat-portrait-0410-5699218.jpg";
            var uzytkownikBio3 = "";

            if (await userManager.FindByEmailAsync(uzytkownikEmail3) == null)
            {
                var uzytkownik3 = new ForumUser
                {
                    UserName = uzytkownikUserName3,
                    Email = uzytkownikEmail3,
                    ProfilePictureURL = uzytkownikImage3,
                    Bio = uzytkownikBio3

                };

                var result = await userManager.CreateAsync(uzytkownik3, uzytkownikPassword3);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(uzytkownik3, "Użytkownik");
                }
            }

            // Tworzenie konta użytkownika4
            var uzytkownikEmail4 = "Adrian@xyz.com";
            var uzytkownikPassword4 = "adrian@123";
            var uzytkownikUserName4 = "Adek";
            var uzytkownikImage4 = "https://greenvilleanimal.com/wp-content/uploads/2021/08/cat-coughing-greenville-mi.jpg";
            var uzytkownikBio4 = "lubie koty";

            if (await userManager.FindByEmailAsync(uzytkownikEmail4) == null)
            {
                var uzytkownik4 = new ForumUser
                {
                    UserName = uzytkownikUserName4,
                    Email = uzytkownikEmail4,
                    ProfilePictureURL = uzytkownikImage4,
                    Bio = uzytkownikBio4

                };

                var result = await userManager.CreateAsync(uzytkownik4, uzytkownikPassword4);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(uzytkownik4, "Użytkownik");
                }
            }
        }
    }
}
