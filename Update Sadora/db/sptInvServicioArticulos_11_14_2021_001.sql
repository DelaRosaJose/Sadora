

USE [Sadora]
GO

/****** Object:  Table [dbo].[TinvServicioArticulos]    Script Date: 11/14/2021 10:40:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TinvServicioArticulos](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[ArticuloID] [int] NULL,
	[NombreServicio] [varchar](50) NULL,
	[Precio] [float] NULL,
	[Alta] [bit] NULL,
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
create proc [dbo].[sp_invServicioArticulos]
(
@flag int,
@ArticuloID int,
@NombreServicio varchar(50),
@Precio float,
@Alta bit,
@UsuarioID int
)

as

if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TinvServicioArticulos
		where ArticuloID = (select max(ArticuloID) from TinvServicioArticulos)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS CLIENTES
	begin
		select * from TinvServicioArticulos
		where (ArticuloID = @ArticuloID  or @ArticuloID = 0)-- and (NombreServicio like '%'+@NombreServicio+'%' or @NombreServicio = '')
	end

	if (@flag = 1) -- INSERTAR CLIENTES
	begin
		insert into TinvServicioArticulos (ArticuloID, NombreServicio, Precio, Alta, UsuarioID)
		values (@ArticuloID,@NombreServicio, @Precio, @Alta,  @UsuarioID)
	end

	--if (@flag = 2) -- EDITAR CLIENTES
	--begin
	--	Update TinvServicioArticulos set Nombre = @Nombre
	--	where ClaseID = @ClaseID
	--end
