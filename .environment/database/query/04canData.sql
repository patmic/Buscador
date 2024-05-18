/*-----------------------------------------------------------------------------------------\
|         2©24 Copyright      												Quito		   |
| Todos los Derechos Reservados                                    República del Ecuador   |
|------------------------------------------------------------------------------------------|
| Este código está protegido por las leyes y tratados internacionales de derechos de autor |
| por lo que el uso, reproducción o distribución del código total o parcial no autorizada  |
| se atendrá a las sanciones contempladas en la ley con su máximo rigor.                   |
\------------------------------------------------------------------------------------------/
	- Developer		: patmic										 
	- Descripction  : datos base
	- Validate		: [ ] Stress o Stress concurrente       [ ] ETL       
                      [ ] Quality Data                      [X] Log & Audit   
                      [x] Standart Code 
\*----------------------------------------------------------------------------------------*/

USE CAN_DB;
GO
-- ingresando grupos base del sistema
INSERT INTO Homologacion    
 (CodigoHomologacion, MostrarWebOrden,	MostrarWeb,	TooltipWeb) VALUES
 ('KEY_DIM_ESQUEMA'		,00    ,'CAMPOS ESQUEMA','Campos de los esquemas a migrar')
,('KEY_DIM_PAI_FIL'		,01    ,'PAIS ONA'		,'Filtro 1')
,('KEY_DIM_ORG_FIL'		,02    ,'ORGANISMO'		,'Filtro 2')
,('KEY_DIM_ESQ_FIL'		,03    ,'ESQUEMA '		,'Filtro 3')
,('KEY_DIM_NOR_FIL'		,04    ,'NORMA'			,'Filtro 4')
,('KEY_DIM_EST_FIL'		,05    ,'ESTADO'		,'Filtro 5')
,('KEY_DIM_REC_FIL'		,06    ,'RECOMOCIMIENTO','Filtro 6')
GO

--filtros base
DECLARE  
 @KEY_DIM_ESQUEMA INT = (select IdHomologacion from Homologacion where CodigoHomologacion = 'KEY_DIM_ESQUEMA')
,@KEY_DIM_PAI_FIL INT = (select IdHomologacion from Homologacion where CodigoHomologacion = 'KEY_DIM_PAI_FIL')
,@KEY_DIM_ORG_FIL INT = (select IdHomologacion from Homologacion where CodigoHomologacion = 'KEY_DIM_ORG_FIL')
,@KEY_DIM_ESQ_FIL INT = (select IdHomologacion from Homologacion where CodigoHomologacion = 'KEY_DIM_ESQ_FIL')
,@KEY_DIM_NOR_FIL INT = (select IdHomologacion from Homologacion where CodigoHomologacion = 'KEY_DIM_NOR_FIL')
,@KEY_DIM_EST_FIL INT = (select IdHomologacion from Homologacion where CodigoHomologacion = 'KEY_DIM_EST_FIL')
,@KEY_DIM_REC_FIL INT = (select IdHomologacion from Homologacion where CodigoHomologacion = 'KEY_DIM_REC_FIL')
,@JSON_PAI NVARCHAR(500) ='{"Responsable":"no definido" ,"Identificación":"" ,"Teléfono":"no definido" ,"Correo Electrónico":"" }'
,@JSON_ORG NVARCHAR(500) ='{"ONA":"" ,"Identificación":"" ,"Razón social":"" ,"País":"" ,"Dirección":"" ,"Teléfono":"" ,"Pagina web":"" ,"Correo Electrónico":"" }'
-- Datos de los filtros
INSERT INTO Homologacion 
(IdHomologacionGrupo, MostrarWebOrden, MostrarWeb, InfoExtraJson)    VALUES 
--NIVEL 1
 (@KEY_DIM_PAI_FIL  ,1   ,'Bolivia'			,@JSON_PAI)
