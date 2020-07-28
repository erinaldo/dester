SELECT FacturasVentas.NROFACTURAINTERNO, DetalleFacturaVentas.IDDETALLEFACTURAVENTAS, Articulo.IDARTICULO, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL as 'AntidadArticuloDisponible', DetalleFacturaVentas.CANTIDAD as 'CantidadArtFactura'
FROM FacturasVentas, DetalleFacturaVentas, Articulo 
WHERE FacturasVentas.IDEMPRESA = 1
AND DetalleFacturaVentas.IDARTICULO = Articulo.IDARTICULO 
AND FacturasVentas.NROFACTURAINTERNO = DetalleFacturaVentas.NROFACTURAINTERNO
AND FacturasVentas.NROFACTURAINTERNO = 34967







