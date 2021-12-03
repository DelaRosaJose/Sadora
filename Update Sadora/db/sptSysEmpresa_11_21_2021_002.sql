use sadora

go
/****** Object:  Table [dbo].[TsysEmpresa]    Script Date: 11/14/2021 10:40:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TsysEmpresa](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[EmpresaID] [int] NULL,
	[Nombre] [varchar](100) NULL,
	[RNC] [varchar](15) NULL,
	[Razon_Social] [varchar](100) NULL,
	[Direccion] [varchar](255) NULL,
	[Logo] [Varbinary](max) NULL,
	--[Alta] [bit] NULL,
	[UsuarioID] int null
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO






USE [Sadora]
GO
/****** Object:  StoredProcedure [dbo].[sp_invServicioArticulos]    Script Date: 11/14/2021 1:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_sysEmpresa]
(
@flag int,
@EmpresaID int NULL,
@Nombre varchar(100) NULL,
@RNC varchar(15) NULL,
@Razon_Social varchar(100) NULL,
@Direccion varchar(255) NULL,
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
		insert into TsysEmpresa (EmpresaID, Nombre, RNC, Razon_Social, Direccion, Logo, UsuarioID)
		values (@EmpresaID,@Nombre, @RNC, @Razon_Social, @Direccion, @Logo, @UsuarioID)
	end

	if (@flag = 2) -- EDITAR CLIENTES
	begin
		Update TsysEmpresa set Nombre = @Nombre, RNC = @RNC, Razon_Social = @Razon_Social, Direccion = @Direccion, Logo = logo
		where EmpresaID = @EmpresaID
	end

	go
	
	
