using System;
using System.Globalization;
using System.Windows.Data;

namespace AplicacionAxendaTrimestre2_Wendel.Util
{

        public class DateTimeToTimeStringConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is DateTime dateTime)
                {
                    return dateTime.ToString("HH:mm:ss");
                }
                return string.Empty;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    
}
