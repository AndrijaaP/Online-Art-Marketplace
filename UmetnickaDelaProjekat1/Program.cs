using Microsoft.EntityFrameworkCore;
using UmetnickaDelaProjekat1.Models; // Adjust the namespace as needed

var builder = WebApplication.CreateBuilder(args);

// Register the DbContext
builder.Services.AddDbContext<UmetnickaDelaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add other services
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

var app = builder.Build();

app.UseSession();

// Middleware and routing configuration
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

app.Run();