using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using TashanSofrasi.DataAccessLayer.Concrete;
using TashanSofrasi.EntityLayer.Entities;
using TashanSofrasiWebApp.HttpsWeb;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.WebApp.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.WebApp.Production.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddDbContext<TashanSofrasiContext>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<TashanSofrasiContext>();
var certificate = new X509Certificate2("HttpsWeb/tashansofrasi.pfx", "Passw@rd1.");
var certificateValidator = new SingleCertificateValidator(certificate);
builder.Services.AddHttpClient("Default").ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = certificateValidator.Validate
    };
});
var requireAuthorizationPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
builder.Services.AddControllersWithViews(opt => opt.Filters.Add(new AuthorizeFilter(requireAuthorizationPolicy)));
builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.LoginPath = "/Login/";
    opts.LogoutPath = "/Login/Logout";
    opts.AccessDeniedPath = "/Error/NotFound404";
});

AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);

var app = builder.Build();

app.UseStatusCodePages(async x =>
{
    if (x.HttpContext.Response.StatusCode == 404)
    {
        x.HttpContext.Response.Redirect("/Error/NotFound404");
    }

});

app.Use(async (context, next) =>
{
    if (context.Request.Path.Equals("/Admin", StringComparison.OrdinalIgnoreCase))
    {
        context.Response.Redirect("/Admin/Statistic");
    }
    else if (context.Request.Path.Equals("/Admin/", StringComparison.OrdinalIgnoreCase))
    {
        context.Response.Redirect("/Admin/Statistic");
    }
    else
    {
        await next();
    }
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStatusCodePagesWithReExecute("/Error/Error404", "?code={0}");
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseEndpoints(endpoints =>
{
    // Area route
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    // Default route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();