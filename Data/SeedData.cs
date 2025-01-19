using ITstudyv4.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ITstudyv4.Data
{
    // Dodawałem dane (nie licząc SeedUsersAndRoles) wraz z Id, po zmianie na async musiał wystąpić problem... baza tego nie ogarnia i przy ręcznym dodawaniu zaczyna iterować od 1, dlatego teraz jest bez określania Id
    public static class SeedData
    {

        public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ForumUser>>();

            // Role
            var roles = new[] { "Admin", "Moderator", "Użytkownik"};

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

            // Tworzenie konta użytkownika5
            var uzytkownikEmail5 = "test5@xyz.com";
            var uzytkownikPassword5 = "test5@123";
            var uzytkownikUserName5 = "test5";
            var uzytkownikImage5 = "https://greenvilleanimal.com/wp-content/uploads/2021/08/cat-coughing-greenville-mi.jpg";
            var uzytkownikBio5 = "lubie koty";

            if (await userManager.FindByEmailAsync(uzytkownikEmail5) == null)
            {
                var uzytkownik5 = new ForumUser
                {
                    UserName = uzytkownikUserName5,
                    Email = uzytkownikEmail5,
                    ProfilePictureURL = uzytkownikImage5,
                    Bio = uzytkownikBio5

                };

                var result = await userManager.CreateAsync(uzytkownik5, uzytkownikPassword5);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(uzytkownik5, "Użytkownik");
                }
            }

            // Tworzenie konta użytkownika6
            var uzytkownikEmail6 = "test6@xyz.com";
            var uzytkownikPassword6 = "test6@123";
            var uzytkownikUserName6 = "test6";
            var uzytkownikImage6 = "https://greenvilleanimal.com/wp-content/uploads/2021/08/cat-coughing-greenville-mi.jpg";
            var uzytkownikBio6 = "lubie koty";

            if (await userManager.FindByEmailAsync(uzytkownikEmail6) == null)
            {
                var uzytkownik6 = new ForumUser
                {
                    UserName = uzytkownikUserName6,
                    Email = uzytkownikEmail6,
                    ProfilePictureURL = uzytkownikImage6,
                    Bio = uzytkownikBio6

                };

                var result = await userManager.CreateAsync(uzytkownik6, uzytkownikPassword6);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(uzytkownik6, "Użytkownik");
                }
            }

            // Tworzenie konta użytkownika7
            var uzytkownikEmail7 = "test7@xyz.com";
            var uzytkownikPassword7 = "test7@123";
            var uzytkownikUserName7 = "test7";
            var uzytkownikImage7 = "https://greenvilleanimal.com/wp-content/uploads/2021/08/cat-coughing-greenville-mi.jpg";
            var uzytkownikBio7 = "lubie koty";

            if (await userManager.FindByEmailAsync(uzytkownikEmail7) == null)
            {
                var uzytkownik7 = new ForumUser
                {
                    UserName = uzytkownikUserName7,
                    Email = uzytkownikEmail7,
                    ProfilePictureURL = uzytkownikImage7,
                    Bio = uzytkownikBio7

                };

                var result = await userManager.CreateAsync(uzytkownik7, uzytkownikPassword7);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(uzytkownik7, "Użytkownik");
                }
            }

            // Tworzenie konta użytkownika8
            var uzytkownikEmail8 = "test8@xyz.com";
            var uzytkownikPassword8 = "test8@123";
            var uzytkownikUserName8 = "test8";
            var uzytkownikImage8 = "https://greenvilleanimal.com/wp-content/uploads/2021/08/cat-coughing-greenville-mi.jpg";
            var uzytkownikBio8 = "lubie koty";

            if (await userManager.FindByEmailAsync(uzytkownikEmail8) == null)
            {
                var uzytkownik8 = new ForumUser
                {
                    UserName = uzytkownikUserName8,
                    Email = uzytkownikEmail8,
                    ProfilePictureURL = uzytkownikImage8,
                    Bio = uzytkownikBio8

                };

                var result = await userManager.CreateAsync(uzytkownik8, uzytkownikPassword8);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(uzytkownik8, "Użytkownik");
                }
            }

            // Tworzenie konta użytkownika9
            var uzytkownikEmail9 = "test9@xyz.com";
            var uzytkownikPassword9 = "test9@123";
            var uzytkownikUserName9 = "test9";
            var uzytkownikImage9 = "https://greenvilleanimal.com/wp-content/uploads/2021/08/cat-coughing-greenville-mi.jpg";
            var uzytkownikBio9 = "lubie koty";

            if (await userManager.FindByEmailAsync(uzytkownikEmail9) == null)
            {
                var uzytkownik9 = new ForumUser
                {
                    UserName = uzytkownikUserName9,
                    Email = uzytkownikEmail9,
                    ProfilePictureURL = uzytkownikImage9,
                    Bio = uzytkownikBio9

                };

                var result = await userManager.CreateAsync(uzytkownik9, uzytkownikPassword9);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(uzytkownik9, "Użytkownik");
                }
            }
        }

        public static async Task SeedCategories(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            using var context = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());


            if (await context.Categories.AnyAsync())
                return;

            await context.Categories.AddRangeAsync(
                new Categories { Name = "Programowanie", Description = "Wszystko o programowaniu" },
                new Categories { Name = "Hardware", Description = "Wszystko  o sprzęcie komputerowym - od smartfonów po przełączniki" },
                new Categories { Name = "OS", Description = "Wszystko związane z systemami operacyjnymi" },
                new Categories { Name = "OFFTOPIC", Description = "Wszystkie tematy niezwiązane z Informatyką" }
            );
            await context.SaveChangesAsync();
        }

        public static async Task SeedThreads(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ForumUser>>();

            Random rnd = new Random();

            if (await context.Threads.AnyAsync())
                return;

            var admin = await userManager.FindByEmailAsync("admin@example.com");
            var mod = await userManager.FindByEmailAsync("moderator@example.com");
            var user = await userManager.FindByEmailAsync("uzytkownik@example.com");
            var kacper = await userManager.FindByEmailAsync("Kacper@google.com");
            var baran = await userManager.FindByEmailAsync("Blazej@wp.pl");
            var adek = await userManager.FindByEmailAsync("Adrian@xyz.com");

            await context.Threads.AddRangeAsync(
                // Programowanie
                new Threads { Title = "C# - ASP.NET core MVC - jak stworzyć projekt?", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = mod.Id, CategoryId = 1 }, // AnswersId = 1
                new Threads { Title = "Haskell - co to?", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = kacper.Id, CategoryId = 1 },
                new Threads { Title = "C++ - Jak zrobić kalkulator?", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = user.Id, CategoryId = 1 },
                new Threads { Title = "C# - Co to MAUI?", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = user.Id, CategoryId = 1 },
                new Threads { Title = "F# - czy jest sens korzystać z tego w 2025?", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = adek.Id, CategoryId = 1 },
                // Hardware
                new Threads { Title = "Czy procesory intela 14 gen naprawdę są wadliwe?", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = baran.Id, CategoryId = 2 },
                new Threads { Title = "Jaki laptop na studia?", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = kacper.Id, CategoryId = 2 },
                new Threads { Title = "Jaki procesor do programowania?", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = adek.Id, CategoryId = 2 },
                new Threads { Title = "Lepiej wybrać rx6600 czy rtx 3050?", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = baran.Id, CategoryId = 2 },
                new Threads { Title = "Mój komputer się nie uruchamia - potrzebuje pomocy na teraz!!!", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = mod.Id, CategoryId = 2 },
                // OS
                new Threads { Title = "Windows - Visual studio się wywala", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = adek.Id, CategoryId = 3 },
                new Threads { Title = "Linux - jaki polecacie?", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = adek.Id, CategoryId = 3 },
                new Threads { Title = "Windows - pożera 50% procka cały czas - co zrobić?", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = kacper.Id, CategoryId = 3 },
                new Threads { Title = "Mac OS - jak zacząć zabawe z chocolate?", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = baran.Id, CategoryId = 3 },
                // OFTOPIC
                new Threads { Title = "Podzielcie się swoimi obrazkami kotów", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = adek.Id, CategoryId = 4 },
                new Threads { Title = "Jaki samochód dla programisty?", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = user.Id, CategoryId = 4 },
                new Threads { Title = "Czemu seedowanie danych jest takie żmudne?", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = adek.Id, CategoryId = 4 },
                new Threads { Title = "Patrzcie jaki fajny link!", CreatedAt = DateTime.UtcNow, Views = rnd.Next(0, 300), UserId = mod.Id, CategoryId = 4 }
            );
            await context.SaveChangesAsync();
        }

        public static async Task SeedPosts(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ForumUser>>();

            if (await context.Posts.AnyAsync())
                return;

            var admin = await userManager.FindByEmailAsync("admin@example.com");
            var mod = await userManager.FindByEmailAsync("moderator@example.com");
            var user = await userManager.FindByEmailAsync("uzytkownik@example.com");
            var kacper = await userManager.FindByEmailAsync("Kacper@google.com");
            var baran = await userManager.FindByEmailAsync("Blazej@wp.pl");
            var adek = await userManager.FindByEmailAsync("Adrian@xyz.com");

            await context.Posts.AddRangeAsync(
                    // Post 1
                new Posts { Content = "klikasz utwórz projekt i wybierasz ASP.NET core MVC, tyle", CreatedDate = DateTime.UtcNow, Edited = false, UserId = admin.Id, ThreadId = 1},
                new Posts { Content = "Ale gdzie mam to kliknąć?", CreatedDate = DateTime.UtcNow, Edited = false, UserId = baran.Id, ThreadId = 1 },
                new Posts { Content = "W Visualu", CreatedDate = DateTime.UtcNow, Edited = false, UserId = admin.Id, ThreadId = 1 },
                new Posts { Content = "A co to visual?", CreatedDate = DateTime.UtcNow, Edited = false, UserId = baran.Id, ThreadId = 1 },
                new Posts { Content = "Serio?", CreatedDate = DateTime.UtcNow, Edited = false, UserId = kacper.Id, ThreadId = 1 },
                new Posts { Content = "XD", CreatedDate = DateTime.UtcNow, Edited = false, UserId = adek.Id, ThreadId = 1 },
                new Posts { Content = "Zamykam temat, powód: ... nie trzeba podawać", CreatedDate = DateTime.UtcNow, Edited = false, UserId = mod.Id, ThreadId = 1 },
                    // Post 2
                new Posts { Content = "Zamykam temat - nie rozmawiamy tutaj o czarnej magii", CreatedDate = DateTime.UtcNow, Edited = false, UserId = mod.Id, ThreadId = 2 },
                    // Post 3
                new Posts { Content = "Googla nie masz?", CreatedDate = DateTime.UtcNow, Edited = false, UserId = adek.Id, ThreadId = 3 },
                new Posts { Content = "A co to, elektroda?", CreatedDate = DateTime.UtcNow, Edited = false, UserId = user.Id, ThreadId = 3 },
                new Posts { Content = "...", CreatedDate = DateTime.UtcNow, Edited = false, UserId = adek.Id, ThreadId = 3 },
                    // Post 4
                new Posts { Content = "Takie dziwne coś bez takiego czegoś", CreatedDate = DateTime.UtcNow, Edited = false, UserId = admin.Id, ThreadId = 4 },
                new Posts { Content = "Dzięki...", CreatedDate = DateTime.UtcNow, Edited = false, UserId = user.Id, ThreadId = 4 },
                    // Post 5
                new Posts { Content = "Nie", CreatedDate = DateTime.UtcNow, Edited = false, UserId = admin.Id, ThreadId = 5 },
                new Posts { Content = "Zdecydowanie nie", CreatedDate = DateTime.UtcNow, Edited = false, UserId = mod.Id, ThreadId = 5 },
                new Posts { Content = "Tak bardzo nie", CreatedDate = DateTime.UtcNow, Edited = false, UserId = baran.Id, ThreadId = 5 },
                new Posts { Content = "Masz tyle języków, a ty takie coś wybierasz?", CreatedDate = DateTime.UtcNow, Edited = false, UserId = kacper.Id, ThreadId = 5 },
                new Posts { Content = "Nie", CreatedDate = DateTime.UtcNow, Edited = false, UserId = user.Id, ThreadId = 5 },
                new Posts { Content = "OK, ok...", CreatedDate = DateTime.UtcNow, Edited = false, UserId = adek.Id, ThreadId = 5 },
                    // Post 6
                new Posts { Content = "I to Jak!", CreatedDate = DateTime.UtcNow, Edited = false, UserId = mod.Id, ThreadId = 6 },
                    // Post 7
                new Posts { Content = "Taki żeby wytrzymał visuala i chroma", CreatedDate = DateTime.UtcNow, Edited = false, UserId = admin.Id, ThreadId = 7 },
                    // Post Ostatni
                new Posts { Content = "https://youtu.be/dQw4w9WgXcQ", CreatedDate = DateTime.UtcNow, Edited = false, UserId = mod.Id, ThreadId = 18 }
            );
            await context.SaveChangesAsync();
        }
    }
}
