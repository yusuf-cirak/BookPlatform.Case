using System.Reflection;
using BookPlatform.SharedKernel.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BookPlatform.SharedKernel.Extensions;

public static class LifetimeServicesExtensions
{
    public static void AddLifetimedServices(this IServiceCollection services, Assembly assembly)
    {
        Dictionary<string, ServiceLifetime> serviceLifeTimes = new()
        {
            { nameof(ITransientService), ServiceLifetime.Transient },
            { nameof(IScopedService), ServiceLifetime.Scoped },
            { nameof(ISingletonService), ServiceLifetime.Singleton }
        };

        // Get all types that implement ITransientService, IScopedService, ISingletonService


        List<Type> serviceRegisterTypes =
            [typeof(ITransientService), typeof(IScopedService), typeof(ISingletonService)];
        
        var allServices = assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i => serviceRegisterTypes.Contains(i)) && !t.IsAbstract &&
                        !t.IsInterface);
        

        foreach (var serviceType in allServices)
        {
            // Get all interfaces implemented by the concrete type, excluding ITransientService
            var @interface = serviceType.GetInterfaces().SingleOrDefault(i => !serviceRegisterTypes.Contains(i));

            var lifetime = serviceType.GetInterfaces().SingleOrDefault(i => serviceRegisterTypes.Contains(i));

            ArgumentNullException.ThrowIfNull(@interface, nameof(@interface));
            ArgumentNullException.ThrowIfNull(lifetime, nameof(lifetime));

            // Register the service with the interface and lifetime
            services.Add(new ServiceDescriptor(@interface, serviceType, serviceLifeTimes[lifetime.Name]));
        }
    }
}