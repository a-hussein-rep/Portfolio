using OnlineShopPlattfrom.WebUI.Components;
using OnlineShopPlattfrom.WebUI.Services.Implementations;
using OnlineShopPlattfrom.WebUI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

builder.Services.AddHttpClient("ProductClient", (options) =>
{
    options.BaseAddress = new Uri("https://localhost:7047");
});

builder.Services.AddScoped<IMultimediaProductsService, MultimediaProductsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>();

app.Run();
