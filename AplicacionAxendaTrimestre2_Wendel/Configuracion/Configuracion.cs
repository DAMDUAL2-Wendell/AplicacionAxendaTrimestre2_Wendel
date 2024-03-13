using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionAxendaTrimestre2_Wendel.Configuracion
{
    public static class Configuracion
    {
        // Configuración de conexión a base de datos en memoria
        public static string BDMEMORIA = "DataSource=:memory:;Cache=shared";

        // Ruta del archivo de base de datos local
        public static string RUTAFICHERO = "Data\\LocalBD\\MiBaseDeDatosLocal.db";

        // Prefijo para la conexión a base de datos de archivo
        public static string BDFICHERO = "DataSource=";

        // Configuración de conexión a base de datos MySQL
        public static string BDMYSQL = "Persist Security Info=False;User ID=root;Password=abc123.;Initial Catalog=prueba;Server=localhost";

        // URL de conexión a la base de datos
        public static string UrlConexion { get; set; } = "localhost";

        // Usuario de la base de datos
        public static string Usuario { get; set; } = "root";

        // Contraseña de la base de datos
        public static string Contraseña { get; set; } = "abc123.";

        // Nombre de la base de datos
        public static string NombreBD { get; set; } = "prueba";

        // Indicador de cambios en la configuración
        public static bool DatosConexionCambiados { get; set; } = false;

    }
}

