FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SD_Restaurant.Gateway/SD_Restaurant.Gateway.csproj", "SD_Restaurant.Gateway/"]
RUN dotnet restore "SD_Restaurant.Gateway/SD_Restaurant.Gateway.csproj"
COPY . .
WORKDIR "/src/SD_Restaurant.Gateway"
RUN dotnet build "SD_Restaurant.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SD_Restaurant.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SD_Restaurant.Gateway.dll"] 