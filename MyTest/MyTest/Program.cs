using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyTest;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDBContent>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog=UserManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")));

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
name: "user",
pattern: "User/{controller=User}/{action=Verify}/{id?}");

app.Run();
