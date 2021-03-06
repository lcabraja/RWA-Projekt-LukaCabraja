USE AdventureWorksOBP
GO
----------------------------------------------Drzava----------------------------------------------
IF OBJECT_ID('proc_select_Drzava') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_Drzava
END
GO
CREATE PROCEDURE proc_select_Drzava
    @IDDrzava INT
AS
BEGIN
    SELECT * FROM Drzava WHERE IDDrzava = @IDDrzava
END
GO

IF OBJECT_ID('proc_select_multiple_Drzava') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Drzava
END
GO
CREATE PROCEDURE proc_select_multiple_Drzava
AS
BEGIN
    SELECT * FROM Drzava
END
GO

----------------------------------------------Grad----------------------------------------------
IF OBJECT_ID('proc_select_Grad') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_Grad
END
GO
CREATE PROCEDURE proc_select_Grad
    @IDGrad INT
AS
BEGIN
    SELECT * FROM Grad WHERE IDGrad = @IDGrad
END
GO

IF OBJECT_ID('proc_select_multiple_Grad') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Grad
END
GO
CREATE PROCEDURE proc_select_multiple_Grad
AS
BEGIN
    SELECT * FROM Grad
END
GO

IF OBJECT_ID('proc_select_multiple_Grad_targeted') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Grad_targeted
END
GO
CREATE PROCEDURE proc_select_multiple_Grad_targeted
    @DrzavaID INT
AS
BEGIN
    SELECT * FROM Grad WHERE DrzavaID = @DrzavaID
END
GO
----------------------------------------------Kategorija----------------------------------------------
IF OBJECT_ID('proc_create_Kategorija') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_create_Kategorija
END
GO
CREATE PROCEDURE proc_create_Kategorija
    @Naziv nvarchar(50)
AS
BEGIN
INSERT INTO Kategorija
    VALUES (@Naziv)

SELECT SCOPE_IDENTITY() AS IDKategorija
END
GO

IF OBJECT_ID('proc_select_Kategorija') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_Kategorija
END
GO
CREATE PROCEDURE proc_select_Kategorija
    @IDKategorija INT
AS
BEGIN
    SELECT * FROM Kategorija WHERE IDKategorija = @IDKategorija
END
GO

IF OBJECT_ID('proc_select_multiple_Kategorija') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Kategorija
END
GO
CREATE PROCEDURE proc_select_multiple_Kategorija
AS
BEGIN
    SELECT * FROM Kategorija
END
GO

IF OBJECT_ID('proc_update_Kategorija') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_update_Kategorija
END
GO
CREATE PROCEDURE proc_update_Kategorija
    @IDKategorija int,
    @Naziv nvarchar(50)
AS
BEGIN
    UPDATE Kategorija SET Naziv = @Naziv WHERE IDKategorija = @IDKategorija
END
GO


IF OBJECT_ID('proc_delete_Kategorija') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_delete_Kategorija
END
GO
CREATE PROCEDURE proc_delete_Kategorija
    @IDKategorija INT
AS
BEGIN
    DELETE FROM Potkategorija WHERE KategorijaID = @IDKategorija
    DELETE FROM KATEGORIJA WHERE IDKategorija = @IDKategorija
END
GO

----------------------------------------------Komercijalist----------------------------------------------
IF OBJECT_ID('proc_select_Komercijalist') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_Komercijalist
END
GO
CREATE PROCEDURE proc_select_Komercijalist
    @IDKomercijalist INT
AS
BEGIN
    SELECT * FROM Komercijalist WHERE IDKomercijalist = @IDKomercijalist
END
GO
IF OBJECT_ID('proc_select_multiple_Komercijalist') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Komercijalist
END
GO
CREATE PROCEDURE proc_select_multiple_Komercijalist
AS
BEGIN
    SELECT * FROM Komercijalist
END
GO
----------------------------------------------KreditnaKartica----------------------------------------------
IF OBJECT_ID('proc_select_KreditnaKartica_broj') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_KreditnaKartica_broj
END
GO
CREATE PROCEDURE proc_select_KreditnaKartica_broj
    @Broj nvarchar(25)
