create database C3DCombiner;

use C3DCombiner;


create table usuario(
	id int identity(1,1) primary key,
	username varchar(50) unique not null,
	clave varchar(50) not null,
	nombre varchar(500) not null
);

insert into usuario values('titus93','registrate1.','Marvin Emmanuel Pivaral Orellana');

insert into usuario values('titus99','registrate1.','Mario Ortega');

create table Archivo(
	id int primary key identity(1,1),
	nombre varchar(100) not null,
	creacion datetime not null default getdate(),
	modificacion datetime not null default getdate(),
	descripcion text null,
	codigo text not null,
	url text not null,
	usuario int,
	unique(nombre, usuario),

	foreign key(usuario) references usuario(id)
);

SELECT * FROM USUARIO, ARCHIVO WHERE Usuario.id = archivo.usuario;

SELECT * FROM ARCHIVO;

SELECT * FROM usuario;



insert into Archivo(nombre, descripcion, codigo, url, usuario) values ('tree', 'Esto es una prueba', 'abcde', 'http://prueba.tree', 1);
insert into Archivo(nombre, descripcion, codigo, url, usuario) values ('tree2', 'Esto es una prueba2', 'abcde2', 'http://prueba2.tree', 5);

select * from usuario, archivo where Archivo.id = 8 and usuario.id = Archivo.usuario;
SELECT username, CONVERT(NVARCHAR(10), creacion, 103) FROM USUARIO, ARCHIVO WHERE archivo.id = 8 AND usuario.id = archivo.usuario;


select IDENT_CURRENT('archivo');

update archivo set nombre = 'asdf' where nombre = 'tree';

select * from archivo where CAST(url AS nvarchar(max)) = 'http://localhost:1993/Archivo?id=1';

select 'http://localhost:1993/Archivo?id=1'

truncate table archivo;
select id from archivo where nombre = 'prueba' and usuario =  1;