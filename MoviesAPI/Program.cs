using Microsoft.EntityFrameworkCore;
using MoviesAPI.Controllers;
using MoviesAPI.Data;
using MoviesAPI.Services;
 
var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.
 
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnString")
    ?? throw new InvalidOperationException("Connection string DefaultConnString not found.")));
 
builder.Services.AddScoped<MoviesService>();
 
 
builder.Services.AddControllers();
builder.Services.AddScoped<MovieController>();
 
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
