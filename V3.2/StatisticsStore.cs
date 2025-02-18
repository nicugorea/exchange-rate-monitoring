namespace V3._2;

internal class StatisticsStore
{
    public ExchangeRate LowestExchangeRate { get; private set; }
    public ExchangeRate HighestExchangeRate { get; private set; }
    public ExchangeRate AverageExchangeRate { get; private set; }

    public void Update(ExchangeRate exchangeRate)
    {
        LowestExchangeRate = ExchangeRateMath.Min(LowestExchangeRate, exchangeRate);
        HighestExchangeRate = ExchangeRateMath.Max(HighestExchangeRate, exchangeRate);
        AverageExchangeRate = ExchangeRateMath.Average(AverageExchangeRate, exchangeRate);
    }
}