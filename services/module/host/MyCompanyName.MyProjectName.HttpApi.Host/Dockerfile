#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
COPY . .
EXPOSE 80
EXPOSE 443
ENV TZ Asia/Shanghai
 
ENTRYPOINT ["dotnet", "MyCompanyName.MyProjectName.HttpApi.Host.dll"]