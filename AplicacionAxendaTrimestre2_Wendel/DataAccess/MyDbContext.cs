using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AplicacionAxendaTrimestre2_Wendel.Models;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;


namespace AplicacionAxendaTrimestre2_Wendel.bbdd
{
    public class MyDbContext : DbContext
    {

        // Set de Contactos, crea la tabla contactos en la BBDD
        public DbSet<Contacto> Contactos { get; set; }


        // Este constructor hereda de la clase base DbContext, y la llamada base(options) se encarga de pasar las opciones de configuración al constructor de la clase base.
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             //  Asegura de que cualquier configuración predeterminada o lógica interna proporcionada por Entity Framework Core 
             //  se aplique primero antes de aplicar otras configuraciones específicas del modelo 
            base.OnModelCreating(modelBuilder);

            // Habilita la eliminación en cascada de los telefonos al eliminar un contacto.
            modelBuilder.Entity<Contacto>()
                .HasMany(c => c.Numbers)
                .WithOne(p => p.Contacto)
                .OnDelete(DeleteBehavior.Cascade);
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

                //  Borrar y crear base de datos
                Database.EnsureDeletedAsync().Wait();
                Database.EnsureCreatedAsync().Wait();
            }
        }

        public async Task<List<Evento>> ObtenerListaEventosAsync()
        {
            var contactosConEventos = await Contactos
                .Include(c => c.Eventos) // Incluimos los eventos relacionados con cada contacto
                .ToListAsync();

            // Creamos una lista para almacenar todos los eventos
            List<Evento> todosLosEventos = new List<Evento>();

            // Recorremos cada contacto para agregar sus eventos a la lista
            foreach (var contacto in contactosConEventos)
            {
                todosLosEventos.AddRange(contacto.Eventos);
            }

            return todosLosEventos;
        }

        public List<Evento> ObtenerListaEventos()
        {
            var contactosConEventos = Contactos
                .Include(c => c.Eventos) // Incluimos los eventos relacionados con cada contacto
                .ToList();

            // Creamos una lista para almacenar todos los eventos
            List<Evento> todosLosEventos = new List<Evento>();

            // Recorremos cada contacto para agregar sus eventos a la lista
            foreach (var contacto in contactosConEventos)
            {
                todosLosEventos.AddRange(contacto.Eventos);
            }

            return todosLosEventos;
        }




        public async Task<List<Nota>> ObtenerListaNotasAsync()
        {
            var contactosConNotas = await Contactos
                .Include(c => c.Notas) // Incluimos las notas relacionadas con cada contacto
                .ToListAsync();

            // Creamos una lista para almacenar todas las notas
            List<Nota> todasLasNotas = new List<Nota>();

            // Recorremos cada contacto para agregar sus notas a la lista
            foreach (var contacto in contactosConNotas)
            {
                todasLasNotas.AddRange(contacto.Notas);
            }

            return todasLasNotas;
        }


    }






}
