using Microsoft.AspNetCore.Cors;
using GestionDepot.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GestionDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GestionDbContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

// Configure CORS
app.UseCors(options => options
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200")  // Remplacez ceci par l'URL de votre frontend
);


app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
