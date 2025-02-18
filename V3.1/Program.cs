using System.Net.Http.Json;
using V3._1;

var lowestExchangeRate = new ExchangeRate(null);
var highestExchangeRate = new ExchangeRate(null);
var averageExchangeRate = new ExchangeRate(null);

if (args.Length < 1)
{
    Console.Error.WriteLine("The API endpoint URL is not provided.");
    return;
}

if (!Uri.TryCreate(args[0], UriKind.RelativeOrAbsolute, out var uri))
{
    Console.Error.WriteLine("The API endpoint URL is not valid.");
    return;
}

while (true)
{
    using var httpClient = new HttpClient();
    using var response = await httpClient.GetAsync(uri);
    if (!response.IsSuccessStatusCode)
    {
        Console.Error.WriteLine($"The response status code ({response.StatusCode}) does not indicate success.");
        continue;
    }

    try
    {
        var currentExchangeRate = await response.Content.ReadFromJsonAsync<ExchangeRate>();

        if (!currentExchangeRate.HasValue)
        {
            Console.Out.WriteLine("Current exchange rate could not be retrieved.");
            continue;
        }

        if (currentExchangeRate < lowestExchangeRate)
        {
#pragma warning disable CA1416 This call site is reachable on all platforms. 'Console. Beep(int, int)' is only supported on: 'windows'.
            Console.Beep(500, 100);
#pragma warning restore CA1416
        }
        else if (currentExchangeRate < averageExchangeRate)
        {
#pragma warning disable CA1416 This call site is reachable on all platforms. 'Console. Beep(int, int)' is only supported on: 'windows'.
            Console.Beep(100, 100);
#pragma warning restore CA1416
        }

        Console.Out.WriteLine($"Current exchange rate is: {currentExchangeRate}.");
        Console.Out.WriteLine($"Lowest exchange rate is: {lowestExchangeRate}.");
        Console.Out.WriteLine($"Highest exchange rate is: {highestExchangeRate}.");
        Console.Out.WriteLine($"Average exchange rate is: {averageExchangeRate}.");

        lowestExchangeRate = ExchangeRateMath.Min(currentExchangeRate, lowestExchangeRate);
        highestExchangeRate = ExchangeRateMath.Max(currentExchangeRate, highestExchangeRate);
        averageExchangeRate = ExchangeRateMath.Average(currentExchangeRate, averageExchangeRate);
    }
    catch (Exception exception)
    {
        Console.Error.WriteLine($"An error occured while deserializing the response content: {exception}");
        continue;
    }

    await Task.Delay(TimeSpan.FromMinutes(5));
}
