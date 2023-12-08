-- inicio: base de datos
create database rutas;
go

use rutas;
go
-- fin: base de datos
go
-- inicio: tablas de seguridad
create table usuarios (
	id_usuario int identity(100000,1) not null primary key,
	usuario varchar(50) not null,
	contrasena varchar(50),
    esta_activo bit
)
go

create table roles (
	id_rol int identity(100000,1) not null primary key,
	rol varchar(255) not null,
    esta_activo bit
)
go

create table roles_por_usuario (
	id int identity(100000,1) not null primary key,
	id_usuario int,
	id_rol int 
)
go

alter table roles_por_usuario
	with check 
	add constraint fk_roles_usuarios_roles
	foreign key (id_rol) 
	references roles (id_rol)
	on delete set null
	on update cascade
go

alter table roles_por_usuario
	check constraint fk_roles_usuarios_roles
go

alter table roles_por_usuario
	with check 
	add constraint fk_roles_usuarios_usuarios
	foreign key (id_usuario) 
	references usuarios (id_usuario)
	on delete set null
	on update cascade
go

alter table roles_por_usuario
	check constraint fk_roles_usuarios_usuarios
go

insert into usuarios (
    usuario,
    contrasena,
    esta_activo
)
values (
    'admin',
    'admin',
    1
)
go

insert into roles (
    rol,
    esta_activo
)
values (
    'admin',
    1
)
go

insert into roles_por_usuario (
    id_usuario,
    id_rol
) values (
    100000,
    100000
)
go
-- fin: tablas de seguridad
go
-- inicio: tablas de control de rutas
create table rutas (
	id_ruta int identity(100000,1) not null primary key,
	ruta varchar(100),
	cantidad_de_paradas int,
	precio_por_persona money,
    esta_activo bit
)
go

create table estados_de_unidad (
	id_estado_de_unidad int identity (100000,1) not null primary key,
	estado varchar(1) not null,
	descripcion varchar(50) not null
)
go

create table unidades (
	id_unidad int identity(100000,1) not null primary key,
	numero_de_placa varchar(8) not null,
	capacidad_de_pasajeros int,
	id_estado_de_unidad int
)
go

create table conductores (
	id_conductor int identity(100000,1) not null primary key,
	nombre_completo varchar(100) not null,
	esta_activo bit,
	esta_disponible bit
)
go

create table conductores_por_unidad (
	id int identity(100000,1) not null primary key,
	id_unidad int,
	id_conductor int
)
go

create table montos_por_ruta_por_unidad (
	id int identity(100000,1) not null primary key,
	fecha date not null,
	id_ruta int not null,
	id_unidad int not null,
	monto_estimado money,
	monto_recaudado money
)
go

alter table unidades
	with check 
	add constraint fk_unidades_estados_de_unidad
	foreign key (id_estado_de_unidad) 
	references estados_de_unidad (id_estado_de_unidad)
	on delete set null
	on update cascade
go

alter table unidades
	check constraint fk_unidades_estados_de_unidad
go

alter table conductores_por_unidad
	with check 
	add constraint fk_conductores_por_unidad_unidad
	foreign key (id_unidad) 
	references unidades (id_unidad)
	on delete set null
	on update cascade
go

alter table conductores_por_unidad
	check constraint fk_conductores_por_unidad_unidad
go

alter table conductores_por_unidad
	with check 
	add constraint fk_conductores_por_unidad_conductor
	foreign key (id_conductor) 
	references conductores (id_conductor)
	on delete set null
	on update cascade
go

alter table conductores_por_unidad
	check constraint fk_conductores_por_unidad_conductor
go

alter table montos_por_ruta_por_unidad
	with check 
	add constraint fk_montos_por_ruta_por_unidad_ruta
	foreign key (id_ruta) 
	references rutas (id_ruta)
	on delete cascade
	on update cascade
go

alter table montos_por_ruta_por_unidad
	check constraint fk_montos_por_ruta_por_unidad_ruta
go

