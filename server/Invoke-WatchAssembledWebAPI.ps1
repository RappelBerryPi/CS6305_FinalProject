[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [string] $UserName,

    [Parameter(Mandatory = $true)]
    [string] $DualAuthCode,

    [Parameter(Mandatory = $true)]
    [string] $GUID,

    [Parameter(Mandatory = $true)]
    [string] $physicalAddress

)

$PostData = @{
    UserName        = $UserName
    DualAuthCode    = $DualAuthCode
    PhysicalAddress = $physicalAddress
}

$json = $PostData | ConvertTo-Json
write-output $json

Invoke-WebRequest -Uri "https://localhost:5001/api/Info/${GUID}/WatchAssembled" -Method Patch -Body $json -ContentType "application/json"