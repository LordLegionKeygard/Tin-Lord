param(
  [Parameter(Mandatory=$true)][string]$InputPath,
  [Parameter(Mandatory=$true)][string]$TranslationsJson,
  [Parameter(Mandatory=$true)][int]$LangIndex,
  [Parameter(Mandatory=$true)][string]$OutputPath
)
$ErrorActionPreference = 'Stop'
$translations = Get-Content -Path $TranslationsJson -Raw -Encoding UTF8 | ConvertFrom-Json
$map = @{}
foreach ($t in $translations) {
  if ($null -ne $t.id) {
    $map[[int]$t.id] = $t
  }
}
$lines = Get-Content -Path $InputPath -Encoding UTF8
$out = New-Object System.Collections.Generic.List[string]
$ruPattern = '^\s*_text\[(\d+),\s*1\]\s*=\s*[$@]*"(.*)";\s*(//.*)?$'
$ruInterpolatedPattern = '^\s*_text\[(\d+),\s*1\]\s*=\s*\$"(.*)";\s*(//.*)?$'
$langPattern = '^\s*_text\[(\d+),\s*' + $LangIndex + '\]\s*=\s*.*;\s*(//.*)?$'

# Collect ids that already have target language lines.
$existingLangIds = New-Object 'System.Collections.Generic.HashSet[int]'
foreach ($line in $lines) {
  if ($line -match $langPattern) {
    $existingLangIds.Add([int]$Matches[1]) | Out-Null
  }
}

# Collect ids that use interpolated strings in RU.
$interpolatedIds = New-Object 'System.Collections.Generic.HashSet[int]'
foreach ($line in $lines) {
  if ($line -match $ruInterpolatedPattern) {
    $interpolatedIds.Add([int]$Matches[1]) | Out-Null
  }
}

$seenLangIds = New-Object 'System.Collections.Generic.HashSet[int]'
foreach ($line in $lines) {
  if ($line -match $langPattern) {
    $id = [int]$Matches[1]
    if ($seenLangIds.Contains($id)) {
      continue
    }
    $seenLangIds.Add($id) | Out-Null

    if ($map.ContainsKey($id)) {
      $t = $map[$id]
      $value = $null
      switch ($LangIndex) {
        2 { $value = $t.fr }
        3 { $value = $t.it }
        4 { $value = $t.de }
        5 { $value = $t.'es-ES' }
        6 { $value = $t.pl }
        7 { $value = $t.'pt-BR' }
        8 { $value = $t.ja }
        9 { $value = $t.'zh-Hans' }
      }
      if ($null -ne $value) {
        $indent = ($line -replace '^(\s*).*','$1')
        $value = $value -replace '\\\\\"', '\"'
        $escaped = $value -replace '\\', '\\\\'
        $escaped = $escaped -replace "`r`n", '\n'
        $escaped = $escaped -replace "`n", '\n'
        $escaped = $escaped -replace "`t", '\t'
        $escaped = $escaped -replace '"', '\\"'
        $prefix = if ($interpolatedIds.Contains($id)) { '$' } else { '' }
        $out.Add("${indent}_text[$id, $LangIndex] = ${prefix}`"$escaped`";") | Out-Null
        continue
      }
    }
    $out.Add($line) | Out-Null
    continue
  }

  $out.Add($line) | Out-Null
  if ($line -match $ruPattern) {
    $id = [int]$Matches[1]
    if ($map.ContainsKey($id) -and -not $existingLangIds.Contains($id)) {
      $t = $map[$id]
      $value = $null
      switch ($LangIndex) {
        2 { $value = $t.fr }
        3 { $value = $t.it }
        4 { $value = $t.de }
        5 { $value = $t.'es-ES' }
        6 { $value = $t.pl }
        7 { $value = $t.'pt-BR' }
        8 { $value = $t.ja }
        9 { $value = $t.'zh-Hans' }
      }
      if ($null -ne $value) {
        $indent = ($line -replace '^(\s*).*','$1')
        $value = $value -replace '\\\\\"', '\"'
        $escaped = $value -replace '\\', '\\\\'
        $escaped = $escaped -replace "`r`n", '\n'
        $escaped = $escaped -replace "`n", '\n'
        $escaped = $escaped -replace "`t", '\t'
        $escaped = $escaped -replace '"', '\\"'
        $prefix = if ($interpolatedIds.Contains($id)) { '$' } else { '' }
        $out.Add("${indent}_text[$id, $LangIndex] = ${prefix}`"$escaped`";") | Out-Null
      }
    }
  }
}
Set-Content -Path $OutputPath -Value $out -Encoding UTF8
