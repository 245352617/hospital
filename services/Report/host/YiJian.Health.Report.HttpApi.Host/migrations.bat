@echo off

set remark=
set /p remark=Please enter migrations file name:
echo The code files you migrated: %remark%

dotnet ef migrations add %remark%
dotnet ef database update

echo The migration completed
echo .