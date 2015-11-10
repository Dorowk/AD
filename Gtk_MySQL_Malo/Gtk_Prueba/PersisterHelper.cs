using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Gtk_Prueba
{
	public class PersisterHelper
	{
		public static QuerryResult Get(string selectText)
		{
			//IDbConnection dbConnection = App.Instance.DBConnection;
			//IDbCommand dbCommand = dbConnection.CreateCommand ();
			//dbCommand.CommandText = selectText; 
				
			QuerryResult queryResult = new QuerryResult();
			queryResult.Columnas = new string[] { "Columna 1", "Columna 2" };
			List<IList> rows = new List<IList> ();
			rows.Add (new object[] { 1, "art. uno" });
			rows.Add (new object[] { 2, "art. dos" });

			queryResult.Rows = rows;

			return queryResult;
		}

		private static string[] getColumnNames(IDataReader dataReader) {
			List<string> columnNames = new List<string> ();
			int count = dataReader.FieldCount;
			for (int index = 0; index < count; index++)
				columnNames.Add (dataReader.GetName (index));
			return columnNames.ToArray ();
		}

		private static IList getRow(IDataReader dataReader) {
			List<object> values = new List<object> ();
			int count = dataReader.FieldCount;
			for (int index = 0; index < count; index++)
				values.Add (dataReader [index]);
			return values;
		}
	}
}

