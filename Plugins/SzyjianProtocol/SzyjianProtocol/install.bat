@echo off

md "C:\Program Files\Szyjian\"
copy "%~dp0SzyjianProtocol.exe" "C:\Program Files\Szyjian\SzyjianProtocol.exe"
regedit.exe /S %~dp0SzyjianProtocal.reg


echo 服务安装成功
pause