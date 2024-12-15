using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using System.Text.Json.Serialization;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.BusinessLayer.Concrete;
using TashanSofrasi.BusinessLayer.ValidationRules.AboutValidations;
using TashanSofrasi.BusinessLayer.ValidationRules.CategoryValidations;
using TashanSofrasi.BusinessLayer.ValidationRules.DiscountValidations;
using TashanSofrasi.BusinessLayer.ValidationRules.FeaturesValidations;
using TashanSofrasi.BusinessLayer.ValidationRules.FooterValidations;
using TashanSofrasi.DataAccessLayer.Abstract;
using TashanSofrasi.DataAccessLayer.Concrete;
using TashanSofrasi.DataAccessLayer.EntityFramework;
using TashanSofrasiSignalRApi.Hubs;
using TashanSofrasi.BusinessLayer.Container;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.SignalRApi.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.SignalRApi.Production.json", optional: false, reloadOnChange: true);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:8083") 
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

builder.Services.AddSignalR();
builder.Services.AddDbContext<TashanSofrasiContext>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.ContainerDependencies();

builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true;
});

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCookiePolicy(new CookiePolicyOptions
{
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
    MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None,
    Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always
});
app.MapControllers();

app.MapHub<SignalRHub>("/signalrhub");

app.Run();
