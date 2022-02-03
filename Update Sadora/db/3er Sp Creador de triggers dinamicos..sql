IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME= 'Audit')
CREATE TABLE Audit
(
AuditID [int]IDENTITY(1,1) NOT NULL,
Type char(1),
TableName varchar(128),
PrimaryKeyField varchar(1000),
PrimaryKeyValue varchar(1000),
FieldName varchar(1000),
OldValue varchar(2000),
NewValue varchar(2000),
UpdateDate datetime DEFAULT (GetDate()),
UserName varchar(128)
)
GO

DECLARE @sql varchar(8000), @TABLE_NAME sysname
SET NOCOUNT ON

SELECT @TABLE_NAME= MIN(a.TABLE_NAME)
FROM	INFORMATION_SCHEMA.Tables a join 
		INFORMATION_SCHEMA.COLUMNS b on a.TABLE_NAME = b.TABLE_NAME
WHERE
TABLE_TYPE= 'BASE TABLE'
AND a.TABLE_NAME!= 'sysdiagrams'
AND a.TABLE_NAME!= 'Audit'
and	b.COLUMN_NAME = 'UsuarioID'

WHILE @TABLE_NAME IS NOT NULL
BEGIN
EXEC('IF OBJECT_ID (''TR_AUDIT_' + @TABLE_NAME + ''', ''TR'') IS NOT NULL DROP TRIGGER TR_AUDIT_' + @TABLE_NAME+ '')
SELECT @sql =
'
create trigger TR_AUDIT_' + @TABLE_NAME + ' on ' + @TABLE_NAME+ ' for insert, update, delete
as

declare @bit int ,
@field int ,
@maxfield int ,
@char int ,
@fieldname varchar(64) ,
@TableName varchar(128) ,
@PKCols varchar(1000) ,
@sql varchar(8000),
@UpdateDate varchar(21) ,
@UserName varchar(128) ,
@Type char(1) ,
@PKFieldSelect varchar(1000),
@PKValueSelect varchar(1000),
@fieldsNames varchar(1000) ,
@ValuesOld varchar(2000),
@ValuesNew varchar(2000)

select @TableName = ''' + @TABLE_NAME+ '''

-- date and user
select --@UserName = system_user ,
@UpdateDate = convert(varchar(8), getdate(), 112) + '' '' + convert(varchar(12), getdate(), 114)

-- Action
if exists (select * from inserted)
if exists (select * from deleted)
select @Type = ''U''
else
select @Type = ''I''
else
select @Type = ''D''

-- get list of columns
select * into #ins from inserted
select * into #del from deleted

-- Get primary key columns for full outer join
select
@PKCols = coalesce(@PKCols + '' and'', '' on'') + '' i.'' + c.COLUMN_NAME + '' = d.'' + c.COLUMN_NAME,
@PKFieldSelect = coalesce(@PKFieldSelect+''+''''|''''+'','''') + '''''''' + COLUMN_NAME + '''''''',
@PKValueSelect = coalesce(@PKValueSelect+''+''''|''''+'','''') + ''replace(convert(varchar(1000), coalesce(i.'' + COLUMN_NAME + '',d.'' + COLUMN_NAME + '')),'''' '''' ,'''''''')''
from INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk ,
INFORMATION_SCHEMA.KEY_COLUMN_USAGE c
where pk.TABLE_NAME = @TableName
and CONSTRAINT_TYPE = ''PRIMARY KEY''
and c.TABLE_NAME = pk.TABLE_NAME
and c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME


select	@UserName = coalesce(@UserName +''+''''|''''+'','''', '''') + ''replace(convert(varchar(2000), coalesce(i.'' + c.COLUMN_NAME + '','''''''')),'''' '''' ,'''''''')''
from	INFORMATION_SCHEMA.COLUMNS c
		where c.TABLE_NAME = @TableName
		and c.COLUMN_NAME = ''UsuarioID''


if @PKCols is null
begin
raiserror(''no PK on table %s'', 16, -1, @TableName)
return
end

select @field = 0, @maxfield = max(ORDINAL_POSITION) from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = @TableName
while @field < @maxfield begin select @field = min(ORDINAL_POSITION),@fieldname = COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = @TableName and ORDINAL_POSITION > @field
group by ORDINAL_POSITION,COLUMN_NAME
order by ORDINAL_POSITION desc

select @bit = (@field - 1 )% 8 + 1
select @bit = power(2,@bit - 1)
select @char = ((@field - 1) / 8) + 1
if substring(COLUMNS_UPDATED(),@char, 1) & @bit > 0 or @Type in (''I'',''D'')
begin
select
@fieldsNames = coalesce(@fieldsNames +''+''''|''''+'','''', '''') + '''''''' + COLUMN_NAME + '''''''',
@ValuesOld = coalesce(@ValuesOld +''+''''|''''+'','''', '''') + ''replace(convert(varchar(2000), coalesce(d.'' + c.COLUMN_NAME + '','''''''')),'''' '''' ,'''''''')'',
@ValuesNew = coalesce(@ValuesNew +''+''''|''''+'','''', '''') + ''replace(convert(varchar(2000), coalesce(i.'' + c.COLUMN_NAME + '','''''''')),'''' '''' ,'''''''')''

from
INFORMATION_SCHEMA.COLUMNS c
where c.TABLE_NAME = @TableName
and c.COLUMN_NAME=@fieldname
end
end

select @sql = ''insert Audit (Type, TableName, PrimaryKeyField, PrimaryKeyValue, FieldName, OldValue, NewValue, UpdateDate, UserName)''
select @sql = @sql + '' select '''''' + @Type + ''''''''
select @sql = @sql + '','''''' + @TableName + ''''''''
select @sql = @sql + '','' + @PKFieldSelect
select @sql = @sql + '','' + @PKValueSelect
select @sql = @sql + '','' + @fieldsNames
select @sql = @sql + '','' + @ValuesOld
select @sql = @sql + '','' + @ValuesNew
select @sql = @sql + '','''''' + @UpdateDate + ''''''''
select @sql = @sql + '','' + @UserName 
select @sql = @sql + '' from #ins i full outer join #del d ''
select @sql = @sql + @PKCols

exec (@sql)

'
SELECT @sql
EXEC(@sql)
SELECT @TABLE_NAME= MIN(TABLE_NAME) FROM INFORMATION_SCHEMA.Tables
WHERE TABLE_NAME> @TABLE_NAME
AND TABLE_TYPE= 'BASE TABLE'
AND TABLE_NAME!= 'sysdiagrams'
AND TABLE_NAME!= 'Audit'
END