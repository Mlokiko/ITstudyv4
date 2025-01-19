using ITstudyv4.Data;
using ITstudyv4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Teoretycznie mo�na zmieni� .UseNpgsql na .UseSqlServer, (po uwczesnym dodaniu odpowiedniego pakietu nuget) ale od dawna tego nie testowa�em
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Gdyby kto� marudzi� �e s�abe zabezpieczenia - my tak specjalnie zrobili�my
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

// Je�li u�ytkownik nie jest zalogowany, przekieruje do strony logowania, w ka�dym przypadku,
// gdy w kodzie nie jest okre�lone jakie wymagane s� uprawnienia
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
// Mo�na zakomentowa� aby projekt nie tworzy� nowych u�ytkownik�w i reszty rzeczy przy odpalaniu projektu
// ale tworzy je w momencie gdy nie ma �ADNYCH danych w danej tabeli, tak�e mo�e zosta� przez ca�y czas test�w
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
// app.UseAuthentication(); psuje apke, i tak zarz�dzanie uprawnieniami dzia�a

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();