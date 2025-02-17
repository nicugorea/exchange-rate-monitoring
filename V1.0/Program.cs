using System.Net.Http.Json;

while (true)
{
    using var httpClient = new HttpClient();
    using var response = await httpClient.GetAsync(args[0]);
    var deserializedResponse = await response.Content.ReadFromJsonAsync<dynamic>();
    Console.WriteLine($"Current exchange rate is: {deserializedResponse?.Value}.");
    await Task.Delay(TimeSpan.FromMinutes(5));
}