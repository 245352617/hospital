#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
COPY . .

#RUN apt-get update 
#RUN apt-get install libgdiplus -y && ln -s libgdiplus.so gdiplus.dll

EXPOSE 80
EXPOSE 443 

ENV TZ Asia/Shanghai 
ENTRYPOINT ["dotnet", "YiJian.EMR.HttpApi.Host.dll"]
