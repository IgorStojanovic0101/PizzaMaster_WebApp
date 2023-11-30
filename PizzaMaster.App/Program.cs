using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using PizzaMaster.Application.Services;
using PizzaMaster.BusinessLogic.Services;
using PizzaMaster.Infrastructure.System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache(); // Use an appropriate distributed cache provider in a production environment
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor(); // Add this line to register IHttpContextAccessor
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomLeft; });



var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddSingleton<IConfiguration>(configuration);


builder.Services.AddSingleton<RestClient>(); // Assuming RestClient is designed to be a singleton

builder.Services.AddScoped<IAccountService,AccountService>(); // Assuming RestClient is designed to be a singleton

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.ReturnUrlParameter = "ReturnUrl";
    });


// Singleton registration
//builder.Services.AddSingleton<ISomeService, SomeService>();

// Scoped registration
builder.Services.AddScoped<ISomeService, SomeService>();

builder.Services.AddScoped<IAdminService, AdminService>();

//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();


// Get the service provider from the builder
//using (var scope = app.Services.CreateScope())
//{
//    // Get the scoped service from the scope
//    var someService = scope.ServiceProvider.GetRequiredService<ISomeService>();

//    // Use the service
//    someService.DoSomething();

//    var someService2 = scope.ServiceProvider.GetRequiredService<ISomeService>();
//    someService2.DoSomething();

//}
app.UseNotyf();
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Restoran}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
         pattern: "{controller=Account}/{action=Login}/{id?}"
    );
});

app.Run();
