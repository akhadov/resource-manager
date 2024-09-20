using Asp.Versioning;
using Asp.Versioning.Builder;
using ResourceManager.Api.Extensions;
using ResourceManager.Application;
using ResourceManager.Infrastructure;
using Serilog;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);


builder.Services.AddCors(options =>
{
    options.AddPolicy("blazorApp",
        builder =>
        {
            builder.WithOrigins("https://localhost:5001/api/v1/", "https://localhost:7206")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});


builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());


WebApplication app = builder.Build();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

app.MapEndpoints(versionedGroup);

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseCors("blazorApp");

//app.UseAuthentication();

//app.UseAuthorization();

// REMARK: If you want to use Controllers, you'll need this.
app.MapControllers();

app.Run();

// REMARK: Required for functional and integration tests to work.
public partial class Program;