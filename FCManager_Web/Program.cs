using FCManager.DataAccess.Data;
using FCManager.DataAccess.DbInitializer;
using FCManager.DataAccess.Interfaces;
using FCManager.DataAccess.Repository;
using FCManager.IdentityManger;
using Microsoft.EntityFrameworkCore;
using static FCManager.Models.Wrappers.Enum;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentityInfrastructure(builder.Configuration);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddHttpContextAccessor();

string dbConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(dbConnection,
  b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IApiRepository, ApiRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole(Roles.GlobalAdmin.ToString()));
    options.AddPolicy("RequireCoachRole", policy => policy.RequireRole(Roles.Coach.ToString()));
    options.AddPolicy("RequirePlayerRole", policy => policy.RequireRole(Roles.Player.ToString()));
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    aspnetcore - hsts.
        app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
SeedDatabase();

app.MapControllerRoute(
       name: "areas",
       pattern: "{area:exists}/{controller=Home}/{action=Index}"
     );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();


void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}