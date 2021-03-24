
CREATE TABLE [dbo].[TsupMovimientosCuentasDetalle](
	[RowID] [int] NOT NULL identity,
	[MovimientoID] [int] NULL,
	[Debito] [float] NULL,
	[Credito] [float] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



CREATE procedure [dbo].[sp_supMovimientosCuentasDetalle]
(
	@flag int, @MovimientoID int, @Debito float, @Credito float
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from [TsupMovimientosCuentasDetalle]
		where MovimientoID = (select max(MovimientoID) from [TsupMovimientosCuentasDetalle])
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS MOVIMIENTOS
	begin
		select * from [TsupMovimientosCuentasDetalle]
		where (MovimientoID = @MovimientoID) 
	end


	if (@flag = 1) -- INSERTAR MOVIMIENTOS
	begin
		insert into [TsupMovimientosCuentasDetalle] (MovimientoID,	Debito, Credito)
		values (@MovimientoID, @Debito, @Credito)
	end

	if (@flag = 2) -- EDITAR MOVIMIENTOS
	begin
		Update [TsupMovimientosCuentasDetalle] set Debito = @Debito, Credito = @Credito
		where MovimientoID = @MovimientoID
	end


GO




