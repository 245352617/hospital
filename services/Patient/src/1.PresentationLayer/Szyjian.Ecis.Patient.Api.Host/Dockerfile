#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic
#FROM registry.cn-shanghai.aliyuncs.com/shangzhe/dotnet:3.1
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY . .
EXPOSE 80
EXPOSE 443
EXPOSE 57502
ENV TZ Asia/Shanghai
ENTRYPOINT [ "dotnet", "Szyjian.Ecis.Patient.Api.Host.dll" ]
