[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [string] $UserName,

    [Parameter(Mandatory = $true)]
    [string] $DualAuthCode,

    [Parameter(Mandatory = $true)]
    [string] $GUID

)

$PostData = @{
    UserName = $UserName
    DualAuthCode = $DualAuthCode
}

$json = $PostData | ConvertTo-Json
write-output $json

Invoke-WebRequest -Uri "https://localhost:5001/api/Info/${GUID}/MaterialsReceived" -Method Patch -Body $json -ContentType "application/json"