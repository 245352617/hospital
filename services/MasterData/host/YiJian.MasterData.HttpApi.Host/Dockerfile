FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# 修改配置以用于支持低版本 SQL Server（https://stackoverflow.com/questions/61518238/azure-linux-web-app-change-openssl-default-security-level/61523341#61523341）
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
WORKDIR /app
COPY . .
EXPOSE 80
EXPOSE 443
EXPOSE 666
EXPOSE 57505
ENV TZ Asia/Shanghai
ENTRYPOINT [ "dotnet", "YiJian.MasterData.HttpApi.Host.dll" ]

 