ALTER TABLE sadora.dbo.TinvArticulos
ADD Cantidad float default(0);

GO
ALTER procedure [dbo].[sp_invArticulos]
(
@flag int, @ArticuloID int,	@Nombre varchar(70), @Descripcion varchar(70), @Modelo varchar(50), @ClaseArticuloID int, @DepartamentoID int,
@MarcaID int,	@Tarjeta varchar(20),	@Costo float, @Precio float, @UsuarioID int, @Cantidad float
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
		insert into TinvArticulos (ArticuloID,Nombre, Descripcion, Modelo, ClaseArticuloID, DepartamentoID, MarcaID, Tarjeta, Costo, Precio, UsuarioID, Cantidad)
		values (@ArticuloID,@Nombre, @Descripcion, @Modelo, @ClaseArticuloID, @DepartamentoID, @MarcaID, @Tarjeta, @Costo, @Precio, @UsuarioID, @Cantidad)
	end

	if (@flag = 2) -- EDITAR COMPROBANTES
	begin
		Update TinvArticulos set Nombre = @Nombre,Descripcion = @Descripcion , Modelo = @Modelo, ClaseArticuloID = @ClaseArticuloID, DepartamentoID = @DepartamentoID, MarcaID = @MarcaID, Tarjeta = @Tarjeta, 
		Costo = @Costo, Precio = @Precio, UsuarioID = @UsuarioID, Cantidad = @Cantidad
		where ArticuloID = @ArticuloID
	end

