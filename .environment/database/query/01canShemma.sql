-- https: //www.red-gate.com/simple-talk/databases/sql-server/learn/understanding-full-text-indexing-in-sql-server/
-- agegar campo estado a las tablas para borrado lógico
-- agregar constrains para borrados lógicos
--   

USE CAN_DB;
GO

IF OBJECT_ID('DataLakeOrganizacion', 'U') IS NOT NULL
    DROP TABLE DataLakeOrganizacion;
GO
IF OBJECT_ID('WebSiteLog', 'U') IS NOT NULL
    DROP TABLE WebSiteLog;
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
IF OBJECT_ID('UsuarioEndpointPermiso', 'U') IS NOT NULL
    DROP TABLE UsuarioEndpointPermiso;
GO
IF OBJECT_ID('Usuario', 'U') IS NOT NULL
    DROP TABLE Usuario;
GO
IF OBJECT_ID('Endpoint', 'U') IS NOT NULL
    DROP TABLE Endpoint;
GO
IF OBJECT_ID('Homologacion', 'U') IS NOT NULL
    DROP TABLE Homologacion;
GO


CREATE TABLE Usuario (
    IdUsuario		INT IDENTITY(1,1) PRIMARY KEY
    ,Email			NVARCHAR(100) NOT NULL
    ,Nombre			NVARCHAR(500) NOT NULL
    ,Apellido		NVARCHAR(500)
    ,Rol			NVARCHAR(20)	NOT NULL DEFAULT('USER')
    ,Clave			NVARCHAR(MAX)	NOT NULL

	-- Campos base para auditoría
    ,FechaCrea				    DATETIME	NOT NULL DEFAULT(GETDATE())
    ,FechaModifica			    DATETIME	NOT NULL DEFAULT(GETDATE())  
    ,IdUserCrea				    INT			NOT NULL DEFAULT(0)
    ,IdUserModifica			    INT			NOT NULL DEFAULT(0)

    ,CONSTRAINT UK_Email	UNIQUE (Email)
    ,CONSTRAINT CK_Rol		CHECK (Rol IN ('ADMIN', 'USER'))
);
GO

CREATE TABLE Endpoint (
    IdEndpoint			INT IDENTITY(1,1) PRIMARY KEY,
    Nombre				NVARCHAR(100) NOT NULL,
    Url					NVARCHAR(MAX)

    -- Campos base para auditoría
    ,FechaCrea				    DATETIME	NOT NULL DEFAULT(GETDATE())
    ,FechaModifica			    DATETIME	NOT NULL DEFAULT(GETDATE())  
    ,IdUserCrea				    INT			NOT NULL DEFAULT(0)
    ,IdUserModifica			    INT			NOT NULL DEFAULT(0)

    ,CONSTRAINT UK_Nombre UNIQUE (Nombre)
);
GO

CREATE TABLE UsuarioEndpointPermiso (
    IdUsuarioEndpointPermiso     INT IDENTITY(1,1) PRIMARY KEY
    ,IdUsuario                   INT NOT NULL DEFAULT(0)
    ,IdEndpoint                  INT NOT NULL DEFAULT(0)
    ,Accion                      NVARCHAR(10) NOT NULL
    -- Campos base para auditoría
    ,FechaCrea				    DATETIME	NOT NULL DEFAULT(GETDATE())
    ,FechaModifica			    DATETIME	NOT NULL DEFAULT(GETDATE())  
    ,IdUserCrea				    INT			NOT NULL DEFAULT(0)
    ,IdUserModifica			    INT			NOT NULL DEFAULT(0)

    ,CONSTRAINT UK_Accion       CHECK (Accion IN ('GET', 'POST', 'PUT', 'DELETE'))
    ,CONSTRAINT FK_IdUsuario    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
    ,CONSTRAINT FK_IdEndpoint   FOREIGN KEY (IdEndpoint) REFERENCES Endpoint(IdEndpoint)
);
GO

CREATE TABLE Homologacion (
     IdHomologacion     	INT IDENTITY(1,1) NOT NULL
    ,IdHomologacionGrupo	INT DEFAULT(NULL) 
    ,ClaveBuscar			NVARCHAR(20)  NOT NULL 
    ,Homologado				NVARCHAR(90)  NOT NULL
    ,MostrarWeb				NVARCHAR(90)  NOT NULL
    ,Descripcion			NVARCHAR(200)
    ,InfoExtraJson			NVARCHAR(max)

    ,FechaCrea				DATETIME	NOT NULL DEFAULT(GETDATE())
    ,FechaModifica			DATETIME	NOT NULL DEFAULT(GETDATE())  
    ,IdUserCrea				INT			NOT NULL DEFAULT(0)
    ,IdUserModifica			INT			NOT NULL DEFAULT(0)  
    
	,CONSTRAINT  [PK_IdHomologacion]    PRIMARY KEY CLUSTERED (IdHomologacion DESC) 
	,CONSTRAINT  [UK_ClaveBuscar]		UNIQUE (ClaveBuscar)
);
GO
--CREATE TABLE Sinonimo (
--    SinonimoId      INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_SinonimoId] PRIMARY KEY CLUSTERED ,
--    Palabra         NVARCHAR(100),
--    Sinonimo        NVARCHAR(100)
--);
CREATE TABLE Persona (
    PersonaId				INT IDENTITY(1,1) NOT NULL 
    ,Nombre					NVARCHAR(500)   NOT NULL  
    ,Direccion				NVARCHAR(1000)  NOT NULL  
    ,Celular				NVARCHAR(100)   NOT NULL  
    ,Fecha					NVARCHAR(100)   NOT NULL 
    ,FechaCrea				DATETIME	NOT NULL DEFAULT(GETDATE())
    ,FechaModifica			DATETIME	NOT NULL DEFAULT(GETDATE())  
    ,IdUserCrea				INT			NOT NULL DEFAULT(0)
    ,IdUserModifica			INT			NOT NULL DEFAULT(0) 
    ,CONSTRAINT      [PK_PersonaId] PRIMARY KEY CLUSTERED (PersonaId ASC)  
);
GO
--CREATE TABLE RelacionSemantica (
--    PersonaId		INT CONSTRAINT FK_RelacionSemantica1 FOREIGN KEY REFERENCES Persona(PersonaId), 
--    PersonaIdRef	INT CONSTRAINT FK_RelacionSemantica2 FOREIGN KEY REFERENCES Persona(PersonaId) 
--);
--GO

