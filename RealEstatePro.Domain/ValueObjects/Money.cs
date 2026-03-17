using System;

namespace RealEstatePro.Domain;

public record Money(decimal Amount, string Currency)
{
    public static implicit operator decimal(Money money) => money.Amount;

    public bool IsPositive() => Amount > 0;

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot add money with different currencies: '{Currency}' vs '{other.Currency}'");

        return this with { Amount = Amount + other.Amount };
    }

    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot subtract money with different currencies: '{Currency}' vs '{other.Currency}'");

        return this with { Amount = Amount - other.Amount };
    }
}