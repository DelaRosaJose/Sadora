CREATE TABLE [dbo].[TvenCajas](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[CajaID] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
	[UsuarioID] [int] NULL
) ON [PRIMARY]

GO

CREATE CLUSTERED INDEX [IX_TvenCajas] ON [dbo].[TvenCajas]
(
	[CajaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

SET ANSI_PADDING OFF
GO

create procedure [dbo].[sp_venCajas]
(
@flag int, @CajaID int, @Nombre varchar(50), @Activo bit, @UsuarioID int
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TvenCajas
		where CajaID = (select max(CajaID) from TvenCajas)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS COMPROBANTES
	begin
		select * from TvenCajas
		where (CajaID = @CajaID or @CajaID = 0) /*and (RNC = @RNC or @RNC = '')*/ --and (Nombre like '%'+@Nombre+'%' or @Nombre = '')
		--order by FacturaID
	end


	if (@flag = 1) -- INSERTAR COMPROBANTES
	begin
		insert into TvenCajas (CajaID, Nombre, Activo, UsuarioID)
		values ((SELECT isnull(MAX(CajaID),100000)+1 FROM TvenCajas), @Nombre, @Activo, @UsuarioID)
	end

	if (@flag = 2) -- EDITAR COMPROBANTES
	begin
		Update TvenCajas set Nombre = @Nombre, Activo = @Activo, UsuarioID = @UsuarioID
		where CajaID = @CajaID
	end

GO