AS
BEGIN
    SELECT * FROM KreditnaKartica WHERE Broj = @Broj
END
GO

IF OBJECT_ID('proc_select_KreditnaKartica_id') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_KreditnaKartica_id
END
GO
CREATE PROCEDURE proc_select_KreditnaKartica_id
    @IDKreditnaKartica INT
AS
BEGIN
    SELECT * FROM KreditnaKartica WHERE IDKreditnaKartica = @IDKreditnaKartica
END
GO

IF OBJECT_ID('proc_select_multiple_KreditnaKartica') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_KreditnaKartica
END
GO
CREATE PROCEDURE proc_select_multiple_KreditnaKartica
AS
BEGIN
    SELECT * FROM KreditnaKartica
END
GO
----------------------------------------------Kupac----------------------------------------------
IF OBJECT_ID('proc_select_multiple_Kupac_Grad') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Kupac_Grad
END
GO
CREATE PROCEDURE proc_select_multiple_Kupac_Grad
    @COLUMNSORT NVARCHAR(15),
    @GradID INT,
    @skip INT,
    @count INT
AS
BEGIN
    IF @COLUMNSORT = 'IDKupacAsc'
        BEGIN
            SELECT *
            FROM Kupac
            WHERE GradID = @GradID
            ORDER BY IDKupac ASC
            OFFSET (@skip) ROWS FETCH NEXT (@count) ROWS ONLY
        END
    ELSE IF @COLUMNSORT = 'IDKupacDesc'
        BEGIN
            SELECT *
            FROM Kupac
            WHERE GradID = @GradID
            ORDER BY IDKupac DESC
            OFFSET (@skip) ROWS FETCH NEXT (@count) ROWS ONLY
        END
    IF @COLUMNSORT = 'ImeAsc'
        BEGIN
            SELECT *
            FROM Kupac
            WHERE GradID = @GradID
            ORDER BY Ime ASC
            OFFSET (@skip) ROWS FETCH NEXT (@count) ROWS ONLY
        END
    ELSE IF @COLUMNSORT = 'ImeDesc'
        BEGIN
            SELECT *
            FROM Kupac
            WHERE GradID = @GradID
            ORDER BY Ime DESC
            OFFSET (@skip) ROWS FETCH NEXT (@count) ROWS ONLY
        END
    ELSE IF @COLUMNSORT = 'PrezimeAsc'
        BEGIN
            SELECT *
            FROM Kupac
            WHERE GradID = @GradID
            ORDER BY Prezime ASC
            OFFSET (@skip) ROWS FETCH NEXT (@count) ROWS ONLY
        END
    ELSE IF @COLUMNSORT = 'PrezimeDesc'
        BEGIN
            SELECT *
            FROM Kupac
            WHERE GradID = @GradID
            ORDER BY Prezime DESC
            OFFSET (@skip) ROWS FETCH NEXT (@count) ROWS ONLY
        END
END
GO


IF OBJECT_ID('proc_select_multiple_Kupac') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Kupac
END
GO
CREATE PROCEDURE proc_select_multiple_Kupac
    @COLUMNSORT NVARCHAR(15),
    @skip INT,
    @count INT
AS
BEGIN
    IF @COLUMNSORT = 'IDKupacAsc'
        BEGIN
            SELECT *
            FROM Kupac
            ORDER BY IDKupac ASC
            OFFSET (@skip) ROWS FETCH NEXT (@count) ROWS ONLY
        END
    ELSE IF @COLUMNSORT = 'IDKupacDesc'
        BEGIN
            SELECT *
            FROM Kupac
            ORDER BY IDKupac DESC
            OFFSET (@skip) ROWS FETCH NEXT (@count) ROWS ONLY
        END
    IF @COLUMNSORT = 'ImeAsc'
        BEGIN
            SELECT *
            FROM Kupac
            ORDER BY Ime ASC
            OFFSET (@skip) ROWS FETCH NEXT (@count) ROWS ONLY
        END
    ELSE IF @COLUMNSORT = 'ImeDesc'
        BEGIN
            SELECT *
            FROM Kupac
            ORDER BY Ime DESC
            OFFSET (@skip) ROWS FETCH NEXT (@count) ROWS ONLY
        END
    ELSE IF @COLUMNSORT = 'PrezimeAsc'
        BEGIN
            SELECT *
            FROM Kupac
            ORDER BY Prezime ASC
            OFFSET (@skip) ROWS FETCH NEXT (@count) ROWS ONLY
        END
    ELSE IF @COLUMNSORT = 'PrezimeDesc'
        BEGIN
            SELECT *
            FROM Kupac
            ORDER BY Prezime DESC
            OFFSET (@skip) ROWS FETCH NEXT (@count) ROWS ONLY
        END
