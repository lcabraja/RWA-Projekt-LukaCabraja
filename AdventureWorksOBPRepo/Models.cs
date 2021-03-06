using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksOBPRepo
{
    #region ---------------------------------------------------------------------------------------------------------------------Drzava---------------------------------------------
    [Serializable]
    public class Drzava
    {
        public int IDDrzava { get; set; }
        public string Naziv { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return IDDrzava.Equals(obj);
        }
        public override int GetHashCode()
        {
            return IDDrzava.GetHashCode();
        }
    }
    #endregion
    #region ---------------------------------------------------------------------------------------------------------------------Grad-----------------------------------------------
    [Serializable]
    public class Grad
    {
        public int IDGrad { get; set; }
        public string Naziv { get; set; }
        public Drzava Drzava { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return IDGrad.Equals(obj);
        }
        public override int GetHashCode()
        {
            return IDGrad.GetHashCode();
        }
    }
    #endregion
    #region ---------------------------------------------------------------------------------------------------------------------Kategorija-----------------------------------------
    [Serializable]
    public class Kategorija
    {
        public int IDKategorija { get; set; }
        [Required]
        public string Naziv { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return IDKategorija.Equals(obj);
        }
        public override int GetHashCode()
        {
            return IDKategorija.GetHashCode();
        }
    }
    #endregion
    #region ---------------------------------------------------------------------------------------------------------------------Komercijalist--------------------------------------
    [Serializable]
    public class Komercijalist
    {
        public int IDKomercijalist { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public bool StalniZaposlenik { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return IDKomercijalist.Equals(obj);
        }
        public override int GetHashCode()
        {
            return IDKomercijalist.GetHashCode();
        }
    }
    #endregion 
    #region ---------------------------------------------------------------------------------------------------------------------KreditnaKartica------------------------------------
    public enum TipKreditnaKartica
    {
        MasterCard = 1,
        Diners = 2,
        AmericanExpress = 3,
        Visa = 4,
        Other = 5
    }
    [Serializable]
    public class KreditnaKartica
    {
        public int IDKreditnaKartica { get; set; }
        public TipKreditnaKartica Tip { get; set; }
        public string Broj { get; set; }
        public short IstekMjesec { get; set; }
        public short IstekGodina { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return IDKreditnaKartica.Equals(obj);
        }
        public override int GetHashCode()
        {
            return IDKreditnaKartica.GetHashCode();
        }
    }
    #endregion
    #region ---------------------------------------------------------------------------------------------------------------------Kupac----------------------------------------------
    [Serializable]
    public class Kupac
    {
        public int IDKupac { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public Grad Grad { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return IDKupac.Equals(obj);
        }
        public override int GetHashCode()
        {
            return IDKupac.GetHashCode();
        }
    }
    #endregion
    #region ---------------------------------------------------------------------------------------------------------------------Potkategorija--------------------------------------
    [Serializable]
    public class Potkategorija
    {
        public int IDPotkategorija { get; set; }
        public Kategorija Kategorija { get; set; }
        [Required]
        public string Naziv { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return IDPotkategorija.Equals(obj);
        }
        public override int GetHashCode()
        {
            return IDPotkategorija.GetHashCode();
        }
    }
    #endregion 
    #region ---------------------------------------------------------------------------------------------------------------------Proizvod-------------------------------------------
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
        Sarena = 9,
        Zuta = 10

    }
    [Serializable]
    public class Proizvod
    {
        public int IDProizvod { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public string BrojProizvoda { get; set; }
        public Boja Boja { get; set; }
        public int MinimalnaKolicinaNaSkladistu { get; set; }
        public decimal CijenaBezPDV { get; set; }
        public Potkategorija Potkategorija { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return IDProizvod.Equals(obj);
        }
        public override int GetHashCode()
        {
            return IDProizvod.GetHashCode();
        }
    }
    #endregion
    #region ---------------------------------------------------------------------------------------------------------------------Racun----------------------------------------------
    [Serializable]
    public class Racun
    {
        public int IDRacun { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public string BrojRacuna { get; set; }
        public Kupac Kupac { get; set; }
        public Komercijalist Komercijalist { get; set; }
        public KreditnaKartica KreditnaKartica { get; set; }
        public string Komentar { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return IDRacun.Equals(obj);
        }
        public override int GetHashCode()
        {
            return IDRacun.GetHashCode();
        }
    }
    #endregion
    #region ---------------------------------------------------------------------------------------------------------------------Stavka---------------------------------------------
    [Serializable]
    public class Stavka
    {
        public int IDStavka { get; set; }
        public Racun Racun { get; set; }
        public short Kolicina { get; set; }
        public Proizvod Proizvod { get; set; }
        public decimal CijenaPoKomadu { get; set; }
        public decimal PopustUPostocima { get; set; }
        public decimal UkupnaCijena { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return IDStavka.Equals(obj);
        }
        public override int GetHashCode()
        {
            return IDStavka.GetHashCode();
        }
    }
    #endregion
    #region ---------------------------------------------------------------------------------------------------------------------LoginData---------------------------------------------
    [Serializable]
    public class LoginData
    {
        public int IDLoginData { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return IDLoginData.Equals(obj);
        }
        public override int GetHashCode()
        {
            return IDLoginData.GetHashCode();
        }
    }
    #endregion
}