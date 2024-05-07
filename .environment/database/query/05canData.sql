USE CAN_DB;
GO

    --  IdHomologacion     	 
    -- ,IdHomologacionGrupo	 
    -- ,ClaveBuscar			 codigo de busqueda
    -- ,Homologado			 nombre del comite 
    -- ,MostrarWeb			 etiqueta como debe mostrar en la web
    -- ,Descripcion			 tooltip
    -- ,InfoExtraJson		 Para mostrar informacion extra 

--filtros base
DECLARE  @KEY_PAI INT
        ,@KEY_ORG INT
        ,@KEY_ESQ INT
        ,@KEY_RAL INT
        ,@KEY_ALC INT
        ,@KEY_EST INT 
        ,@KEY_DIM INT 

INSERT INTO Homologacion    (ClaveBuscar,  Homologado,      MostrarWeb, Descripcion)
VALUES                      ('KEY_PAI',    'PAIS DE LA CAN'         , 'PAIS'            ,'Nivel 1')
SET @KEY_PAI = @@IDENTITY;
INSERT INTO Homologacion    (ClaveBuscar,  Homologado,      MostrarWeb, Descripcion)
VALUES                      ('KEY_ORG',   'ORGANIZACION ACREDITADA'	,'ORGANIZACION '	,'Nivel 2')
SET @KEY_ORG = @@IDENTITY;
INSERT INTO Homologacion    (ClaveBuscar,  Homologado,      MostrarWeb, Descripcion)
VALUES                      ('KEY_ESQ',    'ESQUEMA ACREDITACION'   ,'ESQUEMA '	        ,'Nivel 3')
SET @KEY_ESQ = @@IDENTITY;
INSERT INTO Homologacion    (ClaveBuscar,  Homologado,      MostrarWeb, Descripcion)
VALUES                      ('KEY_RAL',    'TIPO ACREDITACION'   ,'ACREDITACION POR'    ,'Nivel 4')
SET @KEY_RAL = @@IDENTITY;
INSERT INTO Homologacion    (ClaveBuscar,  Homologado,      MostrarWeb, Descripcion)
VALUES                      ('KEY_EST',    'ESTADO'         ,'ESTADO'                   ,'Nivel 5')
SET @KEY_EST = @@IDENTITY;
INSERT INTO Homologacion    (ClaveBuscar,  Homologado,      MostrarWeb, Descripcion)
VALUES                      ('KEY_DIM',    'DIMENSION'      ,'CAMPO HOMOLOGADO'  ,'Columnas de las grillas')
SET @KEY_DIM = @@IDENTITY;
    -- Datos de los filtros
INSERT INTO Homologacion (IdHomologacionGrupo, ClaveBuscar,  Homologado,      MostrarWeb, Descripcion ,InfoExtraJson)
VALUES 
--NIVEL 1
 (@KEY_PAI,     'KEY_DIM_COL'           ,'Colombia'   ,'Colombia'  ,'Colombiana'     ,'{responsable:"pepe"; email:"Colombia@gob.ec"}')	
