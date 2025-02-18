using System.Net.Http.Json;

double? lowestExchangeRate = null;
double? highestExchangeRate = null;
double? averageExchangeRate = null;

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
        var deserializedResponse = await response.Content.ReadFromJsonAsync<dynamic>();
        double? currentExchangeRate = deserializedResponse?.Value;

        if (!currentExchangeRate.HasValue)
        {
            Console.Out.WriteLine("Current exchange rate could not be retrieved.");
            continue;
        }

        Console.Out.WriteLine($"Current exchange rate is: {deserializedResponse?.Value}.");
        
        var lowestExchangeRateText = lowestExchangeRate.HasValue ? lowestExchangeRate.ToString() : "unknown";
        Console.Out.WriteLine($"Lowest exchange rate is: {lowestExchangeRateText}.");
        
        var highestExchangeRateText = highestExchangeRate.HasValue ? highestExchangeRate.ToString() : "unknown";
        Console.Out.WriteLine($"Highest exchange rate is: {highestExchangeRateText}.");
        
        var averageExchangeRateText = averageExchangeRate.HasValue ? averageExchangeRate.ToString() : "unknown";
        Console.Out.WriteLine($"Average exchange rate is: {averageExchangeRateText}.");
        
        if (!lowestExchangeRate.HasValue || currentExchangeRate < lowestExchangeRate)
        {
            lowestExchangeRate = currentExchangeRate;
        }
        
        if (!highestExchangeRate.HasValue || currentExchangeRate > highestExchangeRate)
        {
            highestExchangeRate = currentExchangeRate;
        }
        
        if (!averageExchangeRate.HasValue)
        {
            averageExchangeRate = currentExchangeRate;
        }
        else
        {
            averageExchangeRate = (averageExchangeRate + currentExchangeRate) / 2;
        }
    }
    catch (Exception exception)
    {
        Console.Error.WriteLine($"An error occured while deserializing the response content: {exception}");
        continue;
    }

    await Task.Delay(TimeSpan.FromMinutes(5));
}