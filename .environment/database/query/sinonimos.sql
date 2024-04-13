use SAE;
GO

-- IF OBJECT_ID('vwBusqueda', 'V') IS NOT NULL
--     DROP VIEW vwBusqueda;

IF OBJECT_ID('RelacionSemantica', 'U') IS NOT NULL
    DROP TABLE RelacionSemantica;
IF OBJECT_ID('Sinonimo', 'U') IS NOT NULL
    DROP TABLE Sinonimo;
CREATE TABLE Sinonimo (
    SinonimoID      INT PRIMARY KEY,
    Palabra         NVARCHAR(100),
    Sinonimo        NVARCHAR(100)
);

-- UPDATE Empleado set Nombre ='Jose' WHERE EmpleadoID=1;
-- UPDATE Empleado set Nombre ='luis' WHERE EmpleadoID=2;
-- UPDATE Empleado set Nombre ='pepe' WHERE EmpleadoID=3;
-- UPDATE Empleado set Nombre ='Lucho' WHERE EmpleadoID=4;

CREATE TABLE RelacionSemantica (
    EmpleadoID1 INT,
    EmpleadoID2 INT,
    CONSTRAINT FK_RelacionSemantica1 FOREIGN KEY (EmpleadoID1) REFERENCES Empleado(EmpleadoID),
    CONSTRAINT FK_RelacionSemantica2 FOREIGN KEY (EmpleadoID2) REFERENCES Empleado(EmpleadoID)
);

INSERT INTO Sinonimo (SinonimoID, Palabra, Sinonimo)
VALUES 
    (1, 'jose', 'pepe'),
    (2, 'luis', 'lucho'),
    (3, 'Empleado4', 'Empleado5');

INSERT INTO RelacionSemantica (EmpleadoID1, EmpleadoID2)
VALUES 
    (3, 1), -- teléfono está relacionado con computadora
    (4, 2); -- computadora está relacionada con pantalla


-- Consulta para obtener productos relacionados semánticamente
SELECT E.Nombre
FROM Empleado E
JOIN RelacionSemantica RS ON E.EmpleadoID = RS.EmpleadoID1
WHERE RS.EmpleadoID2 = (SELECT EmpleadoID FROM Empleado WHERE Nombre = 'Jose');

-- Consulta para buscar un producto por su sinónimo
SELECT E.Nombre
FROM Empleado E
JOIN Sinonimo S ON E.Nombre = S.Palabra
WHERE S.Sinonimo = 'pepe';


-- Agregar columna Full-Text
ALTER TABLE Area
ADD DescripcionFullText AS (Descripcion) PERSISTED;

-- Crear el índice Full-Text
CREATE FULLTEXT INDEX ON MiTabla(DescripcionFullText) KEY INDEX PK_MiTabla_ID;

