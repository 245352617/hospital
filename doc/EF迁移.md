# EF Core 迁移常用操作
## 环境
. dotnet tool install --global dotnet-ef
> 在解决方案中对实体进行变更后，切换到包控制台(Package Manager Console)，将启动项目和默认项目设置为YiJian.ECIS.xxx.HttpApi.Host,即可使用命令
### 添加迁移 Add-Migration InitialCreate
### 应用迁移 Updata-DataBase
### 撤销迁移 Remove-Migration -force