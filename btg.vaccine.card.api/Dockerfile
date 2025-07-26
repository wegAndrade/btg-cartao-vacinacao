# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["../btg.vaccine.card.api/btg.vaccine.card.api.csproj", "btg.vaccine.card.api/"]
COPY ["../btg.vaccine.card.domain/btg.vaccine.card.domain.csproj", "btg.vaccine.card.domain/"]
COPY ["../btg.vaccine.card.infra/btg.vaccine.card.infra.data.csproj", "btg.vaccine.card.infra/"]
COPY ["../btg.vaccine.card.application/btg.vaccine.card.application.csproj", "btg.vaccine.card.application/"]
RUN dotnet restore "./btg.vaccine.card.api/btg.vaccine.card.api.csproj"
COPY . .
WORKDIR "/src/btg.vaccine.card.api"
RUN dotnet build "./btg.vaccine.card.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./btg.vaccine.card.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "btg.vaccine.card.api.dll"]