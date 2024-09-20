FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
COPY . .
EXPOSE 80
EXPOSE 443

ENV TZ Asia/Shanghai
ENTRYPOINT [ "dotnet", "YiJian.Nursing.HttpApi.Host.dll" ]

