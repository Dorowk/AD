using System;
using Gtk;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Console.WriteLine ("MainWindow ctor.");
		MySqlConnection connection = new MySqlConnection (
			"Database=dbprueba;Data Source=localhost;User Id=root; Password=sistemas"
			);
		connection.Open ();
		MySqlCommand command = connection.CreateCommand ();
		command.CommandText = "select * from articulo";

		MySqlDataReader reader = command.ExecuteReader ();

		Type[] types = new Type[reader.FieldCount];


		for (int i =0; i< reader.FieldCount; i++) {
			treeview.AppendColumn (reader.GetName (i), new CellRendererText (), "text", i);
			types [i] = typeof(String);
		}
		ListStore list = new ListStore (types);

		string[] listaNombres = new string[reader.FieldCount] ;

		while (reader.Read()){
			for (int i =0; i< reader.FieldCount; i++) {
				listaNombres[i]=reader [reader.GetName(i)].ToString() ;
			};
			list.AppendValues(listaNombres);
		}

		treeview.Model = list;


		connection.Close ();		

	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}