using Gtk;
using System;
using System.Collections;
using System.Data;
using GTK_Serpis;

namespace PArticulo
{	
	public delegate void SaveDelegate();

	public partial class ArticuloView : Gtk.Window	
	{	
		private Articulo articulo = new Articulo();
		private object id;
		private object categoria = null;
		private SaveDelegate save;

		public ArticuloView ():base(Gtk.WindowType.Toplevel)
		{
			articulo.Nombre = "";
			init ();
			save = insert;
	
		}
		public ArticuloView(object id):base(Gtk.WindowType.Toplevel)
		{
			this.id = id;
			articulo.Nombre = "";
			init ();
			save = update;
		}

		private void init(){
			this.Build ();
			entryNombre.Text = articulo.Nombre;
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult,articulo.Categoria);
			spinButtonPrecio.Value = Convert.ToDouble (articulo.Precio);
			saveAction.Activated += delegate {	save();	};
		}
	


		private void insert() {
			articulo.Nombre = entryNombre.Text;
			articulo.Categoria = ComboBoxHelper.GetId (comboBoxCategoria);
			articulo.Precio = Convert.ToDecimal(spinButtonPrecio.Value);

			ArticuloPersister.Insert (articulo);

			Destroy ();
		}

		private void update(){
			articulo.Nombre = entryNombre.Text;
			articulo.Categoria = ComboBoxHelper.GetId (comboBoxCategoria);
			articulo.Precio = Convert.ToDecimal(spinButtonPrecio.Value);
			articulo.Id = id;	

			ArticuloPersister.Insert (articulo);

			Destroy ();
		}
	}
}

