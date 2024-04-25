


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

