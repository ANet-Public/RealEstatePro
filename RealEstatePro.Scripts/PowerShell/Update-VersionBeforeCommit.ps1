#
# Copyright (c) 2026 ANet-Public
# All rights reserved.
#

$ErrorActionPreference = 'Stop'

try {
    $repoRoot = (git rev-parse --show-toplevel).Trim()
}
catch {
    throw "Не удалось определить корень git-репозитория."
}

if ([string]::IsNullOrWhiteSpace($repoRoot)) {
    throw "Git вернул пустой путь к корню репозитория."
}

$versionFile = Join-Path $repoRoot "version.txt"

if (-not (Test-Path $versionFile)) {
    throw "Файл version.txt не найден: $versionFile"
}

function Read-VersionFile {
    param([string]$Path)

    $data = [ordered]@{}

    foreach ($line in Get-Content $Path -Encoding UTF8) {
        $trimmed = $line.Trim()

        if ([string]::IsNullOrWhiteSpace($trimmed)) {
            continue
        }

        if ($trimmed.StartsWith("#")) {
            continue
        }

        $parts = $trimmed.Split("=", 2)
        if ($parts.Length -ne 2) {
            throw "Некорректная строка в version.txt: $trimmed"
        }

        $key = $parts[0].Trim()
        $value = $parts[1].Trim()

        $data[$key] = $value
    }

    return $data
}

function Write-VersionFile {
    param(
        [string]$Path,
        [hashtable]$Data
    )

    $lines = @(
        "AppVersion=$($Data['AppVersion'])"
        "WebApiVersion=$($Data['WebApiVersion'])"
        "TelegramApiVersion=$($Data['TelegramApiVersion'])"
    )

    Set-Content -Path $Path -Value $lines -Encoding UTF8
}

$data = Read-VersionFile -Path $versionFile

foreach ($requiredKey in @('AppVersion', 'WebApiVersion', 'TelegramApiVersion')) {
    if (-not $data.Contains($requiredKey)) {
        throw "В version.txt отсутствует ключ: $requiredKey"
    }
}

$appVersion = $data['AppVersion']
$parts = $appVersion.Split('.')

if ($parts.Length -ne 4) {
    throw "AppVersion должен быть в формате YY.Global.Internal.CommitCount. Текущее значение: $appVersion"
}

$yearPart = [int]$parts[0]
$globalChanges = [int]$parts[1]
$internalReleases = [int]$parts[2]
$commitCount = [int]$parts[3]

$currentYearPart = [int](Get-Date -Format "yy")

if ($yearPart -ne $currentYearPart) {
    $yearPart = $currentYearPart
    $commitCount = 0
}

$commitCount++

$data['AppVersion'] = "$yearPart.$globalChanges.$internalReleases.$commitCount"

Write-VersionFile -Path $versionFile -Data $data

git add -- $versionFile

Write-Host "version.txt обновлён: $($data['AppVersion'])" -ForegroundColor Green
