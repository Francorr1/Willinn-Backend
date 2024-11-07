using Api.Extensions;
using Data;
using Microsoft.EntityFrameworkCore;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var retryCount = 10;
        for (int i = 0; i <= retryCount; i++)
        {
            try
            {
                var context = services.GetRequiredService<UserDbContext>();
                context.Database.Migrate();
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Database migration completed successfully.");
                break;
            }
            catch (Exception ex)
            {
                // Log the error
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, $"An error occurred while migrating the database. Attempt {i + 1}");
                System.Threading.Thread.Sleep(10000);

            }
        }
    }

    app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
