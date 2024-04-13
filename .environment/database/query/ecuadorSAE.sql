USE MASTER;
GO
CREATE DATABASE SAE ON PRIMARY 
(   NAME = 'SAE_data', 
    FILENAME = '/var/opt/mssql/data/SAE.mdf', SIZE = 100MB, MAXSIZE = UNLIMITED, FILEGROWTH = 10MB
) LOG ON 
(   NAME = 'SAE_log', 
    FILENAME = '/var/opt/mssql/data/SAE.ldf', SIZE = 50MB, MAXSIZE = 200MB, FILEGROWTH = 5MB
);
GO

USE SAE;
GO

-- Crear la tabla Empresa
CREATE TABLE Empresa (
    EmpresaID   INT PRIMARY KEY,
    Nombre      NVARCHAR(100),
    Direccion   NVARCHAR(100),
    Telefono    NVARCHAR(20)
);
GO

-- Crear la tabla Empleado
CREATE TABLE Empleado (
    EmpleadoID      INT PRIMARY KEY,
    Nombre          NVARCHAR(100),
    Apellido        NVARCHAR(100),
    Cargo           NVARCHAR(100),
    Salario         DECIMAL(10, 2),
    EmpresaID       INT FOREIGN KEY REFERENCES Empresa(EmpresaID)
);
GO

-- Insertar datos de empresas
INSERT INTO Empresa (EmpresaID, Nombre, Direccion, Telefono)
VALUES
    (1, 'Empresa A', 'Dirección A', '1234567890'),
    (2, 'Empresa B', 'Dirección B', '0987654321'),
    (3, 'Empresa C', 'Dirección C', '1357924680'),
    (4, 'Empresa D', 'Dirección D', '2468013579'),
    (5, 'Empresa E', 'Dirección E', '9876543210');

-- Insertar datos de empleados por empresa
INSERT INTO Empleado (EmpleadoID, Nombre, Apellido, Cargo, Salario, EmpresaID)
VALUES
    (1, 'Empleado1', 'Apellido1', 'Cargo1', 30000.00, 1),
    (2, 'Empleado2', 'Apellido2', 'Cargo2', 35000.00, 1),
    (3, 'Empleado3', 'Apellido3', 'Cargo3', 32000.00, 1),
    (4, 'Empleado4', 'Apellido4', 'Cargo4', 28000.00, 1),
    (5, 'Empleado5', 'Apellido5', 'Cargo5', 31000.00, 1),
    (6, 'Empleado6', 'Apellido6', 'Cargo1', 29000.00, 2),
    (7, 'Empleado7', 'Apellido7', 'Cargo2', 33000.00, 2),
    (8, 'Empleado8', 'Apellido8', 'Cargo3', 34000.00, 2),
    (9, 'Empleado9', 'Apellido9', 'Cargo4', 36000.00, 2),
    (10, 'Empleado10', 'Apellido10', 'Cargo5', 32000.00, 2),
    (11, 'Empleado11', 'Apellido11', 'Cargo1', 30000.00, 3),
    (12, 'Empleado12', 'Apellido12', 'Cargo2', 35000.00, 3),
    (13, 'Empleado13', 'Apellido13', 'Cargo3', 32000.00, 3),
    (14, 'Empleado14', 'Apellido14', 'Cargo4', 28000.00, 3),
    (15, 'Empleado15', 'Apellido15', 'Cargo5', 31000.00, 3),
    (16, 'Empleado16', 'Apellido16', 'Cargo1', 29000.00, 4),
    (17, 'Empleado17', 'Apellido17', 'Cargo2', 33000.00, 4),
    (18, 'Empleado18', 'Apellido18', 'Cargo3', 34000.00, 4),
    (19, 'Empleado19', 'Apellido19', 'Cargo4', 36000.00, 4),
    (20, 'Empleado20', 'Apellido20', 'Cargo5', 32000.00, 4),
    (21, 'Empleado21', 'Apellido21', 'Cargo1', 30000.00, 5),
    (22, 'Empleado22', 'Apellido22', 'Cargo2', 35000.00, 5),
    (23, 'Empleado23', 'Apellido23', 'Cargo3', 32000.00, 5),
    (24, 'Empleado24', 'Apellido24', 'Cargo4', 28000.00, 5),
    (25, 'Empleado25', 'Apellido25', 'Cargo5', 31000.00, 5);
GO
CREATE VIEW Vista_Empresa_Empleado AS
SELECT
    e.EmpresaID     EmpresaId,
    e.Nombre        EmpresaNombre,
    em.EmpleadoID   EmpleadoID   ,
    em.Nombre       EmpleadoNombre      ,
    em.Apellido     EmpleadoApellido    ,
    em.Cargo        EmpleadoCargo       ,
    em.Salario      EmpleadoSalario
FROM
    Empresa e
JOIN
    Empleado em ON e.EmpresaID = em.EmpresaID;