# Fluxo de Caixa

## Descrição
Sistema para controle de lançamentos (débitos e créditos) e geração de relatório consolidado diário.

## Como Executar Localmente

### Pré-requisitos
- Docker
- .NET 8 SDK
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Npgsql.EntityFrameworkCore.PostgreSQL

### Passos
1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/fluxocaixa.git
   cd fluxocaixa

2. Suba os containers:
    docker-login
    docker-compose up --build

3. Acesse o a pasta do projeto: FluxoCaixa.Lancamentos
    Execute os comando: 
        dotnet tool install --global dotnet-ef
        dotnet ef migrations add InitialCreate
        dotnet ef database update