,(@KEY_DIM_PAI_FIL  ,2   ,'Colombia'		,@JSON_PAI)	
,(@KEY_DIM_PAI_FIL  ,3   ,'Ecuador'			,@JSON_PAI)	
,(@KEY_DIM_PAI_FIL  ,4   ,'Perú'			,@JSON_PAI)	
--NIVEL 2
,(@KEY_DIM_ORG_FIL  ,1   ,'DTA-IBMETRO'		,@JSON_ORG)
,(@KEY_DIM_ORG_FIL  ,2   ,'CONAC'			,@JSON_ORG)
,(@KEY_DIM_ORG_FIL  ,3   ,'SAE'				,@JSON_ORG)
,(@KEY_DIM_ORG_FIL  ,4   ,'INACAL-DA'		,@JSON_ORG)
--NIVEL 3
,(@KEY_DIM_ESQ_FIL  ,1  ,'Organismo Certificación de Sistemas de Gestión'  ,'{}')
,(@KEY_DIM_ESQ_FIL  ,2  ,'Organismo Certificación de Producto'             ,'{}')
,(@KEY_DIM_ESQ_FIL  ,3  ,'Organismo Certificación de Personas'             ,'{}')
,(@KEY_DIM_ESQ_FIL  ,4  ,'Organismo Inspección'                            ,'{}')
,(@KEY_DIM_ESQ_FIL  ,5  ,'Laboratorios Ensayo'                             ,'{}')
,(@KEY_DIM_ESQ_FIL  ,6  ,'Laboratorios Calibración'                        ,'{}')
,(@KEY_DIM_ESQ_FIL  ,7  ,'Laboratorios Clínicos'                           ,'{}')
,(@KEY_DIM_ESQ_FIL  ,8  ,'Proveedores  Ensayos de aptitud'                 ,'{}')
--NIVEL 4 	
,(@KEY_DIM_NOR_FIL  ,1  ,'ISO/IEC 17021-1'	               ,'{}')
,(@KEY_DIM_NOR_FIL  ,2  ,'ISO/IEC 71067'	               ,'{}')
,(@KEY_DIM_NOR_FIL  ,3  ,'ISO/IEC 17024'	               ,'{}')
,(@KEY_DIM_NOR_FIL  ,4  ,'ISO/IEC 17020'	               ,'{}')
,(@KEY_DIM_NOR_FIL  ,5  ,'ISO/IEC 17025'	               ,'{}')
,(@KEY_DIM_NOR_FIL  ,6  ,'ISO 15189'		               ,'{}')
,(@KEY_DIM_NOR_FIL  ,7  ,'ISO 17034'		               ,'{}')
,(@KEY_DIM_NOR_FIL  ,8  ,'ISO/IEC 17043'	               ,'{}')
,(@KEY_DIM_NOR_FIL  ,9  ,'ISO/IEC 17029'	               ,'{}')
--NIVEL 5 
,(@KEY_DIM_EST_FIL  ,1  ,'Acreditado'		               ,'{}')
,(@KEY_DIM_EST_FIL  ,2  ,'Suspendido'	                   ,'{}')
,(@KEY_DIM_EST_FIL  ,3  ,'Suspendido Parcialmente'	       ,'{}')
,(@KEY_DIM_EST_FIL  ,4  ,'Retirado'		                   ,'{}')
,(@KEY_DIM_EST_FIL  ,5  ,'Inactivo'		                   ,'{}')
,(@KEY_DIM_EST_FIL  ,6  ,'Vencido'		                   ,'{}')
--NIVEL 6
,(@KEY_DIM_REC_FIL  ,1  ,'Nacional'							,'{}')	
,(@KEY_DIM_REC_FIL  ,2  ,'Internacional'					,'{}')	
--EsquemaGRILLA    
,(@KEY_DIM_ESQUEMA  ,1  ,'Organismo'						,'{}')	  
,(@KEY_DIM_ESQUEMA  ,2  ,'Reconocimiento'					,'{}')
,(@KEY_DIM_ESQUEMA  ,3  ,'OEC/Razón social'					,'{}') --*
,(@KEY_DIM_ESQUEMA  ,4  ,'País'								,'{}')
,(@KEY_DIM_ESQUEMA  ,5  ,'Esquema'							,'{}')
,(@KEY_DIM_ESQUEMA  ,6  ,'Norma'							,'{}')
,(@KEY_DIM_ESQUEMA  ,7  ,'Identificación'					,'{}') --*
,(@KEY_DIM_ESQUEMA  ,8  ,'Estado'							,'{}')
,(@KEY_DIM_ESQUEMA  ,9  ,'Alcance Detalle'					,'{}') --*
,(@KEY_DIM_ESQUEMA  ,10 ,'Alcance acreditado'				,'{}') --*
--Esquema
,(@KEY_DIM_ESQUEMA  ,1  ,'oa_pais'							,'{}') 
,(@KEY_DIM_ESQUEMA  ,2  ,'oa_inicialpais'					,'{}') 
,(@KEY_DIM_ESQUEMA  ,3  ,'oa_ciudad'						,'{}') 
,(@KEY_DIM_ESQUEMA  ,4  ,'oa_nombre'						,'{}') 
,(@KEY_DIM_ESQUEMA  ,5  ,'oa_nombreInicial'					,'{}') 
,(@KEY_DIM_ESQUEMA  ,6  ,'oa_direcion'						,'{}') 
,(@KEY_DIM_ESQUEMA  ,7  ,'oa_telefono'						,'{}') 
,(@KEY_DIM_ESQUEMA  ,8  ,'oa_sitioweb'						,'{}') 
,(@KEY_DIM_ESQUEMA  ,9  ,'oa_logo'							,'{}') 
,(@KEY_DIM_ESQUEMA  ,10 ,'oa_descripcion'					,'{}') 
GO
--select * from  Homologacion

