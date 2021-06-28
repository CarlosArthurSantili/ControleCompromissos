CREATE TABLE [dbo].[TBTarefa] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Titulo]              VARCHAR (200) NOT NULL,
    [Prioridade]          INT           NOT NULL,
    [DataCriacao]         DATETIME      NOT NULL,
    [DataConclusao]       DATETIME      NOT NULL,
    [PercentualConcluido] INT           NOT NULL,
	[Concluido] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

