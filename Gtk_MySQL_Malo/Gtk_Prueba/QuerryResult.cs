using System;
using System.Collections;
using System.Collections.Generic;

namespace Gtk_Prueba
{
	public class QuerryResult{

		private string[] colum;
		public string[] Columnas {
			get{return colum;}
			set{ colum = value;}
		}
		private IEnumerable<IList> rows;
		public IEnumerable<IList> Rows {
			get{ return rows;}
			set{ rows = value;}
		}
	}
}

