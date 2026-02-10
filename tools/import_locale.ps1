param(
    [Parameter(Mandatory = $true)]
    [string]$JsonPath,
    [Parameter(Mandatory = $true)]
    [int]$LanguageIndex,
    [string]$LanguageCsPath = "e:\LegionProjects\Tin-Lord\Assets\Scripts\Static\Language.cs"
)

if (-not (Test-Path $JsonPath)) { throw "JSON not found: $JsonPath" }
if (-not (Test-Path $LanguageCsPath)) { throw "Language.cs not found: $LanguageCsPath" }
if ($LanguageIndex -lt 0 -or $LanguageIndex -gt 9) { throw "LanguageIndex must be 0..9" }

# Read JSON as UTF-8 to preserve accents.
$jsonText = Get-Content -Path $JsonPath -Raw -Encoding UTF8
$data = $jsonText | ConvertFrom-Json

# Build map of index -> string
$map = @{}
$data.PSObject.Properties | ForEach-Object {
    $map[[int]$_.Name] = [string]$_.Value
}

$lines = Get-Content -Path $LanguageCsPath -Encoding UTF8
$updated = 0
$missing = @()

$pattern = '_text\[(\d+),\s*{0}\]\s*=\s*"((?:[^"\\]|\\.)*)"\s*;' -f $LanguageIndex

# Replace _text[index, LanguageIndex] lines with values
for ($i = 0; $i -lt $lines.Length; $i++) {
    if ($lines[$i] -match $pattern) {
        $idx = [int]$Matches[1]
        if ($map.ContainsKey($idx)) {
            $val = $map[$idx]
            $escaped = $val.Replace('\', '\\').Replace('"', '\"')
            $lines[$i] = '        _text[{0}, {1}] = "{2}";' -f $idx, $LanguageIndex, $escaped
            $updated++
        }
    }
}

# Collect missing indices (present in JSON but no line found)
foreach ($k in $map.Keys) {
    $pattern2 = '_text\[{0},\s*{1}\]' -f $k, $LanguageIndex
    if (-not ($lines -match $pattern2)) { $missing += $k }
}

Set-Content -Path $LanguageCsPath -Value $lines -Encoding UTF8

Write-Host "Updated: $updated"
if ($missing.Count -gt 0) {
    $missing = $missing | Sort-Object
    Write-Host "Missing indices in Language.cs: $($missing -join ', ')"
}
