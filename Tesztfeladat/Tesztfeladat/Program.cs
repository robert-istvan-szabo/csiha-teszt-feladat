using Microsoft.OpenApi.Models;
using Tesztfeladat.Components;
using Tesztfeladat.Interfaces.Repository;
using Tesztfeladat.Interfaces.Service;
using Tesztfeladat.Repositorys;
using Tesztfeladat.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "tesztfeladat", Version = "v1" });
});

builder.Services.AddLogging(logging =>
{
    logging.SetMinimumLevel(LogLevel.Debug);
    logging.AddConsole();
});

builder.Services.AddSingleton<IFelhasznaloRepository, FelhasznaloRepository>();
builder.Services.AddSingleton<INyugtaRepository, NyugtaRepository>();
builder.Services.AddSingleton<ITetelRepository, TetelRepository>();

builder.Services.AddSingleton<IFelhasznaloService, FelhasznaloService>();
builder.Services.AddScoped<INyugtaService, NyugtaService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "tesztfeladat v1");
});

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
