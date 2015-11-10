
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Gtk_Prueba
{
	public class App
	{
		private App(){
		}

		private IDbConnection connection;
		public IDbConnection DBConnection {
			get { 
				if (connection == null) {
					connection = new MySqlConnection (
						"Database=dbprueba;Data Source=localhost;User Id=root; Password=sistemas"
					);
					connection.Open ();
				}
				return connection;
			
			}
		}

		private static App instance = new App();
		public static App Instance {
			get { return instance;}
		}
	}
}