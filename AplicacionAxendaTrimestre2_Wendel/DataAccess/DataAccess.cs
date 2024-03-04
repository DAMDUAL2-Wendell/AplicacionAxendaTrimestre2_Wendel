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
    public class DataAccess : IDisposable
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

        public DataAccess(String conexion)
        {

            // Verificar si la base de datos es en memoria
            bool esBaseDeDatosEnMemoria = conexion.Contains(":memory:");

            IDbConnection conn; // IDbConnection para admitir tanto SQLite como MySQL

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

            // Si es base de datos en memoria borramos los datos
            if (esBaseDeDatosEnMemoria)
            {
                // Borrar la base de datos
                _dbContext.Database.EnsureDeleted();
            }

            // Nos aseguramos de crear las tablas de la base de datos
            _dbContext.Database.EnsureCreatedAsync().Wait();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}
