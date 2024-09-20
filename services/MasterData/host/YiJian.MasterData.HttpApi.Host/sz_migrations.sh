
export ASPNETCORE_ENVIRONMENT="Production"

# 添加迁移文件
dotnet ef migrations add V2.2.6.2 --context MasterDataHttpApiHostMigrationsDbContext --project ../../migrations/YiJian.MasterData.Migrations.Release/YiJian.MasterData.Migrations.Release.csproj

# 生成迁移脚本
dotnet ef migrations script --context MasterDataHttpApiHostMigrationsDbContext --project ../../migrations/YiJian.MasterData.Migrations.Release/YiJian.MasterData.Migrations.Release.csproj -o bin/publish/release/masterdata-update.sql --idempotent
