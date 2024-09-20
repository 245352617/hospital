@echo off
@title 卸载院前急救分诊病历服务
@echo off
echo= 停止服务
@sc stop YiJian.Health.Pec.TriageService
echo= 卸载服务
@sc delete YiJian.Health.Pec.TriageService
@pause