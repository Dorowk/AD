import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class Connectar {
	public static void main(String arg []) throws SQLException{
		Connection connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/dbprueba","root","sistemas");
		connection.close();
		System.out.println("Fin");
	}

}
