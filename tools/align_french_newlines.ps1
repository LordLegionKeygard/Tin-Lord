param(
    [string]$LanguageCsPath = "e:\LegionProjects\Tin-Lord\Assets\Scripts\Static\Language.cs",
    [int]$SourceIndex = 1,
    [int]$TargetIndex = 2
)

if (-not (Test-Path $LanguageCsPath)) { throw "Language.cs not found: $LanguageCsPath" }

$lines = Get-Content -Path $LanguageCsPath -Encoding UTF8

$srcPattern = '_text\[(\d+),\s*{0}\]\s*=\s*(\$?)"((?:[^"\\]|\\.)*)"\s*;' -f $SourceIndex
$tgtPattern = '_text\[(\d+),\s*{0}\]\s*=\s*(\$?)"((?:[^"\\]|\\.)*)"\s*;' -f $TargetIndex

$srcMap = @{}
$tgtMap = @{}
$tgtPrefix = @{}

for ($i = 0; $i -lt $lines.Length; $i++) {
    if ($lines[$i] -match $srcPattern) {
        $idx = [int]$Matches[1]
        $val = [regex]::Unescape($Matches[3])
        $srcMap[$idx] = $val
    }
    if ($lines[$i] -match $tgtPattern) {
        $idx = [int]$Matches[1]
        $val = [regex]::Unescape($Matches[3])
        $tgtMap[$idx] = $val
        $tgtPrefix[$idx] = $Matches[2]
    }
}

function Split-Words([string]$s) {
    if ([string]::IsNullOrWhiteSpace($s)) { return @() }
    return ($s -split '\s+')
}

function Escape-CSharpString([string]$s) {
    $sb = New-Object System.Text.StringBuilder
    foreach ($ch in $s.ToCharArray()) {
        switch ($ch) {
            "`r" { }
            "`n" { [void]$sb.Append("\\n") }
            '"' { [void]$sb.Append('\"') }
            '\\' { [void]$sb.Append('\\\\') }
            default { [void]$sb.Append($ch) }
        }
    }
    return $sb.ToString()
}

$updated = 0

for ($i = 0; $i -lt $lines.Length; $i++) {
    if ($lines[$i] -match $tgtPattern) {
        $idx = [int]$Matches[1]
        if ($srcMap.ContainsKey($idx) -and $tgtMap.ContainsKey($idx)) {
            $src = $srcMap[$idx]
            if ($src -match "`n") {
                $srcLines = $src -split "`n"
                $srcWordCounts = @()
                foreach ($sl in $srcLines) { $srcWordCounts += (Split-Words $sl).Count }

                $tgtWords = Split-Words $tgtMap[$idx]
                if ($tgtWords.Count -gt 0) {
                    $pos = 0
                    $newLines = @()
                    for ($j = 0; $j -lt $srcWordCounts.Count; $j++) {
                        $count = $srcWordCounts[$j]
                        if ($count -le 0) {
                            $newLines += ""
                            continue
                        }
                        $take = [Math]::Min($count, $tgtWords.Count - $pos)
                        if ($take -le 0) { break }
                        $newLines += ($tgtWords[$pos..($pos + $take - 1)] -join ' ')
                        $pos += $take
                    }
                    if ($pos -lt $tgtWords.Count) {
                        $newLines += ($tgtWords[$pos..($tgtWords.Count - 1)] -join ' ')
                    }

                    $newVal = ($newLines -join "`n")
                    $escaped = Escape-CSharpString $newVal
                    $prefix = ''
                    if ($tgtPrefix.ContainsKey($idx)) { $prefix = $tgtPrefix[$idx] }
                    $lines[$i] = '        _text[{0}, {1}] = {2}"{3}";' -f $idx, $TargetIndex, $prefix, $escaped
                    $updated++
                }
            }
        }
    }
}

Set-Content -Path $LanguageCsPath -Value $lines -Encoding UTF8
Write-Host "Updated: $updated"
