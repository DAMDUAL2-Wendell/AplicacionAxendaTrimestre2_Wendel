using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestUnitarios
{
    [TestClass]
    public class PagContactosTests
    {

        [STAThread] // Configurar el método de prueba para que se ejecute en un subproceso STA
        [TestMethod]
        public void Next_NoContact_ReturnsOne()
        {
            var pagContactos = new AplicacionAxendaTrimestre2_Wendel.UI.Views.Paginas.PagContactos();
            var result = pagContactos.GetNextContactoId();
            Assert.AreEqual(1, result);
        }

    }
}
