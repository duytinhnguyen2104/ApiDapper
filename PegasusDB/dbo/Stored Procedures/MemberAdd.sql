CREATE PROCEDURE [dbo].[MemberAdd]
@MemberName NVARCHAR (200) NULL, @Email VARCHAR (200) NULL, @PhoneNumber CHAR (10) NULL, @ImageID INT NULL
AS
DECLARE @result AS INT = 0;
BEGIN
    INSERT  INTO Member (MemberName, Email, PhoneNumber, ImageID)
    VALUES             (@MemberName, @Email, @PhoneNumber, @ImageID);
    SELECT CAST (SCOPE_IDENTITY() AS INT);
END