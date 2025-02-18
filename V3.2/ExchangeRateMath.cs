namespace V3._2;

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

    public static ExchangeRate Min(ExchangeRate left, ExchangeRate right)
    {
        if (!left.Value.HasValue)
        {
            return right;
        }

        if (!right.Value.HasValue)
        {
            return left;
        }

        return left < right ? left : right;
    }

    public static ExchangeRate Max(ExchangeRate left, ExchangeRate right)
    {
        if (!left.Value.HasValue)
        {
            return right;
        }

        if (!right.Value.HasValue)
        {
            return left;
        }

        return left > right ? left : right;
    }
}