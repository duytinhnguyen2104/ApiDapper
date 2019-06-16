CREATE PROCEDURE [dbo].[MemberSelectOne]
@MemberID INT NULL
AS
SELECT mem.MemberID,
       mem.MemberName,
       mem.Email,
       mem.PhoneNumber,
       img.ImageID,
       img.Thumbnail
FROM   Member AS mem
       LEFT OUTER JOIN
       Image AS img
       ON mem.ImageID = img.ImageID
WHERE  (mem.MemberID = @MemberID)
       AND mem.Disable = 0;
RETURN 0;