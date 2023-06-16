using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    throw new ("Connection String 'DefaultConnection' is not initialized");
builder.Services.AddDbContext<StoreContext>(options => 
    options.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) 
{
    var Services = scope.ServiceProvider;
    var loggerFactory = Services.GetRequiredService<ILoggerFactory>();
    try {
        var context = Services.GetRequiredService<StoreContext>();
        // Apply any pending migration to the database
        await context.Database.MigrateAsync();
    }
    catch (Exception ex) {
        var logger = loggerFactory.CreateLogger<StoreContext>();
        logger.LogError(ex, "An error occured during migration.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
