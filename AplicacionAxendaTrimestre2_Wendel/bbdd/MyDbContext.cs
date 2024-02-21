using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;


namespace AplicacionAxendaTrimestre2_Wendel.bbdd
{
    public class MyDbContext: DbContext
    {

        // Set de Contactos, crea la tabla contactos en la BBDD
        public DbSet<Contacto> Contactos { get; set; }


        // Constructor de clase pasando como parámetro el OptionsBuilder
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        
       // Sobreescritura del método de clase de configuración de la base de datos.
       // Si no está configurado establecemos por defecto un base de datos en memoria con SQLITE.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Si las opciones no están configuradas, configurar por defecto una
            // base de datos en memoria.
            if (!optionsBuilder.IsConfigured)
            {
                // Configurar la conexión a la base de datos en memoria
                var conn = new SqliteConnection("DataSource=:memory:;Cache=shared");
                conn.Open();
                optionsBuilder.UseSqlite(conn);

                //  Ya que es en memoria nos aseguramos de borrar y cargar las tablas
                // nuevamente
                Database.EnsureDeletedAsync().Wait();
                Database.EnsureCreatedAsync().Wait();
            }
        }


    }






}
