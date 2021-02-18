Param (
    [String]$Run_Migration = "No",
    [String]$Unique_Identifier
)
if ($Run_Migration -eq "Yes" -or $Run_Migration -eq "y") {
    $Unique_Identifier = $env:USERNAME + ((Get-Date).ToLongDateString() + " " + (Get-Date).ToLongTimeString()) + $Unique_Identifier
    $Unique_Identifier = $Unique_Identifier.Replace(" ","_").Replace(":","_").Replace(",","")
    & { dotnet ef migrations add $Unique_Identifier --context server.Models.Database.DefaultContext}
}