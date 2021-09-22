CREATE TABLE [dbo].[TvenCajasDetalle](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[CajaID] [int] NOT NULL,
	[MetodoID] [int] NOT NULL,
	[Alta] [bit] NULL,
	[UsuarioID] [int] NULL
) ON [PRIMARY]

GO

CREATE CLUSTERED INDEX [IX_TvenCajasDetalle] ON [dbo].[TvenCajasDetalle]
(
	[CajaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

SET ANSI_PADDING OFF
GO

CREATE procedure [dbo].[sp_venCajasDetalle]
(
@flag int, @CajaID int, @MetodoID int, @Alta bit, @UsuarioID int
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TvenCajasDetalle
		where CajaID = (select max(CajaID) from TvenCajasDetalle)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS COMPROBANTES
	begin
		select * from TvenCajasDetalle
		where (CajaID = @CajaID or @CajaID = 0) /*and (RNC = @RNC or @RNC = '')*/ --and (Nombre like '%'+@Nombre+'%' or @Nombre = '')
		--order by FacturaID
	end


	if (@flag = 1) -- INSERTAR COMPROBANTES
	begin
		insert into TvenCajasDetalle (CajaID, MetodoID, Alta, UsuarioID)
		values ((SELECT isnull(MAX(CajaID),100000)+1 FROM TvenCajasDetalle), @MetodoID, @Alta, @UsuarioID)
	end

	if (@flag = 2) -- EDITAR COMPROBANTES
	begin
		Update TvenCajasDetalle set MetodoID = @MetodoID, Alta = @Alta, UsuarioID = @UsuarioID
		where CajaID = @CajaID
	end

GO






