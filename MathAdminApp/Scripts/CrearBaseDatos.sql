-- ============================================================
-- Script SQL para crear la base de datos MathAdminDB
-- Compatible con SQL Server / SQL Server Express
-- ============================================================

-- Crear la base de datos (ejecutar si no existe)
CREATE DATABASE MathAdminDB;
GO
USE MathAdminDB;
GO

-- ============================================================
-- Tabla: Usuarios
-- Almacena alumnos y administradores
-- ============================================================

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    Correo NVARCHAR(200) NOT NULL,
    NombreUsuario NVARCHAR(100) NOT NULL UNIQUE,
    Contrasena NVARCHAR(200) NOT NULL,
    Grado NVARCHAR(20) NOT NULL DEFAULT '6to',
    Rol NVARCHAR(20) NOT NULL DEFAULT 'Alumno',  -- 'Administrador' o 'Alumno'
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);

-- ============================================================
-- Tabla: Unidades
-- Unidades tematicas del curso de matematicas
-- ============================================================
CREATE TABLE Unidades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL,
    Descripcion NVARCHAR(500) NOT NULL DEFAULT '',
    NumeroUnidad INT NOT NULL,
    Activa BIT NOT NULL DEFAULT 1
);

-- ============================================================
-- Tabla: Examenes
-- Examenes asociados a una unidad
-- ============================================================
CREATE TABLE Examenes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL,
    UnidadId INT NOT NULL FOREIGN KEY REFERENCES Unidades(Id),
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1
);

-- ============================================================
-- Tabla: Preguntas
-- Preguntas de un examen (opcion multiple o abierta)
-- ============================================================
CREATE TABLE Preguntas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ExamenId INT NOT NULL FOREIGN KEY REFERENCES Examenes(Id) ON DELETE CASCADE,
    Texto NVARCHAR(1000) NOT NULL,
    Tipo NVARCHAR(20) NOT NULL DEFAULT 'Multiple',  -- 'Multiple' o 'Abierta'
    OpcionA NVARCHAR(500) DEFAULT '',
    OpcionB NVARCHAR(500) DEFAULT '',
    OpcionC NVARCHAR(500) DEFAULT '',
    OpcionD NVARCHAR(500) DEFAULT '',
    RespuestaCorrecta NVARCHAR(500) NOT NULL
);

-- ============================================================
-- Tabla: ResultadosExamen
-- Calificaciones de los alumnos en cada examen
-- ============================================================
CREATE TABLE ResultadosExamen (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    ExamenId INT NOT NULL FOREIGN KEY REFERENCES Examenes(Id),
    Calificacion DECIMAL(5,2) NOT NULL DEFAULT 0,
    FechaPresentacion DATETIME NOT NULL DEFAULT GETDATE()
);

-- ============================================================
-- Datos de ejemplo: Usuario administrador
-- ============================================================
INSERT INTO Usuarios (Nombre, Correo, NombreUsuario, Contrasena, Grado, Rol)
VALUES ('Administrador', 'admin@mathadmin.com', 'admin', 'admin123', 'N/A', 'Administrador');

-- ============================================================
-- Datos de ejemplo: Unidades
-- ============================================================
INSERT INTO Unidades (Nombre, Descripcion, NumeroUnidad) VALUES
('Numeros naturales', 'Operaciones basicas con numeros naturales', 1),
('Fracciones', 'Suma, resta, multiplicacion y division de fracciones', 2),
('Decimales', 'Operaciones con numeros decimales', 3),
('Geometria', 'Figuras geometricas, areas y perimetros', 4),
('Estadistica basica', 'Promedio, moda, mediana y graficas', 5);

-- ============================================================
-- Datos de ejemplo: Alumnos
-- ============================================================
INSERT INTO Usuarios (Nombre, Correo, NombreUsuario, Contrasena, Grado, Rol) VALUES
('Maria Lopez', 'maria@escuela.com', 'maria.lopez', '123456', '6to', 'Alumno'),
('Juan Perez', 'juan@escuela.com', 'juan.perez', '123456', '6to', 'Alumno'),
('Ana Garcia', 'ana@escuela.com', 'ana.garcia', '123456', '6to', 'Alumno');

PRINT 'Base de datos MathAdminDB creada exitosamente.';
