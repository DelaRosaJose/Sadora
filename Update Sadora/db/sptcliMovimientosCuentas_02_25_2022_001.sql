
CREATE TABLE [dbo].[TcliMovimientosCuentas](
	[RowID] [int] NOT NULL identity,
	[MovimientoID] [int] NULL,
	[TipoMovimiento] [varchar](20) null,
	[Descripcion] [varchar](40) NULL,
	[ClienteID] [int] NULL,
	[Fecha] [datetime] NULL,
	[Debito] [float] NULL,
	[Credito] [float] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



CREATE procedure [dbo].[sp_cliMovimientosCuentas]
(
	@flag int, @MovimientoID int, @TipoMovimiento varchar(20), @Descripcion varchar(40), @ClienteID int, @Fecha datetime, @Debito float, 
	@Credito float
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from [TcliMovimientosCuentas]
		where MovimientoID = (select max(MovimientoID) from [TcliMovimientosCuentas])
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS MOVIMIENTOS
	begin
		select * from [TcliMovimientosCuentas]
		where (MovimientoID = @MovimientoID or @MovimientoID = 0) and (Descripcion = @Descripcion or @Descripcion = '') and (ClienteID = @ClienteID or @ClienteID = '')
		order by MovimientoID
	end


	if (@flag = 1) -- INSERTAR MOVIMIENTOS
	begin
		insert into [TcliMovimientosCuentas] (MovimientoID, TipoMovimiento, Descripcion, ClienteID, Fecha,	Debito, Credito)
		values (@MovimientoID, @TipoMovimiento, @Descripcion, @ClienteID, @Fecha, @Debito, @Credito)
	end

	if (@flag = 2) -- EDITAR MOVIMIENTOS
	begin
		Update [TcliMovimientosCuentas] set TipoMovimiento = @TipoMovimiento, Descripcion = @Descripcion, ClienteID = @ClienteID, Fecha = @Fecha, Debito = @Debito, 
		Credito = @Credito
		where MovimientoID = @MovimientoID
	end


GO




