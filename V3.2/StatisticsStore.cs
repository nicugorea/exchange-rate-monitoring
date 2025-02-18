using V3._2;

internal class StatisticsStore
{
    public ExchangeRate LowestExchangeRate { get; private set; } = new();
    public ExchangeRate HighestExchangeRate { get; private set; } = new();
    public ExchangeRate AverageExchangeRate { get; private set; } = new();
}