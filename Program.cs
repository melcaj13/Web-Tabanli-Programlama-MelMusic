using Microsoft.EntityFrameworkCore;
using MelMusic.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("CookieAuth") 
    .AddCookie("CookieAuth", options => 
    { 
        options.LoginPath = "/Account/Login";   
    }); 
 
builder.Services.AddAuthorization(options => 
{ 
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));   
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User")); 
});

builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlServer("server=LAPTOP-H0KHFLR0\\SQLEXPRESS; uid=sa; password=pass; database=MelMusic; TrustServerCertificate=true"));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // Session 30 dakika aktif kalır
    options.Cookie.HttpOnly = true; // Güvenlik için
    options.Cookie.IsEssential = true; // Çerez zorunlu olarak işlenir
});
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
app.UseAuthentication();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=AnaSayfa}/{id?}");

app.Run();
