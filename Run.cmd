@echo off

set DOTNET_ROOT="E:\Portable programma's\PortableDotnet\dotnet"

set PATH=%DOTNET_ROOT%;%PATH%

dotnet --version

start "API" cmd /k "cd /d %~dp0CRR.API && dotnet run"

start "APP" cmd /k "cd /d %~dp0CRR.APP && dotnet run"