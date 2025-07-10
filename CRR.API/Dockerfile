FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopieer alle csproj bestanden (zodat dotnet restore alle dependencies ziet)
COPY CRR.API/*.csproj CRR.API/
COPY CRR.Shared/*.csproj CRR.Shared/

# Restore dependencies
WORKDIR /src/CRR.API
RUN dotnet restore

# Kopieer de rest van de bestanden
COPY . .

# Publish app
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CRR.API.dll"]
