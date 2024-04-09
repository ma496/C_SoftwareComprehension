namespace Capture;

public class ValueReporter : IValueReporter
{
    public void Report(double value)
    {
        Console.WriteLine($"Reported value: {value}");
    }
}