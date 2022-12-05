
  go
  drop table emergencia_resumen
  go
  create table emergencia_resumen
  (
   id int identity(1,1) not null, 
   idFechaHora bigint not null,
   Ambulancia decimal(6,5) not null, 
   Bomberos decimal(6,5) not null, 
   Emergencia decimal(6,5) not null, 
   Policia decimal(6,5) not null, 
   Ruido decimal(6,5) not null,  
   Transito decimal(6,5) not null,
   MoyorLabel varchar(20) not null default '',
   MayorValor decimal(6,5) not null,
   fecha date not null default getdate(), 
   hora time not null default getdate(), 
   constraint emergencia_resumen_pk primary key (id))
   go
   drop table emergencia_detalle
   go
  create table emergencia_detalle
  (
   id int identity(1,1) not null, 
   idFechaHora bigint not null,
   organinismo varchar(20) not null,
   valor decimal(6,5) not null, 
   fecha date not null default getdate(), 
   hora time not null default getdate(), 
   constraint emergencia_detalle_pk primary key (id))

GO
   drop table test
go
CREATE TABLE [dbo].[test](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[data] [varchar](100) NULL,
 CONSTRAINT [test_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

   
