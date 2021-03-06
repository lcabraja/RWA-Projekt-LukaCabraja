USE AdventureWorksOBP
GO
-----------------------Drzava-----------------------
-----------------------Grad-----------------------
-----------------------Kategorija-----------------------
IF OBJECT_ID('proc_create_Kategorija') IS NOT NULL
BEGIN 
    DROP PROCEDURE proc_create_Kategorija 
END
GO
CREATE PROCEDURE proc_create_Kategorija
    @IDKategorija int,
    @Naziv nvarchar(50)
AS
BEGIN
INSERT INTO Kategorija
    VALUES (@IDKategorija, @Naziv)
 
SELECT SCOPE_IDENTITY() AS IDKategorija
END
GO
-----------------------Komercijalist-----------------------
-----------------------KreditnaKartica-----------------------
-----------------------Kupac-----------------------
-----------------------Potkategorija-----------------------
IF OBJECT_ID('proc_create_Potkategorija') IS NOT NULL
BEGIN 
    DROP PROCEDURE proc_create_Potkategorija 
END
GO
CREATE PROCEDURE proc_create_Potkategorija
    @IDPotkategorija int,
    @KategorijaID int,
    @Naziv nvarchar(50)
AS
BEGIN
INSERT INTO Potkategorija
    VALUES (@IDPotkategorija, @KategorijaID, @Naziv)
 
SELECT SCOPE_IDENTITY() AS IDPotkategorija
END
GO
-----------------------Proizvod-----------------------
IF OBJECT_ID('proc_create_Proizvod') IS NOT NULL
BEGIN 
    DROP PROCEDURE proc_create_Proizvod 
END
GO
CREATE PROCEDURE proc_create_Proizvod
    @IDProizvod int,
    @Naziv nvarchar(50),
    @BrojProizvoda nvarchar(25),
    @Boja nvarchar(15),
    @MinimalnaKolicinaNaSkladistu int,
    @CijenaBezPDV money,
    @PotkategorijaID int
AS
BEGIN
INSERT INTO Proizvod
    VALUES (@IDProizvod, @Naziv, @BrojProizvoda, @Boja, @MinimalnaKolicinaNaSkladistu, @CijenaBezPDV, @PotkategorijaID)
 
SELECT SCOPE_IDENTITY() AS IDProizvod
END
GO
-----------------------Racun-----------------------
-----------------------Stavka-----------------------
