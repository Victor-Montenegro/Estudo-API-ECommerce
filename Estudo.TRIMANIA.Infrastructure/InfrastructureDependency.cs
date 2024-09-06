using Estudo.TRIMANIA.Infrastructure.Database;
using Estudo.TRIMANIA.Infrastructure.Migrations;
using Estudo.TRIMANIA.Infrastructure.Repositories;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Data;

namespace Estudo.TRIMANIA.Infrastructure
{
    public static class InfrastructureDependency
    {
        public static IServiceCollection InfrastructureInjection(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDatabase(services,configuration);

            return services;
        }

        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            string? trimaniaConnectionString = configuration.GetConnectionString("Trimania");

            services.AddDbContext<TrimaniaContext>(options =>
            {
                options.UseSqlServer(trimaniaConnectionString).EnableSensitiveDataLogging();
            });

            services.AddFluentMigratorCore().
                ConfigureRunner(config =>
                    {
                        config.AddSqlServer()
                        .WithGlobalConnectionString(trimaniaConnectionString)
                        .ScanIn(typeof(Migration202406091119).Assembly).For.Migrations();
                    })
                .AddLogging(l => l.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

            services.AddScoped<IDbConnection>(config =>
            {
                var sqlConnection = new SqlConnection(trimaniaConnectionString);

                return sqlConnection;
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            ConfigureMongo(services, configuration);
        }

        private static void ConfigureMongo(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionStrings:MongoDb").Value;
            var mongoClient = new MongoClient(connectionString);

            services.AddSingleton(mongoClient);
        }
    }
}
