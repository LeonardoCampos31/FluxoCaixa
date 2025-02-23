FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["FluxoCaixa.Lancamentos.csproj", "FluxoCaixa.Lancamentos/"]

WORKDIR /src/FluxoCaixa.Lancamentos
RUN dotnet restore "FluxoCaixa.Lancamentos.csproj"

COPY . .

WORKDIR /src/FluxoCaixa.Lancamentos
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FluxoCaixa.Lancamentos.dll"]