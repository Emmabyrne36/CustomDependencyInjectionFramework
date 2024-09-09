using Consumer.ConsoleApp.Interfaces;

namespace Consumer.ConsoleApp;

public class IdGenerator : IIdGenerator
{
    public Guid Id { get; } = Guid.NewGuid();
}
