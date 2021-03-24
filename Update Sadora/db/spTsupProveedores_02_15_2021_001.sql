
CREATE TABLE [dbo].[TsupProveedores](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[ProveedorID] [int] NULL,
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

GO

SET ANSI_PADDING OFF
GO




CREATE procedure [dbo].[sp_supProveedores]
(
@flag int, @ProveedorID int, @RNC varchar(13), @Nombre varchar(70), @Representante varchar(70), @ClaseID int,
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
		select * from TsupProveedores
		where ProveedorID = (select max(ProveedorID) from TsupProveedores)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS CLIENTES
	begin
		select * from TsupProveedores
		where (ProveedorID = @ProveedorID or @ProveedorID = 0) and (RNC = @RNC or @RNC = '') and (Nombre like '%'+@Nombre+'%' or @Nombre = '')
		order by ProveedorID
	end


	if (@flag = 1) -- INSERTAR CLIENTES
	begin
		insert into TsupProveedores (ProveedorID,RNC,Nombre,Representante,ClaseID,Direccion,CorreoElectronico,Telefono,Celular,Activo,UsuarioID)
		values (@ProveedorID,@RNC,@Nombre,@Representante,@ClaseID,@Direccion,@CorreoElectronico,@Telefono,@Celular,@Activo,@UsuarioID)
	end

	if (@flag = 2) -- EDITAR CLIENTES
	begin
		Update TsupProveedores set RNC = @RNC,Nombre = @Nombre, Representante = @Representante,ClaseID = @ClaseID,Direccion = @Direccion,CorreoElectronico = @CorreoElectronico,
		Telefono = @Telefono,Celular = @Celular,Activo = @Activo,UsuarioID = @UsuarioID
		where ProveedorID = @ProveedorID
	end





GO




