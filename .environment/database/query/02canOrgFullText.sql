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
     IdOrganizacionFullText	 INT NOT NULL IDENTITY(1,1)
    ,IdDataLakeOrganizacion	 INT NOT NULL 
    ,IdHomologacion          INT NOT NULL
    ,FullTextOrganizacion    NVARCHAR(MAX) NULL			-- fulltext a considerear
    ,CONSTRAINT [PK_IdOrganizacionFullText] PRIMARY KEY CLUSTERED (IdOrganizacionFullText)  
);
GO

CREATE FULLTEXT INDEX ON OrganizacionFullText
( FullTextOrganizacion LANGUAGE 3082 )   --3082	Spanish 
KEY INDEX [PK_IdOrganizacionFullText]
ON OrganizacionFullText_cat
WITH STOPLIST = SYSTEM;
GO

-- select * from sys.fulltext_catalogs;
Select idOrganizacion from OrganizacionFullText WHERE CONTAINS((FullTextOrganizacion), 'empresa');