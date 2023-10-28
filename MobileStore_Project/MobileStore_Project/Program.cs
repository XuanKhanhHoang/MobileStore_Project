using Microsoft.EntityFrameworkCore;
using MobileStore_Project.Models;
<<<<<<< HEAD
using Project_BE_Web.Interfaces.Services;
using Project_BE_Web.Interfaces;
=======
>>>>>>> adb625b050f068147097e31cab97f118394b62e4

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IBufferedFileUploadService, BufferedFileUploadLocalService>();


// Add services to the container.
builder.Services.AddDbContext<MobileStoreContext>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("MobileStoreContext")));
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MobileStoreContext>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("MobileStoreContext")));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
