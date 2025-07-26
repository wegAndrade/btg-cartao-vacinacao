# Script PowerShell para inicializar o banco de dados BTG Vaccine Card
# Execute este script após o SQL Server estar rodando

Write-Host "Inicializando banco de dados BTG Vaccine Card..." -ForegroundColor Green

# Aguarda o SQL Server estar pronto
Write-Host "Aguardando SQL Server estar pronto..." -ForegroundColor Yellow
Start-Sleep -Seconds 30

# Executa o script SQL
Write-Host "Executando script de inicialização..." -ForegroundColor Yellow
docker exec btg-vaccine-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P vacccine@Passw0rd -i /scripts/init-db.sql

if ($LASTEXITCODE -eq 0) {
    Write-Host "Banco de dados inicializado com sucesso!" -ForegroundColor Green
} else {
    Write-Host "Erro ao inicializar o banco de dados!" -ForegroundColor Red
} 