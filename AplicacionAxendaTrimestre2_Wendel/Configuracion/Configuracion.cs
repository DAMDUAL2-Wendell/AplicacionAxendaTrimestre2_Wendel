using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionAxendaTrimestre2_Wendel.Configuracion
{
    public static class Configuracion
    {
        // Configuracion de base de datos.
        public static string BDMEMORIA = "DataSource=:memory:;Cache=shared";
        public static string RUTAFICHERO = "Data\\LocalBD\\MiBaseDeDatosLocal.db";
        //public static string RUTAFICHERO = "MiBaseDeDatosLocal.db";
        //public static string RUTAFICHERO = $"{AppDomain.CurrentDomain.BaseDirectory}Data\\LocalBD\\MiBaseDeDatosLocal.db";

        public static string BDFICHERO = "DataSource=";
        public static string BDMYSQL = "Server=localhost:3306;Database=myDataBase;Uid=root;Pwd=;";
    }
}

