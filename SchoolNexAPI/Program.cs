using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.Extensions;
using SchoolNexAPI.Middleware;
using SchoolNexAPI.Models;
using SchoolNexAPI.Repositories.Abstract;
using SchoolNexAPI.Repositories.Concrete;
using SchoolNexAPI.Security;
using SchoolNexAPI.Services.Abstract;
using SchoolNexAPI.Services.Background;
using SchoolNexAPI.Services.Concrete;
using SchoolNexAPI.Utilities;
using SchoolNexAPI.Utilities.Helpers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
;
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolNexDB"));
});
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.AddIdentity<AppUserModel, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.AppAuthentication();

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
builder.Services.AddHostedService<RefreshTokenCleanupService>();
builder.Services.AddScoped<IAcademicYearService, AcademicYearService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowFrontendDev");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<SubscriptionValidationMiddleware>();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ISubscriptionTypeSeeder>();
    await seeder.SeedAsync();
}

app.ApplyMigration();


app.Run();

