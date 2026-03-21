#
# Copyright (c) 2026 ANet-Public
# All rights reserved.
#

$ErrorActionPreference = 'Stop'

try {
    $repoRoot = (git rev-parse --show-toplevel).Trim()
}
catch {
    throw "Failed to resolve git repository root. Make sure the script is executed inside a git repository."
}

if ([string]::IsNullOrWhiteSpace($repoRoot)) {
    throw "Git returned an empty repository root path."
}

$gitHooksDir = Join-Path $repoRoot ".git\hooks"

if (-not (Test-Path $gitHooksDir)) {
    throw "Git hooks directory was not found: $gitHooksDir"
}

$preCommitPath = Join-Path $gitHooksDir "pre-commit"
$preCommitScriptPath = Join-Path $repoRoot "RealEstatePro.Scripts\PowerShell\Update-VersionBeforeCommit.ps1"

if (-not (Test-Path $preCommitScriptPath)) {
    throw "Pre-commit script was not found: $preCommitScriptPath"
}

$hookContent = @"
#!/bin/sh
"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe" -NoProfile -ExecutionPolicy Bypass -File "$preCommitScriptPath"
RESULT=`$?
if [ `$RESULT -ne 0 ]; then
  echo "pre-commit version hook failed"
  exit `$RESULT
fi
exit 0
"@

$utf8NoBom = New-Object System.Text.UTF8Encoding($false)
$normalizedHookContent = $hookContent -replace "`r`n", "`n"
[System.IO.File]::WriteAllText($preCommitPath, $normalizedHookContent, $utf8NoBom)

Write-Host "Git pre-commit hook installed successfully:" -ForegroundColor Green
Write-Host "  $preCommitPath" -ForegroundColor Green
Write-Host "Using script:" -ForegroundColor Green
Write-Host "  $preCommitScriptPath" -ForegroundColor Green
exit 0
