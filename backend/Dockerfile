# =========================
# Runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# =========================
# Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas os arquivos de projeto primeiro (melhora cache)
COPY ["GestaoTitulos/GestaoTitulos.API/GestaoTitulos.API.csproj", "GestaoTitulos.API/"]
COPY ["GestaoTitulos/GestaoTitulos.Application/GestaoTitulos.Application.csproj", "GestaoTitulos.Application/"]
COPY ["GestaoTitulos/GestaoTitulos.Domain/GestaoTitulos.Domain.csproj", "GestaoTitulos.Domain/"]
COPY ["GestaoTitulos/GestaoTitulos.Infrastructure/GestaoTitulos.Infrastructure.csproj", "GestaoTitulos.Infrastructure/"]

# Restore das dependências
RUN dotnet restore "GestaoTitulos.API/GestaoTitulos.API.csproj"

# Copia o restante do código
COPY . .

# Entra na pasta do projeto
WORKDIR "/src/GestaoTitulos/GestaoTitulos.API"

# Build
RUN dotnet build "GestaoTitulos.API.csproj" -c Release -o /app/build

# =========================
# Publish
# =========================
FROM build AS publish
RUN dotnet publish "GestaoTitulos.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# =========================
# Final
# =========================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "GestaoTitulos.API.dll"]