,(@KEY_PAI,     'KEY_DIM_ECU'           ,'Ecuador'    ,'Ecuador'   ,'Ecuatoriana'    ,'{responsable:"juan"; email:"Ecuador@gob.ec"}')	
,(@KEY_PAI,     'KEY_DIM_PER'           ,'Perú'	      ,'Perú'	   ,'Peruana'        ,'{responsable:"pepe"; email:"Perú@gob.ec"}')	
,(@KEY_PAI,     'KEY_DIM_BOL'           ,'Bolivia'    ,'Bolivia'   ,'Boliviana'      ,'{responsable:"pepe"; email:"Bolivia@gob.ec"}')
--NIVEL 2
,(@KEY_ORG,     'KEY_CONAC'		    ,'CONAC'	  ,'Sistema nacional de Colombi'	,''  ,null)
,(@KEY_ORG,     'KEY_SAE'		    ,'SAE'		  ,'Sistema acreditacion Ecuador'	,''  ,null)
,(@KEY_ORG,     'KEY_INACAL-DA'	    ,'INACAL-DA'  ,'Sistema nacional del Perú	'	,''  ,null)
,(@KEY_ORG,     'KEY_DTA-IBMETRO'   ,'DTA-IBMETRO','Sistema nacional de Bolivia'	,''  ,null)
--NIVEL 3
,(@KEY_ESQ,     'KEY_LAB_CAL'		    ,'LABORATORIO CALIBRACIÓN'							,'LABORATORIO CALIBRACIÓN'							,'' ,null)	
,(@KEY_ESQ,     'KEY_LAB_CLI'		    ,'LABORATORIO CLÍNICO'								,'LABORATORIO CLÍNICO'								,'' ,null)
,(@KEY_ESQ,     'KEY_LAB_INV'		    ,'LABORATORIO INVESTIGACIÓN Y CONTROL DE CALIDAD'	,'LABORATORIO INVESTIGACIÓN Y CONTROL DE CALIDAD'	,'' ,null)							
,(@KEY_ESQ,     'KEY_LAB_PRO'		    ,'LABORATORIO PROVEEDORES DE ENSAYOS DE APTITUD'	,'LABORATORIO PROVEEDORES DE ENSAYOS DE APTITUD'	,'' ,null)							
,(@KEY_ESQ,     'KEY_LAB_ENS'		    ,'LABORATORIO ENSAYOS'								,'LABORATORIO ENSAYOS'								,'' ,null)
,(@KEY_ESQ,     'KEY_CER_PER'           ,'CERTIFICACIÓN PERSONAS'							,'CERTIFICACIÓN PERSONAS'							,'' ,null)	
,(@KEY_ESQ,     'KEY_CER_PRO'           ,'CERTIFICACIÓN PRODUCTOS'							,'CERTIFICACIÓN PRODUCTOS'							,'' ,null)	
,(@KEY_ESQ,     'KEY_CER_SIS_INO'       ,'CERTIFICACIÓN SISTEMAS DE INOCUIDAD ALIMENTARIA'	,'CERTIFICACIÓN SISTEMAS DE INOCUIDAD ALIMENTARIA'	,'' ,null)							
,(@KEY_ESQ,     'KEY_CER_SIS_GES_CAL'   ,'CERTIFICACIÓN SISTEMAS DE GESTIÓN CALIDAD'		,'CERTIFICACIÓN SISTEMAS DE GESTIÓN CALIDAD'		,'' ,null)						
,(@KEY_ESQ,     'KEY_CER_SIS_GES_AMB'   ,'CERTIFICACIÓN SISTEMAS DE GESTIÓN AMBIENTAL'		,'CERTIFICACIÓN SISTEMAS DE GESTIÓN AMBIENTAL'		,'' ,null)						
,(@KEY_ESQ,     'KEY_CER_SIS_GES_ANT'   ,'CERTIFICACIÓN SISTEMAS DE GESTIÓN ANTISOBORNO'	,'CERTIFICACIÓN SISTEMAS DE GESTIÓN ANTISOBORNO'	,'' ,null)							
,(@KEY_ESQ,     'KEY_INS_INS'           ,'INSPECCIÓN INSPECCIÓN'							,'INSPECCIÓN INSPECCIÓN'							,'' ,null)	
,(@KEY_ESQ,     'KEY_INS_AMB'           ,'INSPECCIÓN AMBIENTAL'								,'INSPECCIÓN AMBIENTAL'								,'' ,null)
,(@KEY_ESQ,     'KEY_INS_AGR'           ,'INSPECCIÓN AGRÍCOLA'								,'INSPECCIÓN AGRÍCOLA'								,'' ,null)
,(@KEY_ESQ,     'KEY_VAL_VER'           ,'VALIDACIÓN Y VERIFICACIÓN'						,'VALIDACIÓN Y VERIFICACIÓN'						,'' ,null)		
--NIVEL 4
,(@KEY_RAL,     'KEY_RAZ'  		    ,'RAZON SOCIAL'	   ,'RAZON SOCIAL'				,'' ,null)	
,(@KEY_RAL,     'KEY_ALC'  		    ,'ALCANCE'		   ,'ALCANCE'					,'' ,null)	
--NIVEL 5
,(@KEY_EST,     'KEY_ACT'		    ,'ACTIVO'		    ,'Activo'					,'' ,null)	
,(@KEY_EST,     'KEY_FIN'		    ,'FINALIZADO'	    ,'Finalizado'				,'' ,null)		
,(@KEY_EST,     'KEY_RET'		    ,'RETIRADO'		    ,'Retirado'					,'' ,null)	
-- dimensiones o columnas de la grilla
,(@KEY_DIM		,'KEY_DIM_RAZ'		,'identificado unicoa de CAN'	, 	'RazonSocial'   , 	'Razón Social Unico'        ,null)
,(@KEY_DIM		,'KEY_DIM_COD'		,'identificado de Organizaco'	, 	'CodAcredita'   , 	'Codigo de Acreditacion'    ,null)
,(@KEY_DIM		,'KEY_DIM_NOM'		,'identificado de NombreOrg'	, 	'NombreOrg'     , 	'nombre de la organizacion' ,null)
,(@KEY_DIM		,'KEY_DIM_DIR'		,'direccion de la Organizacion'	, 	'DireccionOrg'  ,	'direccion de organizacion' ,null)
,(@KEY_DIM		,'KEY_DIM_FEC'		,'Fecha de la Organizacion'	    , 	'FechaOrg'      ,	'Fecha de organizacion'     ,null)

