
ALTER proc [dbo].[sp_sysClases]
(
@flag int,
@ClaseID int,
@Nombre varchar(20),
@Tabla varchar(30)
)

as

declare @id varchar(25) = (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME = @Tabla
and ORDINAL_POSITION = 2)

declare @name varchar(25) = (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME = @Tabla
and COLUMN_NAME like '%Nombre%')

--select @id

if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		exec ('select * from '+@Tabla+' 
		where '+ @id + ' = (select max ('+@id+') from '+@Tabla+')')
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS CLIENTES
	begin
		exec ('select * from '+@Tabla+' 
		where (' + @id + ' = '+@ClaseID+' or '+@ClaseID+' = 0) and 
		('+@name+' like ''%'+@Nombre+'%'' or '+@name+' = '''')')
	end

	if (@flag = 1) -- INSERTAR CLIENTES
	begin
		exec ('insert into '+@Tabla+'('+@id+','+@name+')
		values ((SELECT isnull(MAX('+@id+'),0)+1 FROM '+@Tabla+'),'''+@Nombre+''')')
	end

	if (@flag = 2) -- EDITAR CLIENTES
	begin
		exec ('Update '+@Tabla+' set '+@name+' = '''+@Nombre+'''
		where '+@id+' = '+@ClaseID+'')
	end
	go