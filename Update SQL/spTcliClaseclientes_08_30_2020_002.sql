create table TcliClaseClientes
(
ClaseID int,
Nombre varchar(20)
)

create proc sp_cliClaseClientes
(
@flag int,
@ClaseID int,
@Nombre varchar(20)
)

as

if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TcliClaseClientes
		where ClaseID = (select max(ClaseID) from TcliClaseClientes)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS CLIENTES
	begin
		select * from TcliClaseClientes
		where (ClaseID = @ClaseID or @ClaseID = 0)
	end

	if (@flag = 1) -- INSERTAR CLIENTES
	begin
		insert into TcliClaseClientes (ClaseID,Nombre)
		values ((SELECT MAX(ClaseID)+1 FROM TcliClaseClientes),@Nombre)
	end

	if (@flag = 2) -- EDITAR CLIENTES
	begin
		Update TcliClaseClientes set Nombre = @Nombre
		where ClaseID = @ClaseID
	end