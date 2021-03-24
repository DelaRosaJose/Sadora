Create table TcliClientes
(
[RowID] [int] IDENTITY(1,1) NOT NULL,
[ClienteID] [int] NULL,
	[RNC] [varchar](13) NULL,
	[Nombre] [varchar](70) NULL,
	[Representante] [varchar](70) NULL,
	[ClaseID] [int] NULL,
	[Direccion] [varchar](100) NULL,
	[CorreoElectronico] [varchar](40) NULL,
	[Telefono] [varchar](20) NULL,
	[Celular] [varchar](20) NULL,
	[Activo] [bit] NULL,
	[UsuarioID] [int] NULL
) ON [PRIMARY]


Create procedure sp_cliClientes
(
@flag int, @ClienteID int, @RNC varchar(13), @Nombre varchar(70), @Representante varchar(70), @ClaseID int,
@Direccion varchar(100),
@CorreoElectronico varchar(40),
@Telefono varchar(20),
@Celular varchar(20),
@Activo bit,
@UsuarioID int
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
