/*-----------------------------------------------------------------------------------------\
|         2©24 Copyright      												Quito		   |
| Todos los Derechos Reservados                                    República del Ecuador   |
|------------------------------------------------------------------------------------------|
| Este código está protegido por las leyes y tratados internacionales de derechos de autor |
| por lo que el uso, reproducción o distribución del código total o parcial no autorizada  |
| se atendrá a las sanciones contempladas en la ley con su máximo rigor.                   |
\------------------------------------------------------------------------------------------/
	- Developer		: patmic										 
	- Descripction  : Crear vistas y funciones para los esquemas 
	- Validate		: [ ] Stress o Stress concurrente       [ ] ETL       
                      [ ] Quality Data                      [X] Log & Audit   
                      [x] Standart Code 
\*----------------------------------------------------------------------------------------*/

USE CAN_DB;
GO

DROP VIEW if exists vwPais
GO
DROP VIEW if exists vwOrgAcredita
GO
DROP VIEW if exists vwEsqAcredita
GO
DROP VIEW if exists vwAlcanceRazonSocial
GO
DROP VIEW if exists vwEstado
GO

CREATE OR ALTER VIEW vwFiltro AS 
	SELECT	 IdHomologacion
			,MostrarWeb		
			,TooltipWeb	
			,MostrarWebOrden
	FROM	Homologacion		WITH(NOLOCK)
	WHERE	CodigoHomologacion	IN
	( -- codigo 		EtiquetaColumna
	 'KEY_DIM_PAI_FIL'  -- 'Pais origen'               ,1    'filtro 1')
	,'KEY_DIM_ORG_FIL'  -- 'Organismo de Acreditación' ,2    'filtro 2')
	,'KEY_DIM_ESQ_FIL'  -- 'Esquena de Acreditación'   ,3    'filtro 3')
	,'KEY_DIM_NOR_FIL'  -- 'Norma Acreditada'          ,4    'filtro 4')
	,'KEY_DIM_EST_FIL'  -- 'Estado'                    ,5    'filtro 5')
	,'KEY_DIM_REC_FIL'  -- 'Reconocimiento'            ,6    'filtro 6')
	)
GO

CREATE OR ALTER VIEW vwDimension AS 
	SELECT   H.IdHomologacion
			,H.NombreHomologado
			,H.MostrarWeb
			,H.TooltipWeb
			,H.MostrarWebOrden
			,H.MascaraDato
			,H.SiNoHayDato
			,H.NombreHomologado + ' / ' + H.MostrarWeb AS CustomMostrarWeb
	FROM    Homologacion H		WITH (NOLOCK)
	JOIN	(	SELECT DISTINCT IdHomologacion
				FROM	Homologacion		WITH (NOLOCK)
				WHERE	CodigoHomologacion  NOT IN ('KEY_DIM_PAI_FIL', 'KEY_DIM_ORG_FIL')
			)   HG	ON H.IdHomologacionGrupo = HG.IdHomologacion
GO

CREATE OR ALTER VIEW vwGrilla AS 
	SELECT	 H.IdHomologacion
			,H.MostrarWeb
			,H.TooltipWeb
			,H.MostrarWebOrden
	FROM	Homologacion	H	WITH (NOLOCK)
	JOIN	(	SELECT DISTINCT IdHomologacion
				FROM  OPENJSON((	SELECT TOP 1 EsquemaJson
									FROM HomologacionEsquema WITH (NOLOCK)
									ORDER BY MostrarWebOrden
				))
				WITH (IdHomologacion INT '$.IdHomologacion')
			)	HE	ON HE.IdHomologacion = H.IdHomologacion
GO

CREATE OR ALTER FUNCTION fnFiltroDetalle (	@IdHomologacionGrupo	INT )  
RETURNS TABLE AS
RETURN
	SELECT	IdHomologacion,     MostrarWeb,     TooltipWeb,	MostrarWebOrden
	FROM	dbo.Homologacion    WITH (NOLOCK)
	WHERE	IdHomologacionGrupo = @IdHomologacionGrupo and Estado = 'A'
GO

CREATE OR ALTER FUNCTION fnHomologacionEsquemaCampo ( @IdHomologacionEsquema INT )  
--RETURNS TABLE AS
--RETURN
RETURNS NVARCHAR(MAX) AS
BEGIN	
	DECLARE @json NVARCHAR(MAX) ='{}';
	SELECT	@json = (	SELECT	 H.IdHomologacion
								,H.MostrarWeb
								,H.TooltipWeb
								,H.MostrarWebOrden
								,H.NombreHomologado
						FROM	Homologacion	H	WITH (NOLOCK)
						JOIN	(	SELECT DISTINCT IdHomologacion
									FROM  OPENJSON((	SELECT	EsquemaJson
														FROM	HomologacionEsquema		WITH (NOLOCK)
														WHERE	IdHomologacionEsquema = @IdHomologacionEsquema
												  ))
									WITH (IdHomologacion INT '$.IdHomologacion')
								)	HE	ON HE.IdHomologacion = H.IdHomologacion
						FOR JSON AUTO 
					);
	RETURN @json;
END;
GO

CREATE OR ALTER FUNCTION fnHomologacionEsquema ( @IdHomologacionEsquema	INT )  
RETURNS TABLE AS
RETURN
	SELECT	 IdHomologacionEsquema	
			,MostrarWebOrden	
			,MostrarWeb	
			,TooltipWeb	
			,(select dbo.fnHomologacionEsquemaCampo(IdHomologacionEsquema)) EsquemaJson
	FROM	HomologacionEsquema		WITH (NOLOCK)
	WHERE	IdHomologacionEsquema = @IdHomologacionEsquema	
	AND		Estado = 'A'
GO

CREATE OR ALTER FUNCTION fnHomologacionEsquemaTodo ( )  
RETURNS TABLE AS
RETURN
	SELECT	 IdHomologacionEsquema	
			,MostrarWebOrden	
			,MostrarWeb	
			,TooltipWeb	
	FROM	HomologacionEsquema		WITH (NOLOCK)
	WHERE	MostrarWebOrden > 1	
	AND		Estado = 'A'
GO

CREATE OR ALTER FUNCTION fnHomologacionEsquemaDato (  @IdHomologacionEsquema INT, @IdDataLakeOrganizacion INT )  
RETURNS TABLE AS
RETURN
	SELECT	 IdDataLakeOrganizacion	
			,IdHomologacionEsquema
			,DataEsquemaJson
	FROM	DataLakeOrganizacion	 WITH (NOLOCK)	
	WHERE	IdDataLakeOrganizacion = @IdDataLakeOrganizacion
	AND		IdHomologacionEsquema  = @IdHomologacionEsquema
	AND		Estado				   = 'A'
GO

--	para las mostrar los esquemas
--	select * from fnHomologacionEsquemaTodo()		 -- trae todos los esquemas
--	select * from fnHomologacionEsquema (3)			 -- trae las cabeceras de un esquema y columnas

--	select * from fnHomologacionEsquemaDato (3, 7)   -- trae la data a mostrar (IdDataLakeOrganizacion , @IdHomologacionEsquema )
