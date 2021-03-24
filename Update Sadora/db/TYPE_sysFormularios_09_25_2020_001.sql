
drop type [dbo].[T_sysFormularios]

/****** Object:  UserDefinedTableType [dbo].[TsysFormularios]    Script Date: 9/26/2020 12:00:18 AM ******/
CREATE TYPE [dbo].[T_sysFormularios] AS TABLE(
	[FormularioID] int NOT NULL,
	[Nombre] [varchar](25) NOT NULL,
	[Modulo] [varchar](20) NOT NULL,
	[Titulo] [varchar](60) NULL
)
GO


