-- https: //www.red-gate.com/simple-talk/databases/sql-server/learn/understanding-full-text-indexing-in-sql-server/
use CAN_DB;
GO

IF OBJECT_ID('organizacion', 'U') IS NOT NULL
    DROP TABLE organizacion;
GO
IF OBJECT_ID('OrganizacionPais', 'U') IS NOT NULL
    DROP TABLE OrganizacionPais;
GO


IF OBJECT_ID('RelacionSemantica', 'U') IS NOT NULL
    DROP TABLE RelacionSemantica;
GO
IF OBJECT_ID('Sinonimo', 'U') IS NOT NULL
    DROP TABLE Sinonimo;
GO
IF OBJECT_ID(N'Persona', N'U') IS NOT NULL
    DROP TABLE Persona;
GO

CREATE TABLE Homologacion (
     IdHomologacion     	INT IDENTITY(1,1) NOT NULL
    ,IdHomologacionGrupo	INT NOT NULL DEFAULT(NULL) 
    ,BusquedaCodigo			NVARCHAR(20)   NOT NULL 
    ,BusquedaEtiqueta		NVARCHAR(50)   NOT NULL  
    ,Observacion			NVARCHAR(500)   NOT NULL DEFAULT('')

    ,FechaCrea				DATETIME	NOT NULL DEFAULT(GETDATE())
    ,FechaModifica			DATETIME	NOT NULL DEFAULT(GETDATE())  
    ,IdUserCrea				INT			NOT NULL DEFAULT(0)
    ,IdUserModifica			INT			NOT NULL DEFAULT(0)  
    
	,CONSTRAINT  [PK_IdHomologacion] PRIMARY KEY CLUSTERED (IdHomologacion ASC) 
	,CONSTRAINT  [UK_BusquedaCodigo] UNIQUE (BusquedaCodigo)
);

--CREATE TABLE Sinonimo (
--    SinonimoId      INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_SinonimoId] PRIMARY KEY CLUSTERED ,
--    Palabra         NVARCHAR(100),
--    Sinonimo        NVARCHAR(100)
--);
CREATE TABLE Persona (
    PersonaId       INT IDENTITY(1,1) NOT NULL ,
    Nombre          NVARCHAR(500)   NOT NULL,  
    Direccion       NVARCHAR(1000)  NOT NULL,  
    Celular         NVARCHAR(100)   NOT NULL,  
    Fecha           NVARCHAR(100)   NOT NULL,  
    CONSTRAINT      [PK_PersonaId] PRIMARY KEY CLUSTERED (PersonaId ASC)  
);
--CREATE TABLE RelacionSemantica (
--    PersonaId		INT CONSTRAINT FK_RelacionSemantica1 FOREIGN KEY REFERENCES Persona(PersonaId), 
--    PersonaIdRef	INT CONSTRAINT FK_RelacionSemantica2 FOREIGN KEY REFERENCES Persona(PersonaId) 
--);
--GO


CREATE TABLE OrganizacionPais (
     IdOrganizacionPais     INT IDENTITY(1,1) NOT NULL
    ,Pais					NVARCHAR(100)   NOT NULL 
    ,Sistema				NVARCHAR(100)   NOT NULL  
    ,Observacion			NVARCHAR(500)   NOT NULL DEFAULT('')
    ,FechaCrea				DATETIME	NOT NULL DEFAULT(GETDATE())
    ,FechaModifica			DATETIME	NOT NULL DEFAULT(GETDATE())  
    ,IdUserCrea				INT			NOT NULL DEFAULT(0)
    ,IdUserModifica			INT			NOT NULL DEFAULT(0)  
    ,CONSTRAINT  [PK_IdOrganizacionPais] PRIMARY KEY CLUSTERED (IdOrganizacionPais ASC) 
);
GO
-- Organizacion
CREATE TABLE Organizacion (
 	 IdOrganizacion			INT IDENTITY(1,1) NOT NULL
	
	-- campos Id de los datos 
	,IdOrganizacionPais		INT	  FOREIGN KEY REFERENCES OrganizacionPais(IdOrganizacionPais)
	,IdAcreditacion			INT	  NOT NULL
	,IdActividad			INT	  NOT NULL
	,IdCiudad				INT	  NOT NULL
	
	-- campos de los datos
	,CodigoAcreditacion		NVARCHAR(100) NOT NULL DEFAULT('')
	,RazonSocial			NVARCHAR(500) NOT NULL DEFAULT('')
	,AreaAcreditacion		NVARCHAR(500) NOT NULL DEFAULT('')
	,Actividad				NVARCHAR(500) NOT NULL DEFAULT('')
	,Ciudad					NVARCHAR(500) NOT NULL DEFAULT('')
	,Estado					NVARCHAR(500) NOT NULL DEFAULT('')
	
	--campos base para audioria
	,FechaCrea				DATETIME	  NOT NULL DEFAULT(GETDATE())
    ,FechaModifica			DATETIME	  NOT NULL DEFAULT(GETDATE())  
    ,IdUserCrea				INT			  NOT NULL DEFAULT(0)
    ,IdUserModifica			INT			  NOT NULL DEFAULT(0)
	
	--definir indexaccion para la busqueda base
    ,CONSTRAINT [PK_IdOrganizacion]	PRIMARY KEY CLUSTERED (IdOrganizacion ASC) 
	,INDEX	[IX_IdOrganizacionPais] (IdOrganizacionPais)
	,INDEX	[IX_AcreditacionActividadRazon] (AreaAcreditacion, Actividad, RazonSocial)
);
GO

