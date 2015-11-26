using System;
using GTK_Serpis;
using System.Data;

namespace GTK_Categoria
{
	public delegate void SaveDelegate();
	public partial class CategoriaView : Gtk.Window
	{
		private object id;
		private string nombre="";
		private SaveDelegate save;

		public CategoriaView () : base(Gtk.WindowType.Toplevel)
		{
			init();
			save = insert;
		}

		public CategoriaView (object id) : base(Gtk.WindowType.Toplevel)
		{
			this.id = id;
			load();
			init();
			save = update;
		}
		private void init(){
			this.Build ();
			entryNombre.Text = nombre;
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			saveAction.Activated += delegate {	save();	};
		}

		private void update(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "update categoria set nombre=@nombre where id=@id";
			nombre = entryNombre.Text;
			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}

		private void load(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from categoria where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader datareader = dbCommand.ExecuteReader();
			if (!datareader.Read ())
				;
			nombre = (string)datareader ["nombre"];
			datareader.Close ();

		}
		private void insert() {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into categoria (nombre) values (@nombre)";
			nombre = entryNombre.Text;
			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}
	}
}



