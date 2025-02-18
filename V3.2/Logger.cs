internal class Logger
{
    public void LogError(string message)
    {
        Console.Error.WriteLine(message);
    }

    public void LogInformation(string message)
    {
        Console.Out.WriteLine(message);
    }
}