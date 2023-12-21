FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FighterService/FighterService.csproj", "FighterService/"]
RUN dotnet restore "FighterService/FighterService.csproj"
COPY . .
WORKDIR "/src/FighterService"
RUN dotnet build "FighterService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FighterService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FighterService.dll"]
