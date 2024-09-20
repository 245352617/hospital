@echo off
@title 安装医健医嘱服务
@echo off
@sc create YiJian.Health.Pec.RecipeService binPath="%~dp0YiJian.Recipe.HttpApi.Host.exe"
@echo= 启动服务
@echo off
@sc start YiJian.Health.Pec.RecipeService
@echo off
echo= 配置服务
@echo off
@sc config YiJian.Health.Pec.RecipeService start= AUTO
@echo off
echo= 成功安装、启动、配置服务
@pause
