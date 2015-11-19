using Gtk;
using System;
using System.Collections;
using System.Data;
using GTK_Serpis;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{	
		private object id;
		private string nombre="";
		private decimal precio =0;
		private object categoria = null;

		public ArticuloView ():base(Gtk.WindowType.Toplevel)
		{
			init ();
	
		}
		public ArticuloView(object id):base(Gtk.WindowType.Toplevel){
			this.id = id;
			load ();
			init ();
		}

		private void init(){
			this.Build ();
			entryNombre.Text = nombre;
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult,categoria);
			spinButtonPrecio.Value = Convert.ToDouble (precio);
			saveAction.Activated += delegate {	save();	};


		}
		private void load(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader datareader = dbCommand.ExecuteReader();
			if (!datareader.Read ())
				;
			nombre = (string)datareader ["nombre"];
			object categoria = datareader ["categoria"];
			if (comboBoxCategoria is DBNull)
				categoria = null;
			precio = (decimal)datareader ["precio"];
			datareader.Close ();


		}

		private void save() {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";

			nombre = entryNombre.Text;
			categoria = ComboBoxHelper.GetId (comboBoxCategoria);
			precio = Convert.ToDecimal(spinButtonPrecio.Value);

			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", precio);
			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}
	}
}

