using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AplicacionAxendaTrimestre2_Wendel.Controller
{
    public class BaseDatosController
    {

        public static void BorrarBaseDatos() {
            MessageBoxResult result = MessageBox.Show("¿Está seguro de querer borrar todos los datos de la base de datos?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Eliminar todos los contactos de la base de datos
                    //var contactos = await AppData.DataAccess.DbContext.Contactos.ToListAsync();
                    //AppData.DataAccess.DbContext.Contactos.RemoveRange(contactos);
                   // await AppData.DataAccess.DbContext.SaveChangesAsync();

                    //MessageBox.Show("Se han borrado todos los datos de la base de datos correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Actualizar página de contactos
                   // MostrarPaginaContactos(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error al intentar borrar los datos de la base de datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
