using Vax.Enums;

namespace Vax;

public class ServiceCollection : List<ServiceDescriptor>
{
    public ServiceCollection AddService(ServiceDescriptor serviceDescriptor)
    {
        base.Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddSingleton<TService>(Func<ServiceProvider, TService> factory)
        where TService : class
    {
        var serviceDescriptor = new ServiceDescriptor
        {
            ServiceType = typeof(TService),
            ImplementationType = typeof(TService),
            ImplementationFactory = factory,
            Lifetime = ServiceLifetime.Singleton,
        };

        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddSingleton(object implementation)
    {
        var serviceType = implementation.GetType();

        var serviceDescriptor = new ServiceDescriptor
        {
            ServiceType = serviceType,
            ImplementationType = serviceType,
            Implementation = implementation,
            Lifetime = ServiceLifetime.Singleton,
        };

        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddSingleton<TService>()
        where TService : class
    {
        ServiceDescriptor serviceDescriptor = CreateServiceDescriptorWithLifetime<TService, TService>(ServiceLifetime.Singleton);

        base.Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddSingleton<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        ServiceDescriptor serviceDescriptor = CreateServiceDescriptorWithLifetime<TService, TImplementation>(ServiceLifetime.Singleton);

        base.Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddTransient<TService>(Func<ServiceProvider, TService> factory)
        where TService : class
    {
        var serviceDescriptor = new ServiceDescriptor
        {
            ServiceType = typeof(TService),
            ImplementationType = typeof(TService),
            ImplementationFactory = factory,
            Lifetime = ServiceLifetime.Transient,
        };

        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddTransient<TService>()
        where TService : class
    {
        ServiceDescriptor serviceDescriptor = CreateServiceDescriptorWithLifetime<TService, TService>(ServiceLifetime.Transient);

        base.Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddTransient<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
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