-- CREATE TABLE OrganizacionPais (
--      IdOrganizacionPais     INT IDENTITY(1,1) NOT NULL
--     ,Pais					NVARCHAR(100)   NOT NULL 
--     ,Sistema				NVARCHAR(100)   NOT NULL  
--     ,Observacion			NVARCHAR(500)   NOT NULL DEFAULT('')
--     ,FechaCrea				DATETIME	NOT NULL DEFAULT(GETDATE())
--     ,FechaModifica			DATETIME	NOT NULL DEFAULT(GETDATE())  
--     ,IdUserCrea				INT			NOT NULL DEFAULT(0)
--     ,IdUserModifica			INT			NOT NULL DEFAULT(0)  
--     ,CONSTRAINT  [PK_IdOrganizacionPais] PRIMARY KEY CLUSTERED (IdOrganizacionPais ASC) 
-- );
-- GO
-- -- Organizacion
-- CREATE TABLE Organizacion (
--  	 IdOrganizacion			INT IDENTITY(1,1) NOT NULL
	
-- 	-- campos Id de los datos 
-- 	,IdOrganizacionPais		INT	  FOREIGN KEY REFERENCES OrganizacionPais(IdOrganizacionPais)
-- 	,IdAcreditacion			INT	  NOT NULL
-- 	,IdActividad			INT	  NOT NULL
-- 	,IdCiudad				INT	  NOT NULL
	
-- 	-- campos de los datos
-- 	,CodigoAcreditacion		NVARCHAR(100) NOT NULL DEFAULT('')
-- 	,RazonSocial			NVARCHAR(500) NOT NULL DEFAULT('')
-- 	,AreaAcreditacion		NVARCHAR(500) NOT NULL DEFAULT('')
-- 	,Actividad				NVARCHAR(500) NOT NULL DEFAULT('')
-- 	,Ciudad					NVARCHAR(500) NOT NULL DEFAULT('')
-- 	,Estado					NVARCHAR(1) NOT NULL DEFAULT('A') -- cuando se elimine X
	
-- 	,FechaCrea				DATETIME	  NOT NULL DEFAULT(GETDATE())
-- 	--campos base para audioria
--     ,FechaModifica			DATETIME	  NOT NULL DEFAULT(GETDATE())  
--     ,IdUserCrea				INT			  NOT NULL DEFAULT(0)
--     ,IdUserModifica			INT			  NOT NULL DEFAULT(0)
	
-- 	--definir indexaccion para la busqueda base
--     ,CONSTRAINT [PK_IdOrganizacion]	PRIMARY KEY CLUSTERED (IdOrganizacion ASC) 
-- 	,INDEX	[IX_IdOrganizacionPais] (IdOrganizacionPais)
-- 	,INDEX	[IX_AcreditacionActividadRazon] (AreaAcreditacion, Actividad, RazonSocial)
-- );

CREATE TABLE DataLakeOrganizacion(
    IdDataLakeOrganizacion  INT IDENTITY(1,1) NOT NULL
    ,IdHomologacionSistema	INT NOT NULL  FOREIGN KEY REFERENCES Homologacion (IdHomologacion)
    ,DataId					NVARCHAR(10) NOT NULL 
    ,DataJson               NVARCHAR(max) NOT NULL 
    ,DataJsonExtra          NVARCHAR(max) NOT NULL 
	,DataJsonEstado			NVARCHAR(2)   NOT NULL DEFAULT('SI') 

    ,FechaCrea				DATETIME	NOT NULL DEFAULT(GETDATE())
    ,FechaModifica			DATETIME	NOT NULL DEFAULT(GETDATE())  
	,CONSTRAINT  [PK_IdDataLake]   PRIMARY KEY CLUSTERED (IdDataLakeOrganizacion DESC) 
	,CONSTRAINT  [UK_DataEstado]   CHECK (DataJsonEstado IN ('NO', 'SI'))
);
GO

CREATE TABLE WebSiteLog (
    IdWebSiteLog    bigint primary key identity,
    log             nvarchar(max)
);
GO