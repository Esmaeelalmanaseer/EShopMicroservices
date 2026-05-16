using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    private const int defaultLength = 5;
    public string Value { get; set; }
    private OrderName (string value) => Value = value;
    public static OrderName Of(string value)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length,defaultLength);
        return new OrderName(value);
    }
}