-- GRUPO
INSERT INTO Homologacion (BusquedaCodigo, BusquedaEtiqueta, Observacion)
VALUES 
	--filtros base
	 ('KEY_PAI'				,'PAIS'							,'Nivel 1')
	,('KEY_ORG_ACREDITA'	,'ORGANIZACION ACREDITADA'		,'Nivel 2')
	,('KEY_ESQ_ACREDITA'	,'ESQUEMA ACREDITACION'			,'Nivel 3')
	,('KEY_EST'				,'ESTADO'						,'Nivel 6')
	--filtros obtenidos
	,('KEY_RAZ_SOCIAL'		,'RAZON SOCIAL'					,'Nivel 4')
	,('KEY_ALC'				,'ALCANCE'						,'Nivel 5')
	;
GO

INSERT INTO Homologacion (IdHomologacionGrupo, BusquedaCodigo, BusquedaEtiqueta, Observacion)
VALUES 
	--NIVEL 1
     (1		,'KEY_COL'			,'Colombia' ,'')	
    ,(1		,'KEY_ECU'			,'Ecuador'  ,'')	
    ,(1		,'KEY_PER'			,'Perú'		,'')	
    ,(1		,'KEY_BOL'			,'Bolivia'  ,'')
	
	--NIVEL 2
	,(2		,'KEY_SIS_CONAC'		,'Sistema nacional de Colombi'	,'CONAC'	)
	,(2		,'KEY_SIS_SAE'			,'Sistema nacional de Ecuador'	,'SAE'		)
	,(2		,'KEY_SIS_INACAL-DA'	,'Sistema nacional de Perú	 '	,'INACAL-DA')
	,(2		,'KEY_SIS_DTA-IBMETRO'	,'Sistema nacional de Bolivia'	,'DTA-IBMETRO')

	--NIVEL 3
    ,(3		,'KEY_LAB_CALIBRA'			,'LABORATORIO CALIBRACIÓN'												,'')	
    ,(3		,'KEY_LAB_CLINICO'			,'LABORATORIO CLÍNICO'													,'')
    ,(3		,'KEY_LAB_INVESTI'			,'LABORATORIO INVESTIGACIÓN Y CONTROL DE CALIDAD'						,'')							
    ,(3		,'KEY_LAB_PROVEED'			,'LABORATORIO PROVEEDORES DE ENSAYOS DE APTITUD'						,'')							
    ,(3		,'KEY_LAB_ENSAYOS'			,'LABORATORIO ENSAYOS'													,'')
    ,(3		,'KEY_CER_GESTION'			,'CERTIFICACIÓN SISTEMAS DE GESTIÓN DE CALIDAD'							,'')						
    ,(3		,'KEY_CER_PERSONA'			,'CERTIFICACIÓN PERSONAS'												,'')	
    ,(3		,'KEY_CER_PRODUCT'			,'CERTIFICACIÓN PRODUCTOS'												,'')	
    ,(3		,'KEY_CER_INOCUID'			,'CERTIFICACIÓN SISTEMAS DE INOCUIDAD ALIMENTARIA'						,'')							
    ,(3		,'KEY_CER_AMBIENT'			,'CERTIFICACIÓN SISTEMAS DE GESTIÓN AMBIENTAL'							,'')						
    ,(3		,'KEY_CER_ANTISOB'			,'CERTIFICACIÓN SISTEMAS DE GESTIÓN ANTISOBORNO'						,'')							
    ,(3		,'KEY_CER_DISPOSI_MEDICO'	,'CERTIFICACIÓN SISTEMAS DE GESTIÓN DE CALIDAD DE DISPOSITIVOS MÉDICOS'	,'')												
    ,(3		,'KEY_INS_INSPECC'			,'INSPECCIÓN INSPECCIÓN'												,'')	
    ,(3		,'KEY_INS_AMBIENT'			,'INSPECCIÓN AMBIENTAL'													,'')
    ,(3		,'KEY_INS_AGRICOL'			,'INSPECCIÓN AGRÍCOLA'													,'')
    ,(3		,'KEY_VER'					,'VALIDACIÓN Y VERIFICACIÓN'											,'')		
    ,(3		,'KEY_OTR_NIVEL3'			,'CUALQUIER CATEGORIA'													,'')	
	;
 GO


