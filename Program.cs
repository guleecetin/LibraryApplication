/*using LibraryApplication.Models;
using LibraryApplication.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UygulamaDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<UygulamaDbContext>().AddDefaultTokenProviders();
builder.Services.AddRazorPages();
//_kitapTuruRepository Nesnesi
builder.Services.AddScoped<IKitapTuruRepository, KitapTuruRepository>();
builder.Services.AddScoped<IKitapRepository, KitapRepository>();
builder.Services.AddScoped<IKiralamaRepository, KiralamaRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
*/
using LibraryApplication.Models;
using LibraryApplication.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add the DbContext with SQL Server
builder.Services.AddDbContext<UygulamaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = true) // Optional: You can change this depending on your requirement
    .AddEntityFrameworkStores<UygulamaDbContext>()
    .AddDefaultTokenProviders();

// Add Razor Pages (for Identity pages, if used)
builder.Services.AddRazorPages();

// Register repository services
builder.Services.AddScoped<IKitapTuruRepository, KitapTuruRepository>();
builder.Services.AddScoped<IKitapRepository, KitapRepository>();
builder.Services.AddScoped<IKiralamaRepository, KiralamaRepository>();

// Email sender service
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Ensure the application runs and handle user login for auto role assignments
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Authentication & Authorization
app.UseAuthentication(); // Add this to enable authentication
app.UseAuthorization();  // Authorization middleware

// Handle user login and automatically assign roles if needed
app.Use(async (context, next) =>
{
    var userManager = context.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
    var signInManager = context.RequestServices.GetRequiredService<SignInManager<IdentityUser>>();

    var user = await userManager.GetUserAsync(context.User);

    if (user != null && !await userManager.IsInRoleAsync(user, UserRoles.Role_Admin) && !await userManager.IsInRoleAsync(user, UserRoles.Role_Ogrenci))
    {
        // Automatically assign roles to the user based on specific conditions
        if (user.Email.Contains("admin"))
        {
            await userManager.AddToRoleAsync(user, UserRoles.Role_Admin);
        }
        else
        {
            await userManager.AddToRoleAsync(user, UserRoles.Role_Ogrenci);
        }
    }

    await next();
});

app.MapRazorPages(); // For Identity pages

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
