param(
  [Parameter(Mandatory=$true)][string]$InputPath,
  [Parameter(Mandatory=$true)][string]$OutputPath
)
$ErrorActionPreference = 'Stop'
$pattern = '^\s*_text\[(\d+),\s*1\]\s*=\s*[$@]*"(.*)";\s*(//.*)?$'
$lines = Get-Content -Path $InputPath -Encoding UTF8
$out = New-Object System.Collections.Generic.List[object]
foreach ($line in $lines) {
  if ($line -match $pattern) {
    $id = [int]$Matches[1]
    $ru = $Matches[2]
    $out.Add([pscustomobject]@{ id = $id; ru = $ru }) | Out-Null
  }
}
$out | ConvertTo-Json -Depth 3 | Set-Content -Path $OutputPath -Encoding UTF8