--INSERT INTO Sinonimo (Palabra, Sinonimo)
--VALUES 
--    ('jose', 'pepe'),
--    ('luis', 'lucho');
--GO

IF OBJECT_ID(N'fnBuscarPalabraUnica', N'FN') IS NOT NULL
    DROP FUNCTION fnBuscarPalabraUnica;
GO

CREATE FUNCTION fnBuscarPalabraUnica
(
    @parametro  NVARCHAR(100),
    @Columna NVARCHAR(10) = NULL
)
RETURNS TABLE AS
RETURN
(
	select	o.IdOrganizacion
			,CodigoAcreditacion		
			,RazonSocial			
			,AreaAcreditacion		
			,Actividad				
			,Ciudad					
			,Estado					
	from	Organizacion			o
	join	OrganizacionFullText	oft
			on o.IdOrganizacion  =  oft.IdOrganizacion
	where	contains( [FullTextOrganizacion]  , @parametro) 
	or		(@Columna is NULL)
	or		(@Columna = 'CodigoAcreditacion'	AND CodigoAcreditacion	LIKE '%' + @parametro + '%')
	or		(@Columna = 'RazonSocial'			AND RazonSocial			LIKE '%' + @parametro + '%')
	or		(@Columna = 'AreaAcreditacion'		AND AreaAcreditacion	LIKE '%' + @parametro + '%')
	or		(@Columna = 'Actividad'				AND Actividad			LIKE '%' + @parametro + '%')
	or		(@Columna = 'Ciudad'				AND Ciudad				LIKE '%' + @parametro + '%')
)
GO

IF OBJECT_ID(N'fnBuscarPalabras', N'FN') IS NOT NULL
    DROP FUNCTION fnBuscarPalabras;
GO

CREATE FUNCTION fnBuscarPalabras
(
    @parametro  NVARCHAR(100),
    @Columna NVARCHAR(10) = NULL
)
RETURNS TABLE AS
RETURN
(
	select	o.IdOrganizacion
			,CodigoAcreditacion		
			,RazonSocial			
			,AreaAcreditacion		
			,Actividad				
			,Ciudad					
			,Estado					
	from	Organizacion			o
	join	OrganizacionFullText	oft
			on o.IdOrganizacion  =  oft.IdOrganizacion
	where	freeText( [FullTextOrganizacion]  , @parametro) 
	or		(@Columna is NULL)
	or		(@Columna = 'CodigoAcreditacion'	AND CodigoAcreditacion	LIKE '%' + @parametro + '%')
	or		(@Columna = 'RazonSocial'			AND RazonSocial			LIKE '%' + @parametro + '%')
	or		(@Columna = 'AreaAcreditacion'		AND AreaAcreditacion	LIKE '%' + @parametro + '%')
	or		(@Columna = 'Actividad'				AND Actividad			LIKE '%' + @parametro + '%')
	or		(@Columna = 'Ciudad'				AND Ciudad				LIKE '%' + @parametro + '%')
)
GO


	--if @Columna in ('Nombre','Direccion','Celular','Fecha')

--select * from BuscarPalabra('pablo',NULL)

-- INSERT INTO RelacionSemantica (PersonID1, PersonID2)
-- VALUES 
--     (3, 1), -- teléfono está relacionado con computadora
--     (4, 2); -- computadora está relacionada con pantalla


-- Consulta para obtener productos relacionados semánticamente
--SELECT E.Nombre
--FROM Person E
--JOIN  RelacionSemantica RS ON E.PersonID = RS.PersonID1
--WHERE RS.PersonID2                       = (SELECT PersonID FROM Person WHERE Nombre = 'Jose');

---- Consulta para buscar un producto por su sinónimo
--SELECT E.Nombre
--FROM Person E
--JOIN  Sinonimo S ON E.Nombre = S.Palabra
--WHERE S.Sinonimo             = 'pepe';




