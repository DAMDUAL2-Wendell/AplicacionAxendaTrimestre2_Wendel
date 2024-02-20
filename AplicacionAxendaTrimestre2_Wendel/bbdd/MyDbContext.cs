using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;


namespace AplicacionAxendaTrimestre2_Wendel.bbdd
{
    public class MyDbContext: DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Persona> Personas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        
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

    public class Persona
    {
        // Marcar el Id como clave primaria y que sea autoincremental
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

    }

    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; } 

        public DateTime FechaActivacion { get; set; }
    }

    public class Nota
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }
    }
}
