
CREATE TABLE [dbo].[TinvMarca](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[MarcaID] [int] NOT NULL,
	[Nombre] [varchar](70) NOT NULL
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[TinvDepartamento](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[DepartamentoID] [int] NOT NULL,
	[Nombre] [varchar](70) NOT NULL
) ON [PRIMARY]

GO
