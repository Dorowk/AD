package org.institutoserpis.ad;

import java.math.BigDecimal;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.text.DecimalFormat;
import java.text.ParseException;
import java.util.Scanner;



public class PruebaArticulo {
	private static Scanner scanner = new Scanner(System.in);

	private enum Action {Salir, Nuevo, Editar, Eliminar, Consultar, Listar};
	private static Action scanAction() {
		while (true) {
			System.out.print("0-Salir 1-Nuevo 2-Editar 3-Eliminar 4-Consultar 5-Listar: ");
			String action = scanner.nextLine().trim();
			if (action.matches("[012345]"))
				return Action.values()[Integer.parseInt(action)];
			System.out.println("Opción inválida.");
		}
	}

	private static class Articulo {
		private long id;
		private String nombre;
		private long categoria;
		private BigDecimal precio;
	}
	
	private static String scanString(String label) {
		System.out.print(label);
		return scanner.nextLine().trim();
	}
	
	private static long scanLong(String label) {
		while (true) {
			System.out.print(label);
			String data = scanner.nextLine().trim();
			try {
				return Long.parseLong(data);
			} catch (NumberFormatException ex) {
				System.out.println("Debe ser un número");
			}
		}
	}
	
	private static BigDecimal scanBigDecimal(String label) {
		while (true) {
			System.out.print(label);
			String data = scanner.nextLine().trim();
			DecimalFormat decimalFormat = (DecimalFormat)DecimalFormat.getInstance();
			decimalFormat.setParseBigDecimal(true);
			try {
				return (BigDecimal)decimalFormat.parse(data);
			} catch (ParseException e) {
				System.out.println("Debe ser un número decimal");
			}
		}
	}
	
	private static Articulo scanArticulo() {
		Articulo articulo = new Articulo();
		articulo.nombre    = scanString(    "   Nombre: ");
		articulo.categoria = scanLong(      "Categoria: ");
		articulo.precio    = scanBigDecimal("   Precio: ");
		return articulo;
	}
	
	private static void showArticulo(Articulo articulo) {
		System.out.println("       id: " + articulo.id);
		System.out.println("   nombre: " + articulo.nombre);
		System.out.println("categoria: " + articulo.categoria);
		System.out.println("   precio: " + articulo.precio);
	}
	
	private static void showSQLException(SQLException ex) {
		System.out.println("SQLException:");
		while (ex != null) {
			System.out.println("  Message: " + ex.getMessage());
			System.out.println(" SQLState: " + ex.getSQLState());
			System.out.println("ErrorCode: " + ex.getErrorCode());
			ex = ex.getNextException();
		}
	}
	
	private static Connection connection;
	
	private static PreparedStatement insertPreparedStatement;
	private final static String insertSql = "insert into articulo (nombre, categoria, precio) "
			+ "values (?, ?, ?)";
	private static void nuevo() {
		Articulo articulo = scanArticulo();
		//showArticulo(articulo);
		try {
			if (insertPreparedStatement == null)
				insertPreparedStatement = connection.prepareStatement(insertSql);
			insertPreparedStatement.setString(1, articulo.nombre);
			insertPreparedStatement.setLong(2, articulo.categoria);
			insertPreparedStatement.setBigDecimal(3, articulo.precio);
			insertPreparedStatement.executeUpdate();
			System.out.println("artículo guardado.");
		} catch (SQLException ex) {
			showSQLException(ex);
		}
	}
	
	private static PreparedStatement updatePreparedStatement;
	private final static String updateSql = "update articulo "
			+ "set nombre = ?, "
			+ "categoria = ?, "
			+ "precio = ? "
			+ "where id = ?";
	private static void editar() {
		long id = scanLong("       id: ");
		Articulo articulo = scanArticulo(); //resto de campos
		try {
			if (updatePreparedStatement == null)
				updatePreparedStatement = connection.prepareStatement(updateSql);
			updatePreparedStatement.setString(1, articulo.nombre);
			updatePreparedStatement.setLong(2, articulo.categoria);
			updatePreparedStatement.setBigDecimal(3, articulo.precio);
			updatePreparedStatement.setLong(4, id);
			int count = updatePreparedStatement.executeUpdate();
			if (count == 1) 
				System.out.println("artículo guardado.");
			else
				System.out.println("No existe artículo con ese id");
		} catch (SQLException ex) {
			showSQLException(ex);
		}
	}
	
	private static void closePreparedStatements() throws SQLException {
		if (insertPreparedStatement != null)
			insertPreparedStatement.close();
	}
	
	public static void main(String[] args) throws SQLException {
		connection = DriverManager.getConnection(
			"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
		while (true) {
			Action action = scanAction();
			if (action == Action.Salir) break;
			if (action == Action.Nuevo) nuevo();
			if (action == Action.Editar) editar();
			System.out.println();
		}
		closePreparedStatements();
		connection.close();
	}
	

}