INSERT INTO HomologacionEsquema 
( MostrarWebOrden ,MostrarWeb	,TooltipWeb		,EsquemaJson) VALUES
 (	1			  ,'GRILLA'		,'GRILLA'		,'[{"IdHomologacion": "41"},{"IdHomologacion": "42"},{"IdHomologacion": "43"},{"IdHomologacion": "44"},{"IdHomologacion": "46"},{"IdHomologacion": "45"},{"IdHomologacion": "46"},{"IdHomologacion": "47"}]')
,(	2			  ,'ESQ_01'		,'ESQ_01'		,'[{"IdHomologacion": "51"},{"IdHomologacion": "52"},{"IdHomologacion": "53"}]')
,(	3			  ,'ESQ_02'		,'ESQ_02'		,'[{"IdHomologacion": "55"},{"IdHomologacion": "56"},{"IdHomologacion": "57"}]')
GO
--select * from  HomologacionEsquema

INSERT INTO [OrganizacionFullText] 
([IdDataLakeOrganizacion] ,[IdHomologacion] ,[FullTextOrganizacion]) VALUES
 (1, 41	,'1 Organismo	  ')
,(1, 42	,'1 Reconocimiento')
,(1, 43	,'1 OEC/Razón soci')
,(1, 44	,'1 País	      ')
,(1, 45	,'1 Esquema		  ')
,(1, 46	,'1 Norma		  ')
,(1, 47	,'1 Identificación')
,(1, 48	,'1 Estado		  ')
,(1, 49	,'1 Alcance Detall')
,(1, 50	,'1 Alcance acredi')
,(1, 51	,'1 oa_pais		  ')
,(1, 52	,'1 oa_inicialpais')
,(1, 53	,'1 oa_ciudad	  ')
,(1, 54	,'1 oa_nombre	  ')
,(1, 55	,'1 oa_nombreInici')
,(1, 56	,'1 oa_direcion	  ')
,(1, 57	,'1 oa_telefono	  ')
,(1, 58	,'1 oa_sitioweb	  ')
,(1, 59	,'1 oa_logo		  ')
,(1, 60	,'1 oa_descripcion')

,(2, 41	,'2 Organismo	  ')
,(2, 42	,'2 Reconocimiento')
,(2, 43	,'2 OEC/Razón soci')
,(2, 44	,'2 País	      ')
,(2, 45	,'2 Esquema		  ')
,(2, 46	,'2 Norma		  ')
,(2, 47	,'2 Identificación')
,(2, 48	,'2 Estado		  ')
,(2, 49	,'2 Alcance Detall')
,(2, 50	,'2 Alcance acredi')
,(2, 51	,'2 oa_pais		  ')
,(2, 52	,'2 oa_inicialpais')
,(2, 53	,'2 oa_ciudad	  ')
,(2, 54	,'2 oa_nombre	  ')
,(2, 55	,'2 oa_nombreInici')
,(2, 56	,'2 oa_direcion	  ')
,(2, 57	,'2 oa_telefono	  ')
,(2, 58	,'2 oa_sitioweb	  ')
,(2, 59	,'2 oa_logo		  ')
,(2, 60	,'2 oa_descripcion')

,(3, 41	,'3 Organismo	  ')
,(3, 42	,'3 Reconocimiento')
,(3, 43	,'3 OEC/Razón soci')
,(3, 44	,'3 País	      ')
,(3, 45	,'3 Esquema		  ')
,(3, 46	,'3 Norma		  ')
,(3, 47	,'3 Identificación')
,(3, 48	,'3 Estado		  ')
,(3, 49	,'3 Alcance Detall')
,(3, 50	,'3 Alcance acredi')
,(3, 51	,'3 oa_pais		  ')
,(3, 52	,'3 oa_inicialpais')
,(3, 53	,'3 oa_ciudad	  ')
,(3, 54	,'3 oa_nombre	  ')
,(3, 55	,'3 oa_nombreInici')
,(3, 56	,'3 oa_direcion	  ')
,(3, 57	,'3 oa_telefono	  ')
,(3, 58	,'3 oa_sitioweb	  ')
,(3, 59	,'3 oa_logo		  ')
,(3, 60	,'3 oa_descripcion')

