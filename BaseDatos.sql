-- Crear base de datos si no existe
IF DB_ID('prueba') IS NULL
BEGIN
    CREATE DATABASE prueba;
END
GO

USE prueba;
GO

-- Tabla Usuario
IF OBJECT_ID('Usuario', 'U') IS NOT NULL DROP TABLE Usuario;
CREATE TABLE Usuario (
    UsuarioId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    NombreCompleto NVARCHAR(150) NOT NULL,
    Correo NVARCHAR(100) NOT NULL UNIQUE,
    Telefono NVARCHAR(20),
    Rol NVARCHAR(20) NOT NULL CHECK (Rol IN ('Admin', 'Editor', 'Viewer')),
    Estado BIT NOT NULL DEFAULT 1
);
GO

-- Tabla Proyecto
IF OBJECT_ID('Proyecto', 'U') IS NOT NULL DROP TABLE Proyecto;
CREATE TABLE Proyecto (
    ProyectoId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Nombre NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(500),
    FechaInicio DATETIME NOT NULL,
    FechaFin DATETIME NOT NULL,
    Estado NVARCHAR(20) NOT NULL CHECK (Estado IN ('Activo', 'Inactivo')),
    UsuarioId UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId)
);
GO

-- Tabla Actividad
IF OBJECT_ID('Actividad', 'U') IS NOT NULL DROP TABLE Actividad;
CREATE TABLE Actividad (
    ActividadId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Titulo NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(500),
    Fecha DATETIME NOT NULL,
    HorasEstimadas INT NOT NULL,
    HorasReales INT NOT NULL,
    ProyectoId UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (ProyectoId) REFERENCES Proyecto(ProyectoId)
);
GO

INSERT INTO Usuario (UsuarioId, NombreCompleto, Correo, Telefono, Rol, Estado)
VALUES 
(NEWID(), 'Carlos Pérez', 'carlos.perez@example.com', '987654321', 'Admin', 1),
(NEWID(), 'Laura Gómez', 'laura.gomez@example.com', '912345678', 'Editor', 1),
(NEWID(), 'Juan Rodríguez', 'juan.rodriguez@example.com', '934567891', 'Viewer', 1);
GO

DECLARE @UsuarioId UNIQUEIDENTIFIER;
SELECT TOP 1 @UsuarioId = UsuarioId FROM Usuario WHERE Rol = 'Admin';

INSERT INTO Proyecto (ProyectoId, Nombre, Descripcion, FechaInicio, FechaFin, Estado, UsuarioId)
VALUES 
(NEWID(), 'Sistema de Gestión Escolar', 'Proyecto para automatizar procesos escolares.', '2024-01-01', '2024-06-30', 'Activo', @UsuarioId),
(NEWID(), 'Rediseño Web Corporativa', 'Rediseño completo del sitio web de la empresa.', '2024-02-15', '2024-09-15', 'Activo', @UsuarioId);
GO

DECLARE @ProyectoId UNIQUEIDENTIFIER;
SELECT TOP 1 @ProyectoId = ProyectoId FROM Proyecto;

INSERT INTO Actividad (ActividadId, Titulo, Descripcion, Fecha, HorasEstimadas, HorasReales, ProyectoId)
VALUES 
(NEWID(), 'Reunión de levantamiento', 'Reunión con cliente para entender requerimientos', '2024-01-05', 4, 3, @ProyectoId),
(NEWID(), 'Desarrollo de módulo login', 'Construcción del sistema de autenticación', '2024-01-10', 12, 14, @ProyectoId),
(NEWID(), 'Pruebas unitarias', 'Creación y ejecución de pruebas', '2024-01-15', 6, 5, @ProyectoId);
GO