using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using FluentValidation;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.BusinessLayer.Concrete;
using TashanSofrasi.BusinessLayer.ValidationRules.AboutValidations;
using TashanSofrasi.BusinessLayer.ValidationRules.CategoryValidations;
using TashanSofrasi.BusinessLayer.ValidationRules.DiscountValidations;
using TashanSofrasi.BusinessLayer.ValidationRules.FeaturesValidations;
using TashanSofrasi.BusinessLayer.ValidationRules.FooterValidations;
using TashanSofrasi.DataAccessLayer.Abstract;
using TashanSofrasi.DataAccessLayer.EntityFramework;

namespace TashanSofrasi.BusinessLayer.Container
{
    public static class Extensions 
    {
        public static void ContainerDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAboutService, AboutManager>();
            services.AddScoped<IAboutDal, EFAboutDal>();

            services.AddScoped<IBookingService, BookingManager>();
            services.AddScoped<IBookingDal, EFBookingDal>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EFCategoryDal>();

            services.AddScoped<IDiscountService, DiscountManager>();
            services.AddScoped<IDiscountDal, EFDiscountDal>();

            services.AddScoped<IFeatureService, FeatureManager>();
            services.AddScoped<IFeatureDal, EFFeatureDal>();

            services.AddScoped<IFooterService, FooterManager>();
            services.AddScoped<IFooterDal, EFFooterDal>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDal, EFProductDal>();

            services.AddScoped<ISocialMediaService, SocialMediaManager>();
            services.AddScoped<ISocialMediaDal, EFSocialMediaDal>();

            services.AddScoped<ITestimonialService, TestimonialManager>();
            services.AddScoped<ITestimonialDal, EFTestimonialDal>();

            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IOrderDal, EFOrderDal>();

            services.AddScoped<IOrderDetailService, OrderDetailManager>();
            services.AddScoped<IOrderDetailDal, EFOrderDetailDal>();

            services.AddScoped<ICashRegisterService, CashRegisterManager>();
            services.AddScoped<ICashRegisterDal, EFCashRegisterDal>();

            services.AddScoped<IMenuTableService, MenuTableManager>();
            services.AddScoped<IMenuTableDal, EFMenuTableDal>();

            services.AddScoped<IBasketService, BasketManager>();
            services.AddScoped<IBasketDal, EFBasketDal>();

            services.AddScoped<INotificationService, NotificationManager>();
            services.AddScoped<INotificationDal, EFNotificationDal>();

            services.AddScoped<IContactService, ContactManager>();
            services.AddScoped<IContactDal, EFContactDal>();

            services.AddValidatorsFromAssemblyContaining<UpdateAboutValidationRules>();
            services.AddValidatorsFromAssemblyContaining<CreateCategoryValidationRules>();
            services.AddValidatorsFromAssemblyContaining<UpdateCategoryValidationRules>();
            services.AddValidatorsFromAssemblyContaining<UpdateDiscountValidationRules>();
            services.AddValidatorsFromAssemblyContaining<UpdateFeaturesValidationRules>();
            services.AddValidatorsFromAssemblyContaining<UpdateFooterValidationRules>();
        }
    }
}
