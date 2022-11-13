using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> ListaSlider { get; set; }
        public IEnumerable<Producto> ListaProducto { get; set; }
    }
}
