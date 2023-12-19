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
        private Connection connection = new Connection();

        public Statisztika() 
        {
			connection.GetAll();
		}

        public void Run()
        {
            Hosz();
            Van();
            Leghosszab();
            Szerzo();
			Cim();
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

		public void Cim()
		{

		}
	}
}
