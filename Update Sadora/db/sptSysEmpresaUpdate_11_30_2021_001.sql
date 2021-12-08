USE [Sadora]
GO

alter  table TsysEmpresa
add Telefono [varchar](20);
go

/****** Object:  StoredProcedure [dbo].[sp_sysEmpresa]    Script Date: 11/30/2021 7:52:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[sp_sysEmpresa]
(
@flag int,
@EmpresaID int NULL,
@Nombre varchar(100) NULL,
@RNC varchar(15) NULL,
@Razon_Social varchar(100) NULL,
@Direccion varchar(255) NULL,
@Telefono varchar(20),
@Logo Varbinary (max) NULL,
@UsuarioID int
)

as

if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TsysEmpresa
		where EmpresaID = (select max(EmpresaID) from TsysEmpresa)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS CLIENTES
	begin
		select * from TsysEmpresa
		where (EmpresaID = @EmpresaID  or @EmpresaID = 0)-- and (NombreServicio like '%'+@NombreServicio+'%' or @NombreServicio = '')
	end

	if (@flag = 1) -- INSERTAR CLIENTES
	begin
		insert into TsysEmpresa (EmpresaID, Nombre, RNC, Razon_Social, Direccion, Telefono,Logo, UsuarioID)
		values (@EmpresaID,@Nombre, @RNC, @Razon_Social, @Direccion, @Telefono, @Logo, @UsuarioID)
	end

	if (@flag = 2) -- EDITAR CLIENTES
	begin
		Update TsysEmpresa set Nombre = @Nombre, RNC = @RNC, Razon_Social = @Razon_Social, Direccion = @Direccion, Telefono = @Telefono, Logo = @Logo
		where EmpresaID = @EmpresaID
	end

