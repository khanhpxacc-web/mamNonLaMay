using MamNonApp.Data;
using MamNonApp.Interfaces;
using MamNonApp.Repositories;
using MamNonApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ============================================
// DATABASE CONFIGURATION - SQLite for Development
// ============================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// ============================================
// IDENTITY CONFIGURATION
// ============================================
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    
    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    
    // User settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configure Application Cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Login";
    options.LogoutPath = "/Admin/Logout";
    options.AccessDeniedPath = "/Admin/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(2);
    options.SlidingExpiration = true;
});

// ============================================
// DEPENDENCY INJECTION CONFIGURATION
// Design Patterns: Dependency Injection, Repository Pattern, Unit of Work
// ============================================

// Register Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Services (Service Layer Pattern)
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ISchoolInfoService, SchoolInfoService>();
builder.Services.AddScoped<ISeoService, SeoService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();

// Add Response Caching for SEO performance
builder.Services.AddResponseCaching();

// Add Response Compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

// Configure Route Options
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Enable Response Compression
app.UseResponseCompression();

// Enable Response Caching
app.UseResponseCaching();

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Cache static files for 1 day in development, 7 days in production
        var maxAge = app.Environment.IsDevelopment() ? 86400 : 604800;
        ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={maxAge}");
    }
});

app.UseRouting();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// SEO-Friendly Routes
app.MapControllerRoute(
    name: "seo_routes",
    pattern: "{controller}/{action}/{id}/{slug?}",
    defaults: new { controller = "Home", action = "Index" },
    constraints: new { id = @"\d+" }
);

// Custom routes for Vietnamese SEO-friendly URLs
app.MapControllerRoute(
    name: "gioi_thieu",
    pattern: "gioi-thieu",
    defaults: new { controller = "Home", action = "About" }
);

app.MapControllerRoute(
    name: "chuong_trinh_hoc",
    pattern: "chuong-trinh-hoc",
    defaults: new { controller = "Programs", action = "Index" }
);

app.MapControllerRoute(
    name: "chuong_trinh_chi_tiet",
    pattern: "chuong-trinh-hoc/{id}/{slug?}",
    defaults: new { controller = "Programs", action = "Detail" },
    constraints: new { id = @"\d+" }
);

app.MapControllerRoute(
    name: "thu_vien_anh",
    pattern: "thu-vien-anh",
    defaults: new { controller = "Gallery", action = "Index" }
);

app.MapControllerRoute(
    name: "lien_he",
    pattern: "lien-he",
    defaults: new { controller = "Contact", action = "Index" }
);

// New routes for Activities and News
app.MapControllerRoute(
    name: "hoat_dong",
    pattern: "hoat-dong",
    defaults: new { controller = "Activities", action = "Index" }
);

app.MapControllerRoute(
    name: "hoat_dong_chi_tiet",
    pattern: "hoat-dong/{id}/{slug?}",
    defaults: new { controller = "Activities", action = "Detail" },
    constraints: new { id = @"\d+" }
);

app.MapControllerRoute(
    name: "tin_tuc",
    pattern: "tin-tuc",
    defaults: new { controller = "News", action = "Index" }
);

app.MapControllerRoute(
    name: "tin_tuc_chi_tiet",
    pattern: "tin-tuc/{id}/{slug?}",
    defaults: new { controller = "News", action = "Detail" },
    constraints: new { id = @"\d+" }
);

// Admin route
app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{action=Index}/{id?}",
    defaults: new { controller = "Admin" });

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Initialize Database and Seed Admin User
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        
        // Apply migrations
        await context.Database.MigrateAsync();
        
        // Seed admin user
        await SeedAdminUser(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();

// Seed Admin User
static async Task SeedAdminUser(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    // Create Admin role if not exists
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Create default admin user if not exists
    var adminEmail = "admin@mamnonlamay.edu.vn";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, "Admin@123");
        
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
