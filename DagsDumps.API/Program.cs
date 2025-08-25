using DagsDumps.API.Data;
using DagsDumps.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register EF Core with SQL Server
builder.Services.AddDbContext<DagsDumpsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers and Swagger
builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
        o.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in dev mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Map controller endpoints
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DagsDumpsDbContext>();
    if (!db.Dumpsters.Any())
    {
        db.Dumpsters.AddRange(
            new Dumpster { SizeYards = 10, Description = "Small", Price = 349, MaxWeightTons = 1.5f, OverageFeePerTon = 120 },
            new Dumpster { SizeYards = 15, Description = "Medium", Price = 429, MaxWeightTons = 2.0f, OverageFeePerTon = 120 },
            new Dumpster { SizeYards = 20, Description = "Large", Price = 499, MaxWeightTons = 3.0f, OverageFeePerTon = 120 }
        );
        db.SaveChanges();
    }

    if (!db.Customers.Any())
    {
        db.Customers.AddRange(
            new Customer { FullName = "Anthony D'Agostino", PhoneNumber = "732-123-4567", Email = "anthonysdagostino@gmail.com", Address = "123 Mario Party Lane"},
            new Customer { FullName = "John Smith", PhoneNumber = "609-987-6543", Email = "john23smith@gmail.com", Address = "321 Pear Drive" },
            new Customer { FullName = "Joe Burger", PhoneNumber = "108-222-3333", Email = "joeyburger1@gmail.com", Address = "92 Trail Court" }
        );
        db.SaveChanges();
    }
}


app.Run();
