using SchoolNexAPI.Data;
using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Extensions;
using SchoolNexAPI.Models;
using Microsoft.AspNetCore.Identity;
using SchoolNexAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using SchoolNexAPI.Services.Abstract;
using SchoolNexAPI.Services.Concrete;
using SchoolNexAPI.Repositories.Abstract;
using SchoolNexAPI.Repositories.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    // Creating a new authorization policy that requires users to be authenticated
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

    // Adding a filter to enforce the authorization policy to all controllers
    options.Filters.Add(new AuthorizeFilter(policy));
});
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
builder.Services.AddTransient<EmailSender>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.ApplyMigration();


app.Run();