,(4, 41	,'4 Organismo	  ')
,(4, 42	,'4 Reconocimiento')
,(4, 43	,'4 OEC/Razón soci')
,(4, 44	,'4 País	      ')
,(4, 45	,'4 Esquema		  ')
,(4, 46	,'4 Norma		  ')
,(4, 47	,'4 Identificación')
,(4, 48	,'4 Estado		  ')
,(4, 49	,'4 Alcance Detall')
,(4, 50	,'4 Alcance acredi')
,(4, 51	,'4 oa_pais		  ')
,(4, 52	,'4 oa_inicialpais')
,(4, 53	,'4 oa_ciudad	  ')
,(4, 54	,'4 oa_nombre	  ')
,(4, 55	,'4 oa_nombreInici')
,(4, 56	,'4 oa_direcion	  ')
,(4, 57	,'4 oa_telefono	  ')
,(4, 58	,'4 oa_sitioweb	  ')
,(4, 59	,'4 oa_logo		  ')
,(4, 60	,'4 oa_descripcion')

,(5, 41	,'5 Organismo	  ')
,(5, 42	,'5 Reconocimiento')
,(5, 43	,'5 OEC/Razón soci')
,(5, 44	,'5 País	      ')
,(5, 45	,'5 Esquema		  ')
,(5, 46	,'5 Norma		  ')
,(5, 47	,'5 Identificación')
,(5, 48	,'5 Estado		  ')
,(5, 49	,'5 Alcance Detall')
,(5, 50	,'5 Alcance acredi')
,(5, 51	,'5 oa_pais		  ')
,(5, 52	,'5 oa_inicialpais')
,(5, 53	,'5 oa_ciudad	  ')
,(5, 54	,'5 oa_nombre	  ')
,(5, 55	,'5 oa_nombreInici')
,(5, 56	,'5 oa_direcion	  ')
,(5, 57	,'5 oa_telefono	  ')
,(5, 58	,'5 oa_sitioweb	  ')
,(5, 59	,'5 oa_logo		  ')
,(5, 60	,'5 oa_descripcion')
;
GO

DECLARE @CLAVE NVARCHAR(32);
SET @CLAVE = (SELECT LOWER(CONVERT(NVARCHAR(32), HASHBYTES('MD5', 'usuario23'), 2)));

INSERT INTO [Usuario](Email, Nombre, Apellido, Telefono, Clave, Rol, IdUserCreacion, IdUserModifica)
VALUES ('admin@gmail.com', 'admin', 'admin', '593961371400', @CLAVE, 'ADMIN', 0, 0);

-- select * from Usuario

-- INSERT INTO `accreditationoac_v`(`oac_country`
-- `oac_countrycode`
-- `oac_city`
-- `oac_business_name`
-- `oac_area`
-- `oac_activity`
-- `oac_accreditationcode`
-- `oac_status`
-- `oac_certpdf`
-- `level_2`
-- `level_3`
-- `level_4`
-- `level_5`
-- `level_6`
-- `level_7`
-- `level_8`) 
-- VALUES('Ecuador'
--         'EC'
--         'Quito'
--         'VETBLUE CIA. LTDA.'
--         'INSPECCIÓN'
--         'INSPECCION'
--         'E-SAE-OI-21-006'
--         'Retiro'
--         'https://sisac.acreditacion.gob.ec/storage/users/vetbluecf/accreditation_application/21/certificate_accreditation_symbolCertificado_de_acreditacion-2023-10-12 12:05:48.pdf'
--         'Pecuario'
--         'Inspección de animales y rebaños condición genética A2A2'
--         'Animales y rebaños'
--         'Visual
--         Documental'
--         'A','<p><span style="color: rgb(0, 0, 153);">Procedimiento de Inspección A2A2 P/A2</span></p>','<p><span style="color: rgb(0, 0, 153);">Normativa para la Certificación de Animales y Rebaños A2A2. Acuerdo Ministerial No. 61 expedido el 05 de noviembre de 2021</span></p>');


