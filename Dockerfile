# Usar la imagen oficial de .NET 9.0
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Usar la imagen de SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY ["ProductApi/ProductApi.csproj", "ProductApi/"]
COPY ["ProductApi.Application/ProductApi.Application.csproj", "ProductApi.Application/"]
COPY ["ProductApi.Domain/ProductApi.Domain.csproj", "ProductApi.Domain/"]
COPY ["ProductApi.Infrastructure/ProductApi.Infrastructure.csproj", "ProductApi.Infrastructure/"]

RUN dotnet restore "ProductApi/ProductApi.csproj"

# Copiar todo el código fuente
COPY . .

# Compilar la aplicación
WORKDIR "/src/ProductApi"
RUN dotnet build "ProductApi.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "ProductApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Crear directorio para logs
RUN mkdir -p /app/Logs

ENTRYPOINT ["dotnet", "ProductApi.dll"] 