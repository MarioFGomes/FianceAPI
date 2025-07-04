#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Bckend/Finance.API/Finance.API.csproj", "src/Bckend/Finance.API/"]
COPY ["src/Shared/Finance.Communication/Finance.Communication.csproj", "src/Shared/Finance.Communication/"]
COPY ["src/Bckend/Finance.Domain/Finance.Domain.csproj", "src/Bckend/Finance.Domain/"]
COPY ["src/Shared/Finance.Exception/Finance.Exception.csproj", "src/Shared/Finance.Exception/"]
COPY ["src/Bckend/Finance.CrossCutting/Finance.CrossCutting.csproj", "src/Bckend/Finance.CrossCutting/"]
COPY ["src/Bckend/Finance.Application/Finance.Application.csproj", "src/Bckend/Finance.Application/"]
COPY ["src/Bckend/Finance.Infrastructure/Finance.Infrastructure.csproj", "src/Bckend/Finance.Infrastructure/"]
RUN dotnet restore "src/Bckend/Finance.API/Finance.API.csproj"
COPY . .
WORKDIR "/src/src/Bckend/Finance.API"
RUN dotnet build "Finance.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Finance.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Finance.API.dll"]