,(@KEY_DIM		,'KEY_DIM_cou'		,'country'	        , 	'country'             ,	'country'            ,null)
,(@KEY_DIM		,'KEY_DIM_cit'		,'city'	            , 	'city'                ,	'city'               ,null)
,(@KEY_DIM		,'KEY_DIM_are'		,'area'	            , 	'area'                ,	'area'               ,null)
,(@KEY_DIM		,'KEY_DIM_acc'		,'accreditationcode', 	'accreditationcode'   ,	'accreditationcode'  ,null)
,(@KEY_DIM		,'KEY_DIM_cer'		,'certpdf'	        , 	'certpdf'             ,	'certpdf'            ,null)
,(@KEY_DIM		,'KEY_DIM_xxx'		,'countrycode'	    , 	'countrycode'         ,	'countrycode'        ,null)
,(@KEY_DIM		,'KEY_DIM_bus'		,'business'	        , 	'business'            ,	'business'           ,null)
,(@KEY_DIM		,'KEY_DIM_act'		,'activity'	        , 	'activity'            ,	'activity'           ,null)
,(@KEY_DIM		,'KEY_DIM_sta'		,'status'	        , 	'status'              ,	'status'             ,null)
--poner aca mas columnas
;
GO

INSERT INTO [OrganizacionFullText] 
([IdDataLakeOrganizacion] ,[IdHomologacion] ,[FullTextOrganizacion])
VALUES
 (1, 48, '1status' )
,(1, 47, '1activity' )
,(1, 46, '1business' )
,(1, 45, '1countrycode' )
,(1, 44, '1certpdf' )
,(1, 43, '1accreditationcode' )
,(1, 42, '1area' )
,(1, 41, '1city' )
,(1, 40, '1country' )
,(1, 39, '1Fecha de la Organizacion' )
,(1, 38, '1direccion de la Organizacion' )
,(1, 37, '1identificado de NombreOrg' )
,(1, 36, '1identificado de Organizaco' )
,(1, 35, '1identificado unicoa de CAN' )
 
,(2, 48, '2status' )
,(2, 47, '2activity' )
,(2, 46, '2business' )
,(2, 45, '2countrycode' )
,(2, 44, '2certpdf' )
,(2, 43, '2accreditationcode' )
,(2, 42, '2area' )
,(2, 41, '2city' )
,(2, 40, '2country' )
,(2, 39, '2Fecha de la Organizacion' )
,(2, 38, '2direccion de la Organizacion' )
,(2, 37, '2identificado de NombreOrg' )
,(2, 36, '2identificado de Organizaco' )
,(2, 35, '2identificado unicoa de CAN' )

,(3, 48, '3status' )
,(3, 47, '3activity' )
,(3, 46, '3business' )
,(3, 45, '3countrycode' )
,(3, 44, '3certpdf' )
,(3, 43, '3accreditationcode' )
,(3, 42, '3area' )
,(3, 41, '3city' )
,(3, 40, '3country' )
,(3, 39, '3Fecha de la Organizacion' )
,(3, 38, '3direccion de la Organizacion' )
,(3, 37, '3identificado de NombreOrg' )
,(3, 36, '3identificado de Organizaco' )
,(3, 35, '3identificado unicoa de CAN' )

,(4, 48, '4status' )
,(4, 47, '4activity' )
,(4, 46, '4business' )
,(4, 45, '4countrycode' )
,(4, 44, '4certpdf' )
,(4, 43, '4accreditationcode' )
,(4, 42, '4area' )
,(4, 41, '4city' )
,(4, 40, '4country' )
,(4, 39, '4Fecha de la Organizacion' )
,(4, 38, '4direccion de la Organizacion' )
,(4, 37, '4identificado de NombreOrg' )
,(4, 36, '4identificado de Organizaco' )
,(4, 35, '4identificado unicoa de CAN' )
;
go
-- fn_buscaPalabra(SRI)  --> { IdDLO:100,  razonSocial:11036354444, CodAcreditacion : COD-111,  NombreOrg: SRI , ....}
-- IdDataLakeOrganizacion  IdHomologacion   FullTextOrganizacion
-- 1                           10           11036354444
-- 2                           11           COD-111
-- 3                           13           SRI
-- 4                                        11036354444 COD-111 SRI
-- ...


--INSERT INTO [OrganizacionFullText] ([IdDataLakeOrganizacion] ,[IdHomologacion] ,[FullTextOrganizacion])
--VALUES
--           (1, 35, <FullTextOrganizacion, nvarchar(max),>)
--GO

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


