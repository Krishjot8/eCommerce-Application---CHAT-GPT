using eCommerce_App.Data;
using eCommerce_App.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//Configutation
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configuration = builder.Configuration;


//DbContext

builder.Services.AddDbContext<ECommerceContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();

builder.Services.AddScoped<IProductRepository, ProductRepository>();



//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//For Data Seeding

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<ECommerceContext>();
var logger = services.GetService<ILogger<Program>>();

try

{
    await context.Database.MigrateAsync();
    await eCommerceContextSeed.SeedAsync(context);
}

catch (Exception ex)

{

    logger.LogError(ex, "an error occured during migration");

}
//Data Seeding Stops Here

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
