using Estudo.TRIMANIA.Application.Bridges;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Estudo.TRIMANIA.Application
{
    public static class ApplicationDependency
    {
        public static IServiceCollection ApplicationInjection(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddScoped<IStockBridge, StockBridge>();


            return services;
        }
    }
}
