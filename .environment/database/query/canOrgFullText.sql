USE CAN_DB;
GO

--SELECT fulltext_catalog_id, name FROM sys.fulltext_catalogs
IF EXISTS (SELECT * FROM sys.fulltext_catalogs WHERE name = 'OrganizacionFullText_cat')
begin
	DROP FULLTEXT INDEX ON OrganizacionFullText;
	DROP FULLTEXT CATALOG [OrganizacionFullText_cat];
end
CREATE FULLTEXT CATALOG OrganizacionFullText_cat WITH ACCENT_SENSITIVITY = OFF;
GO

IF OBJECT_ID('OrganizacionFullText', 'U') IS NOT NULL
    DROP TABLE OrganizacionFullText;
GO
CREATE TABLE OrganizacionFullText(
    IdOrganizacion			INT NOT NULL,--IDENTITY(1,1),
    FullTextOrganizacion    NVARCHAR(MAX) NULL,			-- fulltext a considerear
    CONSTRAINT [PK_OrganizacionFullText] PRIMARY KEY CLUSTERED (IdOrganizacion ASC)  
);
GO

CREATE FULLTEXT INDEX ON OrganizacionFullText
( FullTextOrganizacion LANGUAGE 3082 )   --3082	Spanish 
KEY INDEX [PK_OrganizacionFullText]
ON OrganizacionFullText_cat
WITH STOPLIST = SYSTEM;
GO


-- Asociar una lista de palabras irrelevantes
IF EXISTS (SELECT * FROM sys.fulltext_stoplists WHERE name = 'OrganizacionFullText_Stoplist')
    DROP FULLTEXT STOPLIST OrganizacionFullText_Stoplist;
CREATE FULLTEXT STOPLIST OrganizacionFullText_Stoplist FROM SYSTEM STOPLIST;  
GO 

-- para no considerar en las busquedas
IF NOT EXISTS (SELECT * FROM sys.fulltext_stopwords WHERE stopword = 'en' AND LANGUAGE ='Spanish')
	ALTER FULLTEXT STOPLIST OrganizacionFullText_Stoplist ADD 'en' LANGUAGE 'Spanish';	

IF NOT EXISTS (SELECT * FROM sys.fulltext_stopwords WHERE stopword = 'la' AND LANGUAGE ='Spanish')
	ALTER FULLTEXT STOPLIST OrganizacionFullText_Stoplist ADD 'la' LANGUAGE 'Spanish';	

IF NOT EXISTS (SELECT * FROM sys.fulltext_stopwords WHERE stopword = 'los' AND LANGUAGE ='Spanish')
	ALTER FULLTEXT STOPLIST OrganizacionFullText_Stoplist ADD 'los' LANGUAGE 'Spanish';	

IF NOT EXISTS (SELECT * FROM sys.fulltext_stopwords WHERE stopword = 'el' AND LANGUAGE ='Spanish')
	ALTER FULLTEXT STOPLIST OrganizacionFullText_Stoplist ADD 'el' LANGUAGE 'Spanish';	

IF NOT EXISTS (SELECT * FROM sys.fulltext_stopwords WHERE stopword = 'entre' AND LANGUAGE ='Spanish')
	ALTER FULLTEXT STOPLIST OrganizacionFullText_Stoplist ADD 'entre' LANGUAGE 'Spanish';	
GO 
--ALTER FULLTEXT STOPLIST OrganizacionFullText_Stoplist ADD 'en' LANGUAGE 'French';  


-- ver palabras ingresadas 
-- SELECT * FROM sys.fulltext_stopwords WHERE LANGUAGE ='Spanish'

-- validado el proceso de busqueda
--select	oId,	FullTextOrganizacion 
--from	OrganizacionFullText
--where	contains(FullTextOrganizacion , '"Ruben" OR "pera"')
--WHERE	CONTAINS(FullTextOrganizacion , '"Snap Happy 100EZ" OR FORMSOF(THESAURUS,"Snap Happy") OR "100EZ"')
--AND product_cost < 200 ;  


--CREATE FULLTEXT INDEX ON Person  
--(Nombre, Direccion, Celular, Fecha ) -- TYPE COLUMN FileExtension LANGUAGE 1033)  -- en el caso de archivos binarios
--KEY INDEX PK_PersonID
--ON personFTC  
--WITH STOPLIST = SYSTEM
--GO


--SELECT * FROM sys.fulltext_languages  ORDER BY lcid

--SELECT t.name AS TableName, c.name AS FTCatalogName  
--FROM sys.tables t JOIN sys.fulltext_indexes i  
--  ON t.object_id = i.object_id  
--JOIN sys.fulltext_catalogs c  
--  ON i.fulltext_catalog_id = c.fulltext_catalog_id

--USE CAN_DB;
--GO
---- Crear el inicio de sesi�n (login)
--CREATE LOGIN pat_mic WITH PASSWORD = 'pat_mic_PASS';

---- Crear el usuario de la base de datos y asignarle el inicio de sesi�n
--CREATE USER pat_mic FOR LOGIN pat_mic;

---- Otorgar permisos al usuario en la base de datos
--GRANT ALL TO pat_mic; -- O tambi�n puedes usar: 
-- GRANT CONTROL TO pat_mic;

