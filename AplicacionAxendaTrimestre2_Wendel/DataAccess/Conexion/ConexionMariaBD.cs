using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;

namespace DI_UD5_CreacionInformes_Wendel
{
    internal class ConexionMariaBD
    {
        // Declaracion de variable estática que será utilizada para almacenar la conexión a la base de datos.
        MySqlConnection conn = null;

        // Nombre de usuario para la conexión a la base de datos
        private static String userName = "root";

        // Contraseña para la conexión a la base de datos
        private static String password = "abc123.";

        // Sistema de gestión de bases de datos (DBMS) que se utilizará (MySQL en este caso)
        private static String dbms = "mysql";

        // Dirección IP del servidor de la base de datos
        private static String serverName = "127.0.0.1";

        // Número de puerto del servidor de la base de datos
        private static String portNumber = "3306";

        // Nombre de la base de datos
        public static String dbName = "prediccionconcellos";

        // URL para la conexión utilizando MariaDB
        private static String URL = "jdbc:mariadb://localhost:" + portNumber + "/" + dbName;

        // URL para la conexión utilizando MySQL
        private static String URL2 = "jdbc:mysql://localhost:" + portNumber;


        string connectionString = "Server=localhost:3306;Database=fabrica;Uid=root;Pwd=;";

        /// <summary>Método que establece una conexión  con un servidor SQL.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void conectar(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    this.conn = connection;
                    connection.Open();
                    Console.WriteLine("Conexión exitosa a MariaDB");
                    //MessageBox.Show("Conexión exitosa a MariaDB");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al conectar a MariaDB: {ex.Message}");
                }
            }
        }

        /// <summary>Método que devuelve el objeto MySqlConnection de la clase.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        /**
     * Obtiene una conexión a la base de datos.
     * 
     * @return La conexión a la base de datos.
     */
        public MySqlConnection getConexion()
        {
            if (conn == null)
            {
                try
                {
                    // Obtener la conexión

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            this.conn = connection;
                            connection.Open();
                            Console.WriteLine("Conexión exitosa a MYSQL");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al conectar a MYSQL: {ex.Message}");
                        }
                    }
                }
                catch (Exception e)
                {
                    // Si ocurre alguna excepción durante el proceso, se captura y se imprime un
                    // mensaje de error junto con la traza de la excepción.
                    Console.WriteLine($"Error al establecer la conexion a MYSQL: {e.Message}");
                }
            }
            return conn;
        }


        /// <summary>Ejecuta una consulta en la base de datos.</summary>
        /// <param name="consulta">La consulta SQL.</param>
        /// <returns>Devuelve un objeto MySqlDataReader, null en caso de error al ejecutar la consulta.</returns>
        public MySqlDataReader ExecuteQuery(string consulta)
        {
            MySqlDataReader sqlDataReader = null;
            if (this.conn.State ==ConnectionState.Open)
            {
                try
                {
                    MySqlConnection connection = getConexion();
                    MySqlCommand command = new MySqlCommand(consulta, connection);
                    sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return sqlDataReader;
        }



        /**
 * Cierra la conexión a la base de datos.
 */
        public void cerrarConexion()
        {
            try
            {
                // Comprobamos si la conexión no es nula antes de cerrarla.
                if (conn != null)
                {
                    // Cerramos la conexión.
                    conn.Close();
                    Console.WriteLine("Conexión cerrada correctamente");
                }
                else
                {
                    // Si la conexión ya estaba cerrada, mostramos un mensaje indicándolo.
                    Console.WriteLine("La conexión ya estaba cerrada.");
                }
            }
            catch (Exception e)
            {
                // Si se produce una SQLException al intentar cerrar la conexión, mostramos la
                // traza de la excepción.
                Console.WriteLine("Error al cerrar la conexión" + e.Message);
            }
        }


    }
}
