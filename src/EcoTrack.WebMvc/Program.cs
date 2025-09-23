// --- Add all necessary using statements ---
using Microsoft.EntityFrameworkCore;
using EcoTrack.WebMvc.Data;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Repositories;
using EcoTrack.WebMvc.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// --- START: Service Registration ---

// 1. Add services for the MVC framework.
builder.Services.AddControllersWithViews();

// 2. Configure the database connection (DbContext).
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=ecotrack.db";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// 3. Register all your repositories.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IBadgeRepository, BadgeRepository>();
builder.Services.AddScoped<ISuggestionRepository, SuggestionRepository>();
builder.Services.AddScoped<ILeaderboardEntryRepository, LeaderboardEntryRepository>();
builder.Services.AddScoped<IFamilyRepository, FamilyRepository>();
builder.Services.AddScoped<IEmissionFactorRepository, EmissionFactorRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login"; // Redirect here if a user is not logged in
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });
// 4. Register the Unit of Work.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 5. Register your services. (THIS SECTION WAS MISSING)
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<ISuggestionService, SuggestionService>();
builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();
builder.Services.AddScoped<IBadgeService, BadgeService>(); 

builder.Services.AddHostedService<LeaderboardUpdateService>();
// 6. Register AutoMapper.
builder.Services.AddAutoMapper(typeof(MappingProfile));

// --- END: Service Registration ---

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();