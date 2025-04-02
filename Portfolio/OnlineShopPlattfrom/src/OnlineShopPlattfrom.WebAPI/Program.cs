using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using OnlineShopPlattfrom.WebAPI.Data;
using OnlineShopPlattfrom.WebAPI.Repositories.Implementations;
using OnlineShopPlattfrom.WebAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseCors(
    policy => policy.WithOrigins("https://localhost:7047", "https://localhost:7025")
    .AllowAnyHeader()
    .AllowAnyMethod()
);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
