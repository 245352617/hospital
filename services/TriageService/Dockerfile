FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
EXPOSE 80
ENV TZ Asia/Shanghai
COPY . .

ENTRYPOINT ["dotnet", "TriageService.dll","--urls=http://ptriagesrv"]
