//----------------------------------------------Drzava----------------------------------------------
public class Drzava {
	public int IDDrzava { get; set; }
	public string Naziv { get; set; }
}


//----------------------------------------------Grad----------------------------------------------
public class Grad {
	public int IDGrad { get; set; }
	public string Naziv { get; set; }
	public Drzava Drzava { get; set; }
}


//----------------------------------------------Kategorija----------------------------------------------
public class Kategorija {
	public int IDKategorija { get; set; }
	public string Naziv { get; set; }
}


//----------------------------------------------Komercijalist----------------------------------------------
public class Komercijalist {
	public int IDKomercijalist { get; set; }
	public string Ime { get; set; }
	public string Prezime { get; set; }
	public bool StalniZaposlenik { get; set; }
}


//----------------------------------------------KreditnaKartica----------------------------------------------
public class KreditnaKartica {
	public int IDKreditnaKartica { get; set; }
	public string Tip { get; set; }
	public string Broj { get; set; }
	public string IstekMjesec { get; set; }
	public string IstekGodina { get; set; }
}


//----------------------------------------------Kupac----------------------------------------------
public class Kupac {
	public int IDKupac { get; set; }
	public string Ime { get; set; }
	public string Prezime { get; set; }
	public string Email { get; set; }
	public string Telefon { get; set; }
	public Grad Grad { get; set; }
}


//----------------------------------------------Potkategorija----------------------------------------------
public class Potkategorija {
	public int IDPotkategorija { get; set; }
	public Kategorija Kategorija { get; set; }
	public string Naziv { get; set; }
}


//----------------------------------------------Proizvod----------------------------------------------
public class Proizvod {
	public int IDProizvod { get; set; }
	public string Naziv { get; set; }
	public string BrojProizvoda { get; set; }
	public Boja Boja { get; set; }
	public int MinimalnaKolicinaNaSkladistu { get; set; }
	public string CijenaBezPDV { get; set; }
	public int PotkategorijaID { get; set; }
}


//----------------------------------------------Racun----------------------------------------------
public class Racun {
	public int IDRacun { get; set; }
	public string DatumIzdavanja { get; set; }
	public string BrojRacuna { get; set; }
	public int KupacID { get; set; }
	public int KomercijalistID { get; set; }
	public int KreditnaKarticaID { get; set; }
	public string Komentar { get; set; }
}


//----------------------------------------------Stavka----------------------------------------------
public class Stavka {
	public int IDStavka { get; set; }
	public int RacunID { get; set; }
	public string Kolicina { get; set; }
	public int ProizvodID { get; set; }
	public string CijenaPoKomadu { get; set; }
	public string PopustUPostocima { get; set; }
	public string UkupnaCijena { get; set; }
}


