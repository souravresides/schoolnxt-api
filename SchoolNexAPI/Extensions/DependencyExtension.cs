using Microsoft.Extensions.DependencyInjection;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Helpers;
using SchoolNexAPI.Mapping;
using SchoolNexAPI.Repositories.Abstract;
using SchoolNexAPI.Repositories.Concrete;
using SchoolNexAPI.Security;
using SchoolNexAPI.Services.Abstract;
using SchoolNexAPI.Services.Abstract.Fee;
using SchoolNexAPI.Services.Background;
using SchoolNexAPI.Services.Concrete;
using SchoolNexAPI.Services.Concrete.Fee;
using SchoolNexAPI.Utilities;
using SchoolNexAPI.Utilities.Helpers;

namespace SchoolNexAPI.Extensions
{
    public static class DependencyExtension
    {
        public static WebApplicationBuilder DependencyInjection(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<ISchoolService, SchoolService>();
            builder.Services.AddScoped<ISubscriptionTypeService, SubscriptionTypeService>();
            builder.Services.AddScoped<IAppHelper, AppHelper>();
            builder.Services.AddScoped<ISubscriptionTypeSeeder, SubscriptionTypeSeeder>();
            builder.Services.AddScoped<ISchoolSubscriptionService, SchoolSubscriptionService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddTransient<EmailSender>();
            builder.Services.AddScoped<IAdministrativeService, AdministrativeService>();
            builder.Services.AddScoped<IAcademicYearService, AcademicYearService>();
            builder.Services.Configure<AzureOptions>(builder.Configuration.GetSection("Azure"));
            builder.Services.AddScoped<IAzureService, AzureService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<UserHelper>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<ITenantContext, TenantContext>();
            builder.Services.AddAutoMapper(cfg => { }, typeof(FeeMappingProfile));



            builder.Services.AddScoped<IFeeStructureService, FeeStructureService>();
            builder.Services.AddScoped<IStudentFeePlanService, StudentFeePlanService>();
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IAdjustmentService, AdjustmentService>();
            builder.Services.AddScoped<IAuditService, AuditService>();

            //builder.Services.AddHostedService<LateFeeHostedService>();



            return builder;
        }
    }
}
