go
Create table TsysGruposUsuarios
(
[RowID] [int] IDENTITY(1,1) NOT NULL,
GrupoID int,
Nombre varchar(70),
Activo bit
)

go 

Create procedure sp_sysGruposUsuarios
(
@flag int, @GrupoID int, @Nombre varchar(70), @Activo bit
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TsysGruposUsuarios
		where GrupoID = (select max(GrupoID) from TsysGruposUsuarios)
	end


	if (@flag = 0) -- CONSULTA UNO O TODOS LOS CLIENTES
	begin
		select * from TsysGruposUsuarios
		where (GrupoID = @GrupoID or @GrupoID = 0) and (Nombre like '%'+@Nombre+'%' or @Nombre = '')
	end


	if (@flag = 1) -- INSERTAR CLIENTES
	begin
		insert into TsysGruposUsuarios (GrupoID, Nombre, Activo)
		values (@GrupoID, @Nombre, @Activo)
	end

	if (@flag = 2) -- EDITAR CLIENTES
	begin
		Update TsysGruposUsuarios set Nombre = @Nombre, Activo = @Activo
		where GrupoID = @GrupoID
	end


go
