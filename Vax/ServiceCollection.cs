using Vax.Enums;

namespace Vax;

public class ServiceCollection : List<ServiceDescriptor>
{
    public ServiceCollection AddSingleton<TService, TImplementation>()
    {
        ServiceDescriptor serviceDescriptor = CreateServiceDescriptorWithLifetime<TService, TImplementation>(ServiceLifetime.Singleton);

        base.Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddTransient<TService, TImplementation>()
    {
        ServiceDescriptor serviceDescriptor = CreateServiceDescriptorWithLifetime<TService, TImplementation>(ServiceLifetime.Transient);

        base.Add(serviceDescriptor);
        return this;
    }

    public ServiceProvider BuildServiceProvider()
    {
        return new ServiceProvider(this);
    }

    private static ServiceDescriptor CreateServiceDescriptorWithLifetime<TService, TImplementation>(ServiceLifetime lifetime)
    {
        return new ServiceDescriptor
        {
            ServiceType = typeof(TService),
            ImplementationType = typeof(TImplementation),
            Lifetime = lifetime,
        };
    }
}
