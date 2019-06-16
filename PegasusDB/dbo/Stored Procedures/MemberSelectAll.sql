CREATE PROCEDURE [dbo].[MemberSelectAll]
@MemberID INT NULL=0, @MemberName NVARCHAR (200) NULL=NULL, @Email VARCHAR (200) NULL=NULL, @PhoneNumber VARCHAR (10) NULL=NULL
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
WHERE  (@MemberID = 0
        OR mem.MemberID = @MemberID)
       AND (@MemberName IS NULL
            OR mem.MemberName LIKE '%' + @MemberName + '%')
       AND (@Email IS NULL
            OR mem.Email LIKE '%' + @Email + '%')
       AND (@PhoneNumber IS NULL
            OR CONVERT (VARCHAR, mem.PhoneNumber) LIKE +CONVERT (VARCHAR, ('%' + @PhoneNumber + '%')))
       AND mem.Disable = 1;
RETURN 0;