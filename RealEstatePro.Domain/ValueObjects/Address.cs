using System;

namespace RealEstatePro.Domain;

public record Address(string Street, string City, string PostalCode, string Country)
{
    public Address Normalize() => this with
    {
        Street = Street?.Trim() ?? string.Empty, //to avoid mistakes NullReferenceException
        City = City?.Trim() ?? string.Empty,
        PostalCode = Postal?.Trim() ?? string.Empty,
        Country = Country?.Trim() ?? string.Empty
    };

    public override string ToString() =>
        $"{Street}, {City}, {PostalCode}, {Country}".TrimEnd(','); //an example 
}