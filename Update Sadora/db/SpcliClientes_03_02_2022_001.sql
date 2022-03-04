USE [Sadora]
GO
/****** Object:  StoredProcedure [dbo].[sp_cliClientes]    Script Date: 3/3/2022 6:36:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_cliClientes]
(
@flag int, @ClienteID int, @RNC varchar(13), @Nombre varchar(70), @Representante varchar(70), @ClaseID int, @Direccion varchar(100), @CorreoElectronico varchar(40), @Telefono varchar(20),
@Celular varchar(20), @Activo bit, @UsuarioID int, @ClaseComprobanteID int, @DiasCredito int null
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TcliClientes
		where ClienteID = (select max(ClienteID) from TcliClientes)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS CLIENTES
	begin
		select * from TcliClientes
		where (ClienteID = @ClienteID or @ClienteID = 0) and (RNC = @RNC or @RNC = '') and (Nombre like '%'+@Nombre+'%' or @Nombre = '')
		order by ClienteID
	end


	if (@flag = 1) -- INSERTAR CLIENTES
	begin
		insert into TcliClientes (ClienteID,RNC,Nombre,Representante,ClaseID,ClaseComprobanteID,DiasCredito,Direccion,CorreoElectronico,Telefono,Celular,Activo,UsuarioID)
		values ((select isnull(max(ClienteID),0)+1 from TcliClientes),@RNC,@Nombre,@Representante,@ClaseID,@ClaseComprobanteID,@DiasCredito,@Direccion,@CorreoElectronico,@Telefono,@Celular,@Activo,@UsuarioID)
	end

	if (@flag = 2) -- EDITAR CLIENTES
	begin
		Update TcliClientes set RNC = @RNC,Nombre = @Nombre,Representante = @Representante,ClaseID = @ClaseID, ClaseComprobanteID=@ClaseComprobanteID, DiasCredito = @DiasCredito,
		Direccion = @Direccion,CorreoElectronico = @CorreoElectronico, Telefono = @Telefono,Celular = @Celular,Activo = @Activo,UsuarioID = @UsuarioID
		where ClienteID = @ClienteID
	end

