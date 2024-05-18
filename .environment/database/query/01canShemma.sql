/*-----------------------------------------------------------------------------------------\
|         2©24 Copyright      												Quito		   |
| Todos los Derechos Reservados                                    República del Ecuador   |
|------------------------------------------------------------------------------------------|
| Este código está protegido por las leyes y tratados internacionales de derechos de autor |
| por lo que el uso, reproducción o distribución del código total o parcial no autorizada  |
| se atendrá a las sanciones contempladas en la ley con su máximo rigor.                   |
\------------------------------------------------------------------------------------------/
	- Developer		: patmic										 
	- Descripction  : crea las entidades de base de datos
	- Validate		: [ ] Stress o Stress concurrente       [ ] ETL       
                      [ ] Quality Data                      [X] Log & Audit   
                      [x] Standart Code 
\*----------------------------------------------------------------------------------------*/

USE CAN_DB;
GO

IF OBJECT_ID('UsuarioEndpointPermiso', 'U') IS NOT NULL
    DROP TABLE UsuarioEndpointPermiso;
GO
IF OBJECT_ID('DataLakePersona', 'U') IS NOT NULL
    DROP TABLE DataLakePersona;
GO
IF OBJECT_ID('DataLakeOrganizacion', 'U') IS NOT NULL
    DROP TABLE DataLakeOrganizacion;
GO
IF OBJECT_ID('DataLake', 'U') IS NOT NULL
    DROP TABLE DataLake;
GO
IF OBJECT_ID('HomologacionEsquema', 'U') IS NOT NULL
    DROP TABLE HomologacionEsquema;
GO
IF OBJECT_ID('EndPointWeb', 'U') IS NOT NULL
    DROP TABLE EndPointWeb;
GO
IF OBJECT_ID('WebSiteLog', 'U') IS NOT NULL
    DROP TABLE WebSiteLog;
GO
IF OBJECT_ID('OrganizacionPais', 'U') IS NOT NULL
    DROP TABLE OrganizacionPais;
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
IF OBJECT_ID('Homologacion', 'U') IS NOT NULL
    DROP TABLE Homologacion;
GO
IF OBJECT_ID('HomologacionEsquema', 'U') IS NOT NULL
    DROP TABLE HomologacionEsquema;
GO

CREATE TABLE Usuario (
    IdUsuario				INT IDENTITY(1,1) 
    ,Email					NVARCHAR(100) NOT NULL
    ,Nombre					NVARCHAR(500) NOT NULL
    ,Apellido				NVARCHAR(500)
	,Telefono				nvarchar(20)
    ,Clave					NVARCHAR(MAX)	NOT NULL
    ,Rol					NVARCHAR(20)	NOT NULL DEFAULT('USER')
	,Estado					NVARCHAR(1) NOT NULL DEFAULT('A')
    ,FechaCreacion			DATETIME	NOT NULL DEFAULT(GETDATE())
    ,FechaModifica			DATETIME	NOT NULL DEFAULT(GETDATE())  
    ,IdUserCreacion			INT			NOT NULL DEFAULT(0)
    ,IdUserModifica			INT			NOT NULL DEFAULT(0)

    ,CONSTRAINT PK_U_IdUsuario	PRIMARY KEY CLUSTERED (IdUsuario)  
    ,CONSTRAINT UK_U_Email		UNIQUE (Email)
    ,CONSTRAINT CK_U_Rol		CHECK  (Rol IN ('ADMIN', 'USER'))
    ,CONSTRAINT CK_U_Estado		CHECK  (Estado IN ('A', 'X'))
)
GO

CREATE TABLE EndPointWeb (
    IdEndPointWeb			INT IDENTITY(1,1)  
    ,UrlWeb					NVARCHAR(MAX)
    ,Nombre					NVARCHAR(100) NOT NULL
	,Estado					NVARCHAR(1) NOT NULL DEFAULT('A')
    ,FechaCreacion			DATETIME	NOT NULL DEFAULT(GETDATE())
    ,FechaModifica			DATETIME	NOT NULL DEFAULT(GETDATE())  
    ,IdUserCreacion			INT			NOT NULL DEFAULT(0)
    ,IdUserModifica			INT			NOT NULL DEFAULT(0)
    
	,CONSTRAINT PK_EP_IdEndpoint	PRIMARY KEY CLUSTERED (IdEndPointWeb)  
    ,CONSTRAINT UK_EP_Nombre		UNIQUE (Nombre)
    ,CONSTRAINT CK_EP_Estado		CHECK  (Estado IN ('A', 'X'))
)
GO

