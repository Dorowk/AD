using Gtk;
using System;
using System.Collections;

namespace GTK_Serpis
{
	public class TreeViewHelper
	{
		public static void Fill(TreeView treeView, QueryResult queryResult) {
			removeAllColumns (treeView);
			string[] columnNames = queryResult.ColumnNames;
			CellRendererText cellRendererText = new CellRendererText ();
			for (int index = 0; index < columnNames.Length; index++) {
				int column = index;
				treeView.AppendColumn (columnNames [index], cellRendererText, 
				                       delegate(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
					IList row = (IList)tree_model.GetValue(iter, 0);
					cellRendererText.Text = row[column].ToString();
				});
			}
			ListStore listStore = new ListStore (typeof(IList));
			foreach (IList row in queryResult.Rows)
				listStore.AppendValues (row);
			treeView.Model = listStore;
		}

		private static void removeAllColumns (TreeView treeView){
			TreeViewColumn[] columnas = treeView.Columns;
			for (int i = 0; i < columnas.Length;i++)
				treeView.RemoveColumn (columnas[i]);

		}

		public static object GetID(TreeView treeview){
			TreeIter treeIter;
			if (!treeview.Selection.GetSelected (out treeIter))
				return null;
			IList row = (IList)treeview.Model.GetValue (treeIter, 0);
			return row [0];
		}
		public static bool IsSelected (TreeView treeView){
			TreeIter treeIter;
			return treeView.Selection.GetSelected (out treeIter);

		}
	}
}


