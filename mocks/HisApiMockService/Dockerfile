FROM 192.168.1.162:5000/dotnet/aspnet:6.0 AS base

WORKDIR /app
COPY . .
EXPOSE 80
EXPOSE 443

ENV TZ Asia/Shanghai
ENTRYPOINT ["dotnet", "HisApiMockService.dll"]
