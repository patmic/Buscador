use CAN_DB;
go

CREATE OR ALTER PROCEDURE psBuscarPalabra
(
    @BuscarPalabra		NVARCHAR(100)
	,@IdHomologacion1	INT = NULL		--filtro 1
    ,@IdHomologacion2	INT = NULL		--filtro 2
    ,@IdHomologacion3	INT = NULL		--filtro 3
    ,@IdHomologacion4	INT = NULL		--filtro 4
) AS
BEGIN
        DECLARE @tb TABLE (IdDataLakeOrganizacion INT );

        INSERT INTO @tb
        SELECT DISTINCT IdDataLakeOrganizacion
                --,o.IdHomologacion
                --,h.MostrarWeb
                --,o.FullTextOrganizacion
        FROM OrganizacionFullText
        WHERE  (@IdHomologacion1 IS NOT NULL AND IdHomologacion = @IdHomologacion1 AND FullTextOrganizacion like '%'+ @BuscarPalabra +'%')
			or (@IdHomologacion2 IS NOT NULL AND IdHomologacion = @IdHomologacion2 AND FullTextOrganizacion like '%'+ @BuscarPalabra +'%')
            or (@IdHomologacion3 IS NOT NULL AND IdHomologacion = @IdHomologacion3 AND FullTextOrganizacion like '%'+ @BuscarPalabra +'%')
            or (@IdHomologacion4 IS NOT NULL AND IdHomologacion = @IdHomologacion4 AND FullTextOrganizacion like '%'+ @BuscarPalabra +'%')

        SELECT	t.IdDataLakeOrganizacion
                ,(	SELECT  Distinct h.IdHomologacion IdHomologacion, o.FullTextOrganizacion  ValorColumna
                    FROM	OrganizacionFullText	o with(nolock)
                    JOIN	Homologacion			h with(nolock) on o.IdHomologacion = h.IdHomologacion
                    Where	o.IdDataLakeOrganizacion = t.IdDataLakeOrganizacion
                    for json path
                ) DataJson
        FROM	@tb t
END;
GO

SELECT * FROM OrganizacionFullText

--> validar la funcion
exec psBuscarPalabra 'org', 41

--[{"NombreColumna":"Alcance acreditado","ValorColumna":"1 Alcance acredi"},{"NombreColumna":"Alcance acreditado","ValorColumna":"2 Alcance acredi"},{"NombreColumna":"Alcance Detalle","ValorColumna":"1 Alcance Detall"},{"NombreColumna":"Alcance Detalle","ValorColumna":"2 Alcance Detall"},{"NombreColumna":"Esquema","ValorColumna":"1 Esquema\t\t  "},{"NombreColumna":"Esquema","ValorColumna":"2 Esquema\t\t  "},{"NombreColumna":"Estado","ValorColumna":"1 Estado\t\t  "},{"NombreColumna":"Estado","ValorColumna":"2 Estado\t\t  "},{"NombreColumna":"Identificación","ValorColumna":"1 Identificación"},{"NombreColumna":"Identificación","ValorColumna":"2 Identificación"},{"NombreColumna":"Norma","ValorColumna":"1 Norma\t\t  "},{"NombreColumna":"Norma","ValorColumna":"2 Norma\t\t  "},{"NombreColumna":"oa_ciudad","ValorColumna":"1 oa_ciudad\t  "},{"NombreColumna":"oa_ciudad","ValorColumna":"2 oa_ciudad\t  "},{"NombreColumna":"oa_descripcion","ValorColumna":"1 oa_descripcion"},{"NombreColumna":"oa_descripcion","ValorColumna":"2 oa_descripcion"},{"NombreColumna":"oa_direcion","ValorColumna":"1 oa_direcion\t  "},{"NombreColumna":"oa_direcion","ValorColumna":"2 oa_direcion\t  "},{"NombreColumna":"oa_inicialpais","ValorColumna":"1 oa_inicialpais"},{"NombreColumna":"oa_inicialpais","ValorColumna":"2 oa_inicialpais"},{"NombreColumna":"oa_logo","ValorColumna":"1 oa_logo\t\t  "},{"NombreColumna":"oa_logo","ValorColumna":"2 oa_logo\t\t  "},{"NombreColumna":"oa_nombre","ValorColumna":"1 oa_nombre\t  "},{"NombreColumna":"oa_nombre","ValorColumna":"2 oa_nombre\t  "},{"NombreColumna":"oa_nombreInicial","ValorColumna":"1 oa_nombreInici"},{"NombreColumna":"oa_nombreInicial","ValorColumna":"2 oa_nombreInici"},{"NombreColumna":"oa_pais","ValorColumna":"1 oa_pais\t\t  "},{"NombreColumna":"oa_pais","ValorColumna":"2 oa_pais\t\t  "},{"NombreColumna":"oa_sitioweb","ValorColumna":"1 oa_sitioweb\t  "},{"NombreColumna":"oa_sitioweb","ValorColumna":"2 oa_sitioweb\t  "},{"NombreColumna":"oa_telefono","ValorColumna":"1 oa_telefono\t  "},{"NombreColumna":"oa_telefono","ValorColumna":"2 oa_telefono\t  "},{"NombreColumna":"OEC\/Razón social","ValorColumna":"1 OEC\/Razón soci"},{"NombreColumna":"OEC\/Razón social","ValorColumna":"2 OEC\/Razón soci"},{"NombreColumna":"Organismo","ValorColumna":"1 Organismo\t  "},{"NombreColumna":"Organismo","ValorColumna":"2 Organismo\t  "},{"NombreColumna":"País","ValorColumna":"1 País\t      "},{"NombreColumna":"País","ValorColumna":"2 País\t      "},{"NombreColumna":"Reconocimiento","ValorColumna":"1 Reconocimiento"},{"NombreColumna":"Reconocimiento","ValorColumna":"2 Reconocimiento"}]