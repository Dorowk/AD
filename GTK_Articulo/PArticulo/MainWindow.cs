using System;
using Gtk;

using GTK_Serpis;
using PArticulo;
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

		refreshAction.Activated += delegate{
			FillTreeView();
		};

		newAction.Activated += delegate {
			new ArticuloView();
		};

		removeAction.Activated += delegate {
			object id = TreeViewHelper.GetID(treeView);
			BorrarLinea(id);
		};

		treeView.Selection.Changed += delegate {
			removeAction.Sensitive = TreeViewHelper.IsSelected(treeView);
		};

		removeAction.Sensitive = false;

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
		if (ConfirmDelete (this)){
			IDbCommand command = App.Instance.DbConnection.CreateCommand ();
			string sentencia = string.Format ("delete from articulo where id={0}", id);
			command.CommandText = sentencia;
			command.ExecuteNonQuery();
			FillTreeView ();
		};
	}
	
	public bool ConfirmDelete(Window window){
		MessageDialog messageDialog = new MessageDialog (
			window,
			DialogFlags.DestroyWithParent,
			MessageType.Question,
			ButtonsType.YesNo,
			"¿Quieres eliminar el elemento selecionado?");
		messageDialog.Title = window.Title;
		ResponseType response = (ResponseType)messageDialog.Run ();
		messageDialog.Destroy ();
		return response == ResponseType.Yes;
	}



}
