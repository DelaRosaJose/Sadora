
CREATE TABLE [dbo].[TsupMovimientosCuentas](
	[RowID] [int] NOT NULL identity,
	[MovimientoID] [int] NULL,
	[Descripcion] [varchar](40) NULL,
	[ProveedorID] [int] NULL,
	[Fecha] [datetime] NULL,
	[Debito] [float] NULL,
	[Credito] [float] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



CREATE procedure [dbo].[sp_supMovimientosCuentas]
(
	@flag int, @MovimientoID int, @Descripcion varchar(40), @ProveedorID int, @Fecha datetime,	@Debito float, 
	@Credito float
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from [TsupMovimientosCuentas]
		where MovimientoID = (select max(MovimientoID) from [TsupMovimientosCuentas])
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS MOVIMIENTOS
	begin
		select * from [TsupMovimientosCuentas]
		where (MovimientoID = @MovimientoID or @MovimientoID = 0) and (Descripcion = @Descripcion or @Descripcion = '') and (ProveedorID = @ProveedorID or @ProveedorID = '')
		order by MovimientoID
	end


	if (@flag = 1) -- INSERTAR MOVIMIENTOS
	begin
		insert into [TsupMovimientosCuentas] (MovimientoID, Descripcion, ProveedorID, Fecha,	Debito, Credito)
		values (@MovimientoID, @Descripcion, @ProveedorID, @Fecha, @Debito, @Credito)
	end

	if (@flag = 2) -- EDITAR MOVIMIENTOS
	begin
		Update [TsupMovimientosCuentas] set Descripcion = @Descripcion, ProveedorID = @ProveedorID, Fecha = @Fecha, Debito = @Debito, 
		Credito = @Credito
		where MovimientoID = @MovimientoID
	end


GO




