using Estudo.TRIMANIA.Api.Middlewares;
using Estudo.TRIMANIA.Application;
using Estudo.TRIMANIA.Domain;
using Estudo.TRIMANIA.Infrastructure;
using FluentMigrator.Runner;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ServiceConfiguration(builder.Services, builder.Configuration);


var migrationRunner = builder.Services.BuildServiceProvider().GetRequiredService<IMigrationRunner>();
var app = builder.Build();

Configuration(app, migrationRunner);

static void ServiceConfiguration(IServiceCollection services, IConfiguration configuration)
{
    services.DomainInjection();
    services.ApplicationInjection();
    services.InfrastructureInjection(configuration);

    services.AddMvc();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "TRIMANIA", Version = "v1" });
    });

}

static void Configuration(WebApplication app, IMigrationRunner migrationRunner)
{
    migrationRunner.MigrateUp();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseMiddleware<CustomExceptionMiddleware>();
    app.UseMiddleware<CheckAuthorizationMiddleware>();

    app.MapControllers();
    app.UseHttpsRedirection();

    app.Run();
}
