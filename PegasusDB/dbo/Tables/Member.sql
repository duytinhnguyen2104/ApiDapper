CREATE TABLE [dbo].[Member] (
    [MemberID]    INT            IDENTITY (1, 1) NOT NULL,
    [MemberName]  NVARCHAR (200) NOT NULL,
    [Email]       VARCHAR (200)  NOT NULL,
    [PhoneNumber] CHAR (10)      NULL,
    [ImageID]     INT            DEFAULT ((0)) NULL,
    [Disable]     BIT            DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([MemberID] ASC)
);