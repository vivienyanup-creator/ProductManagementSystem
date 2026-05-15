using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using ProductManagement.Components;
using ProductManagement.Features.Data;
using ProductManagement.Features.Data.Enums;
using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Helpers;
using ProductManagement.Features.Repositories.Implementations;
using ProductManagement.Features.Repositories.Interfaces;
using ProductManagement.Features.Services.Implementations;
using ProductManagement.Features.Services.Interfaces;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Razor Components + Interactive Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options => options.DetailedErrors = true);

// MudBlazor UI Framework
builder.Services.AddMudServices();

// EF Core + MySQL via Pomelo (or in-memory for testing)
// Use in-memory for quick testing (uncomment this line and comment the MySQL one):
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseInMemoryDatabase("ProductManagementTestDb"));

// MySQL (uncomment this and comment in-memory when you have MySQL running):
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("ProductManagement"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("ProductManagement"))
    ));

// Custom Authentication
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "Cookies";
})
.AddCookie("Cookies", options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/login";
});

builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

var app = builder.Build();

// Add seed data for in-memory database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try 
    {
        db.Database.Migrate(); // Ensure database is created and migrated
    } 
    catch (Exception ex) 
    {
        Console.WriteLine($"Migration failed: {ex.Message}");
        throw;
    }
    // Check if we have any users yet
    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new User
            {
                Username = "admin",
                Password = "admin123",
                Role = UserRole.Admin
            },
            new User
            {
                Username = "staff",
                Password = "staff123",
                Role = UserRole.Staff
            }
        );
        db.Categories.AddRange(
            new Category
            {
                Name = "Electronics",
                Description = "Electronic devices and gadgets"
            },
            new Category
            {
                Name = "Home",
                Description = "Home appliances and products"
            }
        );
        db.Brands.AddRange(
            new Brand
            {
                Name = "TechCorp",
                Description = "Leading tech brand"
            },
            new Brand
            {
                Name = "HomeComfort",
                Description = "Home products brand"
            }
        );
        db.Suppliers.AddRange(
            new Supplier
            {
                Name = "TechSuppliers Inc.",
                ContactPerson = "John Doe",
                Email = "john@techsuppliers.com",
                Phone = "555-1234",
                Address = "123 Tech Street"
            }
        );
        db.SaveChanges();
        
        var electronicsCategory = db.Categories.First(c => c.Name == "Electronics");
        var techCorpBrand = db.Brands.First(b => b.Name == "TechCorp");
        var supplier = db.Suppliers.First();
        
        db.Products.AddRange(
            new Product
            {
                Name = "Laptop",
                Description = "High-performance laptop",
                CategoryId = electronicsCategory.Id,
                BrandId = techCorpBrand.Id,
                SupplierId = supplier.Id,
                SKU = "LT-001",
                Price = 999.99m,
                Stock = 10
            },
            new Product
            {
                Name = "Mouse",
                Description = "Wireless mouse",
                CategoryId = electronicsCategory.Id,
                BrandId = techCorpBrand.Id,
                SupplierId = supplier.Id,
                SKU = "MS-001",
                Price = 29.99m,
                Stock = 50
            }
        );
        db.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/api/login-test", () => "Hello");

app.MapPost("/api/login", async (
    HttpContext context,
    IUserService userService) =>
{
    var form = await context.Request.ReadFormAsync();
    var username = form["username"].ToString();
    var password = form["password"].ToString();
    var user = await userService.GetUserByUsernameAsync(username);

    if (user == null || user.Password != password)
    {
        return Results.Redirect("/login?error=1");
    }

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role.ToString())
    };
    var identity = new ClaimsIdentity(claims, "Cookies");
    var principal = new ClaimsPrincipal(identity);

    await context.SignInAsync("Cookies", principal);
    return Results.Redirect("/dashboard");
});

app.MapGet("/api/logout", async (HttpContext context) =>
{
    await context.SignOutAsync("Cookies");
    return Results.Redirect("/login");
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();


