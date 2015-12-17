import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;

public class Connectar {
	public static void main(String arg []) throws SQLException{
		Connection connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/dbprueba","root","sistemas");
		Statement stat = connection.createStatement();
		ResultSet resulset = stat.executeQuery("Select * from Articulo");
		ArrayList<String> nombres = new ArrayList<String>();
		ResultSetMetaData meta = resulset.getMetaData();
		
		int colum=meta.getColumnCount();
		
		for(int i = 1; i<=colum;i++){
			nombres.add(meta.getColumnName(i));
		}
		
		while(resulset.next()){
			ArrayList<Object> lista = new ArrayList<Object>();
			for(int i = 0; i<colum;i++){
				lista.add(resulset.getObject(nombres.get(i)));
			}
			for(int i = 0; i<colum;i++){
			System.out.print(nombres.get(i)+" : "+lista.get(i).toString()+"\t");
			}
			System.out.println();
		}
				
		resulset.close();		
		connection.close();
		System.out.println("Fin");
	}

}
