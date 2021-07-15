USE AdventureWorksOBP
GO
---------------------------------------------------proc_create_Author---------------------------------------------------
IF OBJECT_ID('proc_create_Author') IS NOT NULL
BEGIN 
    DROP PROCEDURE proc_create_Author 
END
GO
CREATE PROCEDURE proc_create_Author
    @Username varchar(64),
    @PasswordHash varchar(128),
    @Email varchar(128)
AS
BEGIN
INSERT INTO Author
    VALUES (@Username, @PasswordHash, @Email)
 
SELECT SCOPE_IDENTITY() AS IDkupac
END
GO
---------------------------------------------------proc_select_Author---------------------------------------------------
IF OBJECT_ID('proc_select_Author') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_Author
END
GO
CREATE PROCEDURE proc_select_Author
    @IDAuthor int
AS
BEGIN
    SELECT * FROM Author WHERE IDAuthor = @IDAuthor
END
GO

----------------------------------------------proc_select_multiple_Author-----------------------------------------------
IF OBJECT_ID('proc_select_multiple_Author') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Author
END
GO
CREATE PROCEDURE proc_select_multiple_Author
AS
BEGIN
    SELECT * FROM Author
END
GO

---------------------------------------------------proc_update_Author---------------------------------------------------
IF OBJECT_ID('proc_update_Author') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_update_Author
END
GO
CREATE PROCEDURE proc_update_Author
    @IDAuthor int,
    @Username varchar(64),
    @PasswordHash varchar(128),
    @Email varchar(128)
AS
BEGIN
    UPDATE Author SET Username = @Username, PasswordHash = @PasswordHash, Email = @Email WHERE IDAuthor = @IDAuthor 
END
GO

---------------------------------------------------proc_delete_Author---------------------------------------------------
IF OBJECT_ID('proc_delete_Author') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_delete_Author
END
GO
CREATE PROCEDURE proc_delete_Author
    @IDAuthor int
AS 
BEGIN 
    DELETE FROM Author WHERE IDAuthor = @IDAuthor
END
GO
