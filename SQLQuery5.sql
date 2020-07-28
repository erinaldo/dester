SELECT FacturasVentas.NROFACTURAINTERNO, DetalleFacturaVentas.IDDETALLEFACTURAVENTAS, Articulo.IDARTICULO, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL 
as 'AntidadArticuloDisponible', 
DetalleFacturaVentas.CANTIDAD 
as 'CantidadArtFacturado' 
FROM FacturasVentas, DetalleFacturaVentas, Articulo 
WHERE FacturasVentas.IDEMPRESA = 1 
AND DetalleFacturaVentas.IDARTICULO = Articulo.IDARTICULO 
AND FacturasVentas.NROFACTURAINTERNO = DetalleFacturaVentas.NROFACTURAINTERNO 
AND Articulo.CODIGO = '0005'
AND FacturasVentas.NROFACTURAINTERNO = 34973



SELECT FacturasCompras.NROFACTURAINTERNO, DetalleFacturaCompras.IDDETALLEFACTURACOMPRAS, Articulo.IDARTICULO, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL as 'AntidadArticuloDisponible', 
DetalleFacturaCompras.CANTIDAD as 'CantidadArtFacturado' FROM FacturasCompras, DetalleFacturaCompras, Articulo WHERE FacturasCompras.IDEMPRESA = 1 AND 
DetalleFacturaCompras.IDARTICULO = Articulo.IDARTICULO AND FacturasCompras.NROFACTURAINTERNO = DetalleFacturaCompras.NROFACTURAINTERNO AND Articulo.Codigo = '0003' 
AND FacturasCompras.NROFACTURAINTERNO =3213