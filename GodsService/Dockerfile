FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GodsService/GodsService.csproj", "GodsService/"]
RUN dotnet restore "GodsService/GodsService.csproj"
COPY . .
WORKDIR "/src/GodsService"
RUN dotnet build "GodsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GodsService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GodsService.dll"]