CREATE TABLE UsuarioEndpointPermiso (
    IdUsuarioEndpointPermiso    INT IDENTITY(1,1) 
    ,IdUsuario                  INT NOT NULL DEFAULT(0)
    ,IdEndPointWeb              INT NOT NULL DEFAULT(0)
    ,Accion                     NVARCHAR(10) NOT NULL
	,Estado						NVARCHAR(1) NOT NULL DEFAULT('A')
    ,FechaCreacion				DATETIME	NOT NULL DEFAULT(GETDATE())
    ,FechaModifica			    DATETIME	NOT NULL DEFAULT(GETDATE())  
    ,IdUserCreacion				INT			NOT NULL DEFAULT(0)
    ,IdUserModifica			    INT			NOT NULL DEFAULT(0)

	,CONSTRAINT PK_UEP_IdUsuarioEndpointPermiso	PRIMARY KEY CLUSTERED (IdUsuarioEndpointPermiso)  
    ,CONSTRAINT FK_UEP_IdUsuario		FOREIGN KEY (IdUsuario)		REFERENCES Usuario(IdUsuario)
    ,CONSTRAINT FK_UEP_IdEndPointWeb	FOREIGN KEY (IdEndPointWeb)	REFERENCES EndPointWeb(IdEndPointWeb)
    ,CONSTRAINT UK_UEP_Accion			CHECK (Accion IN ('GET', 'POST', 'PUT', 'DELETE'))
    ,CONSTRAINT CK_UEP_Estado			CHECK (Estado IN ('A', 'X'))
)
GO

CREATE TABLE Homologacion (
     IdHomologacion     	INT IDENTITY(1,1) NOT NULL
    ,IdHomologacionGrupo	INT DEFAULT(NULL) 
    ,CodigoHomologacion		NVARCHAR(25)  NOT NULL
    ,NombreHomologado		NVARCHAR(90)  NOT NULL
    ,MostrarWebOrden		INT DEFAULT(0) 
    ,MostrarWeb				NVARCHAR(90)  NOT NULL
	,Descripcion			NVARCHAR(200)
    ,InfoExtraJson			NVARCHAR(max) NOT NULL DEFAULT('{}')
	,Estado					NVARCHAR(1) NOT NULL DEFAULT('A')
    ,FechaCreacion			DATETIME	NOT NULL DEFAULT(GETDATE())
    ,FechaModifica			DATETIME	NOT NULL DEFAULT(GETDATE())  
    ,IdUserCreacion			INT			NOT NULL DEFAULT(0)
    ,IdUserModifica			INT			NOT NULL DEFAULT(0)  
	
	,CONSTRAINT  [PK_H_IdHomologacion]		PRIMARY KEY CLUSTERED (IdHomologacion) 
	,CONSTRAINT  [UK_H_CodigoHomologacion]	UNIQUE  (CodigoHomologacion)
    ,CONSTRAINT  [CK_H_InfoExtraJson]         CHECK   (ISJSON(InfoExtraJson) = 1 )
    ,CONSTRAINT  [CK_H_Estado]				CHECK   (Estado IN ('A', 'X'))
)
GO

CREATE TABLE HomologacionEsquema(
	 IdHomologacionEsquema     	INT IDENTITY(1,1) NOT NULL
    ,MostrarWebOrden			INT DEFAULT(0) 
    ,MostrarWeb					NVARCHAR(200)  NOT NULL
	,Descripcion				NVARCHAR(200)
    ,EsquemaJson				NVARCHAR(max) NOT NULL DEFAULT('{}')
	,Estado						NVARCHAR(1) NOT NULL DEFAULT('A')
	,FechaCreacion				DATETIME	NOT NULL DEFAULT(GETDATE())
    ,FechaModifica				DATETIME	NOT NULL DEFAULT(GETDATE())  
    ,IdUserCreacion				INT			NOT NULL DEFAULT(0)
    ,IdUserModifica				INT			NOT NULL DEFAULT(0)  
	,CONSTRAINT  [PK_HE_IdHomologacionEsquema]	PRIMARY KEY CLUSTERED (IdHomologacionEsquema) 
    ,CONSTRAINT  [CK_HE_EsquemaJson]			CHECK   (ISJSON(EsquemaJson) = 1 )
    ,CONSTRAINT  [CK_HE_Estado]				CHECK   (Estado IN ('A', 'X'))
)
GO

