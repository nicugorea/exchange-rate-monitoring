using System.Net.Http.Json;


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
        Console.Out.WriteLine($"Current exchange rate is: {deserializedResponse?.Value}.");
    }
    catch (Exception exception)
    {
        Console.Error.WriteLine($"An error occured while deserializing the response content: {exception}");
        continue;
    }

    await Task.Delay(TimeSpan.FromMinutes(5));
}