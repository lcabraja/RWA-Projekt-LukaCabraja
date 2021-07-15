using System;
using System.Collections.Generic;
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
        }

        // R Drzava
        // R Grad
        // CRUD Kategorija
        // R Komercijalist
        // R KreditnaKartica
        // RU Kupac
        // CRUD Potkategorija
        // CRUD Proizvod
        // R Racun
        // R Stavka

        public static int CreateKupac(Author author)
        {
            int IDAuthor = int.Parse(SqlHelper.ExecuteScalar(ConnectionString, "proc_create_Author", author.Username, author.PasswordHash, author.Email).ToString());
            if (IDAuthor > 0)
            {
                Add(author);
                return IDAuthor;
            }
            throw new SQLInsertException("Could not insert values into table \"Author\"");
        }
    }
}
