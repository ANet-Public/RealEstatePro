using System;

namespace RealEstatePro.Domain;
/// Represents a real estate listing (for sale or rent).

public class Listing
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } //name
    public string Description { get; private set; } 
    public Money Price { get; private set; }
    public PropertyType PropertyType { get; private set; }
    public Address Address { get; private set; }
    public ListingStatus Status { get; private set; }
    public Guid OwnerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? PublishedAt { get; private set; }

    private Listing() { }

    /// Creates a new listing in Draft status.

    public static Listing Create(
        string title,
        string description,
        Money price,
        PropertyType propertyType,
        Address address,
        Guid ownerId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required.", nameof(title));

        if (title.Length > 100)
            throw new ArgumentException("Title must be at most 100 characters.", nameof(title));

        if (price == null)
            throw new ArgumentNullException(nameof(price));

        if (!price.IsPositive())
            throw new ArgumentException("Price must be positive.", nameof(price));

        if (address == null)
            throw new ArgumentNullException(nameof(address));

        if (ownerId == Guid.Empty)
            throw new ArgumentException("Owner ID cannot be empty.", nameof(ownerId));

        return new Listing
        {
            Id = Guid.NewGuid(),
            Title = title.Trim(),
            Description = description?.Trim() ?? string.Empty,
            Price = price,
            PropertyType = propertyType,
            Address = address.Normalize(),
            Status = ListingStatus.Draft,
            OwnerId = ownerId,
            CreatedAt = DateTime.UtcNow
        };
    }

}