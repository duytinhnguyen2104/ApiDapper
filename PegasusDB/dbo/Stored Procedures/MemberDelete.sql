CREATE PROCEDURE [dbo].[MemberDelete]
@MemberID INT NULL
AS
BEGIN
    UPDATE Member
    SET    Member.Disable = 0
    WHERE  Member.MemberID = @MemberID;
END
IF @@ERROR <> 0
    RETURN 1;
ELSE
    RETURN 0;