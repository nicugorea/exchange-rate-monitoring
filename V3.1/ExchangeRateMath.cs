namespace V3._1;

internal static class ExchangeRateMath
{
    public static ExchangeRate Average(ExchangeRate left, ExchangeRate right)
    {
        if (!left.Value.HasValue)
        {
            return right;
        }

        if (!right.Value.HasValue)
        {
            return left;
        }

        var averageValue = (left.Value + right.Value) / 2;

        return new ExchangeRate(averageValue);
    }
}