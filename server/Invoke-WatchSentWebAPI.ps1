[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [string] $UserName,

    [Parameter(Mandatory = $true)]
    [string] $DualAuthCode,

    [Parameter(Mandatory = $true)]
    [string] $GUID,

    [Parameter(Mandatory = $true)]
    [string] $SendingAddress,

    [Parameter(Mandatory = $true)]
    [string] $ReceivingAddress
)

$PostData = @{
    UserName         = $UserName
    DualAuthCode     = $DualAuthCode
    SendingAddress   = $SendingAddress
    ReceivingAddress = $ReceivingAddress
}

$json = $PostData | ConvertTo-Json
write-output $json

Invoke-WebRequest -Uri "https://appelbreyer.azurewebsites.net/api/Info/${GUID}/WatchSent" -Method Patch -Body $json -ContentType "application/json"
