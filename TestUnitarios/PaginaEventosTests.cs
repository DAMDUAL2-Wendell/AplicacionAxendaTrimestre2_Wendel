using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Paginas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestUnitarios
{
    [TestClass]
    public class PaginaEventosTests
    {
        [STAThread] // Configura el método de prueba para que se ejecute en un subproceso STA
        [TestMethod]
        public void AsignarListaADataGrid_ValidInput_ItemsSourceNotNull()
        {
            // Código de prueba aquí
            var paginaEventos = new AplicacionAxendaTrimestre2_Wendel.UI.Views.Paginas.PaginaEventos();
            var task = paginaEventos.AsignarListaADataGrid();
            task.Wait(); // Espera a que se complete la tarea antes de realizar las aserciones
            Assert.IsNotNull(paginaEventos.GetDataGrid().ItemsSource);
        }

        [TestMethod]
        public void Tb_FindEvent_TextChanged_FiltersEvents_Correctly()
        {
            var paginaEventos = new PaginaEventos();
            var eventos = new List<Evento>
            {
                new Evento { Id = 1, Titulo = "Evento 1", Descripcion = "Descripción del evento 1", Contacto = new Contacto { FirstName = "Juan", LastName = "Pérez" } },
                new Evento { Id = 2, Titulo = "Evento 2", Descripcion = "Descripción del evento 2", Contacto = new Contacto { FirstName = "María", LastName = "López" } },
                new Evento { Id = 3, Titulo = "Reunión", Descripcion = "Reunión con el equipo", Contacto = new Contacto { FirstName = "Pedro", LastName = "García" } }
            };
            paginaEventos.GetDataGrid().ItemsSource = eventos;

            // Simulamr el evento de cambio de texto
            paginaEventos.Tb_FindEvent_TextChanged(null, null);

            // Verificamos que inicialmente se muestren todos los eventos
            Assert.AreEqual(3, paginaEventos.GetDataGrid().Items.Count); 

            // Filtramos los eventos por "Evento 1"
            paginaEventos.GetTextBoxFindEven().Text = "Evento 1";
            paginaEventos.Tb_FindEvent_TextChanged(null, null);

            // Verificar que solo se muestre un evento después de filtrar
            Assert.AreEqual(1, paginaEventos.GetDataGrid().Items.Count);

            // Verificamos que el evento mostrado coincida con el filtrado
            Assert.IsTrue(((IEnumerable<Evento>)paginaEventos.GetDataGrid().ItemsSource).First().Titulo.Contains("Evento 1")); 
        }


    }
}
