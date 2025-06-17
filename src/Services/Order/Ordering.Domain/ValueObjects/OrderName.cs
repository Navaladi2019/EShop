namespace Ordering.Domain.ValueObjects;

public class OrderName
{
    private const int DefaultLength = 100;
    public string Value { get; }
    private OrderName(string value)=> Value = value;


    public static OrderName Of(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Order name cannot be empty.");
        }

        if (value.Length > DefaultLength)
        {
            throw new DomainException($"Order name cannot exceed {DefaultLength} characters.");
        }

        return new OrderName(value);
    }
}
