ALTER DATABASE EventHubDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE EventHubDB;

-- 1. Crear la base de datos
IF DB_ID('EventHubDB') IS NULL
BEGIN
    CREATE DATABASE EventHubDB;
END
GO

USE EventHubDB;
GO

-- 2. Tabla Eventos
IF OBJECT_ID('dbo.Eventos') IS NOT NULL DROP TABLE dbo.Eventos;
GO

CREATE TABLE dbo.Eventos
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL,
    Ubicacion NVARCHAR(150) NULL,
    Fecha DATETIME2 NOT NULL,
    Descripcion NVARCHAR(1000) NULL,
    CreadoEn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    Eliminado BIT NOT NULL DEFAULT 0
);
GO

CREATE INDEX IX_Eventos_Fecha ON dbo.Eventos(Fecha);
CREATE INDEX IX_Eventos_Ubicacion ON dbo.Eventos(Ubicacion);
GO

-- 3. Tabla Asistentes
IF OBJECT_ID('dbo.Asistentes') IS NOT NULL DROP TABLE dbo.Asistentes;
GO

CREATE TABLE dbo.Asistentes
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    Telefono NVARCHAR(50) NULL,
    EventoId INT NOT NULL,
    RegistradoEn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Asistentes_Eventos FOREIGN KEY (EventoId) REFERENCES dbo.Eventos(Id) ON DELETE CASCADE
);
GO

CREATE INDEX IX_Asistentes_EventoId ON dbo.Asistentes(EventoId);
CREATE INDEX IX_Asistentes_Nombre ON dbo.Asistentes(Nombre);
GO

-- 4. Tabla Comentarios (reseñas)
IF OBJECT_ID('dbo.Comentarios') IS NOT NULL DROP TABLE dbo.Comentarios;
GO

CREATE TABLE dbo.Comentarios
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Autor NVARCHAR(150) NOT NULL,
    Rating TINYINT NULL, -- 1..5
    Texto NVARCHAR(1000) NULL,
    EventoId INT NOT NULL,
    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    Eliminado BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Comentarios_Eventos FOREIGN KEY (EventoId) REFERENCES dbo.Eventos(Id) ON DELETE CASCADE
);
GO

CREATE INDEX IX_Comentarios_EventoId ON dbo.Comentarios(EventoId);
CREATE INDEX IX_Comentarios_Fecha ON dbo.Comentarios(FechaCreacion);
GO

-- 5. Datos de ejemplo
INSERT INTO dbo.Eventos (Nombre, Ubicacion, Fecha, Descripcion)
VALUES
('Conferencia Tech 2025', 'Buenos Aires', '2025-11-10 19:00', 'Conferencia sobre nuevas tecnologías.'),
('Concierto Rock', 'Córdoba', '2025-12-05 21:00', 'Concierto de bandas locales.'),
('Workshop .NET', 'Rosario', '2025-10-30 09:00', 'Taller práctico de .NET y APIs.');

INSERT INTO dbo.Asistentes (Nombre, Email, Telefono, EventoId)
VALUES
('Ana Pérez','ana.perez@mail.com','+54 9 11 1234 5678',1),
('Juan Gómez','juan.gomez@mail.com',NULL,1),
('María López','maria.lopez@mail.com','+54 9 11 9876 5432',3);

INSERT INTO dbo.Comentarios (Autor, Rating, Texto, EventoId)
VALUES
('Carlos',5,'Excelente evento, muy bien organizado',1),
('Sofía',4,'Buen contenido pero faltó tiempo para preguntas',1),
('Pedro',5,'Increíble concierto!',2);

GO
