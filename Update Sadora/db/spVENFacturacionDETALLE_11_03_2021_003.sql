USE [Sadora]
GO
/****** Object:  StoredProcedure [dbo].[sp_venFacturasDetalles]    Script Date: 11/3/2021 1:41:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[sp_venFacturasDetalles]
(
@Flag int, @FacturaID  varchar(25), @ArticuloID varchar(50), @Cantidad int, @Precio float, @Descuento float, @SubTotal float, @ITBIS float, @Total float, @UsuarioID int
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select b.Tarjeta, b.Nombre, a.Cantidad, a.Precio, a.SubTotal, a.ITBIS, a.Total from TvenFacturasDetalles a inner join
		TinvArticulos b on a.ArticuloID = b.ArticuloID
		where a.FacturaID = (select max(FacturaID) from TvenFacturasDetalles)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS COMPROBANTES
	begin
		select b.Tarjeta, b.Nombre, a.Cantidad, a.Precio, a.SubTotal, a.ITBIS, a.Total from TvenFacturasDetalles a inner join
		TinvArticulos b on a.ArticuloID = b.ArticuloID
		where (FacturaID = @FacturaID or @FacturaID = 0) /*and (RNC = @RNC or @RNC = '') and (Nombre like '%'+@Nombre+'%' or @Nombre = '')*/
		order by FacturaID
	end


	if (@flag = 1) -- INSERTAR COMPROBANTES
	begin
		insert into TvenFacturasDetalles (FacturaID, ArticuloID, Cantidad, Precio, Descuento, SubTotal, ITBIS, Total, UsuarioID)
		values (@FacturaID, @ArticuloID, @Cantidad, @Precio, @Descuento, @SubTotal, @ITBIS, @Total, @UsuarioID)


		
		--declare @factura int = (SELECT isnull(MAX(FacturaID),100000)+1 FROM TvenFacturas),
		--		@NCFReloaded varchar(200) = (Select NCF from getNextNCF(@ClaseNCF,NULL));

		--insert into TvenFacturas (FacturaID, ClienteID, RNC, ClaseNCF,NCF, Nombre, Descuento, SubTotal, ITBIS, Total, Estado, UsuarioID, FechaCreacion)
		--values (@factura, @ClienteID, @RNC, @ClaseNCF, @NCFReloaded, @Nombre, @Descuento, @SubTotal, @ITBIS, @Total, @Estado, @UsuarioID, GETDATE())
	

		update TinvArticulos set Cantidad = (Cantidad - @Cantidad) where ArticuloID = @ArticuloID


		--select 'FacturaID'=@factura, 'ClienteID'=@ClienteID, 'RNC'=@RNC , 'NCF'=@NCF, 'Nombre'=@Nombre, 
		--'Descuento'=@Descuento, 'SubTotal'=@SubTotal, 'ITBIS'=@ITBIS, 'Total'=@Total, 'Estado'=@Estado, 'FechaCreacion'=@FechaCreacion




	end

	if (@flag = 2) -- EDITAR COMPROBANTES
	begin
		Update TvenFacturasDetalles set ArticuloID = @ArticuloID, Cantidad = @Cantidad, Precio = @Precio,Descuento = @Descuento, SubTotal = @SubTotal, ITBIS = @ITBIS, Total = @Total, UsuarioID = @UsuarioID
		where FacturaID = @FacturaID
	end

