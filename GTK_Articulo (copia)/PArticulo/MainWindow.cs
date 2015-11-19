using System;
using Gtk;
using PArticulo;
using GTK_Serpis;
using System.Collections;
using System.Data;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Title = "Articulo";
		Console.WriteLine ("MainWindow ctor.");
		FillTreeView ();

		refreshAction.Activated += delegate{FillTreeView();};

		newAction.Activated += delegate {new ArticuloView();};

		removeAction.Activated += delegate {
			object id = TreeViewHelper.GetID(treeView);
			BorrarLinea(id);
		};

		editAction.Activated += delegate {
			object id = TreeViewHelper.GetID(treeView);
			new ArticuloView(id);

	};

		treeView.Selection.Changed += delegate {
			bool selected = TreeViewHelper.IsSelected(treeView);
			removeAction.Sensitive = selected;
		   	editAction.Sensitive = selected;
		};

		removeAction.Sensitive = false;
		editAction.Sensitive = false;




	}

	protected void FillTreeView(){
		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (treeView, queryResult);
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	private void BorrarLinea (object id){
		if (WindowHelper.ConfirmDelete (this)){
			IDbCommand command = App.Instance.DbConnection.CreateCommand ();
			string sentencia = string.Format ("delete from articulo where id={0}", id);
			command.CommandText = sentencia;
			command.ExecuteNonQuery();
			FillTreeView ();

		};
	}
	
}
