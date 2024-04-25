USE CAN_DB;
GO


DROP VIEW IF EXISTS vwPais;
GO
CREATE VIEW vwPais AS 
SELECT 
    IdHomologacion,
    BusquedaEtiqueta
FROM Homologacion WITH (NOLOCK) WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE BusquedaCodigo = 'KEY_PAI');
GO

DROP VIEW IF EXISTS vwOrgAcredita;
GO
CREATE VIEW vwOrgAcredita AS 
SELECT 
    IdHomologacion,
    BusquedaEtiqueta
FROM Homologacion WITH (NOLOCK) WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE BusquedaCodigo = 'KEY_ORG_ACREDITA');
GO

DROP VIEW IF EXISTS vwEsqAcredita;
GO
CREATE VIEW vwEsqAcredita AS 
SELECT 
    IdHomologacion,
    BusquedaEtiqueta
FROM Homologacion WITH (NOLOCK) WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE BusquedaCodigo = 'KEY_ESQ_ACREDITA');
GO

DROP VIEW IF EXISTS vwRazonSocial;
GO
CREATE VIEW vwRazonSocial AS 
SELECT 
    IdHomologacion,
    BusquedaEtiqueta
FROM Homologacion WITH (NOLOCK) WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE BusquedaCodigo = 'KEY_RAZ_SOCIAL');
GO

DROP VIEW IF EXISTS vwAlcance;
GO
CREATE VIEW vwAlcance AS 
SELECT 
    IdHomologacion,
    BusquedaEtiqueta
FROM Homologacion WITH (NOLOCK) WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE BusquedaCodigo = 'KEY_ALC');
GO

DROP VIEW IF EXISTS vwEstado;
GO
CREATE VIEW vwEstado AS 
SELECT 
    IdHomologacion,
    BusquedaEtiqueta
FROM Homologacion WITH (NOLOCK) WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE BusquedaCodigo = 'KEY_EST');
GO