insert into TBTarefa 
	(
		[Titulo],
		[Prioridade],
		[DataCriacao],
		[DataConclusao],
		[PercentualConcluido],	
		[Concluido]
	)
	values
	(
		'Correr',
		3,
		'06/17/2021',
		'06/18/2021',
		30,
		1
	)

update TBTarefa
	set	
		[Titulo] = 'andar',
		[Prioridade] = 1,
		[DataCriacao] = '01/01/2020',
		[DataConclusao] = '02/02/2020',
		[PercentualConcluido] = 100,
		[CONCLUIDO] = 1
	where
		[Id] = 18

Delete from TBTarefa
	where
		[Id] = 1

select [Id], [Titulo], [Prioridade] from TBTarefa

select * from TBTarefa
	where
		[Id] = 4

select * from TBTarefa