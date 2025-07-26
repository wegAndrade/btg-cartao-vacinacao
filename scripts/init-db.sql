-- Script de inicialização do banco de dados BTG Vaccine Card
-- Cria o banco de dados se não existir
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'btg_vaccine_db')
BEGIN
    CREATE DATABASE btg_vaccine_db;
END
GO

USE btg_vaccine_db;
GO

-- Verifica se as tabelas já existem antes de criar
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'People')
BEGIN
    CREATE TABLE People (
        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        Name NVARCHAR(255) NOT NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Vaccines')
BEGIN
    CREATE TABLE Vaccines (
        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        Name NVARCHAR(255) NOT NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'VaccineRecords')
BEGIN
    CREATE TABLE VaccineRecords (
        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        IdPessoa UNIQUEIDENTIFIER NOT NULL,
        Vacina UNIQUEIDENTIFIER NOT NULL,
        Doses INT NOT NULL,
        DataAplicacao DATETIME2 NOT NULL,
        DataAtualizacao DATETIME2 NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (IdPessoa) REFERENCES People(Id),
        FOREIGN KEY (Vacina) REFERENCES Vaccines(Id)
    );
END
GO

-- Insere alguns dados de exemplo se as tabelas estiverem vazias
IF NOT EXISTS (SELECT * FROM Vaccines)
BEGIN
    INSERT INTO Vaccines (Id, Name) VALUES 
    (NEWID(), 'COVID-19'),
    (NEWID(), 'Influenza'),
    (NEWID(), 'Hepatite B'),
    (NEWID(), 'Sarampo'),
    (NEWID(), 'Poliomielite');
END
GO

PRINT 'Banco de dados BTG Vaccine Card inicializado com sucesso!';
GO 