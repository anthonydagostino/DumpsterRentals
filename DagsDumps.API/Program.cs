using DagsDumps.API.Data;
using DagsDumps.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1) Services
builder.Services.AddDbContext<DagsDumpsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
        o.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS: allow Vite dev server
var allowVite = "_allowVite";
builder.Services.AddCors(o =>
{
    o.AddPolicy(allowVite, p =>
        p.WithOrigins("http://localhost:5173")
         .AllowAnyHeader()
         .AllowAnyMethod());
});

var app = builder.Build();

// 2) Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS must run before MapControllers
app.UseCors(allowVite);

app.UseAuthorization();

app.MapControllers();

// 3) (Optional) Seed dev data once, at startup
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<DagsDumpsDbContext>();

    if (!db.Dumpsters.Any())
    {
        db.Dumpsters.AddRange(
            new Dumpster { SizeYards = 10, Description = "Small", Price = 349, MaxWeightTons = 1.5f, OverageFeePerTon = 120 },
            new Dumpster { SizeYards = 15, Description = "Medium", Price = 429, MaxWeightTons = 2.0f, OverageFeePerTon = 120 },
            new Dumpster { SizeYards = 20, Description = "Large", Price = 499, MaxWeightTons = 3.0f, OverageFeePerTon = 120 }
        );
    }

    if (!db.Customers.Any())
    {
        db.Customers.AddRange(
            new Customer { FullName = "Anthony D'Agostino", PhoneNumber = "732-123-4567", Email = "anthonysdagostino@gmail.com", Address = "123 Mario Party Lane" },
            new Customer { FullName = "John Smith", PhoneNumber = "609-987-6543", Email = "john23smith@gmail.com", Address = "321 Pear Drive" },
            new Customer { FullName = "Joe Burger", PhoneNumber = "108-222-3333", Email = "joeyburger1@gmail.com", Address = "92 Trail Court" }
        );
    }

    if (db.ChangeTracker.HasChanges())
        db.SaveChanges();
}

app.Run();
