using Microsoft.OpenApi.Models;
using Tesztfeladat.Components;
using Tesztfeladat.Interfaces.Repository;
using Tesztfeladat.Interfaces.Service;
using Tesztfeladat.Repositorys;
using Tesztfeladat.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "tesztfeladat", Version = "v1" });
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".tesztfeladat.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

builder.Services.AddLogging(logging =>
{
    logging.SetMinimumLevel(LogLevel.Debug);
    logging.AddConsole();
});

builder.Services.AddSingleton<INyugtaRepository, NyugtaRepository>();
builder.Services.AddSingleton<ITetelRepository, TetelRepository>();

builder.Services.AddSingleton<INyugtaService, NyugtaService>();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnectionString"));
});

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<UserDbContext>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.MapIdentityApi<IdentityUser>();

app.UseAuthorization().UseCookiePolicy();
app.UseAuthentication().UseCookiePolicy();

app.UseSession();

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

