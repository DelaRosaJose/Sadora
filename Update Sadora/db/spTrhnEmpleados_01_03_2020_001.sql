CREATE TABLE [dbo].[TrhnEmpleados](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[EmpleadoID] [int] NULL,
	[Cedula] [varchar](13) NULL,
	[Nombre] [varchar](70) NULL,
	[FechaNacimiento] [datetime] NULL,
	[Direccion] [varchar](100) NULL,
	[EstadoCivil] [varchar](70) NULL,
	[CorreoElectronico] [varchar](40) NULL,
	[Telefono] [varchar](20) NULL,
	[Celular] [varchar](20) NULL,
	[Activo] [bit] NULL
) ON [PRIMARY]

go

Create procedure sp_rhnEmpleados
(
@flag int, @EmpleadoID int, @Cedula varchar(13), @Nombre varchar(70), @FechaNacimiento datetime, @Direccion varchar(100),
@EstadoCivil varchar(70), @CorreoElectronico varchar(40), @Telefono varchar(20), @Celular varchar(20), @Activo bit
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TrhnEmpleados
		where EmpleadoID = (select max(EmpleadoID) from TrhnEmpleados)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS EMPLEADOS
	begin
		select * from TrhnEmpleados
		where (EmpleadoID = @EmpleadoID or @EmpleadoID = 0) and (Cedula = @Cedula or @Cedula = '') and (Nombre like '%'+@Nombre+'%' or @Nombre = '')
	end

	if (@flag = 1) -- INSERTAR EMPLEADOS
	begin
		insert into TrhnEmpleados (EmpleadoID,Cedula,Nombre,FechaNacimiento,Direccion,EstadoCivil,CorreoElectronico,Telefono,Celular,Activo)
		values (@EmpleadoID,@Cedula,@Nombre,@FechaNacimiento,@Direccion,@EstadoCivil,@CorreoElectronico,@Telefono,@Celular,@Activo)
	end

	if (@flag = 2) -- EDITAR EMPLEADOS
	begin
		Update TrhnEmpleados set Cedula = @Cedula,Nombre = @Nombre,FechaNacimiento = @FechaNacimiento,Direccion = @Direccion,EstadoCivil = @EstadoCivil,CorreoElectronico = @CorreoElectronico,
		Telefono = @Telefono,Celular = @Celular,Activo = @Activo
		where EmpleadoID = @EmpleadoID
	end



go
