// Add these using statements for Entity Framework Core
using Microsoft.EntityFrameworkCore;
using EcoTrack.WebMvc.Data;

var builder = WebApplication.CreateBuilder(args);

// --- START: Add Database Configuration ---
// This defines the connection to your SQLite database.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=ecotrack.db";

// This registers your ApplicationDbContext with the application's services.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
// --- END: Add Database Configuration ---

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // This is the standard way to enable CSS, JS, etc.
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();