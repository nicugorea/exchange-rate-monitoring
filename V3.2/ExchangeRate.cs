using System.Globalization;

namespace V3._2;

internal readonly record struct ExchangeRate(double? Value = null)
{
    private const string FallbackValue = "unknown";

    public override string ToString() => Value.HasValue ? Value.Value.ToString(CultureInfo.InvariantCulture) : FallbackValue;

    public static bool operator <(ExchangeRate left, ExchangeRate right) => left.Value < right.Value;
    public static bool operator >(ExchangeRate left, ExchangeRate right) => left.Value > right.Value;

    public bool HasValue => Value.HasValue;
}