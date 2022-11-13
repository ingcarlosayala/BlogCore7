using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.IRepositorio
{
    public interface ISliderRepositorio:IRepositorio<Slider>
    {
        void Actualizar(Slider slider);
    }
}
