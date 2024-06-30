using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Proji.Domain.Validators;
using System.Reflection;


namespace Proji.Domain.Validation
{
    public static class ValidationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IValidator<>), typeof(GenericValidator<>));
            return services;
        }
    }
}
