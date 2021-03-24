
Create table TsysUsuarios
(
[RowID] [int] IDENTITY(1,1) NOT NULL,
[UsuarioID] [int] NULL,
	[Nombre] [varchar](70) NULL,
	[EmpleadoID] [int] NULL,
	[GrupoID] [int] NULL,
	[Contrase�a] [varchar](30) NULL,
	[Activo] [bit] NULL
) ON [PRIMARY]

go 

Create procedure sp_sysUsuarios
(
@flag int, @UsuarioID int, @Nombre varchar(70), @EmpleadoID int, @GrupoID int, @Contrase�a varchar(30), @Activo bit
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TsysUsuarios
		where UsuarioID = (select max(UsuarioID) from TsysUsuarios)
	end


	if (@flag = 0) -- CONSULTA UNO O TODOS LOS CLIENTES
	begin
		select * from TsysUsuarios
		where (UsuarioID = @UsuarioID or @UsuarioID = 0) and (Nombre like '%'+@Nombre+'%' or @Nombre = '')
	end


	if (@flag = 1) -- INSERTAR CLIENTES
	begin
		insert into TsysUsuarios (UsuarioID, Nombre, EmpleadoID, GrupoID, Contrase�a , Activo)
		values (@UsuarioID, @Nombre, @EmpleadoID, @GrupoID, @Contrase�a, @Activo)
	end

	if (@flag = 2) -- EDITAR CLIENTES
	begin
		Update TsysUsuarios set Nombre = @Nombre, EmpleadoID = @EmpleadoID, GrupoID = @GrupoID, Contrase�a = @Contrase�a, Activo = @Activo
		where UsuarioID = @UsuarioID
	end

go
