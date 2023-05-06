using Microsoft.EntityFrameworkCore;
using Data;
using SPU123_Shop_MVC.Services;
using Microsoft.AspNetCore.Identity;
using DataAccess.Entities;
using System.Data;
using SPU123_Shop_MVC.Helpers;

var builder = WebApplication.CreateBuilder(args);

string connStr = builder.Configuration.GetConnectionString("LocalDb");

// Add services to the Dependency Injection (DI) container.
builder.Services.AddControllersWithViews();

// DbContext congifurations
builder.Services.AddDbContext<ShopDbContext>(x => x.UseSqlServer(connStr));

builder.Services.AddIdentity<User, IdentityRole>()
               .AddDefaultTokenProviders()
               .AddDefaultUI()
               .AddEntityFrameworkStores<ShopDbContext>();

// add custom servies
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// ----------------- Seed Roles and Admin user
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    // seed roles
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    foreach (var role in Enum.GetNames(typeof(Roles)))
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // seed admin user
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

    const string USERNAME = "myadmin@myadmin.com";
    const string PASSWORD = "Admin1@";

    var existingUser = userManager.FindByNameAsync(USERNAME).Result;

    if (existingUser == null)
    {
        var admin = new User()
        {
            UserName = USERNAME,
            Email = USERNAME
        };

        var result = userManager.CreateAsync(admin, PASSWORD).Result;

        if (result.Succeeded)
        {
            userManager.AddToRoleAsync(admin, Roles.Administrator.ToString()).Wait();
        }
    }
}

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

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
