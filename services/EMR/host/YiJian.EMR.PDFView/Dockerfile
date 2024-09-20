#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["host/YiJian.EMR.PDFView/YiJian.EMR.PDFView.csproj", "host/YiJian.EMR.PDFView/"]
RUN dotnet restore "host/YiJian.EMR.PDFView/YiJian.EMR.PDFView.csproj"
COPY . .
WORKDIR "/src/host/YiJian.EMR.PDFView"
RUN dotnet build "YiJian.EMR.PDFView.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YiJian.EMR.PDFView.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YiJian.EMR.PDFView.dll"]