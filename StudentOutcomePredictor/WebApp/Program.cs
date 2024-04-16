using Infrastructure;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;
using PredictorApp.Services;
using WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices(_ =>
{
	_.SnackbarConfiguration.ShowTransitionDuration = 300;
	_.SnackbarConfiguration.HideTransitionDuration = 300;
	_.SnackbarConfiguration.VisibleStateDuration = 1000;
	_.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
	_.SnackbarConfiguration.PreventDuplicates = false;
	_.SnackbarConfiguration.NewestOnTop = false;
	_.SnackbarConfiguration.ShowCloseIcon = true;
	_.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<PredictionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
