# Use the official .NET 9.0 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET 9.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project files
COPY ["SD_Restaurant.API/SD_Restaurant.API.csproj", "SD_Restaurant.API/"]
COPY ["SD_Restaurant.Application/SD_Restaurant.Application.csproj", "SD_Restaurant.Application/"]
COPY ["SD_Restaurant.Core/SD_Restaurant.Core.csproj", "SD_Restaurant.Core/"]
COPY ["SD_Restaurant.Infrastructure/SD_Restaurant.Infrastructure.csproj", "SD_Restaurant.Infrastructure/"]
COPY ["SD_Restaurant.Web/SD_Restaurant.Web.csproj", "SD_Restaurant.Web/"]
COPY ["SD_Restaurant.Gateway/SD_Restaurant.Gateway.csproj", "SD_Restaurant.Gateway/"]

# Restore dependencies
RUN dotnet restore "SD_Restaurant.API/SD_Restaurant.API.csproj"
RUN dotnet restore "SD_Restaurant.Web/SD_Restaurant.Web.csproj"
RUN dotnet restore "SD_Restaurant.Gateway/SD_Restaurant.Gateway.csproj"

# Copy the rest of the source code
COPY . .

# Build the API
WORKDIR "/src/SD_Restaurant.API"
RUN dotnet build "SD_Restaurant.API.csproj" -c Release -o /app/build

# Build the Web UI
WORKDIR "/src/SD_Restaurant.Web"
RUN dotnet build "SD_Restaurant.Web.csproj" -c Release -o /app/build

# Build the Gateway
WORKDIR "/src/SD_Restaurant.Gateway"
RUN dotnet build "SD_Restaurant.Gateway.csproj" -c Release -o /app/build

# Publish the API
FROM build AS publish-api
WORKDIR "/src/SD_Restaurant.API"
RUN dotnet publish "SD_Restaurant.API.csproj" -c Release -o /app/publish

# Publish the Web UI
FROM build AS publish-web
WORKDIR "/src/SD_Restaurant.Web"
RUN dotnet publish "SD_Restaurant.Web.csproj" -c Release -o /app/publish

# Publish the Gateway
FROM build AS publish-gateway
WORKDIR "/src/SD_Restaurant.Gateway"
RUN dotnet publish "SD_Restaurant.Gateway.csproj" -c Release -o /app/publish

# Build the final runtime image for API
FROM base AS final-api
WORKDIR /app
COPY --from=publish-api /app/publish .
ENTRYPOINT ["dotnet", "SD_Restaurant.API.dll"]

# Build the final runtime image for Web UI
FROM base AS final-web
WORKDIR /app
COPY --from=publish-web /app/publish .
ENTRYPOINT ["dotnet", "SD_Restaurant.Web.dll"]

# Build the final runtime image for Gateway
FROM base AS final-gateway
WORKDIR /app
COPY --from=publish-gateway /app/publish .
ENTRYPOINT ["dotnet", "SD_Restaurant.Gateway.dll"] 