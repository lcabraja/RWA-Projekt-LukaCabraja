using AdventureWorksOBPRepo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoTest
{
    class Program
    {
        const string localhost = "Server=.;Database=AdventureWorksOBP;Uid=SA;Pwd=SQL";
        const string cloud = "Server=195.201.100.7;Database=AdventureWorksOBP;Uid=SA;Pwd=Pa$$word";
        static void Main(string[] args)
        {
            //CRUDTest();
            SerializationTest();
            var hold = "the door";
        }

        private static void SerializationTest()
        {

            Repo repo = new Repo(localhost);
            var drzave = repo.GetMultipleDrzava();
            string drzava = JsonConvert.SerializeObject(drzave.First().Value);
            var drzavaback = JsonConvert.DeserializeObject(drzava, typeof(Drzava));

            var gradovi = repo.GetMultipleGrad();
            string grad = JsonConvert.SerializeObject(gradovi.First().Value);
            var gradback = JsonConvert.DeserializeObject(grad, typeof(Grad));

            var kategorije = repo.GetMultipleKategorija();
            string kategorija = JsonConvert.SerializeObject(kategorije.First().Value);
            var kategorijaback = JsonConvert.DeserializeObject(kategorija, typeof(Kategorija));

            var komercijalisti = repo.GetMultipleKomercijalist();
            string komercijalist = JsonConvert.SerializeObject(komercijalisti.First().Value);
            var komercijalistback = JsonConvert.DeserializeObject(komercijalist, typeof(Komercijalist));

            var kreditnekartice = repo.GetMultipleKreditnaKartica();
            string kreditnakartica = JsonConvert.SerializeObject(kreditnekartice.First().Value);
            var kreditnakarticaback = JsonConvert.DeserializeObject(kreditnakartica, typeof(KreditnaKartica));

            var kupci = repo.GetMultipleKupac(30);
            string kupac = JsonConvert.SerializeObject(kupci.First().Value);
            var kupacback = JsonConvert.DeserializeObject(kupac, typeof(Kupac));

            var potkategorije = repo.GetMultiplePotkategorija();
            string potkategorija = JsonConvert.SerializeObject(potkategorije.First().Value);
            var potkategorijaback = JsonConvert.DeserializeObject(potkategorija, typeof(Potkategorija));

            var proizvodi = repo.GetMultipleProizvod(30);
            string proizvod = JsonConvert.SerializeObject(proizvodi.First().Value);
            var proizvodback = JsonConvert.DeserializeObject(proizvod, typeof(Proizvod));

            var racuni = repo.GetMultipleRacun();
            string racun = JsonConvert.SerializeObject(racuni.First().Value);
            var racunback = JsonConvert.DeserializeObject(racun, typeof(Racun));

            var stavke = repo.GetMultipleStavka();
            string stavka = JsonConvert.SerializeObject(stavke.First().Value);
            var stavkaback = JsonConvert.DeserializeObject(stavka, typeof(Stavka));
        }

        private static void CRUDTest()
        {
            Repo repo = new Repo(localhost);
            //Repo repo = new Repo("Server=/;Database=AdventureWorksOBP;UserId=SA;Password=SQL;");
            // R Drzava                                                                                        R Drzava     OK
            var drzave = repo.GetMultipleDrzava();
            var drzava = repo.GetDrzava(drzave.First().Key);
            // R Grad                                                                                            R Grad     OK
            var gradovi = repo.GetMultipleGrad();
            var gradovi2 = repo.GetMultipleGrad(drzava.IDDrzava);
            var grad = repo.GetGrad(gradovi2.First().Key);
            // CRUD Kategorija                                                                          CRUD Kategorija     OK
            var kategorije = repo.GetMultipleKategorija();
            var kategorija = repo.GetKategorija(kategorije.First().Key);
            var newkat = new Kategorija { Naziv = "Strujni transformator" };
            var create = repo.CreateKategorija(newkat);
            newkat.IDKategorija = create;
            newkat.Naziv = "Koncar transformatori";
            var updatekat = repo.UpdateKategorija(newkat);
            repo.DeleteKategorija(newkat.IDKategorija);
            // R Komercijalist      
            var komercijalisti = repo.GetMultipleKomercijalist();
            var komercijalist = repo.GetKomercijalist(komercijalisti.First().Key);
            // R KreditnaKartica    
            var kreditnekartice = repo.GetMultipleKreditnaKartica();
            var kreditnakartica = repo.GetKreditnaKartica(kreditnekartice.First().Key);
            var kreditnakarticabroj = repo.GetKreditnaKartica(kreditnakartica.Broj);
            // RU Kupac             
            var kupci = repo.GetMultipleKupac(50);
            var kupac = repo.GetKupac(kupci.First().Key);
            kupac = new Kupac { Ime = "Luka", Prezime = "Cabraja", Email = "luka@cabraja.eu", Grad = grad, Telefon = "0999999999", IDKupac = kupac.IDKupac };
            var createkupac = repo.UpdateKupac(kupac);
            // CRUD Potkategorija   
            var potkategorije = repo.GetMultiplePotkategorija();
            var potkategorije2 = repo.GetMultiplePotkategorija(kategorija.IDKategorija);
            var potkategorija = repo.GetPotkategorija(potkategorije.First().Key);
            var newpot = new Potkategorija { Kategorija = kategorija, Naziv = "kiflice" };
            var createpot = repo.CreatePotkategorija(newpot);
            newpot.IDPotkategorija = createpot;
            newpot.Naziv = "strudlice";
            var update = repo.UpdatePotkategorija(newpot);
            repo.DeletePotkategorija(newpot.IDPotkategorija);
            // CRUD Proizvod        
            var proizvodi = repo.GetMultipleProizvod(50);
            var proizvod = repo.GetProizvod(proizvodi.Last().Key);
            var newproizvod = new Proizvod { Naziv = "Kroasan", Boja = Boja.Zuta, BrojProizvoda = "123kroasan", CijenaBezPDV = (decimal)25.01, Potkategorija = potkategorija, MinimalnaKolicinaNaSkladistu = 10 };
            var createproizvod = repo.CreateProizvod(newproizvod);
            newproizvod.IDProizvod = createproizvod;
            newproizvod.Naziv = "zaobljena mini bageta";
            var updateproizvod = repo.UpdateProizvod(newproizvod);
            repo.DeleteProizvod(newproizvod.IDProizvod);
            // R Racun              
            //var racuni = repo.GetMultipleRacun();
            var racuni2 = repo.GetMultipleRacun(kupac.IDKupac);
            var racun = repo.GetRacun(racuni2.First().Value.IDRacun);
            // R Stavka             
            //var stavka = repo.GetMultipleStavka();
            var stavka2 = repo.GetMultipleStavka(racun.IDRacun);
            var stavke = repo.GetStavka(racun.IDRacun);
        }
    }
}
