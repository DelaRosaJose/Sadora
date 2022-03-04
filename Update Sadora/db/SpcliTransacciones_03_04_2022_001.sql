USE [Sadora]
GO
/****** Object:  StoredProcedure [dbo].[sp_cliTransacciones]    Script Date: 3/4/2022 9:40:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER procedure [dbo].[sp_cliTransacciones]
(
	@flag int, @TransaccionID int, @TipoTransaccion varchar(20), @ClienteID int, @Fecha datetime,	@MontoExcento float, 
	@MontoGravado float, @ITBIS float, @Estado varchar(10), @FacturaID varchar(25),	@Observacion varchar(100), @UsuarioID int, @DiasCredito int = 0
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
		where (TransaccionID = @TransaccionID or @TransaccionID = 0) --and (TipoTransaccion = @TipoTransaccion or @TipoTransaccion = '')-- and (ClienteID = @ClienteID or @ClienteID = '')
		order by TransaccionID
	end

	declare @Total float = (@MontoExcento+@MontoGravado+@ITBIS);
	if (@flag = 1) -- INSERTAR TRANSACCIONES
	begin
		insert into TcliTransacciones (TransaccionID, TipoTransaccion, ClienteID, DiasCredito, Fecha, MontoExcento, MontoGravado, ITBIS, Estado, FacturaID, Observacion, UsuarioID)
		values ((select isnull(max(TransaccionID),0)+1 from TcliTransacciones), @TipoTransaccion, @ClienteID, @DiasCredito, @Fecha, @MontoExcento, @MontoGravado, @ITBIS, @Estado, @FacturaID, @Observacion, @UsuarioID)
	
		if(@Estado = 'Cerrada')
			exec sp_cliMovimientosCuentas 1, 1, @TipoTransaccion, '', @ClienteID, @Fecha, 0, @Total
	end

	if (@flag = 2) -- EDITAR TRANSACCIONES
	begin
		Update TcliTransacciones set TipoTransaccion = @TipoTransaccion, ClienteID = @ClienteID, DiasCredito = @DiasCredito, Fecha = @Fecha, MontoExcento = @MontoExcento, 
		MontoGravado = @MontoGravado, ITBIS = @ITBIS, Estado = @Estado, FacturaID = @FacturaID, Observacion = @Observacion, UsuarioID = @UsuarioID
		where TransaccionID = @TransaccionID

		if(@Estado = 'Cerrada')
			exec sp_cliMovimientosCuentas 1, 1, @TipoTransaccion, '', @ClienteID, @Fecha, 0, @Total
	end


