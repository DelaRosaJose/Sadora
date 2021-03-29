
CREATE TABLE [dbo].[TinvArticulos](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[ArticuloID] [int] NOT NULL,
	[Nombre] [varchar](70) NOT NULL,
	[Descripcion] [varchar](150) NOT NULL,
	[Modelo] [varchar](50) NOT NULL,
	[DepartamentoID] [int] NULL,
	[MarcaID] [int] NOT NULL,
	[Tarjeta] [varchar](20) NOT NULL,
	[Costo] [float] NOT NULL,
	[Precio] [float] NOT NULL,
	[UsuarioID] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


CREATE procedure [dbo].[sp_invArticulos]
(
@flag int, @ArticuloID int,	@Nombre varchar(70), @Descripcion varchar(70), @Modelo varchar(50), @DepartamentoID int,
@MarcaID int,	@Tarjeta varchar(20),	@Costo float, @Precio float, @UsuarioID int
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TinvArticulos
		where ArticuloID = (select max(ArticuloID) from TinvArticulos)
	end

	if (@flag = 0) -- CONSULTA UNO O TODOS LOS COMPROBANTES
	begin
		select * from TinvArticulos
		where (ArticuloID = @ArticuloID or @ArticuloID = 0) /*and (RNC = @RNC or @RNC = '')*/ and (Nombre like '%'+@Nombre+'%' or @Nombre = '')
		order by ArticuloID
	end


	if (@flag = 1) -- INSERTAR COMPROBANTES
	begin
		insert into TinvArticulos (ArticuloID,Nombre, Descripcion, Modelo, DepartamentoID, MarcaID, Tarjeta, Costo, Precio, UsuarioID)
		values (@ArticuloID,@Nombre, @Descripcion,@Modelo,@DepartamentoID,@MarcaID,@Tarjeta,@Costo,@Precio, @UsuarioID)
	end

	if (@flag = 2) -- EDITAR COMPROBANTES
	begin
		Update TinvArticulos set Nombre = @Nombre,Descripcion = @Descripcion , Modelo = @Modelo, DepartamentoID = @DepartamentoID, MarcaID = @MarcaID, Tarjeta = @Tarjeta, 
		Costo = @Costo, Precio = @Precio, UsuarioID = @UsuarioID
		where ArticuloID = @ArticuloID
	end

GO






