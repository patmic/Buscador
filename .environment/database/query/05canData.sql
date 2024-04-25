USE CAN_DB;
GO
---------------------------------
-- poner truncate table
------------------------------------------
-- GRUPO
--filtros base
DECLARE @KEY_PAI INT;
DECLARE @KEY_ORG_ACREDITA INT;
DECLARE @KEY_ESQ_ACREDITA INT;
DECLARE @KEY_RAZ_SOCIAL INT;
DECLARE @KEY_ALC INT;
DECLARE @KEY_EST INT;

INSERT INTO Homologacion (BusquedaCodigo, BusquedaEtiqueta, Observacion)
VALUES ('KEY_PAI'				,'PAIS'							,'Nivel 1')
SET @KEY_PAI = @@IDENTITY;
INSERT INTO Homologacion (BusquedaCodigo, BusquedaEtiqueta, Observacion)
VALUES ('KEY_ORG_ACREDITA'	,'ORGANIZACION ACREDITADA'		,'Nivel 2')
SET @KEY_ORG_ACREDITA = @@IDENTITY;
INSERT INTO Homologacion (BusquedaCodigo, BusquedaEtiqueta, Observacion)
VALUES('KEY_ESQ_ACREDITA'	,'ESQUEMA ACREDITACION'			,'Nivel 3')
SET @KEY_ESQ_ACREDITA = @@IDENTITY;
	--filtros obtenidos
INSERT INTO Homologacion (BusquedaCodigo, BusquedaEtiqueta, Observacion)
VALUES('KEY_RAZ_SOCIAL'		,'RAZON SOCIAL'					,'Nivel 4')
SET @KEY_RAZ_SOCIAL = @@IDENTITY;
INSERT INTO Homologacion (BusquedaCodigo, BusquedaEtiqueta, Observacion)
VALUES('KEY_ALC'				,'ALCANCE'						,'Nivel 5')
SET @KEY_ALC = @@IDENTITY;
INSERT INTO Homologacion (BusquedaCodigo, BusquedaEtiqueta, Observacion)
VALUES('KEY_EST'				,'ESTADO'						,'Nivel 6')
SET @KEY_EST = @@IDENTITY;
GO

--NIVEL 1
INSERT INTO Homologacion (IdHomologacionGrupo, BusquedaCodigo, BusquedaEtiqueta, Observacion)
VALUES 
     (@KEY_PAI		,'KEY_COL'			,'Colombia' ,'')	
    ,(@KEY_PAI		,'KEY_ECU'			,'Ecuador'  ,'')	
    ,(@KEY_PAI		,'KEY_PER'			,'Perú'		,'')	
    ,(@KEY_PAI		,'KEY_BOL'			,'Bolivia'  ,'')
	
	--NIVEL 2
	,(@KEY_ORG_ACREDITA		,'KEY_SIS_CONAC'		,'Sistema nacional de Colombi'	,'CONAC'	)
	,(@KEY_ORG_ACREDITA		,'KEY_SIS_SAE'			,'Sistema nacional de Ecuador'	,'SAE'		)
	,(@KEY_ORG_ACREDITA		,'KEY_SIS_INACAL-DA'	,'Sistema nacional de Perú	 '	,'INACAL-DA')
	,(@KEY_ORG_ACREDITA		,'KEY_SIS_DTA-IBMETRO'	,'Sistema nacional de Bolivia'	,'DTA-IBMETRO');
GO

INSERT INTO Homologacion (IdHomologacionGrupo, BusquedaCodigo, BusquedaEtiqueta, Observacion)
VALUES 
	--NIVEL 3
     (@KEY_ESQ_ACREDITA		,'KEY_LAB_CALIBRA'			,'LABORATORIO CALIBRACIÓN'												,'')	
    ,(@KEY_ESQ_ACREDITA		,'KEY_LAB_CLINICO'			,'LABORATORIO CLÍNICO'													,'')
    ,(@KEY_ESQ_ACREDITA		,'KEY_LAB_INVESTI'			,'LABORATORIO INVESTIGACIÓN Y CONTROL DE CALIDAD'						,'')							
    ,(@KEY_ESQ_ACREDITA		,'KEY_LAB_PROVEED'			,'LABORATORIO PROVEEDORES DE ENSAYOS DE APTITUD'						,'')							
    ,(@KEY_ESQ_ACREDITA		,'KEY_LAB_ENSAYOS'			,'LABORATORIO ENSAYOS'													,'')
    ,(@KEY_ESQ_ACREDITA		,'KEY_CER_GESTION'			,'CERTIFICACIÓN SISTEMAS DE GESTIÓN DE CALIDAD'							,'')						
    ,(@KEY_ESQ_ACREDITA		,'KEY_CER_PERSONA'			,'CERTIFICACIÓN PERSONAS'												,'')	
    ,(@KEY_ESQ_ACREDITA		,'KEY_CER_PRODUCT'			,'CERTIFICACIÓN PRODUCTOS'												,'')	
    ,(@KEY_ESQ_ACREDITA		,'KEY_CER_INOCUID'			,'CERTIFICACIÓN SISTEMAS DE INOCUIDAD ALIMENTARIA'						,'')							
    ,(@KEY_ESQ_ACREDITA		,'KEY_CER_AMBIENT'			,'CERTIFICACIÓN SISTEMAS DE GESTIÓN AMBIENTAL'							,'')						
    ,(@KEY_ESQ_ACREDITA		,'KEY_CER_ANTISOB'			,'CERTIFICACIÓN SISTEMAS DE GESTIÓN ANTISOBORNO'						,'')							
    ,(@KEY_ESQ_ACREDITA		,'KEY_CER_DISP_MED'			,'CERTIFICACIÓN SISTEMAS DE GESTIÓN DE CALIDAD DE DISPOSITIVOS MÉDICOS'	,'')												
    ,(@KEY_ESQ_ACREDITA		,'KEY_INS_INSPECC'			,'INSPECCIÓN INSPECCIÓN'												,'')	
    ,(@KEY_ESQ_ACREDITA		,'KEY_INS_AMBIENT'			,'INSPECCIÓN AMBIENTAL'													,'')
    ,(@KEY_ESQ_ACREDITA		,'KEY_INS_AGRICOL'			,'INSPECCIÓN AGRÍCOLA'													,'')
    ,(@KEY_ESQ_ACREDITA		,'KEY_VER'					,'VALIDACIÓN Y VERIFICACIÓN'											,'')		
    ,(@KEY_ESQ_ACREDITA		,'KEY_OTR_NIVEL3'			,'CUALQUIER CATEGORIA'													,'')	
	;
 GO


--INSERT INTO Sinonimo (Palabra, Sinonimo)
--VALUES 
--    ('jose', 'pepe'),
--    ('luis', 'lucho');
--GO