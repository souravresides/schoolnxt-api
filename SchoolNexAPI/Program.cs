using SchoolNexAPI.Data;
using SchoolNexAPI.Extensions;
using SchoolNexAPI.Middleware;
using SchoolNexAPI.Services.Background;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
});
builder.Logging.ClearProviders();
builder.Logging.AddApplicationInsights();

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.AppAuthentication();

builder.DependencyInjection();


builder.Services.AddHostedService<RefreshTokenCleanupService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowFrontendDev");

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
app.UseMiddleware<TenantMiddleware>();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ISubscriptionTypeSeeder>();
    await seeder.SeedAsync();
}

app.ApplyMigration();

app.Run();

