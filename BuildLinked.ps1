# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/Nep3TestMod/*" -Force -Recurse
dotnet publish "./Nep3ArchipelagoClient.csproj" -c Release -o "$env:RELOADEDIIMODS/Nep3ArchipelagoClient" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location