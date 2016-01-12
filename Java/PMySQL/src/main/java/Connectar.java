import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.SQLException;
import java.sql.Statement;
import java.sql.PreparedStatement;
import java.util.ArrayList;
import java.util.Scanner;

public class Connectar {
	private static Scanner scanner = new Scanner(System.in);
	
	public static void main(String arg []) throws SQLException{
		Connection con = DriverManager.getConnection(
				"jdbc:mysql://localhost/dbprueba","root","sistemas");
		int opcion;
		System.out.println("0 - Salir");
		System.out.println("1 - Leer");
		System.out.println("2 - Nuevo");
		System.out.println("3 - Editar");
		System.out.println("4 - Eliminar");
		System.out.println("5 - Mostrar");
		try{
		do{
			System.out.println("Introduce una opcion");
			opcion=scanner.nextInt();
			switch(opcion){
				case 0:
					System.out.println("Adios");
					break;
					
				case 1:
					mostrarFilas(con,true);
					break;
					
				case 2:
					insertFila(con);
					break;
					
				case 3:
					actualizarFila(con);
					break;
					
				case 4:
					eliminarFila(con);
					break;
					
				case 5:
					mostrarFilas(con,false);
					System.out.println("\n");
					break;
					
				default:
					System.out.println("Opcion no valida \n");	
			}
			
		}while(opcion !=0);
		
		con.close();
		}catch(Exception e){
			System.out.println("Se ha producido un error");
			System.out.println(e);
		}
		
	}
	
	
	
	public static void mostrarFilas(Connection con, boolean aux) throws SQLException{
		Statement stat = con.createStatement();
		String querry = "Select * from Articulo";
		if(aux){
			System.out.println("Introduce la ID de la fila a mostrar");
			scanner.nextLine();
			int num = scanner.nextInt();
			querry+=" WHERE ID = "+num;
		}
		ResultSet resulset = stat.executeQuery(querry);
		
		int colum= nombreFilas(resulset);
		while(resulset.next()){
			ArrayList<Object> lista = new ArrayList<Object>();
			for(int i = 1; i<=colum;i++){
				lista.add(resulset.getObject(i));
			}
			for(int i = 0; i<lista.size();i++){
				System.out.print(lista.get(i).toString()+"\t");
				}
			System.out.println("");
		}
		
		resulset.close();		
		
	}
	
	public static int nombreFilas(ResultSet resulset) throws SQLException{
		ArrayList<String> nombres = new ArrayList<String>();
		ResultSetMetaData meta = resulset.getMetaData();
		
		int colum=meta.getColumnCount();
		
		for(int i = 1; i<=colum;i++){
			System.out.print(meta.getColumnName(i) +"\t");
		}
		System.out.println();
		return colum;
	}
	
	public static void insertFila(Connection con) throws SQLException{
		scanner.nextLine();
		System.out.println("Introduce el nombre del nuevo elemendo");
		String name = scanner.nextLine();
		System.out.println("Introduce la categoria");
		int categori = scanner.nextInt();
		scanner.nextLine();
		System.out.println("Introduce el precio");
		String price = scanner.nextLine();
		
		PreparedStatement state = con.prepareStatement(
				"INSERT INTO Articulo (Nombre,Categoria,Precio) VALUES (?,?,?)");
		state.setString(1, name);
		state.setInt(2, categori);
		state.setString(3, price);
		
		state.executeUpdate();
		state.close();
		System.out.println();
	}
	
	
	public static void eliminarFila(Connection con) throws SQLException{
		System.out.println("Introduce el ID de la fila a eliminar");
		int id = scanner.nextInt();
		String aux = "DELETE FROM Articulo WHERE ID = "+id;
		PreparedStatement state = (PreparedStatement) con.prepareStatement(
				aux);
		state.executeUpdate();
		state.close();
		System.out.println();
	}
	
	public static void actualizarFila(Connection con) throws SQLException{
		System.out.println("Introduce el ID de la fila a cambiar"); 
		int Id = scanner.nextInt();
		scanner.nextLine();
		System.out.println("Introduce el nombre nuevo");
		String name = scanner.nextLine();
		
		PreparedStatement state = con.prepareStatement(
				"UPDATE Articulo SET Nombre = ? WHERE ID = ?");
		state.setString(1, name);
		state.setInt(2, Id);
		state.executeUpdate();
		state.close();
		System.out.println();
	}
}
