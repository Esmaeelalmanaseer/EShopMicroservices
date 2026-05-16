using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;

public class Customer : Entity<CustomerId>
{
    public string Email { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public static Customer Create(CustomerId customerId ,string email, string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        return new Customer
        {
            Id = customerId,
            Email = email,
            Name = name,
        };
    }
}
