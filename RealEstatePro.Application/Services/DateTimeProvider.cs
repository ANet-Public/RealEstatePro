/*
    Copyright (c) 2026 ANet-Public
    All rights reserved.
 */

using RealEstatePro.Application.Abstractions;

namespace RealEstatePro.Application.Services
{
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
