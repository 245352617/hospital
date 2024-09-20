@echo off
@title 安装数据字典服务
@echo off
@sc create YiJian.Ecis.MasterDataService binPath="%~dp0/YiJian.MasterData.HttpApi.Host.exe"
@echo= 启动服务
@echo off
@sc start YiJian.Ecis.MasterDataService
@echo off
echo= 配置服务
@echo off
@sc config YiJian.Ecis.MasterDataService start= AUTO
@echo off
echo= 成功安装、启动、配置服务
@pause
