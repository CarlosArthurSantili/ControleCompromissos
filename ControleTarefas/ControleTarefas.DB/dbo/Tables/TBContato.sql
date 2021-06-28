CREATE TABLE [dbo].[TBContato] (
    [Id]       INT        IDENTITY (1, 1) NOT NULL,
    [Nome]     NCHAR (50) NULL,
    [Email]    NCHAR (50) NULL,
    [Telefone] INT        NULL,
    [Empresa]  NCHAR (50) NULL,
    [Cargo]    NCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

