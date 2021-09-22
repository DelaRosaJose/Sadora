CREATE TABLE [dbo].[TvenMetodoPagos](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[MetodoID] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
	[UsuarioID] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

create procedure [dbo].[sp_venMetodoPagos]
(
@flag int, @MetodoID int, @Nombre varchar(50), @Activo bit, @UsuarioID int
)

as 

	--if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	--begin
	--	select * from TvenFacturas
	--	where FacturaID = (select max(FacturaID) from TvenFacturas)
	--end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS COMPROBANTES
	begin
		select * from TvenMetodoPagos
		where (MetodoID = @MetodoID or @MetodoID = 0) /*and (RNC = @RNC or @RNC = '')*/   -- and (Nombre like '%'+@Nombre+'%' or @Nombre = '')
		--order by MetodoID
	end


	if (@flag = 1) -- INSERTAR COMPROBANTES
	begin
		insert into TvenMetodoPagos (MetodoID, Nombre, Activo, UsuarioID)
		values ((SELECT isnull(MAX(MetodoID),100000)+1 FROM TvenMetodoPagos), @Nombre, @Activo, @UsuarioID)
	end

	if (@flag = 2) -- EDITAR COMPROBANTES
	begin
		Update TvenMetodoPagos set Nombre = @Nombre, Activo = @Activo, UsuarioID = @UsuarioID
		where MetodoID = @MetodoID
	end

GO






