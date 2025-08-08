using SchoolNexAPI.Data;
using SchoolNexAPI.Repositories.Abstract;
using SchoolNexAPI.Repositories.Concrete;
using SchoolNexAPI.Security;
using SchoolNexAPI.Services.Abstract;
using SchoolNexAPI.Services.Concrete;
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
            builder.Services.AddScoped<ICustomFieldService, CustomFieldService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddTransient<EmailSender>();
            builder.Services.AddScoped<IAcademicYearService, AcademicYearService>();

            return builder;
        }
    }
}
