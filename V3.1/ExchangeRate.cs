using System.Globalization;

namespace V3._1;

internal readonly record struct ExchangeRateStatistic(double? Value)
{
    private const string FallbackValue = "unknown";

    public override string ToString()
    {
        if (Value.HasValue)
        {
            return Value.Value.ToString(CultureInfo.InvariantCulture);
        }

        return FallbackValue;
    }

    public static bool operator <(ExchangeRateStatistic left, ExchangeRateStatistic right) => left.Value < right.Value;
    public static bool operator >(ExchangeRateStatistic left, ExchangeRateStatistic right) => left.Value < right.Value;
}