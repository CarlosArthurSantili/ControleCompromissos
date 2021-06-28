CREATE TABLE [dbo].[TBTarefa] (
    [Id]                  INT      IDENTITY (1, 1) NOT NULL,
    [Titulo]              TEXT     NOT NULL,
    [Prioridade]          INT      NOT NULL,
    [DataCriacao]         DATETIME NOT NULL,
    [DataConclusao]       DATETIME NULL,
    [PercentualConclusao] INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

