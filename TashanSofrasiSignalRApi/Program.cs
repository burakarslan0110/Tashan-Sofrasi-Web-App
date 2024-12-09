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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:7114") 
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddDbContext<TashanSofrasiContext>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IAboutService, AboutManager>();
builder.Services.AddScoped<IAboutDal, EFAboutDal>();

builder.Services.AddScoped<IBookingService, BookingManager>();
builder.Services.AddScoped<IBookingDal, EFBookingDal>();

builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, EFCategoryDal>();

builder.Services.AddScoped<IDiscountService, DiscountManager>();
builder.Services.AddScoped<IDiscountDal, EFDiscountDal>();

builder.Services.AddScoped<IFeatureService, FeatureManager>();
builder.Services.AddScoped<IFeatureDal, EFFeatureDal>();

builder.Services.AddScoped<IFooterService, FooterManager>();
builder.Services.AddScoped<IFooterDal, EFFooterDal>();

builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, EFProductDal>();

builder.Services.AddScoped<ISocialMediaService, SocialMediaManager>();
builder.Services.AddScoped<ISocialMediaDal, EFSocialMediaDal>();

builder.Services.AddScoped<ITestimonialService, TestimonialManager>();
builder.Services.AddScoped<ITestimonialDal, EFTestimonialDal>();

builder.Services.AddScoped<IOrderService, OrderManager>();
builder.Services.AddScoped<IOrderDal, EFOrderDal>();

builder.Services.AddScoped<IOrderDetailService, OrderDetailManager>();
builder.Services.AddScoped<IOrderDetailDal, EFOrderDetailDal>();

builder.Services.AddScoped<ICashRegisterService, CashRegisterManager>();
builder.Services.AddScoped<ICashRegisterDal, EFCashRegisterDal>();

builder.Services.AddScoped<IMenuTableService, MenuTableManager>();
builder.Services.AddScoped<IMenuTableDal, EFMenuTableDal>();

builder.Services.AddScoped<IBasketService, BasketManager>();
builder.Services.AddScoped<IBasketDal, EFBasketDal>();

builder.Services.AddScoped<INotificationService, NotificationManager>();
builder.Services.AddScoped<INotificationDal, EFNotificationDal>();

builder.Services.AddScoped<IContactService, ContactManager>();
builder.Services.AddScoped<IContactDal, EFContactDal>();

builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true;
});

builder.Services.AddValidatorsFromAssemblyContaining<UpdateAboutValidationRules>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryValidationRules>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryValidationRules>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateDiscountValidationRules>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateFeaturesValidationRules>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateFooterValidationRules>();


builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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
