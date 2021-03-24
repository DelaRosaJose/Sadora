create table TsupClaseProveedores
(
[RowID] [int] IDENTITY(1,1) NOT NULL,
ClaseID int,
Nombre varchar(20)
)

go

create proc sp_supClaseProveedores
(
@flag int,
@ClaseID int,
@Nombre varchar(20)
)

as

if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TsupClaseProveedores
		where ClaseID = (select max(ClaseID) from TsupClaseProveedores)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS CLIENTES
	begin
		select * from TsupClaseProveedores
		where (ClaseID = @ClaseID or @ClaseID = 0) and (Nombre like '%'+@Nombre+'%' or @Nombre = '')
	end

	if (@flag = 1) -- INSERTAR CLIENTES
	begin
		insert into TsupClaseProveedores (ClaseID,Nombre)
		values ((SELECT isnull(MAX(ClaseID),0)+1 FROM TsupClaseProveedores),@Nombre)
	end

	if (@flag = 2) -- EDITAR CLIENTES
	begin
		Update TsupClaseProveedores set Nombre = @Nombre
		where ClaseID = @ClaseID
	end
	go