alter table montos_por_ruta_por_unidad
	with check 
	add constraint fk_montos_por_ruta_por_unidad_unidad
	foreign key (id_unidad) 
	references unidades (id_unidad)
	on delete cascade
	on update cascade
go

alter table montos_por_ruta_por_unidad
	check constraint fk_montos_por_ruta_por_unidad_unidad
go
-- fin: tablas de control de rutas
go
-- inicio: vistas
create view vw_usuarios_por_estado as
	select
		a.esta_activo as 'estado_usuario',
		count(a.usuario) as 'usuarios'
	from
		usuarios a
	group by
		a.esta_activo
go

create view vw_usuarios_por_rol as
	select
		b.rol,
		b.esta_activo as 'estado_rol',
		count(c.usuario) as 'usuarios'
	from
		roles_por_usuario a inner join roles b on b.id_rol = a.id_rol
			inner join usuarios c on c.id_usuario = a.id_usuario
	group by
		b.rol, b.esta_activo 
go

create view vw_recaudacion as
	select
		a.fecha,
		b.ruta,
		sum(a.monto_estimado) as 'total_estimado',
		sum(a.monto_recaudado) as 'total_recaudado'
	from
		montos_por_ruta_por_unidad a inner join rutas b on b.id_ruta = a.id_ruta
			inner join unidades c on c.id_unidad = a.id_unidad
	group by
		a.fecha, b.ruta
go

create view vw_unidades_por_estado as
	select
		b.descripcion as 'estado',
		count(a.numero_de_placa) as 'unidades'
	from
		unidades a inner join estados_de_unidad b on b.id_estado_de_unidad = a.id_estado_de_unidad
	group by
		b.descripcion
go
-- fin: vistas
go
-- inicio: funciones
--create function ufn_validar_usuario (@usuario as varchar(50), @contrasena as varchar(50)) returns int with returns null on null input as
--begin
--	declare @usuarioValido int;

--	set @usuarioValido = 0; -- asume el usuario como válido y código de error es 0

--	if not exists (select a.usuario from usuarios a where a.usuario = @usuario)
--	begin
--		set @usuarioValido = 1; -- el usuario no existe y el código de error es 1
--	end

--	if not exists (select a.usuario from usuarios a where a.usuario = @usuario and a.contrasena = @contrasena)
--	begin
--		set @usuarioValido = 2; -- el usuario existe pero la contraseña no corresponde y el código de error es 2
--	end

--	if not exists (select a.usuario from usuarios a where a.usuario = @usuario and a.contrasena = @contrasena and a.esta_activo = 1)
--	begin
--		set @usuarioValido = 3; -- el usuario existe, la contraseña es correcta pero el usuario no está activo y el código de error es 3
--	end

--	if not exists (select a.id_rol from roles_por_usuario a inner join usuarios b on b.id_usuario = a.id_usuario where b.usuario = @usuario and b.contrasena = @contrasena and b.esta_activo = 1)
--	begin
--		set @usuarioValido = 4; -- el usuario existe, la contraseña es correcta y está activo pero no tiene roles asignados y el código de error es 4
--	end

--	if not exists (select b.rol from roles_por_usuario a inner join roles b on b.id_rol = a.id_rol inner join usuarios c on c.id_usuario = a.id_usuario where c.usuario = @usuario and c.contrasena = @contrasena and c.esta_activo = 1 and b.esta_activo = 1)
--	begin
--		set @usuarioValido = 5; -- el usuario existe, la contraseña es correcta, está activo, tiene roles asignados pero no están activos y el código de error es 5
--	end

--	return @usuarioValido;	
--end;
--go

--create function ufn_calcular_monto_estimado (@placa as varchar(8)) returns money as
--begin
--	declare @monto_estimado as money;

--	select
--		@monto_estimado = avg(a.monto_recaudado)
--	from
--		montos_por_ruta_por_unidad a inner join unidades b on b.id_unidad = a.id_unidad
--	where
--		b.numero_de_placa = @placa
	
--	return @monto_estimado;
--end;
--go
-- fin: funciones