using API.Errors;
using API.Helper;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options => 
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(e => e.Value.Errors)
            .Select(e => e.ErrorMessage)
            .ToArray();
        
        var errorResponse = new ApiValidationErrorResponse 
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    }
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    throw new ("Connection String 'DefaultConnection' is not initialized");
builder.Services.AddDbContext<StoreContext>(options => 
    options.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(typeof(MapperProfiles));


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
        // Seed data at the time of initialization
        await StoreContextSeed.SeedAsync(context, loggerFactory);
    }
    catch (Exception ex) {
        var logger = loggerFactory.CreateLogger<StoreContext>();
        logger.LogError(ex, "An error occured during migration.");
    }
}

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseRouting();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
