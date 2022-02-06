
Declare @TableNam table(Id int identity,Table_Name varchar(50)); 

insert @TableNam
select distinct TABLE_NAME from INFORMATION_SCHEMA.COLUMNS
where COLUMN_NAME = 'RowID' and TABLE_NAME in 
(select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME not in ('CeduladosJCE','DGII_RNC','xCedulados','Audit'))
and TABLE_NAME not in (select TABLE_NAME from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_TYPE = 'PRIMARY KEY');


Declare @Count int = (select count(*) from @TableNam), @CountPosition int = 1;


while @Count >= @CountPosition
begin

	Declare @Setter varchar(40) = (select Max(Table_Name) from @TableNam where Id = @CountPosition);

	exec ('alter table '+ @Setter +' ADD PRIMARY KEY (RowID)');

	select @CountPosition = @CountPosition + 1;

end

