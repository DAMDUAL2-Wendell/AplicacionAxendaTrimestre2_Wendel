using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace AplicacionAxendaTrimestre2_Wendel.Controller
{
    public class ContactoController
    {
        async public static void DuplicarContacto(DataAcceso _dataAccess, DataGrid dataGrid, Contacto contacto)
        {

            MessageBoxResult result = MessageBox.Show("¿Está seguro de querer duplicar al usuario " + contacto.FirstName +
                        "?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //_dataAccess.DbContext.Contactos.Remove((Contacto)contacto);
                Contacto nuevoContacto = contacto;
                nuevoContacto.Id = GetNextContactoId(_dataAccess);
                _dataAccess.DbContext.Contactos.Add(contacto);
                // Guardar los cambios en la base de datos
                await _dataAccess.DbContext.SaveChangesAsync();

                // Actualizar el DataGrid
                var contactos = await _dataAccess.DbContext.Contactos.ToListAsync();
                dataGrid.ItemsSource = contactos;
            }
        }

        public static Contacto GetContactoDataGridSeleccionado(DataGrid dataGrid)
        {
            Contacto contacto = null;
            // Verificar si se seleccionaron contactos para eliminar
            if (dataGrid.SelectedItems.Count > 0)
            {
                // Eliminar los contactos seleccionados de la base de datos
                foreach (var item in dataGrid.SelectedItems)
                {
                    contacto = (Contacto)item;
                }
                if (contacto != null)
                {
                    return contacto;
                }
            }
            return null;
        }

        public async static void EliminaContacto(DataAcceso _dataAccess, DataGrid dataGrid, Contacto contacto)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro de querer eliminar al usuario " + contacto.FirstName +
                "?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _dataAccess.DbContext.Contactos.Remove((Contacto)contacto);
                // Guardar los cambios en la base de datos
                await _dataAccess.DbContext.SaveChangesAsync();

                // Actualizar el DataGrid
                var contactos = await _dataAccess.DbContext.Contactos.ToListAsync();
                dataGrid.ItemsSource = contactos;
            }
        }

        public static Contacto GetcontactoSeleccinoadoDataGrid(DataGrid dataGrid)
        {
            Contacto contacto = null;
            // Verificar si se seleccionaron contactos para eliminar
            if (dataGrid.SelectedItems.Count > 0)
            {
                // Eliminar los contactos seleccionados de la base de datos
                foreach (var item in dataGrid.SelectedItems)
                {
                    contacto = (Contacto)item;
                }
            }
            return contacto;
        }

        public static int GetNextContactoId(DataAcceso _dataAccess)
        {
            // Ordenar los contactos por Id de forma descendente y luego seleccionar el primer elemento
            var ultimoContacto = _dataAccess.DbContext.Contactos.OrderByDescending(c => c.Id).FirstOrDefault();
            if (ultimoContacto != null)
            {
                return ultimoContacto.Id + 1;
            }
            else
            {
                return 1;
            }
        }

        

    }

}

