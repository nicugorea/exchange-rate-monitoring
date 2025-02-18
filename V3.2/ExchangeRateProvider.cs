using System.Net.Http.Json;
using V3._2;

internal class ExchangeRateProvider
{
    public async Task<ExchangeRate> GetAsync(string uri)
    {
        using var httpClient = new HttpClient();
        using var response = await httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            await Console.Error.WriteLineAsync($"The response status code ({response.StatusCode}) does not indicate success.");
            return new ExchangeRate();
        }

        var currentExchangeRate = await response.Content.ReadFromJsonAsync<ExchangeRate>();

        if (!currentExchangeRate.HasValue)
        {
            await Console.Out.WriteLineAsync("Current exchange rate could not be retrieved.");
        }

        return currentExchangeRate;
    }
}