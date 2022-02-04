
Declare @TableNam table(Id int identity,Table_Name varchar(50)); 
Declare @Count int, @CountPosition int = 1;

insert @TableNam
select distinct TABLE_NAME from INFORMATION_SCHEMA.COLUMNS 
where TABLE_NAME not in ((select distinct TABLE_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME = 'UsuarioID'))
and TABLE_NAME not in ('CeduladosJCE','DGII_RNC','xCedulados');

select @Count =  (select count(*) from @TableNam);

select * from @TableNam;

while @Count >= @CountPosition
begin

	Declare @Setter varchar(40) = (select Max(Table_Name) from @TableNam where Id = @CountPosition);

	exec ('alter table '+ @Setter +' add UsuarioID	int');

	select @CountPosition = @CountPosition + 1;

end



