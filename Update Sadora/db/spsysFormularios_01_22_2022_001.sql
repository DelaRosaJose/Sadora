USE [Sadora]
GO
/****** Object:  StoredProcedure [dbo].[sp_sysFormularios]    Script Date: 1/22/2022 4:59:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[sp_sysFormularios]
(
@flag int, @Nombre varchar(25), @Modulo varchar(20), @Titulo varchar(60)
)

as 
begin

if (@flag = 1) -- INSERTAR FORMULARIOS
	begin
		if ((select count(*) from TsysFormularios where Nombre = @Nombre and Modulo= @Modulo and Titulo=@Titulo) = 0)
			insert into TsysFormularios(RowID,Nombre,Modulo,Titulo) values ((select isnull(max(RowID),0)+1 from TsysFormularios),@Nombre,@Modulo,@Titulo)
	end
end
