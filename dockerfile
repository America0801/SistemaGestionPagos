FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj y restaurar dependencias
COPY ["SistemaGestionPagos.Web/SistemaGestionPagos.Web.csproj", "SistemaGestionPagos.Web/"]
RUN dotnet restore "SistemaGestionPagos.Web/SistemaGestionPagos.Web.csproj"

# Copiar todo y compilar
COPY . .
WORKDIR "/src/SistemaGestionPagos.Web"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SistemaGestionPagos.Web.dll"]

# yes sir