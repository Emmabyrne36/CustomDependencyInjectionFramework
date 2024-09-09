namespace Vax;

public class ServiceProvider : IServiceProvider
{
    private readonly Dictionary<Type, Func<object>> _transientTypes = new();
    private readonly Dictionary<Type, Lazy<object>> _singletonTypes = new();

    internal ServiceProvider(ServiceCollection serviceCollection)
    {
        GenerateServices(serviceCollection);
    }

    public T? GetService<T>()
    {
        return (T?)GetService(typeof(T));
    }

    public object? GetService(Type serviceType)
    {
        var service = _singletonTypes.GetValueOrDefault(serviceType);

        if (service is not null)
        {
            return service.Value;
        }

        var transientService = _transientTypes.GetValueOrDefault(serviceType);

        return transientService?.Invoke();
    }

    private void GenerateServices(ServiceCollection serviceCollection)
    {
        foreach (var serviceDescriptor in serviceCollection)
        {
            switch (serviceDescriptor.Lifetime)
            {
                case Enums.ServiceLifetime.Singleton:
                    _singletonTypes[serviceDescriptor.ServiceType] = new Lazy<object>(() =>
                        Activator.CreateInstance(serviceDescriptor.ImplementationType, GetConstructorParameters(serviceDescriptor))!);
                    continue;

                case Enums.ServiceLifetime.Transient:
                    _transientTypes[serviceDescriptor.ServiceType] = () =>
                       Activator.CreateInstance(serviceDescriptor.ImplementationType, GetConstructorParameters(serviceDescriptor))!;
                    continue;
            }

        }
    }

    private object?[] GetConstructorParameters(ServiceDescriptor serviceDescriptor)
    {
        var constructorInfo = serviceDescriptor.ImplementationType.GetConstructors().First();
        var parameters = constructorInfo.GetParameters()
            .Select(x => GetService(x.ParameterType)).ToArray();

        return parameters;
    }
}