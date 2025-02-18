namespace V3._2;

internal class NotificationService
{
    private const int HighImportanceFrequency = 500;
    private const int LowImportanceFrequency = 100;
    private const int DurationInMs = 100;

    public void NotifyHighImportance()
    {
#pragma warning disable CA1416 This call site is reachable on all platforms. 'Console. Beep(int, int)' is only supported on: 'windows'.
        Console.Beep(HighImportanceFrequency, DurationInMs);
#pragma warning restore CA1416
    }

    public void NotifyLowImportance()
    {
#pragma warning disable CA1416 This call site is reachable on all platforms. 'Console. Beep(int, int)' is only supported on: 'windows'.
        Console.Beep(LowImportanceFrequency, DurationInMs);
#pragma warning restore CA1416
    }
}