END
GO

IF OBJECT_ID('proc_select_Kupac') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_Kupac
END
GO
CREATE PROCEDURE proc_select_Kupac
    @IDKupac INT
AS
BEGIN
    SELECT * FROM Kupac WHERE IDKupac = @IDKupac
END
GO

IF OBJECT_ID('proc_update_Kupac') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_update_Kupac
END
GO
CREATE PROCEDURE proc_update_Kupac
    @IDKupac int,
    @Ime nvarchar(50),
	@Prezime nvarchar(50),
	@Email nvarchar(50),
	@Telefon nvarchar(25),
	@GradID int
AS
BEGIN
    UPDATE Kupac SET Ime = @Ime, Prezime = @Prezime, Email = @Email, Telefon = @Telefon, GradID = @GradID WHERE IDKupac = @IDKupac
END
GO

----------------------------------------------Potkategorija----------------------------------------------
IF OBJECT_ID('proc_create_Potkategorija') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_create_Potkategorija
END
GO
CREATE PROCEDURE proc_create_Potkategorija
    @KategorijaID int,
    @Naziv nvarchar(50)
AS
BEGIN
INSERT INTO Potkategorija
    VALUES (@KategorijaID, @Naziv)

SELECT SCOPE_IDENTITY() AS IDPotkategorija
END
GO

IF OBJECT_ID('proc_select_Potkategorija') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_Potkategorija
END
GO
CREATE PROCEDURE proc_select_Potkategorija
    @IDPotkategorija INT
AS
BEGIN
    SELECT * FROM Potkategorija WHERE IDPotkategorija = @IDPotkategorija
END
GO

IF OBJECT_ID('proc_select_multiple_Potkategorija') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Potkategorija
END
GO
CREATE PROCEDURE proc_select_multiple_Potkategorija
AS
BEGIN
    SELECT * FROM Potkategorija
END
GO

IF OBJECT_ID('proc_select_multiple_Potkategorija_targeted') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Potkategorija_targeted
END
GO
CREATE PROCEDURE proc_select_multiple_Potkategorija_targeted
    @KategorijaID INT
AS
BEGIN
    SELECT * FROM Potkategorija WHERE KategorijaID = @KategorijaID
END
GO

IF OBJECT_ID('proc_update_Potkategorija') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_update_Potkategorija
END
GO
CREATE PROCEDURE proc_update_Potkategorija
    @IDPotkategorija int,
	@KategorijaID int,
	@Naziv nvarchar(50)
AS
BEGIN
    UPDATE Potkategorija SET KategorijaID = @KategorijaID, Naziv = @Naziv WHERE IDPotkategorija = @IDPotkategorija
END
GO

IF OBJECT_ID('proc_delete_Potkategorija') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_delete_Potkategorija
END
GO
CREATE PROCEDURE proc_delete_Potkategorija
    @IDPotkategorija int
AS
BEGIN
    DELETE FROM Potkategorija WHERE IDPotkategorija = @IDPotkategorija
END
GO

----------------------------------------------Proizvod----------------------------------------------
IF OBJECT_ID('proc_create_Proizvod') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_create_Proizvod
END
GO
CREATE PROCEDURE proc_create_Proizvod
    @Naziv nvarchar(50),
    @BrojProizvoda nvarchar(25),
    @Boja nvarchar(15),
    @MinimalnaKolicinaNaSkladistu int,
    @CijenaBezPDV money,
    @PotkategorijaID int
