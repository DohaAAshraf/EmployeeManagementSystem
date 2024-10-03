using EmployeeSystem.ApplicationDbContexts;
using EmployeeSystem.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using EmployeeSystem.Repositories;
using EmployeeSystem.Models;
using EmployeeSystem;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services with ApplicationUser and IdentityRole
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Register the Unit of Work and repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepository<EmployeeModel>, Repository<EmployeeModel>>(); // Register the repository for EmployeeModel

var app = builder.Build();

// Seed the Admin role and an Admin user
async Task SeedAdminAsync(IServiceProvider services)
{
    using (var scope = services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Ensure the roles exist
        string[] roles = { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Seed a default admin user
string adminEmail = "Doha@admin.com";
string adminPassword = "Admin@1234"; // Strong password

var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            // Create the admin user if it doesn't exist
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Doha",
                LastName = "Ashraf",
                DateOfBirth = new DateTime(1999, 12, 10), // Default date of birth
                PhoneNumber = "123-456-7890" // Default phone number
            };

            var createAdminResult = await userManager.CreateAsync(adminUser, adminPassword);
            if (createAdminResult.Succeeded)
            {
                // Assign the Admin role to the user
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else
            {
                // Log errors
                foreach (var error in createAdminResult.Errors)
                {
                    Console.WriteLine($"Error creating admin user: {error.Description}");
                }
            }
        }
    }
}

// Call SeedAdminAsync after the app has started
await SeedAdminAsync(app.Services);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Handle errors in production
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add middleware to handle authentication and authorization
app.UseAuthentication();  // This ensures authentication is handled on each request
app.UseAuthorization();   // This ensures authorization policies are applied

// Map the default route to Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
