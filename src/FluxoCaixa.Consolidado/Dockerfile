FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["FluxoCaixa.Consolidado.csproj", "FluxoCaixa.Consolidado/"]

WORKDIR /src/FluxoCaixa.Consolidado
RUN dotnet restore "FluxoCaixa.Consolidado.csproj"

COPY . .

WORKDIR /src/FluxoCaixa.Consolidado
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FluxoCaixa.Consolidado.dll"]