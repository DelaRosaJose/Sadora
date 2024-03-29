USE [Sadora]
GO
/****** Object:  StoredProcedure [dbo].[sp_venFacturas]    Script Date: 11/3/2021 1:05:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[sp_venFacturas]
(
@flag int, @FacturaID int, @ClienteID int, @RNC varchar(20),@ClaseNCF varchar(2) ,@NCF varchar(15), @Nombre varchar(50), @Descuento float, @SubTotal float, @ITBIS float, @Total float, @Estado varchar(10), @UsuarioID int, @FechaCreacion datetime
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TvenFacturas
		where FacturaID = (select max(FacturaID) from TvenFacturas)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS COMPROBANTES
	begin
		select * from TvenFacturas
		where (FacturaID = @FacturaID or @FacturaID = 0) /*and (RNC = @RNC or @RNC = '') and (Nombre like '%'+@Nombre+'%' or @Nombre = '')*/
		order by FacturaID
	end


	if (@flag = 1) -- INSERTAR COMPROBANTES
	begin
		
		declare @factura int = (SELECT isnull(MAX(FacturaID),100000)+1 FROM TvenFacturas),
				@NCFReloaded varchar(200) = (Select NCF from getNextNCF(@ClaseNCF,NULL));

		insert into TvenFacturas (FacturaID, ClienteID, RNC, ClaseNCF,NCF, Nombre, Descuento, SubTotal, ITBIS, Total, Estado, UsuarioID, FechaCreacion)
		values (@factura, @ClienteID, @RNC, @ClaseNCF, @NCFReloaded, @Nombre, @Descuento, @SubTotal, @ITBIS, @Total, @Estado, @UsuarioID, GETDATE())

		update TconComprobantes set NextNCF = @NCFReloaded, Disponibles = Disponibles -1 where ComprobanteID = @ClaseNCF



		select 'FacturaID'=@factura, 'ClienteID'=@ClienteID, 'RNC'=@RNC , 'NCF'=@NCF, 'Nombre'=@Nombre, 
		'Descuento'=@Descuento, 'SubTotal'=@SubTotal, 'ITBIS'=@ITBIS, 'Total'=@Total, 'Estado'=@Estado, 'FechaCreacion'=@FechaCreacion
	end

	if (@flag = 2) -- EDITAR COMPROBANTES
	begin
		Update TvenFacturas set ClienteID = @ClienteID, RNC = RNC, ClaseNCF = @ClaseNCF, NCF = @NCF, Nombre = @Nombre, Descuento = @Descuento, SubTotal = @SubTotal, 
		ITBIS = @ITBIS, Total = @Total, Estado = @Estado, UsuarioID = @UsuarioID
		where FacturaID = @FacturaID
	end

