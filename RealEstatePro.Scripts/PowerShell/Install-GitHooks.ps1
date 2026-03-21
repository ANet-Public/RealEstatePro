#
# Copyright (c) 2026 ANet-Public
# All rights reserved.
#

$ErrorActionPreference = 'Stop'

try {
    $repoRoot = (git rev-parse --show-toplevel).Trim()
}
catch {
    throw "Не удалось определить корень git-репозитория. Убедись, что скрипт запускается внутри репозитория."
}

if ([string]::IsNullOrWhiteSpace($repoRoot)) {
    throw "Git вернул пустой путь к корню репозитория."
}

$gitHooksDir = Join-Path $repoRoot ".git\hooks"

if (-not (Test-Path $gitHooksDir)) {
    throw ".git/hooks not found: $gitHooksDir"
}

$preCommitPath = Join-Path $gitHooksDir "pre-commit"
$preCommitScriptPath = Join-Path $repoRoot "RealEstatePro.Scripts\PowerShell\Update-VersionBeforeCommit.ps1"

if (-not (Test-Path $preCommitScriptPath)) {
    throw "Pre-commit script not found: $preCommitScriptPath"
}

$preCommitContent = @"
#!/bin/sh
powershell -NoProfile -ExecutionPolicy Bypass -File "$preCommitScriptPath"
RESULT=$?
if [ \$RESULT -ne 0 ]; then
  echo "pre-commit version hook failed"
  exit \$RESULT
fi
exit 0
"@

Set-Content -Path $preCommitPath -Value $preCommitContent -Encoding ASCII

Write-Host "Git pre-commit hook installed successfully:" -ForegroundColor Green
Write-Host "  $preCommitPath"
