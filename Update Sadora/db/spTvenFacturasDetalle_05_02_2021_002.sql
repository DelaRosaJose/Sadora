CREATE TABLE [dbo].[TvenFacturasDetalles](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[FacturaID] [int] NOT NULL,
	[ArticuloID] [int] NOT NULL,
	[Cantidad] [int] NULL,
	[Descuento] [float] NOT NULL,
	[SubTotal] [float] NOT NULL,
	[ITBIS] [float] NOT NULL,
	[Total] [float] NOT NULL,
	[UsuarioID] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

CREATE procedure [dbo].[sp_venFacturasDetalles]
(
@flag int, @FacturaID int, @ArticuloID int, @Cantidad int, @Descuento float, @SubTotal float, @ITBIS float, @Total float, @UsuarioID int
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TvenFacturasDetalles
		where FacturaID = (select max(FacturaID) from TvenFacturasDetalles)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS COMPROBANTES
	begin
		select * from TvenFacturasDetalles
		where (FacturaID = @FacturaID or @FacturaID = 0) /*and (RNC = @RNC or @RNC = '') and (Nombre like '%'+@Nombre+'%' or @Nombre = '')*/
		order by FacturaID
	end


	if (@flag = 1) -- INSERTAR COMPROBANTES
	begin
		insert into TvenFacturasDetalles (FacturaID, ArticuloID, Cantidad, Descuento, SubTotal, ITBIS, Total, UsuarioID)
		values ((SELECT isnull(MAX(FacturaID),100000)+1 FROM TvenFacturasDetalles), @ArticuloID, @Cantidad, @Descuento, @SubTotal, @ITBIS, @Total, @UsuarioID)
	end

	if (@flag = 2) -- EDITAR COMPROBANTES
	begin
		Update TvenFacturasDetalles set ArticuloID = @ArticuloID, Cantidad = @Cantidad, Descuento = @Descuento, SubTotal = @SubTotal, ITBIS = @ITBIS, Total = @Total, UsuarioID = @UsuarioID
		where FacturaID = @FacturaID
	end

GO






