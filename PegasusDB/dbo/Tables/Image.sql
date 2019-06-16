CREATE TABLE [dbo].[Image] (
    [ImageID]   INT            IDENTITY (1, 1) NOT NULL,
    [Thumbnail] VARCHAR (500)  NOT NULL,
    [Tiny]      BIT            DEFAULT ((0)) NULL,
    [Small]     BIT            DEFAULT ((0)) NULL,
    [Medium]    BIT            DEFAULT ((0)) NULL,
    [Orginal]   BIT            DEFAULT ((0)) NULL,
    [CreateBy]  NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([ImageID] ASC)
);