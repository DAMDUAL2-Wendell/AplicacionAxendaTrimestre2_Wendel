using AplicacionAxendaTrimestre2_Wendel.POJO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AplicacionAxendaTrimestre2_Wendel.Util
{
    public class ConverterTelefonos : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 1 || values[0] == null || !(values[0] is Contacto))
                return string.Empty;

            var contacto = (Contacto)values[0];
            // if (contacto.Telefonos == null || !contacto.Telefonos.Any())
            //     return "Sin teléfonos";

            // return string.Join(", ", contacto.Telefonos.Select(t => t.Numero));
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
