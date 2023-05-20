using Microsoft.EntityFrameworkCore;
using Data;
using SPU123_Shop_MVC.Services;
using Microsoft.AspNetCore.Identity;
using DataAccess.Entities;
using System.Data;
using SPU123_Shop_MVC.Helpers;
using SPU123_Shop_MVC.Interfaces;

var builder = WebApplication.CreateBuilder(args);

string connStr = builder.Configuration.GetConnectionString("AzureDb");

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
builder.Services.AddScoped<IFileService, AzureFileService>();

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
    Seeder.SeedRoles(serviceProvider).Wait();

    // seed admin user
    Seeder.SeedAdmins(serviceProvider).Wait();
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
