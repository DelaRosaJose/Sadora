drop table[dbo].[TsysFormularios]
go


CREATE TABLE [dbo].[TsysFormularios](
	[FormularioID] [int] NOT NULL,
	[Nombre] [varchar](25) NOT NULL,
	[Modulo] [varchar](15) NOT NULL,
	[Titulo] [varchar](60) NULL,
UNIQUE NONCLUSTERED 
(
	[Nombre] ASC,[Modulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
) ON [PRIMARY]





go

drop procedure[dbo].[sp_sysFormularios]
go
CREATE procedure sp_sysFormularios
(
 /*@flag int, @FormularioID int, @Nombre varchar(25), @Modulo varchar(15),	@Titulo varchar(60),*/@DTItemns as dbo.T_sysFormularios ReadOnly
)

as 
begin

	--if (@flag = -2) -- CONSULTA ULTIMO REGISTRO
	--begin

		INSERT INTO TsysFormularios
			(
			FormularioID,
			Nombre,
			Modulo,
			Titulo
		)SELECT 
			FormularioID,
			Nombre,
			Modulo,
			Titulo
		FROM @DTItemns

	--end

	--if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	--begin
	--	select * from TsysFormularios
	--	where FormularioID = (select max(FormularioID) from TsysFormularios)
	--end

	--if (@flag = 0) -- CONSULTA UNO O TODOS LOS Formularios
	--begin
	--	select * from TsysFormularios
	--	where (FormularioID = @FormularioID or @FormularioID = 0)
	--end

	--if (@flag = 1) -- INSERTAR Formularios
	--begin
	--	insert into TsysFormularios (FormularioID,Nombre,Modulo,Titulo)
	--	values ((SELECT isnull(MAX(FormularioID),0)+1 FROM TsysFormularios),@Nombre,@Modulo,@Titulo)
	--end
	
	--if (@flag = 2) -- EDITAR Formularios
	--begin
	--	Update TsysFormularios set Nombre = @Nombre, Modulo = @Modulo, Titulo = @Titulo
	--	where FormularioID = @FormularioID

	--end

end
go