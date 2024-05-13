USE CAN_DB;
GO

-- CREATE OR ALTER VIEW vwPais AS 
-- SELECT 
--     IdHomologacion,
--     MostrarWeb,
--     Descripcion
-- FROM Homologacion WITH (NOLOCK) 
-- WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE CodigoHomologacion = 'KEY_DIM_PAI_FIL')
-- ORDER BY MostrarWebOrden;
-- GO

-- CREATE OR ALTER VIEW vwOrgAcredita AS 
-- SELECT 
--     IdHomologacion,
--     MostrarWeb,
--     Descripcion
-- FROM Homologacion WITH (NOLOCK) 
-- WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE CodigoHomologacion = 'KEY_DIM_ORG_FIL')
-- ORDER BY MostrarWebOrden;
-- GO

-- CREATE OR ALTER VIEW vwEsqAcredita AS 
-- SELECT 
--     IdHomologacion,
--     MostrarWeb,
--     Descripcion
-- FROM Homologacion WITH (NOLOCK) 
-- WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE CodigoHomologacion = 'KEY_DIM_ESQ_FIL')
-- ORDER BY MostrarWebOrden;
-- GO

-- CREATE OR ALTER VIEW vwAlcanceRazonSocial AS   --cambiar nombre
-- SELECT 
--     IdHomologacion,
--     MostrarWeb,
--     Descripcion
-- FROM Homologacion WITH (NOLOCK) 
-- WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE CodigoHomologacion = 'KEY_DIM_NOR_FIL')
-- ORDER BY MostrarWebOrden;
-- GO

-- CREATE OR ALTER VIEW vwEstado AS 
-- SELECT 
--     IdHomologacion,
--     MostrarWeb,
--     Descripcion
-- FROM Homologacion WITH (NOLOCK) 
-- WHERE IdHomologacionGrupo = (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE CodigoHomologacion = 'KEY_DIM_EST_FIL')
-- ORDER BY MostrarWebOrden;
-- GO

CREATE OR ALTER VIEW vwDimension AS 
SELECT 
    IdHomologacion
    ,MostrarWeb
    ,Descripcion
	,MostrarWebOrden
FROM Homologacion WITH (NOLOCK) 
WHERE IdHomologacionGrupo IN (SELECT IdHomologacion FROM Homologacion WITH (NOLOCK) WHERE CodigoHomologacion = 'KEY_DIM_GRILLA')
GO

CREATE OR ALTER VIEW vwGrilla AS 
SELECT	IdHomologacion, MostrarWeb, Descripcion, '1' MostarNivel
FROM	dbo.Homologacion WITH (NOLOCK)
WHERE	CodigoHomologacion 
IN ( -- codigo			EtiquetaColumna
		 'KDG_ORG'		--,1  ,'Organismo'		
		,'KDG_REC'		--,2  ,'Reconocimiento'	
		,'KDG_RAS'		--,3  ,'OEC/Razón social'	
		,'KDG_PAI'		--,4  ,'País'				
		,'KDG_ESQ'		--,5  ,'Esquema'			
		,'KDG_NOR'		--,6  ,'Norma'			
		,'KDG_IDE'		--,7  ,'Identificación'	
		,'KDG_EST'		--,8  ,'Estado'			
		,'KDG_ALC_DET'  --,9  ,'Alcance Detalle'	
		,'KDG_ALC_ACR'  --,10 ,'Alcance acreditado'
)
--order by MostrarWebOrden	
UNION  
SELECT	IdHomologacion, MostrarWeb, Descripcion, '2'  -- Esquemas
FROM	dbo.Homologacion WITH (NOLOCK)
WHERE	CodigoHomologacion 
IN ( -- codigo			EtiquetaColumna
		  'KDG_can_pai' --,11  ,'oa_pais'            
		 ,'KDG_can_ini' --,12  ,'oa_inicialpais'     
		 ,'KDG_can_ciu' --,13  ,'oa_ciudad'          
		 ,'KDG_can_nom' --,14  ,'oa_nombre'          
		 ,'KDG_can_noi' --,15  ,'oa_nombreInicial'   
		 ,'KDG_can_dir' --,16  ,'oa_direcion'        
		 ,'KDG_can_tel' --,17  ,'oa_telefono'        
		 ,'KDG_can_sit' --,18  ,'oa_sitioweb'        
		 ,'KDG_can_log' --,19  ,'oa_logo'            
		 ,'KDG_can_des' --,20  ,'oa_descripcion'     
)
--order by MostrarWebOrden	;
GO

CREATE OR ALTER VIEW vwFiltro AS 
SELECT	IdHomologacion,     MostrarWeb		,Descripcion	,MostrarWebOrden
FROM	dbo.Homologacion    WITH(NOLOCK)
WHERE	CodigoHomologacion	IN
( -- codigo 		EtiquetaColumna
     'KEY_DIM_PAI_FIL'  -- 'Por Pais origen'               ,1    'filtro 1')
    ,'KEY_DIM_ORG_FIL'  -- 'Por Organismo de Acreditación' ,2    'filtro 2')
    ,'KEY_DIM_ESQ_FIL'  -- 'Por Esquena de Acreditación'   ,3    'filtro 3')
    ,'KEY_DIM_NOR_FIL'  -- 'Por Norma Acreditada'          ,4    'filtro 4')
    ,'KEY_DIM_EST_FIL'  -- 'Por Estado'                    ,5    'filtro 5')
    ,'KEY_DIM_REC_FIL'  -- 'Por Reconocimiento'            ,6    'filtro 6')
)
GO

CREATE OR ALTER FUNCTION fnFiltroDetalle 
(	@IdHomologacionGrupo	INT )  
RETURNS TABLE AS
RETURN
SELECT	IdHomologacion,     MostrarWeb,     Descripcion,	MostrarWebOrden
FROM	dbo.Homologacion    WITH (NOLOCK)
WHERE	IdHomologacionGrupo = @IdHomologacionGrupo
GO


--testing
-- select * from vwGrilla
--select * from vwDimension

-- select * from fnFiltroDetalle(3)
-- select * from vwFiltro