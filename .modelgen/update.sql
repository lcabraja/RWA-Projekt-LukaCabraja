----------------------------------------------Drzava----------------------------------------------
----------------------------------------------Grad----------------------------------------------
----------------------------------------------Kategorija----------------------------------------------
IF OBJECT_ID('proc_update_Kategorija') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_update_Kategorija
END
GO
CREATE PROCEDURE proc_update_Kategorija
    @IDKategorija int,
	@Naziv string
AS
BEGIN
    UPDATE Kategorija SET , @Naziv = @Naziv WHERE IDKategorija = @IDKategorija 
END
GO

----------------------------------------------Komercijalist----------------------------------------------
----------------------------------------------KreditnaKartica----------------------------------------------
----------------------------------------------Kupac----------------------------------------------
IF OBJECT_ID('proc_update_Kupac') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_update_Kupac
END
GO
CREATE PROCEDURE proc_update_Kupac
    @IDKupac int,
	@Ime string,
	@Prezime string,
	@Email string,
	@Telefon string,
	@GradID int
AS
BEGIN
    UPDATE Kupac SET , @Ime = @Ime, @Prezime = @Prezime, @Email = @Email, @Telefon = @Telefon, @GradID = @GradID WHERE IDKupac = @IDKupac 
END
GO

----------------------------------------------Potkategorija----------------------------------------------
IF OBJECT_ID('proc_update_Potkategorija') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_update_Potkategorija
END
GO
CREATE PROCEDURE proc_update_Potkategorija
    @IDPotkategorija int,
	@KategorijaID int,
	@Naziv string
AS
BEGIN
    UPDATE Potkategorija SET , @KategorijaID = @KategorijaID, @Naziv = @Naziv WHERE IDPotkategorija = @IDPotkategorija 
END
GO

----------------------------------------------Proizvod----------------------------------------------
IF OBJECT_ID('proc_update_Proizvod') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_update_Proizvod
END
GO
CREATE PROCEDURE proc_update_Proizvod
    @IDProizvod int,
	@Naziv string,
	@BrojProizvoda string,
	@Boja string,
	@MinimalnaKolicinaNaSkladistu string,
	@CijenaBezPDV string,
	@PotkategorijaID int
AS
BEGIN
    UPDATE Proizvod SET , @Naziv = @Naziv, @BrojProizvoda = @BrojProizvoda, @Boja = @Boja, @MinimalnaKolicinaNaSkladistu = @MinimalnaKolicinaNaSkladistu, @CijenaBezPDV = @CijenaBezPDV, @PotkategorijaID = @PotkategorijaID WHERE IDProizvod = @IDProizvod 
END
GO

----------------------------------------------Racun----------------------------------------------
----------------------------------------------Stavka----------------------------------------------
