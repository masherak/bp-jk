using Adapter;
using Adapter.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;
using WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();

builder.Services.AddMudServices(_ =>
{
	_.SnackbarConfiguration.ShowTransitionDuration = 400;
	_.SnackbarConfiguration.HideTransitionDuration = 400;
	_.SnackbarConfiguration.VisibleStateDuration = 2000;
	_.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
	_.SnackbarConfiguration.PreventDuplicates = false;
	_.SnackbarConfiguration.NewestOnTop = false;
	_.SnackbarConfiguration.ShowCloseIcon = true;
	_.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services
	.AddDbContext<ApplicationDbContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IAdapter, DefaultAdapter>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
