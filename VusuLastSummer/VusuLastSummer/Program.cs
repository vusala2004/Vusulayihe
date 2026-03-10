using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using VusuLastSummer.DAL;
using VusuLastSummer.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opp =>
    opp.UseSqlServer(builder.Configuration.GetConnectionString("default"))
);
//builder.Services.AddScoped<ILayoutService, LayoutService>();

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = false;
    opt.User.RequireUniqueEmail = true;
    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

// Session üçün konfiqurasiya əlavə edirik
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(7); // Səbət 7 gün yadda qalsın
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting(); // Bunu əlavə etmək yaxşı olar
app.UseSession(); // Mütləq UseRouting və UseAuthentication arasında olmalıdır

app.UseAuthentication(); // Əvvəl kim olduğunu yoxla
app.UseAuthorization();  // Sonra icazəni yoxla

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    "default",
    "{controller=home}/{action=index}/{id?}"
    );
app.Run();