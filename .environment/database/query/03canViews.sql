USE CAN_DB;
GO

CREATE OR ALTER VIEW vwPais AS 
SELECT 
    IdHomologacion,
    MostrarWeb,
    Descripcion
FROM Homologacion WITH (NOLOCK) 
WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_PAI');
GO

CREATE OR ALTER VIEW vwOrgAcredita AS 
SELECT 
    IdHomologacion,
    MostrarWeb,
    Descripcion
FROM Homologacion WITH (NOLOCK) 
WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_ORG');
GO

CREATE OR ALTER VIEW vwEsqAcredita AS 
SELECT 
    IdHomologacion,
    MostrarWeb,
    Descripcion
FROM Homologacion WITH (NOLOCK) 
WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_ESQ');
GO

CREATE OR ALTER VIEW vwAlcanceRazonSocial AS 
SELECT 
    IdHomologacion,
    MostrarWeb,
    Descripcion
FROM Homologacion WITH (NOLOCK) 
WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE ClaveBuscar = 'KEY_RAL');
GO

CREATE OR ALTER VIEW vwEstado AS 
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

CREATE OR ALTER VIEW vwGrilla AS 
SELECT	IdHomologacion, MostrarWeb, Descripcion, '1' MostarNivel
FROM	dbo.Homologacion WITH (NOLOCK)
WHERE	ClaveBuscar 
IN ( -- codigo			EtiquetaColumna
		 'KEY_DIM_cou'	-- country
		,'KEY_DIM_cit'	-- city
		,'KEY_DIM_are'	-- area
		,'KEY_DIM_act'	-- activity
		,'KEY_DIM_sta'	-- status
)
UNION  
SELECT	IdHomologacion, MostrarWeb, Descripcion, '2'  -- Esquemas
FROM	dbo.Homologacion WITH (NOLOCK)
WHERE	ClaveBuscar 
IN ( -- codigo			EtiquetaColumna
		'KEY_DIM_xxx'	-- countrycode
		,'KEY_DIM_acc'	-- accreditationcode
		,'KEY_DIM_bus'	-- business
		,'KEY_DIM_cer'	-- certpdf
);
GO

CREATE OR ALTER VIEW vwFiltro AS 
SELECT	IdHomologacion, MostrarWeb, Descripcion
FROM	dbo.Homologacion WITH (NOLOCK)
WHERE	ClaveBuscar 
IN ( -- codigo 		EtiquetaColumna
		 'KEY_PAI'	-- PAIS	
		,'KEY_ORG'	-- ORGANIZACION ACREDITADA	 
		,'KEY_ESQ'	-- ESQUEMA ACREDITACION	 
		,'KEY_RAL'	-- TIPO ALCANCE RAZ SOC					 
		,'KEY_EST'	-- ESTADO					 
);
GO

CREATE OR ALTER FUNCTION fnFiltroDetalle 
(	@IdHomologacionGrupo	int )  
RETURNS TABLE AS
RETURN
SELECT	IdHomologacion, MostrarWeb, Descripcion
FROM	dbo.Homologacion WITH (NOLOCK)
WHERE	IdHomologacionGrupo = @IdHomologacionGrupo;
GO

-- select * from fnFiltroDetalle(3)

select * from vwFiltro