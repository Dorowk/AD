using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Gtk;

using Gtk_Prueba;


public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		QuerryResult result = PersisterHelper.Get ("select * from articulo");


		Console.WriteLine ("MainWindow ctor.");

		IDbConnection connection = App.Instance.DBConnection;
		IDbCommand command = connection.CreateCommand ();
		command.CommandText = "select * from articulo";

		IDataReader reader = command.ExecuteReader ();

		string[] nombres = GetColumnas (reader);
		CellRendererText cellText = new CellRendererText ();
		for (int i =0; i < nombres.Length; i++) {
			int columna = i;
			treeview.AppendColumn (nombres[i], cellText,
			       delegate(TreeViewColumn tree_colum, CellRenderer cell, TreeModel tree_model, TreeIter iter){
						IList row =(IList)tree_model.GetValue(iter,0);
						cellText.Text = row[columna].ToString();
			});
		}
		ListStore list = new ListStore (typeof(IList));

		while (reader.Read()) {
			IList values = getValues (reader);
			list.AppendValues (values);
		}

		/*
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
		*/

		connection.Close ();		
	
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	private string[] GetColumnas(IDataReader reader){
		string[] nombres = new string[reader.FieldCount];
		for (int i =0; i< nombres.Length ; i++){
			nombres[i]= reader.GetName(i);
		}
		return nombres;
	}
	private IList getValues(IDataReader dataReader) {
		List<object> values = new List<object> ();
		int count = dataReader.FieldCount;
		for (int index = 0; index < count; index++)
			values.Add (dataReader [index]);
		return values;
	}

}
