using ITstudyv4.Data;
using ITstudyv4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Teoretycznie mo¿na zmieniæ .UseNpgsql na .UseSqlServer, (po uwczesnym dodaniu odpowiedniego pakietu nuget) ale od dawna tego nie testowa³em
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Gdyby ktoœ marudzi³ ¿e s³abe zabezpieczenia - my tak specjalnie zrobiliœmy
builder.Services.AddIdentity<ForumUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders().AddRoles<IdentityRole>();

// Jeœli u¿ytkownik nie jest zalogowany, przekieruje do strony logowania, w ka¿dym przypadku,
// gdy w kodzie nie jest okreœlone jakie wymagane s¹ uprawnienia
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// Seedowanie danych z SeedData.cs
// Mo¿na zakomentowaæ aby projekt nie tworzy³ nowych u¿ytkowników i reszty rzeczy przy odpalaniu projektu
// ale tworzy je w momencie gdy nie ma ¯ADNYCH danych w danej tabeli, tak¿e mo¿e zostaæ przez ca³y czas testów
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.SeedUsersAndRolesAsync(services);
    await SeedData.SeedCategories(services);
    await SeedData.SeedThreads(services);
    await SeedData.SeedPosts(services);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
// app.UseAuthentication(); psuje apke, i tak zarz¹dzanie uprawnieniami dzia³a

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();