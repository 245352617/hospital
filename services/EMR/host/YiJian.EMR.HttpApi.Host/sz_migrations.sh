
export ASPNETCORE_ENVIRONMENT="Production"

# 添加迁移文件
dotnet ef migrations add V2.2.6.2 --project ../../migrations/YiJian.EMR.Migrations.Release/YiJian.EMR.Migrations.Release.csproj

# 生成迁移脚本
dotnet ef migrations script --project ../../migrations/YiJian.EMR.Migrations.Release/YiJian.EMR.Migrations.Release.csproj -o bin/publish/release/emr-update.sql --idempotent
