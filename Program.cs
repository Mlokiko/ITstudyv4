using ITstudyv4.Data;
using ITstudyv4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddIdentity<ForumUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders().AddRoles<IdentityRole>();  //to .AddRoles<IdentityRole>() nie wiem czy jest wymagane

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
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.SeedUsersAndRolesAsync(services);
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
// app.UseAuthentication(); // to chyba nie jest potrzebne, wklejam bo mo�e sie przyda� XD

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Banowanie u�ytkownik�w mo�emy zrobi� na zasadzie rang/r� - ranga "zbanowany" kt�ra b�dzie wyklucza� z funkcjonalno�ci normalnego usera... �e te� o tym wcze�niej nie pomy�la�em
// Usuwanie samego siebie z poziomu /admin/ShowAllUsers nie wylogowywuje ani nie zmienia aktualnego stanu - tzn. mo�na wci�� przegl�da� projekt bez problemu jako user

// Bug kt�ry nie wiem czy jest wa�ny - po usuni�ciu wszystkich u�ytkownik�w (bez wylogowywania, zwi�zane z tematem u g�ry) i ponownym za�adowaniu strony (z w��czon� opcj� "remember me"), nie wy�wietla si� profilowe w g�rnym prawym rogu, i nie da si� wej�� do panelu konta. Trzeba i wylogowa� i wtedy dzia�a


// INFO

// Ograniczenia do tworzenia userName s� ze standardowego Identity - nie mo�na u�ywa� znak�w specjalnych (polskich liter), ale mo�na u�ywa� cyfr i ma�ych liter