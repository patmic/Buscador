USE CAN_DB;
GO

CREATE OR ALTER VIEW vwPais AS 
SELECT 
    IdHomologacion,
    MostrarWeb
FROM Homologacion WITH (NOLOCK) 
WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_PAI');
GO

CREATE OR ALTER VIEW vwOrgAcredita AS 
SELECT 
    IdHomologacion,
    MostrarWeb
FROM Homologacion WITH (NOLOCK) 
WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_ORG');
GO

CREATE OR ALTER VIEW vwEsqAcredita AS 
SELECT 
    IdHomologacion,
    MostrarWeb
FROM Homologacion WITH (NOLOCK) 
WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_ESQ');
GO

CREATE OR ALTER VIEW vwRazonSocial AS 
SELECT 
    IdHomologacion,
    MostrarWeb
FROM Homologacion WITH (NOLOCK) 
WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_RAL');
GO

CREATE OR ALTER VIEW vwAlcance AS 
SELECT 
    IdHomologacion,
    MostrarWeb
FROM Homologacion WITH (NOLOCK) 
WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_ALC');
GO

CREATE OR ALTER VIEW vwEstado AS 
SELECT 
    IdHomologacion,
    MostrarWeb
FROM Homologacion WITH (NOLOCK) 
WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_EST');
GO