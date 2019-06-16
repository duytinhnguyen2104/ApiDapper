CREATE PROCEDURE [dbo].[MemberUpdate]
@MemberID NVARCHAR (200) NULL, @MemberName NVARCHAR (200) NULL=NULL, @Email VARCHAR (200) NULL=NULL, @PhoneNumber CHAR (10) NULL=NULL, @ImageID INT NULL=0
AS
DECLARE @result AS INT = 0;
BEGIN
    IF @MemberName IS NOT NULL
        BEGIN
            UPDATE Member
            SET    MemberName = @MemberName
            WHERE  MemberID = @MemberID;
        END
    IF @Email IS NOT NULL
        BEGIN
            UPDATE Member
            SET    Email = @Email
            WHERE  MemberID = @MemberID;
        END
    IF @PhoneNumber IS NOT NULL
        BEGIN
            UPDATE Member
            SET    PhoneNumber = @PhoneNumber
            WHERE  MemberID = @MemberID;
        END
    IF @ImageID IS NOT NULL
        BEGIN
            UPDATE Member
            SET    ImageID = @ImageID
            WHERE  MemberID = @MemberID;
        END
END
IF @@ERROR <> 0
    RETURN 0;
ELSE
    RETURN 1;