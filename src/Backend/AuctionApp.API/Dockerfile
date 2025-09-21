FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIRECTIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AuctionApp.API.csproj", "./"]
RUN dotnet restore "AuctionApp.API.csproj"
COPY . .
RUN dotnet build "AuctionApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuctionApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuctionApp.API.dll"]