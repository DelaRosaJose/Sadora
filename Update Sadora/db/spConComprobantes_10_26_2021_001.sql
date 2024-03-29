ALTER TABLE sadora.dbo.TconComprobantes
ADD SinComprobantes bit default(0);

go
ALTER procedure [dbo].[sp_conComprobantes]
(
@flag int, @ComprobanteID int,	@Nombre varchar(70), @Auxiliar varchar(15),	@Nomenclatura varchar(5),
@Desde int,	@Hasta int,	@NextNCF varchar(15), @Disponibles int, @UsuarioID int, @SinComprobantes bit
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
		insert into TconComprobantes (ComprobanteID,Nombre, Auxiliar, Nomenclatura, Desde, Hasta, NextNCF, Disponibles, SinComprobantes, UsuarioID)
		values ((SELECT isnull(MAX(ComprobanteID),0)+1 FROM TconComprobantes),@Nombre,@Auxiliar,@Nomenclatura,@Desde,@Hasta,@NextNCF,@Disponibles, @SinComprobantes, @UsuarioID)
	end

	if (@flag = 2) -- EDITAR COMPROBANTES
	begin
		Update TconComprobantes set Nombre = @Nombre, Auxiliar = @Auxiliar, Nomenclatura = @Nomenclatura, Desde = @Desde, Hasta = @Hasta, 
		NextNCF = @NextNCF, Disponibles = @Disponibles, UsuarioID = @UsuarioID, SinComprobantes = @SinComprobantes
		where ComprobanteID = @ComprobanteID
	end

