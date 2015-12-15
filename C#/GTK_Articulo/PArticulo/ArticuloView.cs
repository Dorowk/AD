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

		private Articulo articulo;

		public ArticuloView ():base(Gtk.WindowType.Toplevel)
		{
			articulo = new Articulo();
			init ();
			saveAction.Activated +=delegate {insert();}
	
		}
		public ArticuloView(object id):base(Gtk.WindowType.Toplevel)
		{
			articulo = ArticuloPersister.Load(id);
			init ();
			saveAction.Activated +=delegate {update();} 
		}

		private void init(){
			this.Build ();
			entryNombre.Text = articulo.Nombre;
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult,articulo.Categoria);
			spinButtonPrecio.Value = Convert.ToDouble (articulo.Precio);
			saveAction.Activated += delegate {	save();	};
		}

		private void updateModel(){
			articulo.Nombre = entryNombre.Text;
			articulo.Categoria = ComboBoxHelper.GetId (comboBoxCategoria);
			articulo.Precio = Convert.ToDecimal(spinButtonPrecio.Value);

		}
	


		private void insert() {
			updateModel();
			ArticuloPersister.Insert (articulo);
			Destroy ();
		}

		private void update(){
			updateModel();
			ArticuloPersister.Update(articulo);
			Destroy ();
		}
	}
}

