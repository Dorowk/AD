using System;
using MySql.Data.MySqlClient;

namespace PMySql
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			MySqlConnection mySql = new MySqlConnection (
				"Database=dbprueba;Data Source=localhost;User Id=root; Password=sistemas"
			);
			mySql.Open ();

			MySqlCommand comand = mySql.CreateCommand ();
			comand.CommandText = "select * from articulo";
			MySqlDataReader reader = comand.ExecuteReader ();

			showColumnNames (reader);
			Console.WriteLine (" ");
			show (reader);
			reader.Close ();

			mySql.Close();
		}

		private static void showColumnNames (MySqlDataReader reader){
			for (int i =0; i< reader.FieldCount; i++)
				Console.WriteLine ("Columna {0} {1}", i, reader.GetName(i));
		}

		private static void show (MySqlDataReader reader){
			while (reader.Read()){
				for (int i =0; i< reader.FieldCount; i++) {
					Console.Write ("{0} = {1} ", reader.GetName (i), reader [reader.GetName (i)]);
				}
				Console.WriteLine (" ");
			}

		}

	}
}
