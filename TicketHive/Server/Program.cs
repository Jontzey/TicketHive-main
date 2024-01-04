using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketHive.Server.Data;
using TicketHive.Server.Enums;
using TicketHive.Server.Models;
using TicketHive.Server.Repositories;
using TicketHive.Server.Repository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var identityConnectionString = builder.Configuration.GetConnectionString("IdentityDbConnection") ?? throw new InvalidOperationException("Connection string 'IdentityDbConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(identityConnectionString));

var mainConnectionString = builder.Configuration.GetConnectionString("MainDbConnection") ?? throw new InvalidOperationException("Connection string 'MainDbConnection' not found.");
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(mainConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
    {
        options.IdentityResources["openid"].UserClaims.Add("role");
        options.ApiResources.Single().UserClaims.Add("role");
    });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//builder.Services.AddBlazoredLocalStorage();	

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "ShoppingCart";
    options.Cookie.MaxAge = TimeSpan.FromDays(31);
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Create an ApplicationUser with Admin role and a UserModel user
using (var serviceProvider = builder.Services.BuildServiceProvider())
{
    // Create instances from DI container 
    var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
    var mainDbContext = serviceProvider.GetRequiredService<MainDbContext>();
    var signInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Create database if it doesn't already exist 
    applicationDbContext.Database.Migrate();

    // Create role "Admin" if it doesn't exist in the database  
    // Check if role exists in database 
    if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
    {
        // If role "Admin" doesn't exist, create new instance of IdentityRole with name "Admin" 
        IdentityRole adminRole = new()
        {
            Name = "Admin"
        };

        // ...and add to database 
        roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
    }

    // Create an admin user if it doesn't already exist one in the database 
    // Check if admin user exists
    if (signInManager.UserManager.FindByNameAsync("admin").GetAwaiter().GetResult() == null)
    {
        // If admin user doesn't exist, create new ApplicationUser with name "admin"...
        ApplicationUser admin = new()
        {
            UserName = "admin",
            Country = Country.Sweden
        };

        // ...add to IdentityDb... 
        signInManager.UserManager.CreateAsync(admin, "Password1234!").GetAwaiter().GetResult();

        // ...create new user based on admin user and add to MainDb...
        var mainUser1 = signInManager.UserManager.FindByNameAsync("admin").GetAwaiter().GetResult();
        mainDbContext.Users.Add(new()
        {
            Id = mainUser1.Id,
            Username = mainUser1.UserName!,
            Country = mainUser1.Country
        });

        await mainDbContext.SaveChangesAsync();

        // ...and add the admin user to the "Admin" role in Identity database 
        signInManager.UserManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
    }

    // Create a regular user if it doesn't already exist one in the database
    // Check if regular user exists
    if (signInManager.UserManager.FindByNameAsync("user").GetAwaiter().GetResult() == null)
    {
        // If regular user doesn't exist, create new ApplicationUser with name "user"...
        ApplicationUser user = new()
        {
            UserName = "user",
            Country = Country.Sweden
        };

        // ...and add to IdentityDb 
        signInManager.UserManager.CreateAsync(user, "Password1234!").GetAwaiter().GetResult();

        // ...create new user based on user and add to MainDb...
        var mainUser2 = signInManager.UserManager.FindByNameAsync("user").GetAwaiter().GetResult();
        mainDbContext.Users.Add(new()
        {
            Id = mainUser2.Id,
            Username = mainUser2.UserName!,
            Country = mainUser2.Country
        });

        await mainDbContext.SaveChangesAsync();
    }
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
