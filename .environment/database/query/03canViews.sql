USE CAN_DB;
GO


DROP VIEW IF EXISTS vwPais;
GO
CREATE VIEW vwPais AS 
SELECT 
    IdHomologacion,
    MostrarWeb,
    Descripcion
FROM Homologacion WITH (NOLOCK) WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_PAI');
GO

DROP VIEW IF EXISTS vwOrgAcredita;
GO
CREATE VIEW vwOrgAcredita AS 
SELECT 
    IdHomologacion,
    MostrarWeb,
    Descripcion
FROM Homologacion WITH (NOLOCK) WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_ORG');
GO

DROP VIEW IF EXISTS vwEsqAcredita;
GO
CREATE VIEW vwEsqAcredita AS 
SELECT 
    IdHomologacion,
    MostrarWeb,
    Descripcion
FROM Homologacion WITH (NOLOCK) WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_ESQ');
GO

DROP VIEW IF EXISTS vwTipoAcreditacion;
GO
CREATE VIEW vwTipoAcreditacion AS 
SELECT 
    IdHomologacion,
    MostrarWeb,
    Descripcion
FROM Homologacion WITH (NOLOCK) WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_RAL');
GO

DROP VIEW IF EXISTS vwEstado;
GO
CREATE VIEW vwEstado AS 
SELECT 
    IdHomologacion,
    MostrarWeb,
    Descripcion
FROM Homologacion WITH (NOLOCK) WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_EST');
GO

DROP VIEW IF EXISTS vwDimension;
GO
CREATE VIEW vwDimension AS 
SELECT 
    IdHomologacion,
    MostrarWeb,
    Descripcion
FROM Homologacion WITH (NOLOCK) WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_DIM');
GO

DROP VIEW IF EXISTS vwGrilla;
GO
CREATE VIEW vwGrilla AS 
SELECT 
    IdHomologacion,
    MostrarWeb,
    Descripcion
FROM Homologacion WITH (NOLOCK) WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_DIM');
GO