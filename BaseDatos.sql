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
