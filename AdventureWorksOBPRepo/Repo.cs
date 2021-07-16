﻿using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksOBPRepo
{
    public class Repo
    {
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
        // R Komercijalist
        // R KreditnaKartica
        // RU Kupac
        // CRUD Potkategorija
        // CRUD Proizvod
        // R Racun
        // R Stavka

        //-------------------------------------------------------------------------------------Drzava----------------------------------------------
        public Drzava GetDrzava(int IDDrzava)
        {
            if (cacheDrzava.ContainsKey(IDDrzava))
            {
                return cacheDrzava[IDDrzava];
            }
            if (recacheDrzava)
            {
                GetMultipleDrzava();
            }
            return null;
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
        //-------------------------------------------------------------------------------------Grad----------------------------------------------
        public Grad GetGrad(int IDGrad) =>
            cacheGrad?[IDGrad] ?? null;
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
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, "proc_select_multiple_Grad");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var Grad = GetGradFromDataRow(row);
                    collection[Grad.IDGrad] = Grad;
                }
                cacheGrad = collection;
                recacheGrad = false;
            }

            return cacheGrad.Where(x => x.Value.Drzava.IDDrzava == drzavaID)
                            .Aggregate(new SortedList<int, Grad>(), (x, y) => { x[y.Key] = y.Value; return x; }, (x) => { return x; });
        }
        private Grad GetGradFromDataRow(DataRow row)
        {
            return new Grad
            {
                IDGrad = (int)row["IDGrad"],
                Naziv = row["Naziv"].ToString(),
                Drzava = GetDrzava((int)row["DrzavaID"])
            };
        }
        //-------------------------------------------------------------------------------------Kategorija----------------------------------------------
        public int CreateKategorija(Kategorija kategorija)
        {
            int IDKategorija = int.Parse(SqlHelper.ExecuteScalar(ConnectionString, "proc_create_Kategorija", kategorija.Naziv.Substring(0, 50)).ToString());
            if (IDKategorija > 0)
            {
                kategorija.IDKategorija = IDKategorija;
                Cache(kategorija);
                return IDKategorija;
            }
            return 0;
        }
        public Kategorija GetKategorija(int IDKategorija) =>
           cacheKategorija[IDKategorija];
        public SortedList<int, Kategorija> GetMultipleKategorija()
        {
            if (cacheDrzava.Count == 0 && !recacheKategorija)
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
            int IDKategorija = int.Parse(SqlHelper.ExecuteScalar(
                    ConnectionString,
                    "proc_update_Kategorija",
                    kategorija.IDKategorija,
                    kategorija.Naziv.Substring(0, 50)
                ).ToString());

            if (IDKategorija > 0)
            {
                Cache(kategorija);
                return IDKategorija;
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
        //-------------------------------------------------------------------------------------Komercijalist----------------------------------------------
        public Komercijalist GetKomercijalist(int idKomercijalist) =>
            cacheKomercijalist?[idKomercijalist] ?? null;
        public SortedList<int, Komercijalist> GetMultipleKomercijalist()
        {
            if (cacheDrzava.Count == 0 && !recacheKomercijalist)
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
        //-------------------------------------------------------------------------------------KreditnaKartica----------------------------------------------
        public KreditnaKartica GetKreditnaKarticaBroj(string brojKartice)
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
        public KreditnaKartica GetKreditnaKarticaID(int idKreditnaKartica)
        {
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
            if (cacheDrzava.Count == 0 && !recacheKreditnaKartica)
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
                IstekMjesec = (short)row["IstekGodina"],
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
        //-------------------------------------------------------------------------------------Cache----------------------------------------------
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
                    var Kupac = item as Kupac; cacheKupac[Kupac.IDKupac] = Kupac; break;
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
