Create table TcliClientes
(
ClienteID int,
RNC varchar(13),
Nombre varchar(70),
Representante varchar(70),
ClaseID int,
Direccion varchar(100),
CorreoElectronico varchar(40),
Telefono varchar(20),
Celular varchar(20),
Activo char(1),
UsuarioID int
)


Create procedure sp_cliClientes
(
@flag int, @ClienteID int, @RNC varchar(13), @Nombre varchar(70), @Representante varchar(70), @ClaseID int,
@Direccion varchar(100),
@CorreoElectronico varchar(40),
@Telefono varchar(20),
@Celular varchar(20),
@Activo char(1),
@UsuarioID int
)

as 

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS CLIENTES
	begin
		select * from TcliClientes
		where (ClienteID = ClienteID or @ClienteID = 0)
	end


	if (@flag = 1) -- INSERTAR CLIENTES
	begin
		insert into TcliClientes (ClienteID,RNC,Nombre,Representante,ClaseID,Direccion,CorreoElectronico,Telefono,Celular,Activo,UsuarioID)
		values (@ClienteID,@RNC,@Nombre,@Representante,@ClaseID,@Direccion,@CorreoElectronico,@Telefono,@Celular,@Activo,@UsuarioID)
	end

	if (@flag = 2) -- EDITAR CLIENTES
	begin
		Update TcliClientes set RNC = @RNC,Nombre = @Nombre,Representante = @Representante,ClaseID = @ClaseID,Direccion = @Direccion,CorreoElectronico = @CorreoElectronico,
		Telefono = @Telefono,Celular = @Celular,Activo = @Activo,UsuarioID = @UsuarioID
		where ClienteID = @ClienteID
	end

go
