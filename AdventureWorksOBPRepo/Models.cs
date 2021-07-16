using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksOBPRepo
{
    //----------------------------------------------Drzava----------------------------------------------
    public class Drzava
    {
        public int IDDrzava { get; set; }
        public string Naziv { get; set; }
    }
    //----------------------------------------------Grad----------------------------------------------
    public class Grad
    {
        public int IDGrad { get; set; }
        public string Naziv { get; set; }
        public Drzava Drzava { get; set; }
    }
    //----------------------------------------------Kategorija----------------------------------------------
    public class Kategorija
    {
        public int IDKategorija { get; set; }
        public string Naziv { get; set; }
    }
    //----------------------------------------------Komercijalist----------------------------------------------
    public class Komercijalist
    {
        public int IDKomercijalist { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public bool StalniZaposlenik { get; set; }
    }
    //----------------------------------------------KreditnaKartica----------------------------------------------
    public enum TipKreditnaKartica
    {
        MasterCard = 1,
        Diners = 2,
        AmericanExpress = 3,
        Visa = 4
    }
    public class KreditnaKartica
    {
        public int IDKreditnaKartica { get; set; }
        public TipKreditnaKartica Tip { get; set; }
        public string Broj { get; set; }
        public short IstekMjesec { get; set; }
        public short IstekGodina { get; set; }
    }
    //----------------------------------------------Kupac----------------------------------------------
    public class Kupac
    {
        public int IDKupac { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public Grad Grad { get; set; }
    }
    //----------------------------------------------Potkategorija----------------------------------------------
    public class Potkategorija
    {
        public int IDPotkategorija { get; set; }
        public Kategorija Kategorija { get; set; }
        public string Naziv { get; set; }
    }
    //----------------------------------------------Proizvod----------------------------------------------
    public enum Boja
    {
        NoColor = 1,
        Bijela = 2,
        Crna = 3,
        Crvena = 4,
        Plava = 5,
        Siva = 6,
        Srebrna = 7,
        SrebrnaCrna = 8,
        Šarena = 9,
        Žuta = 10

    }
    public class Proizvod
    {
        public int IDProizvod { get; set; }
        public string Naziv { get; set; }
        public string BrojProizvoda { get; set; }
        public Boja Boja { get; set; }
        public int MinimalnaKolicinaNaSkladistu { get; set; }
        public string CijenaBezPDV { get; set; }
        public Potkategorija Potkategorija { get; set; }
    }
    //----------------------------------------------Racun----------------------------------------------
    public class Racun
    {
        public int IDRacun { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public string BrojRacuna { get; set; }
        public Kupac Kupac { get; set; }
        public Komercijalist Komercijalist { get; set; }
        public KreditnaKartica KreditnaKartica { get; set; }
        public string Komentar { get; set; }
    }
    //----------------------------------------------Stavka----------------------------------------------
    public class Stavka
    {
        public int IDStavka { get; set; }
        public Racun Racun { get; set; }
        public int Kolicina { get; set; }
        public Proizvod Proizvod { get; set; }
        public decimal CijenaPoKomadu { get; set; }
        public decimal PopustUPostocima { get; set; }
        public decimal UkupnaCijena { get; set; }
    }
}