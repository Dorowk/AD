using System;
using Gtk;
using GTK_Serpis;
using System.Data;
using GTK_Categoria;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Title = "Categoria";

		FillTreeView ();

		refreshAction.Activated += delegate{FillTreeView();};

		newAction.Activated += delegate {new CategoriaView();};

		removeAction.Activated += delegate {
			object id = TreeViewHelper.GetID(treeView);
			BorrarLinea(id);
		};

		editAction.Activated += delegate {
			object id = TreeViewHelper.GetID(treeView);
			new CategoriaView(id);

		};

		treeView.Selection.Changed += delegate {
			bool selected = TreeViewHelper.IsSelected(treeView);
			removeAction.Sensitive = selected;
			editAction.Sensitive = selected;
		};

		removeAction.Sensitive = false;
		editAction.Sensitive = false;


	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void FillTreeView(){
		QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
		TreeViewHelper.Fill (treeView, queryResult);
	}

	private void BorrarLinea (object id){
		if (WindowHelper.ConfirmDelete (this)){
			IDbCommand command = App.Instance.DbConnection.CreateCommand ();
			string sentencia = string.Format ("delete from categoria where id={0}", id);
			command.CommandText = sentencia;
			command.ExecuteNonQuery();
			FillTreeView ();

		};
	}

}
