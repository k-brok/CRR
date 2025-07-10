# Dev portion
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
EXPOSE 8080

# Restore portion
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CRR.APP/CRR.APP.csproj", "CRR.APP/"]
COPY ["CRR.Shared/CRR.Shared.csproj", "CRR.Shared/"]
RUN dotnet restore "./CRR.APP/CRR.APP.csproj"

# Build portion
COPY . .
WORKDIR "/src/CRR.APP"
RUN dotnet build "./CRR.APP.csproj" -c $BUILD_CONFIGURATION -o /app/build

#Publish portion
FROM build AS publish 
ARG BUILD_CONFIGURATION=Release
# Publish and put output in /docker-test/docker-test/publish
RUN dotnet publish "./CRR.APP.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM nginx:alpine
WORKDIR /user/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY --from=publish /app/publish/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
