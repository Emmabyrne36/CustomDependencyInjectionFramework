using Consumer.ConsoleApp.Interfaces;

namespace Consumer.ConsoleApp;

// This class is used so we can make it injectable and testable
public class ConsoleWriter : IConsoleWriter
{
    public void WriteLine(string text)
    {
        Console.WriteLine(text);
    }
}
