ALTER procedure [dbo].[sp_sysUsuarios]
(
@flag int, @UsuarioID int, @Nombre varchar(70), @EmpleadoID int, @GrupoID int, @Contraseña varchar(30), @Activo bit
)

as 

	if (@flag = -1) -- CONSULTA ULTIMO REGISTRO
	begin
		select * from TsysUsuarios
		where UsuarioID = (select max(UsuarioID) from TsysUsuarios)
	end


	if (@flag = 0) -- CONSULTA UNO O TODOS LOS CLIENTES
	begin
		select * from TsysUsuarios
		where (UsuarioID = @UsuarioID or @UsuarioID = 0) and (Nombre like '%'+@Nombre+'%' or @Nombre = '')
	end


	if (@flag = 1) -- INSERTAR CLIENTES
	begin
		insert into TsysUsuarios (UsuarioID, Nombre, EmpleadoID, GrupoID, Contraseña , Activo)
		values ((select isnull(max(UsuarioID),0)+1 from TsysUsuarios), @Nombre, @EmpleadoID, @GrupoID, @Contraseña, @Activo)
	end

	if (@flag = 2) -- EDITAR CLIENTES
	begin
		Update TsysUsuarios set Nombre = @Nombre, EmpleadoID = @EmpleadoID, GrupoID = @GrupoID, Contraseña = @Contraseña, Activo = @Activo
		where UsuarioID = @UsuarioID
	end

	if	(@flag = 3) -- Validar Login
	begin
		if	(@Activo = 0)
			begin
					if EXISTS(select * from TsysUsuarios where (UsuarioID = @UsuarioID))
						begin						
							if EXISTS(select * from TsysUsuarios where (UsuarioID = @UsuarioID) and (Activo = 1))
								begin
									if EXISTS(select * from TsysUsuarios where (UsuarioID = @UsuarioID) and (Contraseña = @Contraseña))
										begin
											select 'Acceso Permitido' as Resultado
										end
									else
										begin
											select 'Acceso denegado' as Resultado
										end
								end
							else
								begin
									select 'Usuario desactivado' as Resultado
								end	
						end
					else
						begin
							select 'Error de Usuario' as Resultado
						end
			end
		else if(@Activo = 1)
			begin
				Update TsysUsuarios set Activo = 0
				where UsuarioID = @UsuarioID
				select 'El usuario ha sido bloqueado, pidale a TI que restablesca el usuario' as Resultado
			end

	end

go
