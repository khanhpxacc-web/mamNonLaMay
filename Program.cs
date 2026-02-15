using MamNonApp.Interfaces;
using MamNonApp.Repositories;
using MamNonApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
