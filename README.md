# Bal Bookmark
Bal Bookmark is a simple PowerShell module that helps to bookmark file system locations.

## Marking a location
```powershell
Add-BalBookmark -Alias "dev" -Path "C:\repos\dev"
```
or you can use the alias to store the current location
```powershell
cd C:\repos\dev
mark dev
```

## Getting all bookmarks
```powershell
Get-BalBookmark

Alias Path
----- ----
dev   C:\repos\dev
```

## Setting location to a bookmark
```powershell
# Example 1:
goto dev

# Example 2:
Set-BalLocation -Alias dev
```

## Removing bookmakrs
```powershell
# Removing single bookmark
# Example 1:
"dev" | Remove-BalBookmark

#Example 2:
Remove-BalBookmark -Alias "dev"

# Removing all bookmarks
Get-BlaBookmark | Remove-BalBookmark
```

## Saving Remote Sessions
COMING SOON
