FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic
WORKDIR /app
COPY . .

EXPOSE 80
EXPOSE 443

ENV TZ Asia/Shanghai
ENTRYPOINT ["dotnet", "YiJian.BodyParts.WebAPI.dll"]



