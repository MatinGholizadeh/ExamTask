using Microsoft.EntityFrameworkCore;
using ExamTask.Infrastructure.Identity;
using ExamTask.Application.DependencyInjection;
using ExamTask.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// DI Settings and Services
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);  // For Dependency Injection

// Razor Pages settings
builder.Services.AddRazorPages();

// Database connection settings
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Web API settings
builder.Services.AddControllers();

var app = builder.Build();

// Route settings
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Registering Razor Pages routes
app.MapRazorPages();

// Register Web API routes
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
