
CREATE TABLE [dbo].[TsupTransacciones](
	[RowID] [int] NOT NULL identity,
	[TransaccionID] [int] NULL,
	[TipoTransaccion] [varchar](20) NULL,
	[ProveedorID] [int] NULL,
	[Fecha] [datetime] NULL,
	[MontoExcento] [float] NULL,
	[MontoGravado] [float] NULL,
	[ITBIS] [float] NULL,
	[Estado] [varchar](10) NULL,
	[UsuarioID] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



CREATE procedure [dbo].[sp_supTransacciones]
(
	@flag int, @TransaccionID int, @TipoTransaccion varchar(20), @ProveedorID int, @Fecha datetime,	@MontoExcento float, 
	@MontoGravado float, @ITBIS float, @Estado varchar(10), @UsuarioID int
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TsupTransacciones
		where TransaccionID = (select max(TransaccionID) from TsupTransacciones)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS TRANSACCIONES
	begin
		select * from TsupTransacciones
		where (TransaccionID = @TransaccionID or @TransaccionID = 0) and (TipoTransaccion = @TipoTransaccion or @TipoTransaccion = '') and (ProveedorID = @ProveedorID or @ProveedorID = '')
		order by TransaccionID
	end


	if (@flag = 1) -- INSERTAR TRANSACCIONES
	begin
		insert into TsupTransacciones (TransaccionID, TipoTransaccion, ProveedorID, Fecha,	MontoExcento, MontoGravado, ITBIS, Estado, UsuarioID)
		values (@TransaccionID, @TipoTransaccion, @ProveedorID, @Fecha, @MontoExcento, @MontoGravado, @ITBIS, @Estado, @UsuarioID)
	end

	if (@flag = 2) -- EDITAR TRANSACCIONES
	begin
		Update TsupTransacciones set TipoTransaccion = @TipoTransaccion, ProveedorID = @ProveedorID, Fecha = @Fecha, MontoExcento = @MontoExcento, 
		MontoGravado = @MontoGravado, ITBIS = @ITBIS, Estado = @Estado, UsuarioID = @UsuarioID
		where TransaccionID = @TransaccionID
	end


GO




