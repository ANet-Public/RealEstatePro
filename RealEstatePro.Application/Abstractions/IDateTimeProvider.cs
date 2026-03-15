/*
    Copyright (c) 2026 ANet-Public
    All rights reserved.
 */

namespace RealEstatePro.Application.Abstractions
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
