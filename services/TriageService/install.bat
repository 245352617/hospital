@echo off
@title 安装预检分诊服务
@echo off
@sc create YiJian.Ecis.TriageService binPath="%~dp0TriageService.exe"
@echo= 启动服务
@echo off
@sc start YiJian.Ecis.TriageService
@echo off
echo= 配置服务
@echo off
@sc config YiJian.Ecis.TriageService start= AUTO
@echo off
echo= 成功安装、启动、配置服务
@pause
