using AplicacionAxendaTrimestre2_Wendel.POJO;
using DI_UD5_CreacionInformes_Wendel;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AplicacionAxendaTrimestre2_Wendel.bbdd
{
    public class DataAcceso : IDisposable
    {
        public static MyDbContext _dbContext;

        public MyDbContext DbContext
        {
            get { return _dbContext; }
            set { _dbContext = value; }
        }

        public static MyDbContext getDbContext()
        {
            return _dbContext;
        }

        public DataAcceso(String conexion)
        {

            // Verificar si la base de datos es en memoria
            bool esBaseDeDatosEnMemoria = conexion.Contains(":memory:");

            // IDbConnection para admitir tanto SQLite como MySQL
            IDbConnection conn;

            // Instanciar OptionsBuilder
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();

            if (conexion.Contains("Server="))
            {
                //  Instanciar conexion MYSQL y asignar el string de conexion
                conn = new MySqlConnection(conexion);
                //conn = new ConexionBD().getConexion();
                optionsBuilder.UseMySql(conn.ConnectionString, new MySqlServerVersion(new Version(8, 0, 21)));

            }
            else
            {
                //  Instanciar conexion SQLITE y asignar el string de conexion
                conn = new SqliteConnection(conexion);

                //  MessageBox.Show("String conexion = " +conexion);

                // Abrir conexion
                conn.Open();

                //  asignar a las optionsBuilder la conexion SQLITE
                optionsBuilder.UseSqlite((SqliteConnection)conn);
            }

            // Instanciar y asignar la clase MyDbContext con las opciones creadas anteriormente
            _dbContext = new MyDbContext(optionsBuilder.Options);

            try
            {
                // Nos aseguramos de crear las tablas de la base de datos
                _dbContext.Database.EnsureCreatedAsync().Wait();

                // Si se crean las tablas correctamente, mostramos un mensaje de éxito
                //MessageBox.Show("Conexión establecida correctamente y tablas creadas.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                try
                {
                    // Si se produce un error, eliminar la base de datos y volver a intentar la creación
                    _dbContext.Database.EnsureDeletedAsync().Wait();
                    // Volver a intentar crear las tablas
                    _dbContext.Database.EnsureCreatedAsync().Wait();
                }
                catch(Exception e)
                {
                    // Mostrar un mensaje indicando que no se pudo conectar
                    MessageBox.Show("No se pudo conectar a la base de datos. Verifica la configuración de conexión y asegúrate de que el servidor esté en funcionamiento.", "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}
