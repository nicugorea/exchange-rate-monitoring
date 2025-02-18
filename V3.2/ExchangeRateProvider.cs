using System.Net.Http.Json;

namespace V3._2;

internal class ExchangeRateProvider
{
    public async Task<ExchangeRate> GetAsync(string? uriArg, Logger logger)
    {
        if (!Uri.TryCreate(uriArg, UriKind.RelativeOrAbsolute, out var uri))
        {
            logger.LogError("The API endpoint URL is not valid.");
            return new ExchangeRate();
        }
        
        using var httpClient = new HttpClient();
        using var response = await httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            logger.LogError($"The response status code ({response.StatusCode}) does not indicate success.");
            return new ExchangeRate();
        }

        var currentExchangeRate = await response.Content.ReadFromJsonAsync<ExchangeRate>();

        if (!currentExchangeRate.HasValue)
        {
            logger.LogError("Current exchange rate could not be retrieved.");
        }

        return currentExchangeRate;
    }
}