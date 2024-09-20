FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
COPY . .
EXPOSE 80
EXPOSE 443
EXPOSE 57505
ENV TZ Asia/Shanghai
ENTRYPOINT [ "dotnet", "YiJian.Recipe.HttpApi.Host.dll" ]

