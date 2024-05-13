USE CAN_DB;
GO

 --   ,IdHomologacionGrupo	INT DEFAULT(NULL) 
 --   ,CodigoHomologacion		NVARCHAR(20)  NOT NULL
 --   ,NombreNombreHomologado	NVARCHAR(90)  NOT NULL
 --   ,MostrarWebOrden		INT DEFAULT(0) 
 --   ,MostrarWeb				NVARCHAR(90)  NOT NULL
	--,Descripcion			NVARCHAR(200)
 --   ,InfoExtraJson			NVARCHAR(max)
 --   ,FechaCrea				DATETIME	NOT NULL DEFAULT(GETDATE())
 --   ,FechaModifica			DATETIME	NOT NULL DEFAULT(GETDATE())  
 --   ,IdUserCrea				INT			NOT NULL DEFAULT(0)
 --   ,IdUserModifica			INT			NOT NULL DEFAULT(0)  

--filtros base
DECLARE  @KEY_DIM_PAI_FIL INT
        ,@KEY_DIM_ORG_FIL INT
        ,@KEY_DIM_ESQ_FIL INT
        ,@KEY_DIM_NOR_FIL INT
        ,@KEY_DIM_EST_FIL INT
        ,@KEY_DIM_REC_FIL INT 
        ,@KEY_DIM_GRILLA  INT 
        ,@JSON_PAI NVARCHAR(500) ='{"Responsable":"no definido" ,"Identificación":"" ,"Teléfono":"no definido" ,"Correo Electrónico":"" }'
        ,@JSON_ORG NVARCHAR(500) ='{"ONA":"" ,"Identificación":"" ,"Razón social":"" ,"País":"" ,"Dirección":"" ,"Teléfono":"" ,"Pagina web":"" ,"Correo Electrónico":"" }'

INSERT INTO Homologacion    (CodigoHomologacion,  NombreHomologado,  MostrarWebOrden,	MostrarWeb, Descripcion)
VALUES                      ('KEY_DIM_PAI_FIL', 'Por Pais origen'                ,1      ,'PAIS ONA'        ,'filtro 1')
SET @KEY_DIM_PAI_FIL = @@IDENTITY;
INSERT INTO Homologacion    (CodigoHomologacion,  NombreHomologado,  MostrarWebOrden,	MostrarWeb, Descripcion)
VALUES                      ('KEY_DIM_ORG_FIL', 'Por Organismo de Acreditación'	,2      ,'ORGANISMO'	    ,'filtro 2')
SET @KEY_DIM_ORG_FIL = @@IDENTITY;
INSERT INTO Homologacion    (CodigoHomologacion,  NombreHomologado,  MostrarWebOrden,	MostrarWeb, Descripcion)
VALUES                      ('KEY_DIM_ESQ_FIL', 'Por Esquena de Acreditación'   ,3      ,'ESQUEMA '		    ,'filtro 3')
SET @KEY_DIM_ESQ_FIL = @@IDENTITY;
INSERT INTO Homologacion    (CodigoHomologacion,  NombreHomologado,  MostrarWebOrden,	MostrarWeb, Descripcion)
VALUES                      ('KEY_DIM_NOR_FIL', 'Por Norma Acreditada'          ,4      ,'NORMA'                ,'filtro 4')
SET @KEY_DIM_NOR_FIL = @@IDENTITY;
INSERT INTO Homologacion    (CodigoHomologacion,  NombreHomologado,  MostrarWebOrden,	MostrarWeb, Descripcion)
VALUES                      ('KEY_DIM_EST_FIL', 'Por Estado'                    ,5      ,'ESTADO'               ,'filtro 5')
SET @KEY_DIM_EST_FIL = @@IDENTITY;
INSERT INTO Homologacion    (CodigoHomologacion,  NombreHomologado,  MostrarWebOrden,	MostrarWeb, Descripcion)
VALUES                      ('KEY_DIM_REC_FIL', 'Por Reconocimiento'            ,6      ,'RECOMOCIMIENTO'       ,'filtro 6')
SET @KEY_DIM_REC_FIL = @@IDENTITY;
INSERT INTO Homologacion    (CodigoHomologacion,  NombreHomologado,  MostrarWeb)
VALUES                      ('KEY_DIM_GRILLA',  'Dimensiones de la información' , 'Información homologados a mostrar en la tabla o grilla')
SET @KEY_DIM_GRILLA  = @@IDENTITY;
-- Datos de los filtros
INSERT INTO Homologacion 
(IdHomologacionGrupo, CodigoHomologacion, MostrarWebOrden, MostrarWeb, Descripcion ,NombreHomologado, InfoExtraJson)    VALUES 
--NIVEL 1
 (@KEY_DIM_PAI_FIL  ,'KDP_BOL'           ,1   ,'Bolivia'   ,'País de origen'              ,'sinHomologar' ,@JSON_PAI)
