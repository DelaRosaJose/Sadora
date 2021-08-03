CREATE TABLE [dbo].[TvenFacturas](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[FacturaID] [int] NOT NULL,
	[ClienteID] [int] NOT NULL,
	[RNC] [int] NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descuento] [float] NOT NULL,
	[SubTotal] [float] NOT NULL,
	[ITBIS] [float] NOT NULL,
	[Total] [float] NOT NULL,
	[Estado] [varchar](10) NOT NULL,
	[UsuarioID] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

CREATE procedure [dbo].[sp_venFacturas]
(
@flag int, @FacturaID int, @ClienteID int, @RNC int, @Nombre varchar, @Descuento float, @SubTotal float, @ITBIS float, @Total float, @Estado varchar, @UsuarioID int
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
		where (FacturaID = @FacturaID or @FacturaID = 0) /*and (RNC = @RNC or @RNC = '')*/ and (Nombre like '%'+@Nombre+'%' or @Nombre = '')
		order by FacturaID
	end


	if (@flag = 1) -- INSERTAR COMPROBANTES
	begin
		insert into TvenFacturas (FacturaID, ClienteID, RNC, Nombre, Descuento, SubTotal, ITBIS, Total, Estado, UsuarioID)
		values ((SELECT isnull(MAX(FacturaID),100000)+1 FROM TvenFacturas), @ClienteID, @RNC, @Nombre, @Descuento, @SubTotal, @ITBIS, @Total, @Estado, @UsuarioID)
	end

	if (@flag = 2) -- EDITAR COMPROBANTES
	begin
		Update TvenFacturas set ClienteID = @ClienteID, RNC = RNC, Nombre = @Nombre, Descuento = @Descuento, SubTotal = @SubTotal, ITBIS = @ITBIS, Total = @Total, Estado = @Estado, UsuarioID = @UsuarioID
		where FacturaID = @FacturaID
	end

GO






