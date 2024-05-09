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
                ,(	SELECT  Distinct h.MostrarWeb NombreColumna, o.FullTextOrganizacion  ValorColumna
                    FROM	OrganizacionFullText	o
                    JOIN	Homologacion			h on o.IdHomologacion = h.IdHomologacion
                    Where	o.IdDataLakeOrganizacion = t.IdDataLakeOrganizacion
                    for json path
                ) DataJson
        FROM	@tb t
END;
GO
--> validar la funcion
declare  @BuscarPalabra		NVARCHAR(100) = N'act'
exec psBuscarPalabra 'act', 47