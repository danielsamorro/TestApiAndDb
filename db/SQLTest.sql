create table Clientes
(
	Id int identity (1, 1) NOT NULL,
	constraint PK_Clientes_Id primary key clustered (Id),
    CPF varchar(11) NOT NULL,
	Nome varchar(250) NOT NULL,
	UF varchar(2) NOT NULL,
	Celular varchar (13) NOT NULL
);

create nonclustered index IX_Clientes_CPF on Clientes (CPF); 

create table Financiamentos
(
    Id int identity (1, 1) NOT NULL,
	constraint PK_Financiamentos_Id primary key clustered (Id),
	CPF varchar(11) NOT NULL,
	IdCliente int NOT NULL foreign key references Clientes(Id),
	TipoFinanciamento varchar(25) NOT NULL,
	ValorTotal decimal(18,2) NOT NULL,
	DataUltimoVencimento datetime2 NOT NULL
);

create table Parcelas
(
    Id int identity (1, 1) NOT NULL,
	constraint PK_Parcelas_Id primary key clustered (Id),
	IdFinanciamento int NOT NULL foreign key references Financiamentos(Id),
	NumeroDaParcela int NOT NULL,
	ValorParcela decimal(18,2) NOT NULL,
	DataVencimento datetime2 NOT NULL,
	DataPagamento datetime2
);

----------------
insert into Clientes values ('02018976577', 'Joao Silva Rodrigues', 'SP', '11998776655')
insert into Clientes values ('34667388172', 'Maria Pereira Moura', 'RJ', '21988776655')
insert into Clientes values ('78441785074', 'Paulo Correia', 'SP', '11989534411')
insert into Clientes values ('37165353038', 'Luisa de Matos', 'SC', '1498776122')
insert into Clientes values ('55610961092', 'Paulo Assunção', 'RS', '51995276541')
insert into Clientes values ('33221783078', 'Paula Cavalcanti', 'SP', '11998774355')
insert into Clientes values ('73913023097', 'Marcos Santos', 'MG', '31988773322')
insert into Clientes values ('20849470064', 'Luan Fernandez', 'ES', '419885544')
insert into Clientes values ('76664301083', 'Rosane Marques', 'SP', '11998446655')
insert into Clientes values ('04276409039', 'Daniel Santana', 'AM', '91999881134')
insert into Clientes values ('67487263010', 'Matheus Gonçalves', 'PB', '65987221111')

insert into Financiamentos values ('02018976577', 1, 'Direto', 10000, '2023-08-08')
insert into Parcelas values (1, 5, 2000, '2023-04-08', NULL)
insert into Parcelas values (1, 5, 2000, '2023-05-08', NULL)
insert into Parcelas values (1, 5, 2000, '2023-06-08', NULL)
insert into Parcelas values (1, 5, 2000, '2023-07-08', NULL)
insert into Parcelas values (1, 5, 2000, '2023-08-08', NULL)

insert into Financiamentos values ('34667388172', 2, 'Direto', 20000, '2023-05-08')
insert into Parcelas values (2, 5, 4000, '2023-01-08', '2022-01-06')
insert into Parcelas values (2, 5, 4000, '2023-02-08', NULL)
insert into Parcelas values (2, 5, 4000, '2023-03-08', NULL)
insert into Parcelas values (2, 5, 4000, '2023-04-08', NULL)
insert into Parcelas values (2, 5, 4000, '2023-05-08', NULL)

insert into Financiamentos values ('78441785074', 3, 'Direto', 20000, '2023-04-08')
insert into Parcelas values (3, 5, 3333.33, '2022-11-08', '2021-11-06')
insert into Parcelas values (3, 5, 3333.33, '2022-12-08', '2022-12-06')
insert into Parcelas values (3, 5, 3333.33, '2023-01-08', '2023-01-06')
insert into Parcelas values (3, 5, 3333.33, '2023-02-08', '2023-02-06')
insert into Parcelas values (3, 5, 3333.33, '2023-03-08', '2023-03-06')
insert into Parcelas values (3, 5, 3333.33, '2023-04-08', NULL)

-----------

select c.*  from Clientes c
inner join Financiamentos f on f.IdCliente = c.Id
where c.UF = 'SP' and (select (SUM(ValorParcela) / f.ValorTotal) * 100 from Parcelas where DataPagamento is not null and IdFinanciamento = f.Id) > 60
group by c.Id, c.CPF, c.Nome, c.UF, c.Celular;


select top(4) c.* from Clientes c
inner join Financiamentos f on f.IdCliente = c.Id
inner join Parcelas p on p.IdFinanciamento = f.Id
where p.DataPagamento is null and DATEADD(day, 5, p.DataVencimento) <= GETDATE();
