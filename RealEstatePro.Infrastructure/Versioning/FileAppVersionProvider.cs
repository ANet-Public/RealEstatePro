/*
    Copyright (c) 2026 ANet-Public
    All rights reserved.
 */

using Microsoft.Extensions.Hosting;
using RealEstatePro.Application.Abstractions.Versioning;

namespace RealEstatePro.Infrastructure.Versioning
{
    internal sealed class FileAppVersionProvider : IAppVersionProvider
    {
        private readonly Lazy<AppVersionInfo> _versionInfo;

        public FileAppVersionProvider(IHostEnvironment environment)
        {
            if (environment == null) throw new ArgumentNullException(nameof(environment));
            var versionFilePath = Path.Combine(environment.ContentRootPath, "version.txt");
            _versionInfo = new Lazy<AppVersionInfo>(() => Load(versionFilePath));
        }

        public AppVersionInfo Get()
        {
            return _versionInfo.Value;
        }

        private static AppVersionInfo Load(string versionFilePath)
        {
            if (!File.Exists(versionFilePath))
            {
                throw new FileNotFoundException("File version.txt can not found", versionFilePath);
            }

            var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var rawLine in File.ReadAllLines(versionFilePath))
            {
                var line = rawLine.Trim();

                if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
                {
                    continue;
                }

                var parts = line.Split('=', 2, StringSplitOptions.TrimEntries);
                if (parts.Length != 2)
                {
                    throw new InvalidOperationException($"Incorrect row in version.txt: '{line}'");
                }

                values[parts[0]] = parts[1];
            }

            var appVersionFull = GetRequired(values, "AppVersion");
            var webApiVersion = GetRequired(values, "WebApiVersion");
            var telegramApiVersion = GetRequired(values, "TelegramApiVersion");

            var versionParts = appVersionFull.Split('.');
            if (versionParts.Length != 4)
            {
                throw new InvalidOperationException(
                    $"Incorrect 'AppVersion' value (YY.Global.Internal.CommitCount). Current value: '{appVersionFull}'");
            }

            var applicationVersionPublic = $"{versionParts[0]}.{versionParts[1]}.{versionParts[2]}";

            return new AppVersionInfo(
                ApplicationVersionFull: appVersionFull,
                ApplicationVersionPublic: applicationVersionPublic,
                WebApiVersion: webApiVersion,
                TelegramApiVersion: telegramApiVersion
            );
        }

        private static string GetRequired(IDictionary<string, string> values, string key)
        {
            if (!values.TryGetValue(key, out var value) || string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException($"Can't find '{key}' key in 'version.txt'");
            }

            return value;
        }
    }
}
