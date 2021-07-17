using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksOBPRepo
{
    public class Repo
    {
        public const int MAXTAKE = 50;

        public string ConnectionString { get; private set; }
        public Repo(string connectionString)
        {
            ConnectionString = connectionString;

            cacheDrzava = new SortedList<int, Drzava>();
            cacheGrad = new SortedList<int, Grad>();
            cacheKategorija = new SortedList<int, Kategorija>();
            cacheKomercijalist = new SortedList<int, Komercijalist>();
            cacheKreditnaKartica = new SortedList<int, KreditnaKartica>();
            cacheKupac = new SortedList<int, Kupac>();
            cacheKupacIme = new SortedList<string, Kupac>();
            cacheKupacPrezime = new SortedList<string, Kupac>();
            cachePotkategorija = new SortedList<int, Potkategorija>();
            cacheProizvod = new SortedList<int, Proizvod>();
            cacheRacun = new SortedList<int, Racun>();
            cacheStavka = new SortedList<int, Stavka>();
        }

        private SortedList<int, Drzava> cacheDrzava;
        private SortedList<int, Grad> cacheGrad;
        private SortedList<int, Kategorija> cacheKategorija;
        private SortedList<int, Komercijalist> cacheKomercijalist;
        private SortedList<int, KreditnaKartica> cacheKreditnaKartica;
        private SortedList<int, Kupac> cacheKupac;
        private SortedList<string, Kupac> cacheKupacIme;
        private SortedList<string, Kupac> cacheKupacPrezime;
        private SortedList<int, Potkategorija> cachePotkategorija;
        private SortedList<int, Proizvod> cacheProizvod;
        private SortedList<int, Racun> cacheRacun;
        private SortedList<int, Stavka> cacheStavka;

        public bool recacheDrzava { get; set; } = false;
        public bool recacheGrad { get; set; } = false;
        public bool recacheKategorija { get; set; } = false;
        public bool recacheKomercijalist { get; set; } = false;
        public bool recacheKreditnaKartica { get; set; } = false;
        public bool recacheKupac { get; set; } = false;
        public bool recachePotkategorija { get; set; } = false;
        public bool recacheProizvod { get; set; } = false;
        public bool recacheRacun { get; set; } = false;
        public bool recacheStavka { get; set; } = false;

        // R Drzava             OK
        // R Grad               OK
        // CRUD Kategorija      OK
        // R Komercijalist      OK
        // R KreditnaKartica    OK
        // RU Kupac             OK
        // CRUD Potkategorija   OK
        // CRUD Proizvod        OK
        // R Racun              OK
        // R Stavka             OK

        //---------------------------------------------------------------------------------------------------------------------Drzava-----------------------------------------
        public Drzava GetDrzava(int IDDrzava)
        {
            if (IDDrzava == 0)
                return null;
            Drzava Drzava;
            if (cacheDrzava.TryGetValue(IDDrzava, out Drzava))
            {
                return Drzava;
            }
            else
            {
                Drzava = GetDrzavaFromDataRow(
                    SqlHelper.ExecuteDataset(ConnectionString, "proc_select_Drzava", IDDrzava).Tables[0].Rows[0]);
                Cache(Drzava);
                return Drzava;
            }
        }
        public SortedList<int, Drzava> GetMultipleDrzava()
        {
            if (cacheDrzava.Count == 0 && !recacheDrzava)
            {
                SortedList<int, Drzava> collection = new SortedList<int, Drzava>();
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_Drzava");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var drzava = GetDrzavaFromDataRow(row);
                    collection[drzava.IDDrzava] = drzava;
                }
                cacheDrzava = collection;
                recacheDrzava = false;
            }
            return cacheDrzava;
        }
        private Drzava GetDrzavaFromDataRow(DataRow row)
        {
            return new Drzava
            {
                IDDrzava = (int)row["IDDrzava"],
                Naziv = row["Naziv"].ToString()
            };
        }
        //---------------------------------------------------------------------------------------------------------------------Grad-------------------------------------------
        public Grad GetGrad(int IDGrad)
        {
            if (IDGrad == 0)
                return null;
            Grad Grad;
            if (cacheGrad.TryGetValue(IDGrad, out Grad))
            {
                return Grad;
            }
            else
            {
                Grad = GetGradFromDataRow(
                    SqlHelper.ExecuteDataset(ConnectionString, "proc_select_Grad", IDGrad).Tables[0].Rows[0]);
                Cache(Grad);
                return Grad;
            }
        }
        public SortedList<int, Grad> GetMultipleGrad()
        {
            if (cacheGrad.Count == 0 && !recacheGrad)
            {
                SortedList<int, Grad> collection = new SortedList<int, Grad>();
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_Grad");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var Grad = GetGradFromDataRow(row);
                    collection[Grad.IDGrad] = Grad;
                }
                cacheGrad = collection;
                recacheGrad = false;
            }
            return cacheGrad;
        }
        public SortedList<int, Grad> GetMultipleGrad(int drzavaID)
        {
            if (cacheGrad.Count == 0 && !recacheGrad)
            {
                SortedList<int, Grad> collection = new SortedList<int, Grad>();
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_Grad_targeted", drzavaID);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var Grad = GetGradFromDataRow(row);
                    collection[Grad.IDGrad] = Grad;
                }
                collection.Values.ToList().ForEach(Cache);
                recacheGrad = false;
                return collection;
            }
            return cacheGrad.Where(x => x.Value.Drzava.IDDrzava == drzavaID)
                            .Aggregate(new SortedList<int, Grad>(), (x, y) => { x[y.Key] = y.Value; return x; }, (x) => { return x; });
        }
        private Grad GetGradFromDataRow(DataRow row)
        {
            int result;
            var Drzava = int.TryParse(row["DrzavaID"].ToString(), out result) ? GetDrzava(result) : GetDrzava(0);
            return new Grad
            {
                IDGrad = (int)row["IDGrad"],
                Naziv = row["Naziv"].ToString(),
                Drzava = Drzava
            };
        }
        //---------------------------------------------------------------------------------------------------------------------Kategorija-------------------------------------
        public int CreateKategorija(Kategorija kategorija)
        {
            int IDKategorija = int.Parse(SqlHelper.ExecuteScalar(ConnectionString, "proc_create_Kategorija", kategorija.Naziv.WithMaxLength(50)).ToString());
            if (IDKategorija > 0)
            {
                kategorija.IDKategorija = IDKategorija;
                Cache(kategorija);
                return IDKategorija;
            }
            return 0;
        }
        public Kategorija GetKategorija(int IDKategorija)
        {
            if (IDKategorija == 0)
                return null;
            Kategorija Kategorija;
            if (cacheKategorija.TryGetValue(IDKategorija, out Kategorija))
            {
                return Kategorija;
            }
            else
            {
                Kategorija = GetKategorijaFromDataRow(
                    SqlHelper.ExecuteDataset(ConnectionString, "proc_select_Kategorija", IDKategorija).Tables[0].Rows[0]);
                Cache(Kategorija);
                return Kategorija;
            }
        }
        public SortedList<int, Kategorija> GetMultipleKategorija()
        {
            if (cacheKategorija.Count == 0 && !recacheKategorija)
            {
                SortedList<int, Kategorija> collection = new SortedList<int, Kategorija>();
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_Kategorija");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var Kategorija = GetKategorijaFromDataRow(row);
                    collection[Kategorija.IDKategorija] = Kategorija;
                }
                cacheKategorija = collection;
                recacheKategorija = false;
            }
            return cacheKategorija;
        }
        public int UpdateKategorija(Kategorija kategorija)
        {
            int rows = SqlHelper.ExecuteNonQuery(
                    ConnectionString,
                    "proc_update_Kategorija",
                    kategorija.IDKategorija,
                    kategorija.Naziv.WithMaxLength(50)
                );

            if (rows > 0)
            {
                Cache(kategorija);
                return rows;
            }
            return 0;
        }
        public void DeleteKategorija(int idKategorija)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "proc_delete_Kategorija", idKategorija);
            cacheKategorija.Remove(idKategorija);
        }
        private Kategorija GetKategorijaFromDataRow(DataRow row)
        {
            return new Kategorija
            {
                IDKategorija = (int)row["IDKategorija"],
                Naziv = row["Naziv"].ToString()
            };
        }
        //--------------------------------------------------------------------------------------------------------------------- ----------------------------------
        public Komercijalist GetKomercijalist(int idKomercijalist)
        {
            if (idKomercijalist == 0)
                return null;
            Komercijalist Komercijalist;
            if (cacheKomercijalist.TryGetValue(idKomercijalist, out Komercijalist))
            {
                return Komercijalist;
            }
            else
            {
                Komercijalist = GetKomercijalistFromDataRow(
                    SqlHelper.ExecuteDataset(ConnectionString, "proc_select_Komercijalist", idKomercijalist).Tables[0].Rows[0]);
                Cache(Komercijalist);
                return Komercijalist;
            }
        }
        public SortedList<int, Komercijalist> GetMultipleKomercijalist()
        {
            if (cacheKomercijalist.Count == 0 && !recacheKomercijalist)
            {
                SortedList<int, Komercijalist> collection = new SortedList<int, Komercijalist>();
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_Komercijalist");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var Komercijalist = GetKomercijalistFromDataRow(row);
                    collection[Komercijalist.IDKomercijalist] = Komercijalist;
                }
                cacheKomercijalist = collection;
                recacheKomercijalist = false;
            }
            return cacheKomercijalist;
        }
        private Komercijalist GetKomercijalistFromDataRow(DataRow row)
        {
            return new Komercijalist
            {
                IDKomercijalist = (int)row["IDKomercijalist"],
                Ime = row["Ime"].ToString(),
                Prezime = row["Prezime"].ToString(),
                StalniZaposlenik = (bool)row["StalniZaposlenik"]
            };
        }
        //---------------------------------------------------------------------------------------------------------------------KreditnaKartica--------------------------------
        public KreditnaKartica GetKreditnaKartica(string brojKartice)
        {
            IEqualityComparer<KreditnaKartica> comparer = new KreditnaKarticaComparer();
            if (cacheKreditnaKartica.Values.Contains(new KreditnaKartica { Broj = brojKartice }, comparer))
            {
                return cacheKreditnaKartica.Values.First(x => x.Broj == brojKartice);
            }
            else
            {
                KreditnaKartica kartica = GetKreditnaKarticaFromDataRow(
                    SqlHelper.ExecuteDataset(ConnectionString, "proc_select_KreditnaKartica_broj", brojKartice).Tables[0].Rows[0]);
                Cache(kartica);
                return kartica;
            }
        }
        private class KreditnaKarticaComparer : IEqualityComparer<KreditnaKartica>
        {
            public bool Equals(KreditnaKartica x, KreditnaKartica y)
            {
                return x.Broj.Equals(y.Broj);
            }

            public int GetHashCode(KreditnaKartica obj)
            {
                throw new NotImplementedException();
            }
        }
        public KreditnaKartica GetKreditnaKartica(int idKreditnaKartica)
        {
            if (idKreditnaKartica == 0)
                return null;
            KreditnaKartica kartica;
            if (cacheKreditnaKartica.TryGetValue(idKreditnaKartica, out kartica))
            {
                return kartica;
            }
            else
            {
                kartica = GetKreditnaKarticaFromDataRow(
                    SqlHelper.ExecuteDataset(ConnectionString, "proc_select_KreditnaKartica_id", idKreditnaKartica).Tables[0].Rows[0]);
                Cache(kartica);
                return kartica;
            }
        }
        public SortedList<int, KreditnaKartica> GetMultipleKreditnaKartica()
        {
            if (cacheKreditnaKartica.Count == 0 && !recacheKreditnaKartica)
            {
                SortedList<int, KreditnaKartica> collection = new SortedList<int, KreditnaKartica>();
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_KreditnaKartica");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var KreditnaKartica = GetKreditnaKarticaFromDataRow(row);
                    collection[KreditnaKartica.IDKreditnaKartica] = KreditnaKartica;
                }
                cacheKreditnaKartica = collection;
                recacheKreditnaKartica = false;
            }
            return cacheKreditnaKartica;
        }
        private KreditnaKartica GetKreditnaKarticaFromDataRow(DataRow row)
        {
            return new KreditnaKartica
            {
                IDKreditnaKartica = (int)row["IDKreditnaKartica"],
                Broj = row["Broj"].ToString(),
                IstekGodina = (short)row["IstekGodina"],
                IstekMjesec = short.Parse(row["IstekMjesec"].ToString()),
                Tip = DetermineTipKreditnaKartica(row["Tip"].ToString())

            };
        }
        public static TipKreditnaKartica DetermineTipKreditnaKartica(string stringtip)
        {
            switch (stringtip)
            {
                case "American Express":
                    return TipKreditnaKartica.AmericanExpress;
                case "Diners":
                    return TipKreditnaKartica.Diners;
                case "MasterCard":
                    return TipKreditnaKartica.MasterCard;
                case "Visa":
                    return TipKreditnaKartica.Visa;
                default:
                    return TipKreditnaKartica.Other;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------Kupac------------------------------------------
        public Kupac GetKupac(int idKupac)
        {
            if (idKupac == 0)
                return null;
            Kupac kupac;
            if (cacheKupac.TryGetValue(idKupac, out kupac))
            {
                return kupac;
            }
            else
            {
                kupac = GetKupacFromDataRow(
                    SqlHelper.ExecuteDataset(ConnectionString, "proc_select_Kupac", idKupac).Tables[0].Rows[0]);
                Cache(kupac);
                return kupac;
            }
        }
        public SortedList<int, Kupac> GetMultipleKupac(uint take, uint skip = 0, KupacOrderBy order = KupacOrderBy.IDKupacAsc)
        {
            take = MaxCount(take);
            if (cacheKupac.Count == 0 && !recacheKupac)
            {
                SortedList<int, Kupac> collection = new SortedList<int, Kupac>();
                //Debug.Assert(KupacOrderBy.IDKupacAsc.ToString() != "IDKupacAsc", KupacOrderBy.IDKupacAsc.ToString());
                var kupac = KupacOrderBy.IDKupacAsc.ToString();
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_Kupac", order.ToString(), (int)skip, (int)take);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var Kupac = GetKupacFromDataRow(row);
                    collection[Kupac.IDKupac] = Kupac;
                }
                cacheKupac = collection;
                recacheKupac = false;
                return collection;
            }
            return Aggregate<Kupac>(cacheKupac.Skip((int)skip).Take((int)take));
        }
        public int UpdateKupac(Kupac Kupac)
        {
            int rows = SqlHelper.ExecuteNonQuery(
                    ConnectionString,
                    "proc_update_Kupac",
                    Kupac.IDKupac,
                    Kupac.Ime.WithMaxLength(50),
                    Kupac.Prezime.WithMaxLength(50),
                    Kupac.Email.WithMaxLength(50),
                    Kupac.Telefon.WithMaxLength(25),
                    Kupac.Grad.IDGrad
                );

            if (rows > 0)
            {
                Cache(Kupac);
                return rows;
            }
            return 0;
        }
        private Kupac GetKupacFromDataRow(DataRow row)
        {
            int result;
            var Grad = int.TryParse(row["GradID"].ToString(), out result) ? GetGrad(result) : GetGrad(0);
            return new Kupac
            {
                IDKupac = (int)row["IDKupac"],
                Ime = row["Ime"].ToString(),
                Prezime = row["Prezime"].ToString(),
                Email = row["Email"].ToString(),
                Telefon = row["Telefon"].ToString(),
                Grad = Grad
            };
        }
        //---------------------------------------------------------------------------------------------------------------------Potkategorija----------------------------------
        public int CreatePotkategorija(Potkategorija potkategorija)
        {
            int IDPotkategorija = int.Parse(SqlHelper.ExecuteScalar(ConnectionString, "proc_create_Potkategorija", potkategorija.Kategorija.IDKategorija, potkategorija.Naziv.WithMaxLength(50)).ToString());
            if (IDPotkategorija > 0)
            {
                potkategorija.IDPotkategorija = IDPotkategorija;
                Cache(potkategorija);
                return IDPotkategorija;
            }
            return 0;
        }
        public Potkategorija GetPotkategorija(int IDPotkategorija)
        {
            if (IDPotkategorija == 0)
                return null;
            Potkategorija Potkategorija;
            if (cachePotkategorija.TryGetValue(IDPotkategorija, out Potkategorija))
            {
                return Potkategorija;
            }
            else
            {
                Potkategorija = GetPotkategorijaFromDataRow(
                    SqlHelper.ExecuteDataset(ConnectionString, "proc_select_Potkategorija", IDPotkategorija).Tables[0].Rows[0]);
                Cache(Potkategorija);
                return Potkategorija;
            }
        }
        public SortedList<int, Potkategorija> GetMultiplePotkategorija()
        {
            if (cachePotkategorija.Count == 0 && !recachePotkategorija)
            {
                SortedList<int, Potkategorija> collection = new SortedList<int, Potkategorija>();
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_Potkategorija");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var Potkategorija = GetPotkategorijaFromDataRow(row);
                    collection[Potkategorija.IDPotkategorija] = Potkategorija;
                }
                cachePotkategorija = collection;
                recachePotkategorija = false;
            }
            return cachePotkategorija;
        }
        public SortedList<int, Potkategorija> GetMultiplePotkategorija(int kategorijaID)
        {
            if (cachePotkategorija.Count == 0 && !recachePotkategorija)
            {
                SortedList<int, Potkategorija> collection = new SortedList<int, Potkategorija>();
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_Potkategorija_targeted", kategorijaID);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var Potkategorija = GetPotkategorijaFromDataRow(row);
                    collection[Potkategorija.IDPotkategorija] = Potkategorija;
                }
                cachePotkategorija = collection;
                recachePotkategorija = false;
                return collection;
            }
            return Aggregate(cachePotkategorija.Where(x => x.Value.Kategorija.IDKategorija == kategorijaID));
        }
        public int UpdatePotkategorija(Potkategorija potkategorija)
        {
            int rows = SqlHelper.ExecuteNonQuery(
                    ConnectionString,
                    "proc_update_Potkategorija",
                    potkategorija.IDPotkategorija,
                    potkategorija.Kategorija.IDKategorija,
                    potkategorija.Naziv.WithMaxLength(50)
                );
            if (rows > 0)
            {
                Cache(potkategorija);
                return rows;
            }
            return 0;
        }
        public void DeletePotkategorija(int idPotkategorija)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "proc_delete_Potkategorija", idPotkategorija);
            cachePotkategorija.Remove(idPotkategorija);
        }
        private Potkategorija GetPotkategorijaFromDataRow(DataRow row)
        {
            int result;
            var Kategorija = int.TryParse(row["KategorijaID"].ToString(), out result) ? GetKategorija(result) : GetKategorija(0);
            return new Potkategorija
            {
                IDPotkategorija = (int)row["IDPotkategorija"],
                Naziv = row["Naziv"].ToString(),
                Kategorija = Kategorija
            };
        }
        //---------------------------------------------------------------------------------------------------------------------Proizvod---------------------------------------
        public int CreateProizvod(Proizvod Proizvod)
        {
            int IDProizvod = int.Parse(SqlHelper.ExecuteScalar(
                ConnectionString,
                "proc_create_Proizvod",
                Proizvod.Naziv.WithMaxLength(50),
                Proizvod.BrojProizvoda,
                Proizvod.Boja.ToString(),
                Proizvod.MinimalnaKolicinaNaSkladistu,
                Proizvod.CijenaBezPDV,
                Proizvod.Potkategorija.IDPotkategorija
            ).ToString());
            if (IDProizvod > 0)
            {
                Proizvod.IDProizvod = IDProizvod;
                Cache(Proizvod);
                return IDProizvod;
            }
            return 0;
        }
        public Proizvod GetProizvod(int IDProizvod)
        {
            if (IDProizvod == 0)
                return null;
            Proizvod Proizvod;
            if (cacheProizvod.TryGetValue(IDProizvod, out Proizvod))
            {
                return Proizvod;
            }
            else
            {
                Proizvod = GetProizvodFromDataRow(
                    SqlHelper.ExecuteDataset(ConnectionString, "proc_select_Proizvod", IDProizvod).Tables[0].Rows[0]);
                Cache(Proizvod);
                return Proizvod;
            }
        }
        public SortedList<int, Proizvod> GetMultipleProizvod(uint count, uint skip = 0)
        {
            if (cacheProizvod.Count == 0 && !recacheProizvod)
            {
                SortedList<int, Proizvod> collection = new SortedList<int, Proizvod>();
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_Proizvod", (int)skip, (int)count);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var Proizvod = GetProizvodFromDataRow(row);
                    collection[Proizvod.IDProizvod] = Proizvod;
                }
                cacheProizvod = collection;
                recacheProizvod = false;
            }
            return cacheProizvod;
        }
        public int UpdateProizvod(Proizvod Proizvod)
        {
            int rows = SqlHelper.ExecuteNonQuery(
                    ConnectionString,
                    "proc_update_Proizvod",
                    Proizvod.IDProizvod,
                    Proizvod.Naziv.WithMaxLength(50),
                    Proizvod.BrojProizvoda,
                    Proizvod.Boja.ToString(),
                    Proizvod.MinimalnaKolicinaNaSkladistu,
                    Proizvod.CijenaBezPDV,
                    Proizvod.Potkategorija.IDPotkategorija
                );
            if (rows > 0)
            {
                Cache(Proizvod);
                return rows;
            }
            return 0;
        }
        public void DeleteProizvod(int idProizvod)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "proc_delete_Proizvod", idProizvod);
            cacheProizvod.Remove(idProizvod);
        }
        private Proizvod GetProizvodFromDataRow(DataRow row)
        {
            int result;
            var Potkategorija = int.TryParse(row["PotkategorijaID"].ToString(), out result) ? GetPotkategorija(result) : GetPotkategorija(0);
            return new Proizvod
            {
                IDProizvod = (int)row["IDProizvod"],
                Naziv = row["Naziv"].ToString(),
                BrojProizvoda = row["BrojProizvoda"].ToString(),
                MinimalnaKolicinaNaSkladistu = (short)row["MinimalnaKolicinaNaSkladistu"],
                Boja = DetermineBojaProizvod(row["Boja"].ToString()),
                CijenaBezPDV = (decimal)row["CijenaBezPDV"],
                Potkategorija = Potkategorija
            };
        }
        public static Boja DetermineBojaProizvod(string stringtip)
        {
            switch (stringtip)
            {
                case "Bijela":
                    return Boja.Bijela;                case "Crna":
                    return Boja.Crna;                case "Crvena":
                    return Boja.Crvena;                case "Plava":
                    return Boja.Plava;                case "Siva":
                    return Boja.Siva;                case "Srebena":
                    return Boja.Srebrna;                case "Srebrna/Crna":
                    return Boja.SrebrnaCrna;                case "Šarena":
                    return Boja.Sarena;                case "Žuta":
                    return Boja.Zuta;                default:
                    return Boja.NoColor;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------Racun------------------------------------------
        public Racun GetRacun(int idRacun)
        {
            if (idRacun == 0)
                return null;
            Racun Racun;
            if (cacheRacun.TryGetValue(idRacun, out Racun))
            {
                return Racun;
            }
            else
            {
                Racun = GetRacunFromDataRow(
                    SqlHelper.ExecuteDataset(ConnectionString, "proc_select_Racun", idRacun).Tables[0].Rows[0]);
                Cache(Racun);
                return Racun;
            }
        }
        public SortedList<int, Racun> GetMultipleRacun()
        {
            if (cacheRacun.Count == 0 && !recacheRacun)
            {
                SortedList<int, Racun> collection = new SortedList<int, Racun>();
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_Racun");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var Racun = GetRacunFromDataRow(row);
                    collection[Racun.IDRacun] = Racun;
                }
                cacheRacun = collection;
                recacheRacun = false;
            }
            return cacheRacun;
        }
        public SortedList<int, Racun> GetMultipleRacun(int kupacID)
        {
            if (cacheRacun.Count == 0 && !recacheRacun)
            {
                SortedList<int, Racun> collection = new SortedList<int, Racun>();
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_Racun_targeted", kupacID);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var Racun = GetRacunFromDataRow(row);
                    collection[Racun.IDRacun] = Racun;
                }
                cacheRacun = collection;
                recacheRacun = false;
            }
            return cacheRacun;
        }
        private Racun GetRacunFromDataRow(DataRow row)
        {
            int result;
            var Komercijalist = int.TryParse(row["KomercijalistID"].ToString(), out result) ? GetKomercijalist(result) : GetKomercijalist(0);
            var KreditnaKartica = int.TryParse(row["KreditnaKarticaID"].ToString(), out result) ? GetKreditnaKartica(result) : GetKreditnaKartica(0);
            var Kupac = int.TryParse(row["KupacID"].ToString(), out result) ? GetKupac(result) : GetKupac(0);
            return new Racun
            {
                IDRacun = (int)row["IDRacun"],
                BrojRacuna = row["BrojRacuna"].ToString(),
                DatumIzdavanja = DateTime.Parse(row["DatumIzdavanja"].ToString()),
                Komentar = row["Komentar"].ToString(),
                Komercijalist = Komercijalist,
                KreditnaKartica = KreditnaKartica,
                Kupac = Kupac
            };
        }
        //---------------------------------------------------------------------------------------------------------------------Stavka-----------------------------------------
        public Stavka GetStavka(int idStavka)
        {
            if (idStavka == 0)
                return null;
            Stavka Stavka;
            if (cacheStavka.TryGetValue(idStavka, out Stavka))
            {
                return Stavka;
            }
            else
            {
                Stavka = GetStavkaFromDataRow(
                    SqlHelper.ExecuteDataset(ConnectionString, "proc_select_Stavka", idStavka).Tables[0].Rows[0]);
                Cache(Stavka);
                return Stavka;
            }
        }
        public SortedList<int, Stavka> GetMultipleStavka()
        {
            if (cacheStavka.Count == 0 && !recacheStavka)
            {
                SortedList<int, Stavka> collection = new SortedList<int, Stavka>();
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_Stavka");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var Stavka = GetStavkaFromDataRow(row);
                    collection[Stavka.IDStavka] = Stavka;
                }
                cacheStavka = collection;
                recacheStavka = false;
            }
            return cacheStavka;
        }
        public SortedList<int, Stavka> GetMultipleStavka(int idRacun)
        {
            if (cacheStavka.Count == 0 && !recacheStavka)
            {
                SortedList<int, Stavka> collection = new SortedList<int, Stavka>();
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_Stavka_targeted", idRacun);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var Stavka = GetStavkaFromDataRow(row);
                    collection[Stavka.IDStavka] = Stavka;
                }
                cacheStavka = collection;
                recacheStavka = false;
            }
            return cacheStavka;
        }
        private Stavka GetStavkaFromDataRow(DataRow row)
        {
            int result;
            var Proizvod = int.TryParse(row["ProizvodID"].ToString(), out result) ? GetProizvod(result) : GetProizvod(0);
            var Racun = int.TryParse(row["RacunID"].ToString(), out result) ? GetRacun(result) : GetRacun(0);
            return new Stavka
            {
                IDStavka = (int)row["IDStavka"],
                CijenaPoKomadu = (decimal)row["IDStavka"],
                Kolicina = (int)row["IDStavka"],
                PopustUPostocima = (int)row["IDStavka"],
                Proizvod = Proizvod,
                Racun = Racun,
                UkupnaCijena = (int)row["IDStavka"]
            };
        }
        //---------------------------------------------------------------------------------------------------------------------Helper-----------------------------------------
        private uint MaxCount(uint currentCount)
        {
            return currentCount > MAXTAKE ? MAXTAKE : currentCount;
        }
        private SortedList<int, T> Aggregate<T>(IEnumerable<KeyValuePair<int, T>> enumerable)
        {
            return enumerable.Aggregate(new SortedList<int, T>(), (x, y) => { x[y.Key] = y.Value; return x; });
        }
        public enum KupacOrderBy
        {
            IDKupacAsc,
            IDKupacDesc,
            ImeAsc,
            ImeDesc,
            PrezimeAsc,
            PrezimeDesc
        }
        //---------------------------------------------------------------------------------------------------------------------Cache------------------------------------------
        static Dictionary<Type, int> keyValuePairs = new Dictionary<Type, int>
        {
            { typeof(Drzava), 1 },
            { typeof(Grad), 2 },
            { typeof(Kategorija), 3 },
            { typeof(Komercijalist), 4 },
            { typeof(KreditnaKartica), 5 },
            { typeof(Kupac), 6 },
            { typeof(Potkategorija), 7 },
            { typeof(Proizvod), 8 },
            { typeof(Racun), 9 },
            { typeof(Stavka), 10 }
        };
        private void Cache(object item)
        {
            var intval = keyValuePairs[item.GetType()];
            switch (intval)
            {
                case 1:
                    var Drzava = item as Drzava; cacheDrzava[Drzava.IDDrzava] = Drzava; break;
                case 2:
                    var Grad = item as Grad; cacheGrad[Grad.IDGrad] = Grad; break;
                case 3:
                    var Kategorija = item as Kategorija; cacheKategorija[Kategorija.IDKategorija] = Kategorija; break;
                case 4:
                    var Komercijalist = item as Komercijalist; cacheKomercijalist[Komercijalist.IDKomercijalist] = Komercijalist; break;
                case 5:
                    var KreditnaKartica = item as KreditnaKartica; cacheKreditnaKartica[KreditnaKartica.IDKreditnaKartica] = KreditnaKartica; break;
                case 6:
                    var Kupac = item as Kupac;
                    cacheKupac[Kupac.IDKupac] = Kupac;
                    cacheKupacIme[Kupac.Ime] = Kupac;
                    cacheKupacPrezime[Kupac.Prezime] = Kupac;
                    break;
                case 7:
                    var Potkategorija = item as Potkategorija; cachePotkategorija[Potkategorija.IDPotkategorija] = Potkategorija; break;
                case 8:
                    var Proizvod = item as Proizvod; cacheProizvod[Proizvod.IDProizvod] = Proizvod; break;
                case 9:
                    var Racun = item as Racun; cacheRacun[Racun.IDRacun] = Racun; break;
                case 10:
                    var Stavka = item as Stavka; cacheStavka[Stavka.IDStavka] = Stavka; break;
                default:
                    return;

            }
        }
        public void CacheAll()
        {
            recacheDrzava = true;
            recacheGrad = true;
            recacheKategorija = true;
            recacheKomercijalist = true;
            recacheKreditnaKartica = true;
            recacheKupac = true;
            recachePotkategorija = true;
            recacheProizvod = true;
            recacheRacun = true;
            recacheStavka = true;

            GetMultipleDrzava();
            GetMultipleGrad();
            GetMultipleKategorija();
            GetMultipleKomercijalist();
            GetMultipleKreditnaKartica();
        }
    }
}
