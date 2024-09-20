@echo off
@title 卸载数据字典服务
@echo off
echo= 停止服务
@sc stop YiJian.Ecis.MasterDataService
echo= 卸载服务
@sc delete YiJian.Ecis.MasterDataService
@pause