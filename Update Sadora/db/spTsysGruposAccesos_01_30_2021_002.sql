go

Create table TsysGruposAccesos
(
[RowID] [int] IDENTITY(1,1) NOT NULL,
[GrupoID] [int] NULL,
	[Nombre] [varchar](25) NULL,
	[Modulo] [varchar](20) NULL,
	[Titulo] [varchar](60) NULL,
	[Visualiza] [bit] NULL,
	[Imprime] [bit] NULL,
	[Agrega] [bit] NULL,
	[Modifica] [bit] NULL,
	[Anula] [bit] NULL
) ON [PRIMARY]

go 

Create procedure sp_sysGruposAccesos
(
@flag int, @GrupoID int, @Nombre varchar(25), @Modulo varchar(20), @Titulo varchar(60), @Visualiza bit, @Imprime bit,
@Agrega bit, @Modifica bit, @Anula bit
)

as 

	if (@flag = -1) -- Los accesos que tiene un usuario y los que les faltan
	begin

		select * from TsysGruposAccesos where GrupoID = @GrupoID and Nombre = @Nombre and Modulo = @Modulo and Titulo = @Titulo

	end


	if (@flag = 0) -- CONSULTA UNO O TODOS LOS CLIENTES
	begin

		select nombre,modulo,Titulo,Visualiza,Agrega,Modifica,imprime,anula into #AccesoTemporal
		from TsysGruposAccesos
		where (Modulo = @Modulo or @Modulo = '') and (GrupoID = @GrupoID/* or @GrupoID = 0*/)

		select a.*, isnull(b.visualiza,0) as Visualiza, isnull(b.imprime,0) as Imprime, isnull(b.agrega,0) as Agrega, isnull(b.modifica,0) as Modifica, isnull(b.anula ,0) as Anula
		from TsysFormularios a left join 
			 #AccesoTemporal b on a.Nombre = b.Nombre and a.Modulo = b.Modulo and a.Titulo = b.Titulo

		drop table #AccesoTemporal

	end


	if (@flag = 1) -- INSERTAR CLIENTES
	begin

		if ((select GrupoID from TsysGruposAccesos where GrupoID = @GrupoID and Nombre = @Nombre and Modulo = @Modulo and Titulo = @Titulo) > 0)
		begin
			Update TsysGruposAccesos set Visualiza = @Visualiza, Imprime = @Imprime, Agrega = @Agrega, Modifica= @Modifica, Anula = @Anula
			where GrupoID = @GrupoID and Nombre = @Nombre and Modulo = @Modulo and Titulo = @Titulo
		end
		else
		begin 
			insert into TsysGruposAccesos (GrupoID, Nombre, Modulo, Titulo, Visualiza, Imprime, Agrega, Modifica, Anula)
			values (@GrupoID, @Nombre, @Modulo, @Titulo, @Visualiza, @Imprime, @Agrega, @Modifica, @Anula)
		end

	end



go