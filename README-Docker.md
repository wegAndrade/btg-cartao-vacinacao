# BTG Vaccine Card - Docker Compose

Este projeto inclui uma configuração Docker Compose completa com SQL Server e a aplicação API.

## Pré-requisitos

- Docker Desktop instalado e rodando
- Docker Compose v2.0+

**Solução:**

1. Verifique se o Docker Desktop está rodando
2. Reinicie o Docker Desktop
3. Aguarde o Docker Desktop inicializar completamente
4. Execute novamente o comando docker-compose

## Configuração

### 1. Variáveis de Ambiente

Crie um arquivo `.env` na raiz do projeto com as seguintes variáveis:

```env
# Configurações do SQL Server
SA_PASSWORD=YourStrong@Passw0rd
MSSQL_PID=Express

# Configurações da API
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:8080;https://+:8081

# Connection String
CONNECTION_STRING=Server=sqlserver,1433;Database=btg_vaccine_db;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;MultipleActiveResultSets=true

# Docker Registry (opcional)
DOCKER_REGISTRY=
```

## Uso

### Iniciar todos os serviços

```bash
# Certifique-se de que o Docker Desktop está rodando
docker-compose up -d
```

## Serviços

### SQL Server

- **Porta**: 1433
- **Container**: btg-vaccine-sqlserver
- **Banco**: btg_vaccine_db
- **Usuário**: sa
- **Senha**: vacccine@Passw0rd

### API

- **HTTP**: <http://localhost:8080>
- **HTTPS**: <https://localhost:8081>
- **Container**: btg-vaccine-api
- **Swagger**: <http://localhost:8080/swagger>

## Scripts de Inicialização

O arquivo `scripts/init-db.sql` é executado automaticamente quando o SQL Server inicia pela primeira vez. Ele:

1. Cria o banco de dados `btg_vaccine_db`

## Desenvolvimento

Para desenvolvimento local, você pode:

1. Usar apenas o SQL Server do Docker:

  ```bash
   docker-compose up sqlserver -d
   ```

1. Rodar a API localmente no Visual Studio ou dotnet CLI

2. Usar a connection string local no `appsettings.Development.json`
