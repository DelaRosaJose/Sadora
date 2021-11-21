USE [Sadora]
GO
/****** Object:  StoredProcedure [dbo].[sp_venFacturasDetalles]    Script Date: 11/21/2021 6:54:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[sp_venFacturasDetalles]
(
@Flag int, @FacturaID  varchar(25), @ArticuloID varchar(50), @NombreArticulo varchar(50), @Cantidad int, @Precio float, @Descuento float, @SubTotal float, @ITBIS float, @Total float, @UsuarioID int
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select b.Tarjeta, isnull(a.NombreArticulo,b.Nombre) as Nombre, a.Cantidad, a.Precio, a.SubTotal, a.ITBIS, a.Total from TvenFacturasDetalles a inner join
		TinvArticulos b on a.ArticuloID = b.ArticuloID
		where a.FacturaID = (select max(FacturaID) from TvenFacturasDetalles)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS COMPROBANTES
	begin
		select b.Tarjeta, isnull(a.NombreArticulo,b.Nombre) as Nombre, a.Cantidad, a.Precio, a.SubTotal, a.ITBIS, a.Total from TvenFacturasDetalles a inner join
		TinvArticulos b on a.ArticuloID = b.ArticuloID
		where (FacturaID = @FacturaID or @FacturaID = 0) /*and (RNC = @RNC or @RNC = '') and (Nombre like '%'+@Nombre+'%' or @Nombre = '')*/
		order by FacturaID
	end


	if (@flag = 1) -- INSERTAR COMPROBANTES
	begin
		insert into TvenFacturasDetalles (FacturaID, ArticuloID, NombreArticulo, Cantidad, Precio, Descuento, SubTotal, ITBIS, Total, UsuarioID)
		values (@FacturaID, @ArticuloID, @NombreArticulo, @Cantidad, @Precio, @Descuento, @SubTotal, @ITBIS, @Total, @UsuarioID)

		update TinvArticulos set Cantidad = (Cantidad - @Cantidad) where ArticuloID = @ArticuloID

	end

	if (@flag = 2) -- EDITAR COMPROBANTES
	begin
		Update TvenFacturasDetalles set ArticuloID = @ArticuloID, Cantidad = @Cantidad, Precio = @Precio,Descuento = @Descuento, SubTotal = @SubTotal, ITBIS = @ITBIS, Total = @Total, UsuarioID = @UsuarioID
		where FacturaID = @FacturaID
	end

