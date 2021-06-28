CREATE TABLE [dbo].[TBCompromisso] (
    [id]          INT          IDENTITY (1, 1) NOT NULL,
    [assunto]     VARCHAR (50) NOT NULL,
    [local]       VARCHAR (50) NOT NULL,
    [data]        DATE         NOT NULL,
    [horaInicio]  DATETIME     NOT NULL,
    [horaTermino] DATETIME     NOT NULL,
    [link]        VARCHAR (50) NULL,
    [id_Contato]  INT          NULL,
    CONSTRAINT [PK_TBCompromissos] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_TBCompromisso_TBContato] FOREIGN KEY ([id_Contato]) REFERENCES [dbo].[TBContato] ([Id])
);

