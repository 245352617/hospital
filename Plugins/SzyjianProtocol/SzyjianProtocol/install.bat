@echo off

md "C:\Program Files\Szyjian\"
copy "%~dp0SzyjianProtocol.exe" "C:\Program Files\Szyjian\SzyjianProtocol.exe"
regedit.exe /S %~dp0SzyjianProtocal.reg


echo ����װ�ɹ�
pause