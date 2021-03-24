
CREATE TABLE [dbo].[TconComprobantes](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[ComprobanteID] [int] NOT NULL,
	[Nombre] [varchar](70) NOT NULL,
	[Auxiliar] [varchar](15) NULL,
	[Nomenclatura] [char](5) NOT NULL,
	[Desde] [int] NULL,
	[Hasta] [int] NULL,
	[NextNCF] [varchar](15) NULL,
	[Disponibles] [int] NULL,
	[UsuarioID] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


CREATE procedure [dbo].[sp_conComprobantes]
(
@flag int, @ComprobanteID int,	@Nombre varchar(70), @Auxiliar varchar(15),	@Nomenclatura char(5),
@Desde int,	@Hasta int,	@NextNCF varchar(15), @Disponibles int, @UsuarioID int
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TconComprobantes
		where ComprobanteID = (select max(ComprobanteID) from TconComprobantes)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS COMPROBANTES
	begin
		select * from TconComprobantes
		where (ComprobanteID = @ComprobanteID or @ComprobanteID = 0) /*and (RNC = @RNC or @RNC = '')*/ and (Nombre like '%'+@Nombre+'%' or @Nombre = '')
		order by ComprobanteID
	end


	if (@flag = 1) -- INSERTAR COMPROBANTES
	begin
		insert into TconComprobantes (ComprobanteID,Nombre, Auxiliar, Nomenclatura, Desde, Hasta, NextNCF, Disponibles, UsuarioID)
		values (@ComprobanteID,@Nombre,@Auxiliar,@Nomenclatura,@Desde,@Hasta,@NextNCF,@Disponibles, @UsuarioID)
	end

	if (@flag = 2) -- EDITAR COMPROBANTES
	begin
		Update TconComprobantes set Nombre = @Nombre, Auxiliar = @Auxiliar, Nomenclatura = @Nomenclatura, Desde = @Desde, Hasta = @Hasta, 
		NextNCF = @NextNCF, Disponibles = @Disponibles, UsuarioID = @UsuarioID
		where ComprobanteID = @ComprobanteID
	end

GO






