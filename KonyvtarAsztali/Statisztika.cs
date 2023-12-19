using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvtarAsztali
{
    class Statisztika
    {
        public List<Konyv> konyvek = new Konyv().GetKonyvek();
		private MySqlConnection connection;

        public Statisztika() 
        {
			GetAll();
		}

        public void GetAll()
        {
			MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
			builder.Port = 3306;
			builder.Server = "localhost";
			builder.UserID = "root";
			builder.Password = "";
			builder.Database = "vizsga-2022-14s-wip-db";
			connection = new MySqlConnection(builder.ConnectionString);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT id, title, author, publish_year, page_count FROM books";
			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					int id = reader.GetInt32("id");
					string title = reader.GetString("title");
					string author = reader.GetString("author");
					int publish_year = reader.GetInt32("publish_year");
					int page_count = reader.GetInt32("page_count");
					new Konyv(id, title, author, publish_year, page_count);
				}
			}
			connection.Close();
		}

        public void Run()
        {
            Hosz();
            Van();
            Leghosszab();
            Szerzo();
        }

        public void Hosz()
        {
            int db = 0;
            foreach (var item in konyvek)
            {
                if(item.Page_count > 500)
                {
                    db++;
                }
            }
            Console.WriteLine("500 oldalnál hosszab könyvek száma: " + db);
        }

        public void Van()
        {
            bool van = false;
			foreach (var item in konyvek)
			{
				if (item.Publish_year < 1950)
				{
                    van = true;
				}
			}
            if(van)
            {
                Console.WriteLine("Van 1950-nél régebbi könyv");
            }
            else
            {
				Console.WriteLine("Nincs 1950-nél régebbi könyv");
			}
		}
        
        public void Leghosszab()
        {
            int max = int.MinValue;
            Konyv eredmeny = new Konyv();
			foreach (var item in konyvek)
			{
				if (item.Page_count > max)
				{
					max = item.Page_count;
                    eredmeny = item;
				}
			}
            Console.WriteLine("A leghosszab köny:\n" +
                $"\tSzerző: {eredmeny.Author}\n" +
                $"\tCím: {eredmeny.Title}\n" +
                $"\tKiadás éve: {eredmeny.Publish_year}\n" +
                $"\tOldalszám: {eredmeny.Page_count}\n");
		}

        public void Szerzo()
        {
            Dictionary<string,int> szerzok = new Dictionary<string,int>();

			foreach (var item in konyvek)
			{
				if (!szerzok.ContainsKey(item.Author))
				{
					szerzok.Add(item.Author, 0);
				}
			}

			foreach (var item in konyvek)
			{
                szerzok[item.Author] += 1;
			}


            int max = int.MinValue;
            string author = "";
			foreach (var item in szerzok)
			{
				if(item.Value > max)
                {
                    max = item.Value;
                    author = item.Key;
                }
			}

            Console.WriteLine("A legtöbb könyvvel rendelkező szerző: " + author);
		}

		public void Delete(Konyv selected)
		{
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = "DELETE FROM books WHERE id=@id";
			command.Parameters.AddWithValue("id",selected.Id);
			command.ExecuteNonQuery();
			connection.Close();
		}
	}
}
