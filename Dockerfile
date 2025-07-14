FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ProductApi/ProductApi.csproj", "ProductApi/"]
COPY ["ProductApi.Application/ProductApi.Application.csproj", "ProductApi.Application/"]
COPY ["ProductApi.Domain/ProductApi.Domain.csproj", "ProductApi.Domain/"]
COPY ["ProductApi.Infrastructure/ProductApi.Infrastructure.csproj", "ProductApi.Infrastructure/"]
RUN dotnet restore "ProductApi/ProductApi.csproj"
COPY . .
WORKDIR "/src/ProductApi"
RUN dotnet build "ProductApi.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "ProductApi.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/Logs
ENTRYPOINT ["dotnet", "ProductApi.dll"] 