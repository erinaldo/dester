SELECT Remito.NROREMITOINTERNO, DetalleRemito.IDDETALLEREMITO, Articulo.IDARTICULO, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL as 'AntidadArticuloDisponible', DetalleRemito.CANTIDAD as 'CantidadArtRemito'
FROM Remito, DetalleRemito, Articulo 
WHERE Remito.IDEMPRESA = 1
AND DetalleRemito.IDARTICULO = Articulo.IDARTICULO 
AND Remito.NROREMITOINTERNO = DetalleRemito.NROREMITOINTERNO
AND Remito.NROREMITOINTERNO = 31215