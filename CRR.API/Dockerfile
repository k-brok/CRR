# ----------------------------
# STAP 1: Build stage
# ----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopieer csproj en restore dependencies
COPY CRR.API.csproj ./
RUN dotnet restore

# Kopieer de rest van de projectbestanden en build
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# ----------------------------
# STAP 2: Runtime stage
# ----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Kopieer de gepubliceerde output van de build stage
COPY --from=build /app/publish .

# Start de applicatie
ENTRYPOINT ["dotnet", "CRR.API.dll"]