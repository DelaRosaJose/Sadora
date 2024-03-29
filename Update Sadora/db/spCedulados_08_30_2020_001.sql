
ALTER PROC [dbo].[spCedula]
	(
	@cedula char(11)
	)

as 
DECLARE @MUN_CED CHAR(3),
	@SEQ_CED CHAR(7),
	@VER_CED CHAR(1)
SET @MUN_CED = LEFT(@CEDULA,3)
SET @SEQ_CED = SUBSTRING(@CEDULA,4,7)
SET @VER_CED = RIGHT(@CEDULA,1)


SELECT Nombres, APELLIDO1+' '+APELLIDO2 as Apellidos, FECHA_NAC as FechaNacimiento, LUGAR_NAC as LugarNacimiento, Sexo, isnull(CALLE,'')+' '+isnull(CASA,'')+' '+isnull(EDIFICIO,'')+' '+isnull(PISO,'')+' '+isnull(APTO,'') as Direccion, Telefono
FROM [Padron_RPM].[dbo].[CEDULADOS]
WHERE MUN_CED = @MUN_CED AND SEQ_CED = @SEQ_CED AND VER_CED = @VER_CED 