CREATE TABLE DataLake(
    IdDataLake				INT IDENTITY(1,1) NOT NULL
	,DataTipo				NVARCHAR(15) NOT NULL DEFAULT('NO_DEFINIDO') 
	,DataSistemaOrigen		NVARCHAR(15) NOT NULL	
    ,DataSistemaOrigenId	NVARCHAR(10) NOT NULL 
    ,DataSistemaFecha		DATETIME	
	,Estado					NVARCHAR(1) NOT NULL DEFAULT('A')
    ,DataFechaCarga			DATETIME	 NOT NULL DEFAULT(GETDATE())
	
	,CONSTRAINT  [PK_DL_IdDataLake]	PRIMARY KEY CLUSTERED (IdDataLake) 
	,CONSTRAINT  [UK_DL_DataTipo]		CHECK (DataTipo IN ('ORGANIZACION', 'PERSONA','NO_DEFINIDO'))
    ,CONSTRAINT  [CK_DL_Estado]		CHECK   (Estado IN ('A', 'X'))
)
GO

CREATE TABLE DataLakeOrganizacion(
    IdDataLakeOrganizacion  INT IDENTITY(1,1) NOT NULL
    ,IdHomologacionEsquema	INT NOT NULL  FOREIGN KEY REFERENCES HomologacionEsquema (IdHomologacionEsquema)
    ,IdDataLake				INT NOT NULL  FOREIGN KEY REFERENCES DataLake (IdDataLake)
    ,DataEsquemaJson        NVARCHAR(max) NOT NULL DEFAULT('{}')
    ,DataFechaCarga			DATETIME	  NOT NULL DEFAULT(GETDATE())
    ,FechaCreacion			DATETIME	NOT NULL DEFAULT(GETDATE())
	,Estado					NVARCHAR(1) NOT NULL DEFAULT('A')
	
	,CONSTRAINT  [PK_DKOrganizacion_IdDataLakeOrganizacion]	PRIMARY KEY CLUSTERED (IdDataLakeOrganizacion) 
    ,CONSTRAINT  [CK_DKOrganizacion_DataEsquemaJson]		CHECK   (ISJSON(DataEsquemaJson) = 1 )
    ,CONSTRAINT  [CK_DKOrganizacion_Estado]					CHECK   (Estado IN ('A', 'X'))
)
GO

CREATE TABLE DataLakePersona(
    IdDataLakePersona		INT IDENTITY(1,1) NOT NULL
    ,IdHomologacionEsquema	INT NOT NULL  FOREIGN KEY REFERENCES HomologacionEsquema (IdHomologacionEsquema)
    ,IdDataLake				INT NOT NULL  FOREIGN KEY REFERENCES DataLake (IdDataLake)
    ,DataEsquemaJson        NVARCHAR(max) NOT NULL DEFAULT('{}')
    ,DataFechaCarga			DATETIME	  NOT NULL DEFAULT(GETDATE())
    ,FechaCreacion			DATETIME	NOT NULL DEFAULT(GETDATE())
	,Estado					NVARCHAR(1) NOT NULL DEFAULT('A')
	
	,CONSTRAINT  [PK_DKPersona_DataLakePersona]	PRIMARY KEY CLUSTERED (IdDataLakePersona) 
    ,CONSTRAINT  [CK_DKPersona_DataEsquemaJson]	CHECK   (ISJSON(DataEsquemaJson) = 1 )
    ,CONSTRAINT  [CK_DKPersona_Estado]			CHECK   (Estado IN ('A', 'X'))
)
GO

CREATE TABLE WebSiteLog (
    IdWebSiteLog		INT IDENTITY(1,1) NOT NULL
    ,DataLogJson		NVARCHAR(max)
	,FechaCreacion		DATETIME	NOT NULL DEFAULT(GETDATE())
	,Estado				NVARCHAR(1) NOT NULL DEFAULT('A')
	
	,CONSTRAINT  [PK_WSL_WebSiteLog]		PRIMARY KEY CLUSTERED (IdWebSiteLog) 
    ,CONSTRAINT  [CK_WSL_DataEsquemaJson]	CHECK   (ISJSON(DataLogJson) = 1 )
    ,CONSTRAINT  [CK_WSL_Estado]			CHECK   (Estado IN ('A', 'X'))
)
GO
