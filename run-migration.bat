@echo off
set ASPNETCORE_ENVIRONMENT=Development
cd /d "%~dp0"
dotnet ef migrations add InitialMySql --context ApplicationDbContext
dotnet ef database update --context ApplicationDbContext
pause
