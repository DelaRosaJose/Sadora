

CREATE TABLE [dbo].[TsysAccesos](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioID] [int] NULL,
	[Nombre] [varchar](25) NULL,
	[Modulo] [varchar](20) NULL,
	[Titulo] [varchar](60) NULL,
	[Visualiza] [bit] NULL,
	[Imprime] [bit] NULL,
	[Agrega] [bit] NULL,
	[Modifica] [bit] NULL,
	[Anula] [bit] NULL
) ON [PRIMARY]

GO

Create procedure sp_sysAccesos
(
@flag int, @UsuarioID int, @Nombre varchar(25), @Modulo varchar(20), @Titulo varchar(60), @Visualiza bit, @Imprime bit,
@Agrega bit, @Modifica bit, @Anula bit
)

as 

	if (@flag = -1) -- Los accesos que tiene un usuario y los que les faltan
	begin

	--select * from TsysAccesos where UsuarioID = @UsuarioID and Nombre = @Nombre and Modulo = @Modulo and Titulo = @Titulo
		select nombre,modulo,Titulo,Visualiza,Agrega,Modifica,imprime,anula into #AccesoTemporal1
		from tsysaccesos
		where (UsuarioID = @UsuarioID) and (Modulo = @Modulo) and  (Nombre = @Nombre) and (Titulo = @Titulo)

		select a.*, isnull(b.visualiza,0) as Visualiza, isnull(b.imprime,0) as Imprime, isnull(b.agrega,0) as Agrega, isnull(b.modifica,0) as Modifica, isnull(b.anula ,0) as Anula
		from TsysFormularios a left join 
			 #AccesoTemporal1 b on a.Nombre = b.Nombre and a.Modulo = b.Modulo and a.Titulo = b.Titulo
			 where (a.Nombre = @Nombre) and (a.Titulo = @Titulo) and (a.Modulo = @Modulo)
		order by a.Modulo

		drop table #AccesoTemporal1

	end


	if (@flag = 0) -- CONSULTA UNO O TODOS LOS CLIENTES
	begin

		select nombre,modulo,Titulo,Visualiza,Agrega,Modifica,imprime,anula into #AccesoTemporal
		from tsysaccesos
		where (Modulo = @Modulo or @Modulo = '') and (UsuarioID = @UsuarioID/* or @UsuarioID = 0*/)

		select a.*, isnull(b.visualiza,0) as Visualiza, isnull(b.imprime,0) as Imprime, isnull(b.agrega,0) as Agrega, isnull(b.modifica,0) as Modifica, isnull(b.anula ,0) as Anula
		from TsysFormularios a left join 
			 #AccesoTemporal b on a.Nombre = b.Nombre and a.Modulo = b.Modulo and a.Titulo = b.Titulo
		order by a.Modulo

		drop table #AccesoTemporal

	end


	if (@flag = 1) -- INSERTAR CLIENTES
	begin

		if ((select UsuarioID from TsysAccesos where UsuarioID = @UsuarioID and Nombre = @Nombre and Modulo = @Modulo and Titulo = @Titulo) > 0)
		begin
			Update TsysAccesos set Visualiza = @Visualiza, Imprime = @Imprime, Agrega = @Agrega, Modifica= @Modifica, Anula = @Anula
			where UsuarioID = @UsuarioID and Nombre = @Nombre and Modulo = @Modulo and Titulo = @Titulo
		end
		else
		begin 
			insert into TsysAccesos (UsuarioID, Nombre, Modulo, Titulo, Visualiza, Imprime, Agrega, Modifica, Anula)
			values (@UsuarioID, @Nombre, @Modulo, @Titulo, @Visualiza, @Imprime, @Agrega, @Modifica, @Anula)
		end

	end
go