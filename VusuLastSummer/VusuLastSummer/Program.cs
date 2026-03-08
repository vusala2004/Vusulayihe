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


var app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();




app.UseStaticFiles();

app.MapControllerRoute(
    "admin",
    "{Area:exists}/{controller=dashboard}/{action=index}/{id?}"
    );


app.MapControllerRoute(
    "default",
    "{controller=home}/{action=index}/{id?}"
    );
app.Run();