# Etapa base - imagem para o runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de build - imagem SDK para compilar e restaurar pacotes
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DigitalWalletAPI/DigitalWalletAPI.csproj", "DigitalWalletAPI/"]
COPY ["DigitalWalletAPI.IoC/DigitalWalletAPI.IoC.csproj", "DigitalWalletAPI.IoC/"]
COPY ["DigitalWalletAPI.Infrastructure/DigitalWalletAPI.Infrastructure.csproj", "DigitalWalletAPI.Infrastructure/"]
COPY ["DigitalWalletAPI.Application/DigitalWalletAPI.Application.csproj", "DigitalWalletAPI.Application/"]
COPY ["DigitalWalletAPI.Domain/DigitalWalletAPI.Domain.csproj", "DigitalWalletAPI.Domain/"]
RUN dotnet restore "./DigitalWalletAPI/DigitalWalletAPI.csproj"
COPY . . 
WORKDIR "/src/DigitalWalletAPI"
RUN dotnet build "./DigitalWalletAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publicação - publica os arquivos necessários
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DigitalWalletAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final - Imagem para rodar a aplicação
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "DigitalWalletAPI.dll"]
