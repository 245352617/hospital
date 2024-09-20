@echo off
@title 卸载预检分诊服务
@echo off
echo= 停止服务
@sc stop YiJian.Ecis.TriageService
echo= 卸载服务
@sc delete YiJian.Ecis.TriageService
@pause