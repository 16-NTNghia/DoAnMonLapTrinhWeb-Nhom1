using DoAnMonLapTrinhWeb_Nhom1.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
AddCookie(options =>
{
    options.Cookie.Name = "N1BikerentCookie";
    options.LoginPath = "/Home/Index";

    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.Cookie.MaxAge = options.ExpireTimeSpan; // optional
    options.SlidingExpiration = true;
});

var connectionString = builder.Configuration.GetConnectionString("WebsiteBanHangConnection");
builder.Services.AddDbContext<QuanLyThueXeMayTuLaiContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
