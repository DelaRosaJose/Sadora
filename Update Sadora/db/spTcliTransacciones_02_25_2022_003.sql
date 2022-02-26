
CREATE TABLE [dbo].[TcliTransacciones](
	[RowID] [int] NOT NULL identity,
	[TransaccionID] [int] NULL,
	[TipoTransaccion] [varchar](20) NULL,
	[ClienteID] [int] NULL,
	[Fecha] [datetime] NULL,
	[MontoExcento] [float] NULL,
	[MontoGravado] [float] NULL,
	[ITBIS] [float] NULL,
	[Estado] [varchar](10) NULL,
	[FacturaID] [varchar](25) null,
	[Observacion] [varchar](100) null,
	[UsuarioID] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



CREATE procedure [dbo].[sp_cliTransacciones]
(
	@flag int, @TransaccionID int, @TipoTransaccion varchar(20), @ClienteID int, @Fecha datetime,	@MontoExcento float, 
	@MontoGravado float, @ITBIS float, @Estado varchar(10), @FacturaID varchar(25),	@Observacion varchar(100), @UsuarioID int
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TcliTransacciones
		where TransaccionID = (select max(TransaccionID) from TcliTransacciones)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS TRANSACCIONES
	begin
		select * from TcliTransacciones
		where (TransaccionID = @TransaccionID or @TransaccionID = 0) and (TipoTransaccion = @TipoTransaccion or @TipoTransaccion = '') and (ClienteID = @ClienteID or @ClienteID = '')
		order by TransaccionID
	end


	if (@flag = 1) -- INSERTAR TRANSACCIONES
	begin
		insert into TcliTransacciones (TransaccionID, TipoTransaccion, ClienteID, Fecha, MontoExcento, MontoGravado, ITBIS, Estado, FacturaID, Observacion, UsuarioID)
		values (@TransaccionID, @TipoTransaccion, @ClienteID, @Fecha, @MontoExcento, @MontoGravado, @ITBIS, @Estado, @FacturaID, @Observacion, @UsuarioID)
	end

	if (@flag = 2) -- EDITAR TRANSACCIONES
	begin
		Update TcliTransacciones set TipoTransaccion = @TipoTransaccion, ClienteID = @ClienteID, Fecha = @Fecha, MontoExcento = @MontoExcento, 
		MontoGravado = @MontoGravado, ITBIS = @ITBIS, Estado = @Estado, FacturaID = @FacturaID, Observacion = @Observacion, UsuarioID = @UsuarioID
		where TransaccionID = @TransaccionID
	end


GO




