-- https: //www.red-gate.com/simple-talk/databases/sql-server/learn/understanding-full-text-indexing-in-sql-server/
-- agegar campo estado a las tablas para borrado lógico
-- agregar constrains para borrados lógicos

USE CAN_DB;
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

IF OBJECT_ID('Usuario', 'U') IS NOT NULL
    DROP TABLE Usuario;
GO

IF OBJECT_ID('Endpoint', 'U') IS NOT NULL
    DROP TABLE Endpoint;
GO

IF OBJECT_ID('UsuarioEndpointPermiso', 'U') IS NOT NULL
    DROP TABLE UsuarioEndpointPermiso;
GO

CREATE TABLE Usuario (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(100) NOT NULL,
    Nombre NVARCHAR(500) NOT NULL,
    Apellido NVARCHAR(500),
    Rol NVARCHAR(20) NOT NULL,
    Clave NVARCHAR(MAX) NOT NULL,

	--campos base para audioria
    FechaCrea DATETIME NOT NULL DEFAULT(GETDATE()),
    FechaModifica DATETIME NOT NULL DEFAULT(GETDATE()),
    IdUserCrea INT NOT NULL DEFAULT(0),
    IdUserModifica INT NOT NULL DEFAULT(0),

    CONSTRAINT UK_Email UNIQUE (Email),
    CONSTRAINT CK_Rol CHECK (Rol IN ('ADMIN', 'USER'))
);
GO

CREATE TABLE Endpoint (
    IdEndpoint INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Url NVARCHAR(MAX),

    -- Campos base para auditoría
    FechaCrea DATETIME NOT NULL DEFAULT(GETDATE()),
    FechaModifica DATETIME NOT NULL DEFAULT(GETDATE()),
    IdUserCrea INT NOT NULL DEFAULT(0),
    IdUserModifica INT NOT NULL DEFAULT(0),

    CONSTRAINT UK_Nombre UNIQUE (Nombre)
);
GO

CREATE TABLE UsuarioEndpointPermiso (
    IdUsuarioEndpointPermiso INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL DEFAULT(0),
    IdEndpoint INT NOT NULL DEFAULT(0),
    Accion NVARCHAR(10) NOT NULL,

    -- Campos base para auditoría
    FechaCrea DATETIME NOT NULL DEFAULT(GETDATE()),
    FechaModifica DATETIME NOT NULL DEFAULT(GETDATE()),
    IdUserCrea INT NOT NULL DEFAULT(0),
    IdUserModifica INT NOT NULL DEFAULT(0),

    CONSTRAINT CK_Accion CHECK (Accion IN ('GET', 'POST', 'PUT', 'DELETE')),

    CONSTRAINT FK_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_IdEndpoint FOREIGN KEY (IdEndpoint) REFERENCES Endpoint(IdEndpoint)
);
GO

CREATE TABLE Homologacion (
     IdHomologacion     	INT IDENTITY(1,1) NOT NULL
    ,IdHomologacionGrupo	INT DEFAULT(NULL) 
    ,BusquedaCodigo			NVARCHAR(20)   NOT NULL 
    ,BusquedaEtiqueta		NVARCHAR(75)   NOT NULL  
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
    Fecha           NVARCHAR(100)   NOT NULL 
    ,FechaCrea				DATETIME	NOT NULL DEFAULT(GETDATE())
    ,FechaModifica			DATETIME	NOT NULL DEFAULT(GETDATE())  
    ,IdUserCrea				INT			NOT NULL DEFAULT(0)
    ,IdUserModifica			INT			NOT NULL DEFAULT(0) 
    ,CONSTRAINT      [PK_PersonaId] PRIMARY KEY CLUSTERED (PersonaId ASC)  
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
	,Estado					NVARCHAR(1) NOT NULL DEFAULT('A') -- cuando se elimine X
	
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
);
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




