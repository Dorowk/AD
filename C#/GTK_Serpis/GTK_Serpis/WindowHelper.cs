using System;
using Gtk;

namespace GTK_Serpis
{
	public class WindowHelper{
		public static bool ConfirmDelete(Window window){
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
}