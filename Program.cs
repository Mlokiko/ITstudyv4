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
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.SeedUsersAndRolesAsync(services);
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
// app.UseAuthentication(); // to chyba nie jest potrzebne, wklejam bo mo¿e sie przydaæ XD

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Banowanie u¿ytkowników mo¿emy zrobiæ na zasadzie rang/ró³ - ranga "zbanowany" która bêdzie wykluczaæ z funkcjonalnoœci normalnego usera... ¿e te¿ o tym wczeœniej nie pomyœla³em
// Usuwanie samego siebie z poziomu /admin/ShowAllUsers nie wylogowywuje ani nie zmienia aktualnego stanu - tzn. mo¿na wci¹¿ przegl¹daæ projekt bez problemu jako user

// Bug który nie wiem czy jest wa¿ny - po usuniêciu wszystkich u¿ytkowników (bez wylogowywania, zwi¹zane z tematem u góry) i ponownym za³adowaniu strony (z w³¹czon¹ opcj¹ "remember me"), nie wyœwietla siê profilowe w górnym prawym rogu, i nie da siê wejœæ do panelu konta. Trzeba i wylogowaæ i wtedy dzia³a


// INFO

// Ograniczenia do tworzenia userName s¹ ze standardowego Identity - nie mo¿na u¿ywaæ znaków specjalnych (polskich liter), ale mo¿na u¿ywaæ cyfr i ma³ych liter