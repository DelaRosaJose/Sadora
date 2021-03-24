drop table[dbo].[TsysFormularios]
go
CREATE TABLE [dbo].[TsysFormularios](
	[RowID] [int] NOT NULL,
	[Nombre] [varchar](25) NOT NULL,
	[Modulo] [varchar](20) NOT NULL,
	[Titulo] [varchar](60) NULL,
UNIQUE NONCLUSTERED 
(
	[Nombre] ASC,
	[Modulo] ASC,
	[Titulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


go

drop procedure[dbo].[sp_sysFormularios]

go

create procedure [dbo].[sp_sysFormularios]
(
@flag int, @Nombre varchar(25), @Modulo varchar(20), @Titulo varchar(60)
)

as 
begin

if (@flag = 1) -- INSERTAR FORMULARIOS
	begin
		insert into TsysFormularios(RowID,Nombre,Modulo,Titulo)
		values ((select isnull(max(RowID),0)+1 from TsysFormularios),@Nombre,@Modulo,@Titulo)
	end
end
go