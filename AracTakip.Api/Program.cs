using Microsoft.EntityFrameworkCore;
using AracTakip.Api.Data;
using AracTakip.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddDbContext<AracTakipDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Araç Takip API", Version = "v1" });
});

// CORS - Zorunlu
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// CORS'u en başta ekle
app.UseCors();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// API Endpoints
app.MapGet("/api/araclar", async (AracTakipDbContext db) =>
{
    try
    {
        var araclar = await db.Araclar.ToListAsync();
        return Results.Ok(araclar);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Veritabanı hatası: {ex.Message}");
    }
})
.WithName("GetAraclar")
.WithOpenApi()
.WithTags("Araclar");

app.MapGet("/api/araclar/{id:int}", async (int id, AracTakipDbContext db) =>
{
    try
    {
        var arac = await db.Araclar.FindAsync(id);
        
        if (arac == null)
            return Results.NotFound($"ID {id} ile araç bulunamadı.");
            
        return Results.Ok(arac);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Veritabanı hatası: {ex.Message}");
    }
})
.WithName("GetAracById")
.WithOpenApi()
.WithTags("Araclar");

app.Run();