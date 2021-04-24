[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [string] $UserName,

    [Parameter(Mandatory = $true)]
    [string] $DualAuthCode,

    [Parameter(Mandatory = $true)]
    [string] $WatchName,

    [Parameter(Mandatory = $true)]
    [string] $ShortDescription,

    [Parameter(Mandatory = $true)]
    [string] $LongDescription,

    [Parameter(Mandatory = $true)]
    [string] $Cost
)

$PostData = @{
    UserName = $UserName
    DualAuthCode = $DualAuthCode
    WatchName = $WatchName
    ShortDescription = $ShortDescription
    LongDescription = $LongDescription
    Cost = $Cost
}

write-output ($PostData | ConvertTo-Json)

Invoke-WebRequest -Uri "https://appelbreyer.azurewebsites.net/api/Info/NewWatch" -Method Post -Body ($PostData | ConvertTo-Json) -ContentType "application/json"