,(@KEY_DIM_PAI_FIL  ,'KDP_COL'           ,2   ,'Colombia'  ,'País de origen'              ,'sinHomologar' ,@JSON_PAI)	
,(@KEY_DIM_PAI_FIL  ,'KDP_ECU'           ,3   ,'Ecuador'   ,'País de origen'              ,'sinHomologar' ,@JSON_PAI)	
,(@KEY_DIM_PAI_FIL  ,'KDP_PER'           ,4   ,'Perú'	   ,'País de origen'              ,'sinHomologar' ,@JSON_PAI)	
--NIVEL 2
,(@KEY_DIM_ORG_FIL  ,'KDO_DTA-IBMETRO'  ,1   ,'DTA-IBMETRO' ,'Organismo de acreditación'  ,'sinHomologar' ,@JSON_ORG)
,(@KEY_DIM_ORG_FIL  ,'KDO_CONAC'        ,2   ,'CONAC'	   ,'Organismo de acreditación'   ,'sinHomologar' ,@JSON_ORG)
,(@KEY_DIM_ORG_FIL  ,'KDO_SAE'	        ,3   ,'SAE'	   ,'Organismo de acreditación'   ,'sinHomologar' ,@JSON_ORG)
,(@KEY_DIM_ORG_FIL  ,'KDO_INACAL-DA'	,4   ,'INACAL-DA'   ,'Organismo de acreditación'  ,'sinHomologar' ,@JSON_ORG)
--NIVEL 3
,(@KEY_DIM_ESQ_FIL  ,'KDE_CER_SIS'  ,1  ,'Organismo Certificación de Sistemas de Gestión' ,'Esquema de acreditación'  ,'sinHomologar' ,'{}')
,(@KEY_DIM_ESQ_FIL  ,'KDE_CER_PRO'  ,2  ,'Organismo Certificación de Producto'            ,'Esquema de acreditación'  ,'sinHomologar' ,'{}')
,(@KEY_DIM_ESQ_FIL  ,'KDE_CER_PER'  ,3  ,'Organismo Certificación de Personas'            ,'Esquema de acreditación'  ,'sinHomologar' ,'{}')
,(@KEY_DIM_ESQ_FIL  ,'KDE_INS'      ,4  ,'Organismo Inspección'                           ,'Esquema de acreditación'  ,'sinHomologar' ,'{}')
,(@KEY_DIM_ESQ_FIL  ,'KDE_LAB_ENS'  ,5  ,'Laboratorios Ensayo'                            ,'Esquema de acreditación'  ,'sinHomologar' ,'{}')
,(@KEY_DIM_ESQ_FIL  ,'KDE_LAB_CAL'  ,6  ,'Laboratorios Calibración'                       ,'Esquema de acreditación'  ,'sinHomologar' ,'{}')
,(@KEY_DIM_ESQ_FIL  ,'KDE_LAB_CLI'  ,7  ,'Laboratorios Clínicos'                          ,'Esquema de acreditación'  ,'sinHomologar' ,'{}')
,(@KEY_DIM_ESQ_FIL  ,'KDE_PRO_ENS'  ,8  ,'Proveedores  Ensayos de aptitud'                ,'Esquema de acreditación'  ,'sinHomologar' ,'{}')
--NIVEL 4 	
,(@KEY_DIM_NOR_FIL  ,'KDN_ISO_IEC_17021' ,1  ,'ISO/IEC 17021-1'	           ,'Norma de acreditación'	        ,'sinHomologar' ,'{}')	
,(@KEY_DIM_NOR_FIL  ,'KDN_ISO_IEC_71067' ,2  ,'ISO/IEC 71067'		   ,'Norma de acreditación'		,'sinHomologar' ,'{}')	
,(@KEY_DIM_NOR_FIL  ,'KDN_ISO_IEC_17024' ,3  ,'ISO/IEC 17024'		   ,'Norma de acreditación'		,'sinHomologar' ,'{}')	
,(@KEY_DIM_NOR_FIL  ,'KDN_ISO_IEC_17020' ,4  ,'ISO/IEC 17020'		   ,'Norma de acreditación'		,'sinHomologar' ,'{}')	
,(@KEY_DIM_NOR_FIL  ,'KDN_ISO_IEC_17025' ,5  ,'ISO/IEC 17025'		   ,'Norma de acreditación'		,'sinHomologar' ,'{}')	
,(@KEY_DIM_NOR_FIL  ,'KDN_ISO_15189'  	 ,6  ,'ISO 15189'		   ,'Norma de acreditación'		,'sinHomologar' ,'{}')	
,(@KEY_DIM_NOR_FIL  ,'KDN_ISO_17034'  	 ,7  ,'ISO 17034'		   ,'Norma de acreditación'		,'sinHomologar' ,'{}')	
,(@KEY_DIM_NOR_FIL  ,'KDN_ISO_IEC_17043' ,8  ,'ISO/IEC 17043'		   ,'Norma de acreditación'		,'sinHomologar' ,'{}')	
,(@KEY_DIM_NOR_FIL  ,'KDN_ISO_IEC_17029' ,9  ,'ISO/IEC 17029'		   ,'Norma de acreditación'		,'sinHomologar' ,'{}')	
--NIVEL 5 
,(@KEY_DIM_EST_FIL  ,'KDS_ACR'		,1  ,'Acreditado'		   ,'Acreditado'			,'sinHomologar' ,'{}')	
,(@KEY_DIM_EST_FIL  ,'KDS_SUS'		,2  ,'Suspendido'	           ,'Suspendido'			,'sinHomologar' ,'{}') 
,(@KEY_DIM_EST_FIL  ,'KDS_SUS_PAR'  ,3  ,'Suspendido Parcialmente '	   ,'Suspendido Parcialmente'		,'sinHomologar' ,'{}')	
,(@KEY_DIM_EST_FIL  ,'KDS_RET'		,4  ,'Retirado'		           ,'Retirado'				,'sinHomologar' ,'{}')	
,(@KEY_DIM_EST_FIL  ,'KDS_INA'		,5  ,'Inactivo'		           ,'Inactivo'				,'sinHomologar' ,'{}')	
,(@KEY_DIM_EST_FIL  ,'KDS_VEN'		,6  ,'Vencido'		           ,'Vencido'				,'sinHomologar' ,'{}')	
--NIVEL 6
,(@KEY_DIM_REC_FIL  ,'KDR_NAC'		,1  ,'Nacional'		           ,'Nacional'				,'sinHomologar' ,'{}')	
,(@KEY_DIM_REC_FIL  ,'KDR_INT'		,2  ,'Internacional'		   ,'Internacional'			,'sinHomologar' ,'{}')	
-- Dimensiones o columnas de la grilla deben ser iguales a los de los filtros? 
,(@KEY_DIM_GRILLA   ,'KDG_ORG'		,1  ,'Organismo'			,'Información Organismo de Acreditación'    ,'sinHomologar' ,'{}')	  
,(@KEY_DIM_GRILLA   ,'KDG_REC'		,2  ,'Reconocimiento'		,'Reconocimiento	      '					,'sinHomologar' ,'{}')
,(@KEY_DIM_GRILLA   ,'KDG_RAS'		,3  ,'OEC/Razón social'		,'Nombre del OEC/Razón social'				,'sinHomologar' ,'{}') --*
,(@KEY_DIM_GRILLA   ,'KDG_PAI'		,4  ,'País'					,'País'										,'sinHomologar' ,'{}')
,(@KEY_DIM_GRILLA   ,'KDG_ESQ'		,5  ,'Esquema'				,'Esquema de acreditación	'				,'sinHomologar' ,'{}')
,(@KEY_DIM_GRILLA   ,'KDG_NOR'		,6  ,'Norma'				,'Norma acreditada	 '						,'sinHomologar' ,'{}')
,(@KEY_DIM_GRILLA   ,'KDG_IDE'		,7  ,'Identificación'		,'Identificación única	  '					,'sinHomologar' ,'{}') --*
,(@KEY_DIM_GRILLA   ,'KDG_EST'		,8  ,'Estado'				,'Estado'									,'sinHomologar' ,'{}')
,(@KEY_DIM_GRILLA   ,'KDG_ALC_DET'  ,9  ,'Alcance Detalle'		,'Información del Detalle del alcance'      ,'sinHomologar' ,'{}') --*
,(@KEY_DIM_GRILLA   ,'KDG_ALC_ACR'  ,10 ,'Alcance acreditado'	,'Alcance acreditado '						,'sinHomologar' ,'{}') --*
--* campos sin filtro
,(@KEY_DIM_GRILLA   ,'KDG_can_pai'  ,11  ,'oa_pais'                 ,'pais'			        ,'sinHomologar' ,'{}') 
,(@KEY_DIM_GRILLA   ,'KDG_can_ini'  ,12  ,'oa_inicialpais'          ,'inicialpais'		    ,'sinHomologar' ,'{}') 
,(@KEY_DIM_GRILLA   ,'KDG_can_ciu'  ,13  ,'oa_ciudad'               ,'ciudad'		        ,'sinHomologar' ,'{}') 
,(@KEY_DIM_GRILLA   ,'KDG_can_nom'  ,14  ,'oa_nombre'               ,'nombre'		        ,'sinHomologar' ,'{}') 
,(@KEY_DIM_GRILLA   ,'KDG_can_noi'  ,15  ,'oa_nombreInicial'        ,'nombre inicial'       ,'sinHomologar' ,'{}') 
,(@KEY_DIM_GRILLA   ,'KDG_can_dir'  ,16  ,'oa_direcion'             ,'direccion'		    ,'sinHomologar' ,'{}') 
,(@KEY_DIM_GRILLA   ,'KDG_can_tel'  ,17  ,'oa_telefono'             ,'telefono'		        ,'sinHomologar' ,'{}') 
,(@KEY_DIM_GRILLA   ,'KDG_can_sit'  ,18  ,'oa_sitioweb'             ,'sitioweb'		        ,'sinHomologar' ,'{}') 
,(@KEY_DIM_GRILLA   ,'KDG_can_log'  ,19  ,'oa_logo'                 ,'logo'			        ,'sinHomologar' ,'{}') 
,(@KEY_DIM_GRILLA   ,'KDG_can_des'  ,20  ,'oa_descripcion'          ,'descripcion'		    ,'sinHomologar' ,'{}') 
; 
GO

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


