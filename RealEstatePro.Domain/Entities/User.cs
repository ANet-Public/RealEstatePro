using System;

namespace RealEstatePro.Domain;

/// Represents a user of the platform: owner, buyer, or renter.

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string FullName { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User() { }


    /// Creates a new user with validation.

    public static User Create(string email, string fullName)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        if (email.Length > 254)
            throw new ArgumentException("Email is too long.", nameof(email));

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required.", nameof(fullName));

        if (fullName.Length > 100)
            throw new ArgumentException("Full name must be at most 100 characters.", nameof(fullName));

        return new User
        {
            Id = Guid.NewGuid(),
            Email = email.Trim().ToLowerInvariant(),
            FullName = fullName.Trim(),
            CreatedAt = DateTime.UtcNow
        };
    }
}