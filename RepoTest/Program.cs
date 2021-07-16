using AdventureWorksOBPRepo;
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
            var kreditnakartica = repo.GetKreditnaKarticaID(kreditnekartice.First().Key);
            var kreditnakarticabroj = repo.GetKreditnaKarticaBroj(kreditnakartica.Broj);
            // RU Kupac             
            var kupci = repo.GetMultipleKupac(50);
            var kupac = repo.GetKupac(kupci.First().Key);
            kupac = new Kupac { Ime = "Luka", Prezime = "Cabraja", Email = "luka@cabraja.eu", Grad = grad, Telefon = "0999999999" };
            var createkupac = repo.UpdateKupac(kupac);
            // CRUD Potkategorija   
            var potkategorije = repo.GetMultiplePotkategorija();
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
            var racuni = repo.GetMultipleRacun();
            var racun = repo.GetRacun(racuni[0].IDRacun);
            // R Stavka             
            var stavka = repo.GetMultipleStavka();
            var stavke = repo.GetStavka(racun.IDRacun);
            var hold = "the door";
        }
    }
}
