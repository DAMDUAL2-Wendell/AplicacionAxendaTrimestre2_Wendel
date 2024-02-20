using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AplicacionAxendaTrimestre2_Wendel.bbdd
{
    public class DataAccess : IDisposable
    {
        public static MyDbContext _dbContext;

        public static MyDbContext getDbContext()
        {
            return _dbContext;
        }

       public DataAccess(String conexion)
{
    // Configurar la conexion a la base de datos en memoria
    // var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
    // optionsBuilder.UseSqlite(conexion);

    // Verificar si la base de datos es en memoria
    bool esBaseDeDatosEnMemoria = conexion.Contains(":memory:");

    IDbConnection conn; // Cambio de tipo a IDbConnection para admitir tanto SQLite como MySQL

    // Instanciar OptionsBuilder
    var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();

            if (conexion.Contains("Server="))
    {
        //  Instanciar conexion MYSQL y asignar el string de conexion
        conn = new MySqlConnection(conexion);
               // optionsBuilder.UseMySql((MySqlConnection)conn);
            }
    else
    {
        //  Instanciar conexion SQLITE y asignar el string de conexion
        conn = new SqliteConnection(conexion);
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
        _dbContext.Database.EnsureDeletedAsync().Wait();
    }

    // Nos aseguramos de crear las tablas de la base de datos
    _dbContext.Database.EnsureCreatedAsync().Wait();
}


        public List<Persona> ObtenerPersonas()
        {
            return _dbContext.Personas.ToList();
        }

        public async Task<List<Persona>> ObtenerPersonasAsync()
        {
            return await _dbContext.Personas.ToListAsync();
        }

        public async Task AgregarPersonaAsync(Persona p)
        {
            _dbContext.Personas.Add(p);
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
