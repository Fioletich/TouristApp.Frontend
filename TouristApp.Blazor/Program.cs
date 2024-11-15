using Syncfusion.Blazor;
using TouristApp.Blazor.Components;
using TouristApp.Blazor.Services.Categories;
using TouristApp.Blazor.Services.Featureds;
using TouristApp.Blazor.Services.PinpointCategoryService;
using TouristApp.Blazor.Services.Pinpoints;
using TouristApp.Blazor.Services.Routes;
using TouristApp.Blazor.Services.TouristRoutes;
using TouristApp.Blazor.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<HttpClient>(new HttpClient() { BaseAddress = new Uri(builder.Configuration["BaseUrl"]) });
builder.Services.AddSingleton<ICategoryService, ClientCategoryService>();
builder.Services.AddSingleton<IFeaturedService, ClientFeaturedService>();
builder.Services.AddSingleton<IPinpointService, ClientPinpointService>();
builder.Services.AddSingleton<IRouteService, ClientRouteService>();
builder.Services.AddSingleton<ITouristRouteService, ClientTouristRouteService>();
builder.Services.AddSingleton<IUserService, ClientUserService>();
builder.Services.AddSingleton<IPinpointCategoryService, ClientPinpointCategoryService>();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();