AS
BEGIN
INSERT INTO Proizvod
    VALUES (@Naziv, @BrojProizvoda, @Boja, @MinimalnaKolicinaNaSkladistu, @CijenaBezPDV, @PotkategorijaID)
SELECT SCOPE_IDENTITY() AS IDProizvod
END
GO


IF OBJECT_ID('proc_select_Proizvod') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_Proizvod
END
GO
CREATE PROCEDURE proc_select_Proizvod
    @IDProizvod INT
AS
BEGIN
    SELECT * FROM Proizvod WHERE IDProizvod = @IDProizvod
END
GO

IF OBJECT_ID('proc_select_multiple_Proizvod') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Proizvod
END
GO
CREATE PROCEDURE proc_select_multiple_Proizvod
    @skip INT,
    @count INT
AS
BEGIN
    BEGIN
        SELECT *
        FROM Proizvod
        ORDER BY IDProizvod ASC
        OFFSET (@skip) ROWS FETCH NEXT (@count) ROWS ONLY
    END
END
GO

IF OBJECT_ID('proc_update_Proizvod') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_update_Proizvod
END
GO
CREATE PROCEDURE proc_update_Proizvod
    @IDProizvod INT,
	@Naziv NVARCHAR(50),
	@BrojProizvoda NVARCHAR(25),
	@Boja NVARCHAR(15),
	@MinimalnaKolicinaNaSkladistu INT,
	@CijenaBezPDV MONEY,
	@PotkategorijaID INT
AS
BEGIN
    UPDATE Proizvod SET Naziv = @Naziv, BrojProizvoda = @BrojProizvoda, Boja = @Boja, MinimalnaKolicinaNaSkladistu = @MinimalnaKolicinaNaSkladistu, CijenaBezPDV = @CijenaBezPDV, PotkategorijaID = @PotkategorijaID WHERE IDProizvod = @IDProizvod
END
GO

IF OBJECT_ID('proc_delete_Proizvod') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_delete_Proizvod
END
GO
CREATE PROCEDURE proc_delete_Proizvod
    @IDProizvod INT
AS
BEGIN
   DELETE FROM Proizvod WHERE IDProizvod = @IDProizvod
END
GO
----------------------------------------------Racun----------------------------------------------
IF OBJECT_ID('proc_select_Racun') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_Racun
END
GO
CREATE PROCEDURE proc_select_Racun
    @IDRacun INT
AS
BEGIN
    SELECT * FROM Racun WHERE IDRacun = @IDRacun
END
GO
IF OBJECT_ID('proc_select_multiple_Racun') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Racun
END
GO
CREATE PROCEDURE proc_select_multiple_Racun
AS
BEGIN
    SELECT * FROM Racun
END
GO

IF OBJECT_ID('proc_select_multiple_Racun_targeted') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Racun_targeted
END
GO
CREATE PROCEDURE proc_select_multiple_Racun_targeted
    @KupacID INT
AS
BEGIN
    SELECT * FROM Racun WHERE KupacID = @KupacID
END
GO
----------------------------------------------Stavka----------------------------------------------
IF OBJECT_ID('proc_select_Stavka') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_Stavka
END
GO
CREATE PROCEDURE proc_select_Stavka
    @IDStavka INT
AS
BEGIN
    SELECT * FROM Stavka WHERE IDStavka = @IDStavka
END
GO
IF OBJECT_ID('proc_select_multiple_Stavka') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Stavka
END
GO
CREATE PROCEDURE proc_select_multiple_Stavka
AS
BEGIN
    SELECT * FROM Stavka
END
GO
IF OBJECT_ID('proc_select_multiple_Stavka_targeted') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_Stavka_targeted
END
GO
CREATE PROCEDURE proc_select_multiple_Stavka_targeted
    @RacunID INT
AS
BEGIN
    SELECT * FROM Stavka WHERE RacunID = @RacunID
END
GO

----------------------------------------------LoginData----------------------------------------------
IF OBJECT_ID('proc_select_LoginData') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_LoginData
END
GO
CREATE PROCEDURE proc_select_LoginData
    @Username nvarchar(50)
AS
BEGIN
    SELECT * FROM LoginData WHERE Username = @Username
END
GO