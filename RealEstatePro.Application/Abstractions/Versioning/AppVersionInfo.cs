/*
    Copyright (c) 2026 ANet-Public
    All rights reserved.
 */

namespace RealEstatePro.Application.Abstractions.Versioning
{
    public sealed record AppVersionInfo(
        string ApplicationVersionFull,
        string ApplicationVersionPublic,
        string WebApiVersion,
        string TelegramApiVersion
    );
}
