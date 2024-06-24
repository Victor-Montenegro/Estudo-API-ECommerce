using Estudo.TRIMANIA.Domain.AutoMappers;
using Estudo.TRIMANIA.Domain.FluentValidators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Estudo.TRIMANIA.Domain
{
    public static class DomainDependency
    {
        public static IServiceCollection DomainInjection(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile).Assembly);

            services.AddValidatorsFromAssemblyContaining<SignUpValidator>();
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
