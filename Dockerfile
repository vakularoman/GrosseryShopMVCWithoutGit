FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AquaPlayground/AquaPlayground/AquaPlayground.csproj", "AquaPlayground/"]
COPY ["AquaPlayground/AquaPlayground.UnitTests/AquaPlayground.UnitTests.csproj", "AquaPlayground.UnitTests/"]

RUN dotnet restore "AquaPlayground/AquaPlayground.csproj"
COPY . .
WORKDIR "/src/AquaPlayground"
RUN dotnet build "AquaPlayground/AquaPlayground.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AquaPlayground/AquaPlayground.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "AquaPlayground.dll"]
