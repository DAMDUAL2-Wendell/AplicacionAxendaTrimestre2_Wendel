using AplicacionAxendaTrimestre2_Wendel.POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Registros
{
    /// <summary>
    /// Lógica de interacción para RegNota.xaml
    /// </summary>
    public partial class RegNota : Page
    {
        private Contacto _contacto;
        public RegNota()
        {
            InitializeComponent();
            _contacto = null;
        }

        public RegNota(Contacto c)
        {
            InitializeComponent();
            _contacto = c;
        }
    }
}
