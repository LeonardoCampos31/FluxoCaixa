# Fluxo de Caixa

## Descrição
Sistema para controle de lançamentos (débitos e créditos) e geração de relatório consolidado diário.

## Como Executar Localmente

### Pré-requisitos
- .NET 9 SDK
- Git
- Docker
- Postman
- K6

### Passos
1. Clone o repositório:
   ```bash
   git clone https://github.com/LeonardoCampos31/FluxoCaixa.git
   cd fluxocaixa

2. Acesse a pasta FluxoCaixa pelo Terminar e Suba os containers:
    1. Para excutar esses passo é necessário ter o docker instalado
    2. docker login -u <seu-usuario>
    3. docker-compose up --build -d

3. Acesse o a pasta do projeto: FluxoCaixa
    1. Para executar esses passos é necessário ter o Postman instalado
    2. Import a collection do postman: FluxoCaixa.postamn_colletion.json
        1. Nessa collection será possível excutar Débitos e Créditos, além de consultar o consolidado diário
    3. Na requests sera necessário alterar a data para o dia atual.

4. Acesse o Grafana: http://localhost:3000/
    1. Acesse Data Sources: http://localhost:3000/connections/datasources/new
    2. Clique em Promethues: http://localhost:3000/connections/datasources/edit/fee1e7oonlwqoa
    3. Em Prometheus server URL adicione: http://prometheus:9090
    4. Vá até o final da página e clique em Save & Test
    5. Volte no ínico da página e clique na aba Dashboard: e importe Prometheus e Grafana
    6. No menu lateral clique em Dashboards
    7. No topo da página a direita clique em New -> New Dashboard
    8. Na próxima página clique em Add visualization
    9. Na próxima página selecione Prometheus
    10. Para visualizar as metricas executo o teste de carga: passo 5.

5. Executando o teste de carga
    1. Para executar esses passos é necessário ter o K6 instalado
    2. Abrar o arquivo teste-carga.js e troque para data que foram inserido os dados: 'http://host.docker.internal:5004/api/consolidado/2025-02-24T00:00:00Z'
    2. Acesse a pasta do projeto FluxoCaixa pelo Termina e excecute o comando: k6 run teste-carga.js
