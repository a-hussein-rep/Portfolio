using Microsoft.EntityFrameworkCore;
using TaskHub.API.Data;
using TaskHub.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Task Hub API",
        Version = "v1",
        Description = "API for managing Projects, Users and Tasks in TaskHub application"
    });

    var xmlFile = $"SwaggerDoc.xml";
    options.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), xmlFile));
});

builder.Services.AddScoped<ProjectsRepository>();
builder.Services.AddScoped<UsersRepository>();


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
