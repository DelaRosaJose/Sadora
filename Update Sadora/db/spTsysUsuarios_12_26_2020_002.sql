
Create table TsysUsuarios
(
[RowID] [int] IDENTITY(1,1) NOT NULL,
[UsuarioID] [int] NULL,
	[Nombre] [varchar](70) NULL,
	[EmpleadoID] [int] NULL,
	[GrupoID] [int] NULL,
	[Contraseņa] [varchar](30) NULL,
	[Activo] [bit] NULL
) ON [PRIMARY]

go 

Create procedure sp_sysUsuarios
(
@flag int, @UsuarioID int, @Nombre varchar(70), @EmpleadoID int, @GrupoID int, @Contraseņa varchar(30), @Activo bit
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
		insert into TsysUsuarios (UsuarioID, Nombre, EmpleadoID, GrupoID, Contraseņa , Activo)
		values ((SELECT isnull(MAX(UsuarioID),0)+1 FROM TsysUsuarios), @Nombre, @EmpleadoID, @GrupoID, @Contraseņa, @Activo)
	end

	if (@flag = 2) -- EDITAR CLIENTES
	begin
		Update TsysUsuarios set Nombre = @Nombre, EmpleadoID = @EmpleadoID, GrupoID = @GrupoID, Contraseņa = @Contraseņa, Activo = @Activo
		where UsuarioID = @UsuarioID
	end

go
