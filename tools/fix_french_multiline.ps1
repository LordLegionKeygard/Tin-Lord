param(
    [string]$LanguageCsPath = "e:\LegionProjects\Tin-Lord\Assets\Scripts\Static\Language.cs",
    [int]$LanguageIndex = 2
)

if (-not (Test-Path $LanguageCsPath)) { throw "Language.cs not found: $LanguageCsPath" }

$raw = Get-Content -Path $LanguageCsPath -Raw -Encoding UTF8

$pattern = '(?ms)^(\s*_text\[(\d+),\s*' + $LanguageIndex + '\]\s*=\s*")((?:[^"\\]|\\.|\r?\n)*)"\s*;'

$script:updated = 0

$raw2 = [regex]::Replace($raw, $pattern, {
    param($m)
    $prefix = $m.Groups[1].Value
    $val = $m.Groups[3].Value
    if ($val -match "\r?\n") {
        # Remove actual line breaks and indentation introduced by broken strings
        $val = $val -replace "\r?\n\s*", ""
        $script:updated++
    }
    return ($prefix + $val + '";')
})

if ($script:updated -gt 0) {
    Set-Content -Path $LanguageCsPath -Value $raw2 -Encoding UTF8
}

Write-Host "Updated: $script:updated"
