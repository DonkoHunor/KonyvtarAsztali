using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvtarAsztali
{
	internal class Connection
	{
		private MySqlConnection connection;

		public Connection() 
		{
			MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
			builder.Port = 3306;
			builder.Server = "localhost";
			builder.UserID = "root";
			builder.Password = "";
			builder.Database = "vizsga-2022-14s-wip-db";
			connection = new MySqlConnection(builder.ConnectionString);
		}

		public List<Konyv> GetAll()
		{
			List<Konyv> result = new List<Konyv>();
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
					Konyv temp = new Konyv(id, title, author, publish_year, page_count);
					result.Add(temp);
				}
			}
			connection.Close();
			return result;
		}

		public void Delete(Konyv selected)
		{
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = "DELETE FROM books WHERE id=@id";
			command.Parameters.AddWithValue("id", selected.Id);
			command.ExecuteNonQuery();
			connection.Close();
		}
	}
}
